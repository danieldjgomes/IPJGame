using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aecio : Player
{

    bool waitForNextFrame = false;
    public int WStage = 0;
    public int staminaValue = 4;

    // Start is called before the first frame update
    void Start()
    {
        this.Q = new Basic(2, Mathf.Sqrt(2),false);
        this.W = new Basic(3,5,false);
        this.E = new Ultimate(5,0,false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CastingWTrigger();
        waitForNextFrame = false;
    }



    public override void UseSkillQ()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        Prepare prepare = Resources.Load<Prepare>("Prefabs/Prepare");

        if (prepare)
        {
            foreach (Tile tile in tiles)
            {
                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, tile.gameObject, Q.Range, true))
                {
                    Prepare obj = Instantiate(prepare, new Vector3(0, 1, 0), Quaternion.identity);
                    obj.transform.SetParent(tile.transform, false);
                    this.crowdControl = CrowdControl.ROOTED;
                    this.rootCount = 1;
                }
            }
            this.stamina -= 5;
            Q.ResetCooldown();
        }
        else
        {
            print("Não achou");
        }
    }

    public void CastingWTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WStage == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, hit.transform.gameObject, W.Range, true))
                {
                    Player player = hit.transform.GetComponent<Player>();

                    if (player && this.IsMyTeammate(player))
                    {
                        print("Stamina entregue a: " + player);
                        player.stamina += this.staminaValue;
                        WStage = 0;
                        this.playerStage = PlayerStage.IDLE;
                        W.ResetCooldown();
                        this.stamina -= 5;
                    }




                }




            }

            waitForNextFrame = true;
        }
        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WStage == 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, hit.transform.gameObject, 5 * tile.transform.localScale.x, true))
                {
                    Player player = hit.transform.GetComponent<Player>();
                    
                    if (player && !this.IsMyTeammate(player))
                    {
                        print("Stamina roubada de: " + player);
                        player.stamina -= this.staminaValue;
                        WStage = 1;
                        
                    }




                }

                waitForNextFrame = true;


            }


        }
        
    }



    public override void UseSkillW()
    {
        this.playerStage = PlayerStage.CASTINGW;
    }
}
