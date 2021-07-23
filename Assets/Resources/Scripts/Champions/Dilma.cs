using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilma : Player
{

    bool waitForNextFrame = false;
    public int WCounter = 3;
    //public int QCounter = 1;

    private void Start()
    {
        this.Q = new Basic(2,5,false);
        this.W = new Basic(3,2,false);
        this.E = new Ultimate(5,0,true);

        this.outline.OutlineWidth = 5;

    }
    private void LateUpdate()
    {
        CastingQTrigger();
        CastingWTrigger();
        waitForNextFrame = false;

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

                if (GameUtils.Distance.IsEnoughDistance(this.gameObject, hit.transform.gameObject, Q.Range, true))
                {
                    Player player = hit.transform.GetComponent<Player>();

                    if (player && !this.IsMyTeammate(player))
                    {
                        battle.DoDamage(5 * this.phisicalDamage, player, Battle.AttackType.SKILL);
                        Q.ResetCooldown();
                        this.playerStage = PlayerStage.IDLE;
                    }




                }

                waitForNextFrame = true;


            }


        }
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
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, W.Range, true)
                        
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
                        W.ResetCooldown();
                        this.playerStage = PlayerStage.IDLE;
                    }
                    waitForNextFrame = true;


                }


                

            }


        }
    }

    public override void UseSkillE()
    {
        
        if (this.IsCostEnough(E))
        {
            Player[] players = FindObjectsOfType<Player>();
            foreach (Player player in players)
            {
                battle.DoDamage(3 * phisicalDamage, player, Battle.AttackType.SKILL);
            }

            E.ResetCooldown();
        }

        
    }

}
