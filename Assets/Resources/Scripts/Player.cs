using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{

    public enum AttackRange
    {
        MELEE, RANGED
    }

    public enum CrowdControl
    {
        NONE, TAUNT, ROOTED, CONFUSE
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

                                battle.DoDamage(this.phisicalDamage, player);
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
                    if (Physics.Raycast(ray, out hit))
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

                    if (Physics.Raycast(ray, out hit))
                    {

                        if (hit.transform.Find("movable") != null)
                        {
                            moviment.MovePlayer(hit.transform, this);
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

        

    }

   
    public virtual void UseSkillQ()
    {
    }
    public virtual void UseSkillW()
    {
    }
    public virtual void UseSkillE()
    {
    }


    private void limitStamina()
    {
        if (stamina > MaxStamina){
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







}
