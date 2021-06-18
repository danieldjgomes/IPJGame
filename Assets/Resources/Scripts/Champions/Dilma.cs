using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilma : Player
{

    bool waitForNextFrame = false;
    public int WCounter = 3;
    public int QCounter = 1;

    private void LateUpdate()
    {
        
        CastingQTrigger();
        CastingWTrigger();
        waitForNextFrame = false;

    }

    private void CastingWTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WCounter > 0)
        {

            Tile[] tiles = FindObjectsOfType<Tile>();
            AirBag[] airBags = FindObjectsOfType<AirBag>();
            Player[] players = FindObjectsOfType<Player>();
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                

                if (tile)
                {
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, 2 * tile.transform.localScale.x, true)
                        
                        && (hit.transform.position.x == tile.transform.position.x && hit.transform.position.z == tile.transform.position.z)
                        && !HasSomeHere(tile, airBags) && tile.tileState != Tile.TileState.INUSE
                        )

                    {
                        AirBag airBag = Resources.Load<AirBag>("Prefabs/AirBag");
                        if (airBag)
                        {
                            AirBag obj = Instantiate(airBag, new Vector3(0, 1, 0), Quaternion.identity);
                            obj.transform.SetParent(tile.transform, false);
                            this.WCounter -= 1;


                        }
                        else
                        {
                            print("Não achado");
                        }


                    }


                    if (WCounter <= 0)
                    {
                        WCounter = 3;
                        this.stamina -= 5;

                        this.playerStage = PlayerStage.IDLE;
                    }
                    waitForNextFrame = true;


                }


                

            }


        }
    }

    public void CastingQTrigger()
    {
        if (waitForNextFrame)
            return;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (this.playerStage == PlayerStage.CASTINGQ && Input.GetMouseButtonUp(0))
           
        {
            
            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
               
                if (GameUtils.Distance.IsEnoughDistance(this.gameObject, hit.transform.gameObject, 5 * tile.transform.localScale.x, true))
                    {
                       Player player = hit.transform.GetComponent<Player>();
                       
                        if (player)
                        {
                            battle.DoDamage(5 * this.phisicalDamage, player);
                            this.playerStage = PlayerStage.IDLE;
                    }




                    }
               
                waitForNextFrame = true;


            }


        }
    }

    private bool HasSomeHere(Tile tile, AirBag[] airBags)
    {
       foreach(AirBag airBag in airBags)
        {
            if (tile.transform.position.x == airBag.transform.position.x && tile.transform.position.z == airBag.transform.position.z)
            {
                return true;
            }
        }
        return false;

    }

    private bool HasSomePlayerHere(RaycastHit ray, Player[] players)
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


    public override void UseSkillW()
    {
        this.playerStage = PlayerStage.CASTINGW;
    }

    public override void UseSkillQ()
    {
        this.playerStage = PlayerStage.CASTINGQ;
    }

    public override void UseSkillE()
    {
        Player[] players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            battle.DoDamage(3 * phisicalDamage, player);
        }
    }

}
