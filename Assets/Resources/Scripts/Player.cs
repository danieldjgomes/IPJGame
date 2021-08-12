using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    public enum AttackRange
    {
        MELEE, RANGED
    }

    public enum CrowdControl
    {
        NONE, TAUNT, ROOTED, CONFUSE, ZAPEFFECT
    }

    public enum PlayerStage
    {
        IDLE, MOVING, PREPARINGATTACK, TARGETABLE, CASTINGQ, CASTINGW, CASTINGE
    }

    public int stamina;
    public int MaxStamina;
    public int speed;
    public int health;
    public int maxHealth;
    public int phisicalDamage;
    public int defense;
    public int attackCost;
    public int tauntCount;
    public int confuseCount;
    public int rootCount;
    public int zapCount;

    public Tile tile;
    public PlayerStage playerStage;
    public CrowdControl crowdControl;
    public Player tauntedTarget;
    public RaycastHit hit;
    public Ray ray;
    public Round round;
    public Moviment moviment;


    public UIController ui;
    public Outline outline;
    public Battle battle;
    public AttackRange attackRange;

    public Team team = null;
    protected Basic Q = null;
    protected Basic W = null;
    protected Ultimate E = null;

    System.Random rnd = new System.Random();
    //

    // Start is called before the first frame update
    void Start()
    {
        playerStage = PlayerStage.IDLE;

    }

    // Update is called once per frame
    void Update()
    {

        limitStamina();
        //ui.SetOutLineColor(this);

        if (this.health <= 0)
        {
            this.transform.gameObject.SetActive(false);

        }

        if (round.getActualPlayer() == this.gameObject && !this.gameObject.activeSelf)
        {
            round.finishTurn();
        }



        if (round.getActualPlayer() == this.gameObject && this.gameObject.activeSelf)
        {

            if (this.crowdControl != CrowdControl.CONFUSE)
            {



                if (Input.GetKeyDown(KeyCode.CapsLock))
                {
                    this.playerStage = PlayerStage.PREPARINGATTACK;
                    battle.SelectTarget(this);

                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    this.UseSkillQ();

                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    this.UseSkillW();

                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.UseSkillE();

                }

                if (Input.GetMouseButtonDown(0) && playerStage == PlayerStage.PREPARINGATTACK && this.stamina >= attackCost)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.CompareTag("Player"))
                        {
                            GameObject go = GameObject.Find(hit.collider.gameObject.name);
                            Player player = go.GetComponent<Player>();

                            if (player.playerStage == PlayerStage.TARGETABLE)
                            {

                                if (this.attackRange == AttackRange.MELEE)
                                {
                                    battle.DoDamage(this.phisicalDamage, player, Battle.AttackType.MELEE);
                                    //print("Attack Melee");
                                }

                                else
                                {
                                    if (this.attackRange == AttackRange.RANGED)
                                    {
                                        battle.DoDamage(this.phisicalDamage, player, Battle.AttackType.RANGED);
                                        //print("Attack Ranged");
                                    }
                                }

                                this.stamina -= attackCost;
                            }
                        }


                    }
                }



                if (this.playerStage == PlayerStage.PREPARINGATTACK && Input.GetKeyDown(KeyCode.Escape))
                {
                    playerStage = PlayerStage.IDLE;
                    round.SetIdleAllPlayers();

                }




                if (Input.GetMouseButtonDown(0) && playerStage == PlayerStage.IDLE && !this.IsCastingSkill() && this.crowdControl != CrowdControl.ROOTED)

                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    int layerMask = 1 << LayerMask.NameToLayer("Player");
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {

                        if (hit.transform == this.transform)
                        {
                            moviment.SetMovableTile(this);
                            playerStage = PlayerStage.MOVING;

                        }

                    }
                }

                if (Input.GetMouseButtonDown(0) && playerStage == PlayerStage.MOVING)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {

                        if (hit.transform.Find("movable") != null)
                        {
                            if (this.crowdControl == CrowdControl.ZAPEFFECT)
                            {
                                Tile[] tiles = FindObjectsOfType<Tile>();
                                List<Tile> movableTiles = new List<Tile>();
                                foreach (Tile tile in tiles)
                                {
                                    if (tile.transform.Find("movable") != null)
                                    {
                                        movableTiles.Add(tile);
                                    }

                                }
                                int size = movableTiles.Count;
                                int r = rnd.Next(0, size);
                                Transform param = movableTiles[r].transform;
                                moviment.MovePlayer(param, this);
                                ReduceCountZap(this);

                            }

                            else
                            {
                                moviment.MovePlayer(hit.transform, this);

                            }
                            moviment.RemoveMovableTile();
                            playerStage = PlayerStage.IDLE;
                        }
                    }
                }

                if (playerStage == PlayerStage.MOVING && stamina == 0)
                {
                    playerStage = PlayerStage.IDLE;
                }

                if (playerStage == PlayerStage.MOVING)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        moviment.RemoveMovableTile();
                        playerStage = PlayerStage.IDLE;
                    }
                }





            }
            else
            {
                print(this.name + " Está confuso e não pode realizar ações.");
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                playerStage = PlayerStage.IDLE;
                round.finishTurn();
            }



        }

        if(this.playerStage != PlayerStage.IDLE && Input.GetKey(KeyCode.Escape))
        {
            this.playerStage = PlayerStage.IDLE;
        }

        if(this.playerStage == PlayerStage.CASTINGQ)
        {
            Player[] players = FindObjectsOfType<Player>();
            foreach(Player p in players)
            {
                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, p.gameObject, Q.Range, true) && this != p)
                {
                    if (Q.friendlyFire)
                    {
                        p.SetTargable();
                    }
                    else
                    {
                        if (!this.IsMyTeammate(p))
                        {
                            p.SetTargable();
                        }
                    }


                }
            }
        }

        if (this.playerStage == PlayerStage.CASTINGW)
        {
            Player[] players = FindObjectsOfType<Player>();
            foreach (Player p in players)
            {
                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, p.gameObject, W.Range, true) && this != p)
                {
                    if (W.friendlyFire)
                    {
                        p.SetTargable();
                    }
                    else
                    {
                        if (!this.IsMyTeammate(p))
                        {
                            p.SetTargable();
                        }
                    }


                }
            }
        }

        if (this.playerStage == PlayerStage.CASTINGE)
        {
            Player[] players = FindObjectsOfType<Player>();
            foreach (Player p in players)
            {
                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, p.gameObject, E.Range, true) && this != p)
                {
                    if (E.friendlyFire)
                    {
                        p.SetTargable();
                    }
                    else
                    {
                        if (!this.IsMyTeammate(p))
                        {
                            p.SetTargable();
                        }
                    }


                }
            }
        }



    }

    public Team GetTeam()
    {
        return this.team;
    }

    public void SetTeam(Team team)
    {
        this.team = team;
    }

    public virtual void UseSkillQ()
    {
        if (this.IsCostEnough(Q))
        {
            this.playerStage = PlayerStage.CASTINGQ;
        }
    }
    public virtual void UseSkillW()
    {
        if (this.IsCostEnough(W))
        {
            this.playerStage = PlayerStage.CASTINGW;
        }
    }
    public virtual void UseSkillE()
    {

        if (this.IsCostEnough(E))
        {
            this.playerStage = PlayerStage.CASTINGE;
        }
    }


    private void limitStamina()
    {
        if (stamina > MaxStamina)
        {
            stamina = MaxStamina;
        }
    }

    public void SetTargable()
    {
        this.playerStage = PlayerStage.TARGETABLE;

    }

    public float GetAttackRangeValue()
    {

        if (this.attackRange == AttackRange.MELEE)
        {
            return 1.43f * tile.transform.localScale.x;
        }
        else
        {
            return 4f * tile.transform.localScale.x;
        }
    }

    private bool IsCastingSkill()
    {
        if (this.playerStage == PlayerStage.CASTINGQ || this.playerStage == PlayerStage.CASTINGW || this.playerStage == PlayerStage.CASTINGE)
        {
            return true;
        }

        return false;
    }


    public bool HasSomePlayerHere(RaycastHit ray, Player[] players)
    {
        foreach (Player player in players)
        {
            if (tile.transform.position.x == player.transform.position.x && tile.transform.position.z == player.transform.position.z)
            {
                return true;
            }
        }
        return false;

    }

    public bool HasSomeHere(Tile tile, Obstacle[] obstacles)
    {
        foreach (Obstacle obstacle in obstacles)
        {
            if (tile.transform.position.x == obstacle.transform.position.x && tile.transform.position.z == obstacle.transform.position.z)
            {
                return true;
            }
        }
        return false;

    }

    public void ReduceCountZap(Player player)
    {
        if (player && player.crowdControl == Player.CrowdControl.ZAPEFFECT)
        {
            if (player.zapCount > 0)
            {
                player.zapCount -= 1;
            }
            if (player.zapCount <= 0 || player.stamina == 0)
            {
                player.zapCount = 0;
                player.crowdControl = Player.CrowdControl.NONE;
            }

        }
    }

    public bool IsCostEnough(Skill skill)
    {
        return skill.CostValue == skill.CurrrentValue;
    }

    public void AddCooldown()
    {
        if (this.Q.CurrrentValue < this.Q.CostValue)
        {
            this.Q.CurrrentValue += 1;
        }

        if (this.W.CurrrentValue < this.W.CostValue)
        {
            this.W.CurrrentValue += 1;
        }

        if (this.E.CurrrentValue < this.E.CostValue)
        {
            this.E.CurrrentValue += 1;
        }
    }

    public Skill getQ()
    {
        return this.Q;
    }

    public Skill getW()
    {
        return this.W;
    }

    public Skill getE()
    {
        return this.E;
    }


    public bool IsMyTeammate(Player player)
    {
        if(this.team.name == player.team.name && player)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

   









}
