using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int stamina;
    public int speed;
    public int health;
    public int phisicalDamage;
    public int defense;
   

    public enum Range
    {
        Melee,
        Ranged
    }

    public Range Melee
    {
        get { return Melee; }
        set
        {
            AttackMode = value;
        }
    }

    public Range Ranged
    {
        get { return Ranged; }
        set
        {
            AttackMode = value;
        }
    }





    [SerializeField]
    private Range AttackMode;
   

    public Tile tile;
    public string playerStage;
    RaycastHit hit;
    Ray ray;
    public Round round;
    public Moviment moviment;
    public UIController ui;
    public Outline outline;
    public Attack attack;
    

    // Start is called before the first frame update
    void Start()
    {
        playerStage = "idle";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ui.SetOutlineTurn(this);
        ui.SetMovimentPlayer(this);



        if (round.getActualPlayer() == this.gameObject)
            {


            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                this.playerStage = "preparingAttack";
                attack.DoAttack(this);

               
            }

            if (this.playerStage == "preparingAttack" && Input.GetKeyDown(KeyCode.Escape))
            {
                playerStage = "idle";
            }
            


            if (Input.GetMouseButtonDown(0) && playerStage == "idle")

            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform == this.transform)
                    {
                        moviment.SetMovableTile(this);
                        playerStage = "moving";

                    }

                }
            }

            if (Input.GetMouseButtonDown(0) && playerStage == "moving")
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.Find("movable") != null)
                    {
                        moviment.MovePlayer(hit.transform, this);
                        moviment.RemoveMovableTile();
                        playerStage = "idle";
                    }
                }
            }

            if (playerStage == "moving" && stamina == 0)
            {
                playerStage = "idle";
            }

            if (playerStage == "moving")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    moviment.RemoveMovableTile();
                    playerStage = "idle";
                }
            }



            if (Input.GetKeyDown(KeyCode.Tab))
                {
                    playerStage = "idle";
                    round.finishTurn();
                }

        }

       

    }



    

    

}
