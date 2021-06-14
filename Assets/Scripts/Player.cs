using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{

    public enum AttackRange
    {
        MELEE, RANGED
    }


    public enum PlayerStage
    {
        IDLE, MOVING, PREPARINGATTACK, TARGETABLE
    }

   

    
    public int stamina;
    public int MaxStamina;
    public int speed;
    public int health;
    public int phisicalDamage;
    public int defense;
  

    public Tile tile;
    public PlayerStage playerStage;
    RaycastHit hit;
    Ray ray;
    public Round round;
    public Moviment moviment;


    public UIController ui;
    public Outline outline;
    public Attack attack;
    public AttackRange attackRange;

   


    // Start is called before the first frame update
    void Start()
    {
        playerStage = PlayerStage.IDLE;
        
    }

    // Update is called once per frame
    void Update()
    {
        limitStamina();
        //ui.SetOutlineTurn(this);
        ui.SetMovimentPlayer(this);

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

            


            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                this.playerStage = PlayerStage.PREPARINGATTACK;
                attack.SelectTarget(this);
     
            }

            if (Input.GetMouseButtonDown(0) && playerStage == PlayerStage.PREPARINGATTACK && this.stamina >= 3)
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
                            attack.DoDamage(this.phisicalDamage, player);
                            this.stamina -= 3;
                        }
                    }
                    
                   
                }
            }



            if (this.playerStage == PlayerStage.PREPARINGATTACK && Input.GetKeyDown(KeyCode.Escape))
            {
                playerStage = PlayerStage.IDLE;
                round.SetIdleAllPlayers();

            }

            


            if (Input.GetMouseButtonDown(0) && playerStage == PlayerStage.IDLE)

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



            if (Input.GetKeyDown(KeyCode.Tab))
                {
                    playerStage = PlayerStage.IDLE;
                    round.finishTurn();
                }

        }

        

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

    public int GetAttackRangeValue()
    {
        if (this.attackRange == AttackRange.MELEE)
        {
            return 1;
        }
        else
        {
            return 4;
        }
    }







}
