using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilma : Player
{

    bool waitForNextFrame = false;
    public int WCounter = 3;


    public enum DilmaStage
    {
        IDLE,CASTINGQ, CASTINGW
    }

    public DilmaStage dilmaStage;

    private void LateUpdate()
    {
        castingWTrigger();
        waitForNextFrame = false;
        

    }

    private void castingWTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.dilmaStage == DilmaStage.CASTINGW && Input.GetMouseButtonUp(0) && WCounter > 0)
        {
            Tile[] tiles = FindObjectsOfType<Tile>();
            AirBag[] airBags = FindObjectsOfType<AirBag>();

            if (Physics.Raycast(ray, out hit))
            {
                foreach (Tile tile in tiles)
                {
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, 2 * tile.transform.localScale.x, true)
                        && (tile.transform.position.x != this.transform.position.x || (tile.transform.position.z != this.transform.position.z))
                        && (hit.transform.position.x == tile.transform.position.x && hit.transform.position.z == tile.transform.position.z)
                        && !HasAirBagHere(tile, airBags)
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
                }
                

                if (WCounter <= 0)
                {
                    WCounter = 3;
                    this.stamina -= 5;
                    this.dilmaStage = DilmaStage.IDLE;
                }
                waitForNextFrame = true;


            }
        

        }
    }

    public override void UseSkillQ()
    {
        if (waitForNextFrame)
            return;

        Tile[] tiles = FindObjectsOfType<Tile>();
        AirBag[] airBags = FindObjectsOfType<AirBag>();

        foreach (Tile tile in tiles)
        {
            if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, 1 * tile.transform.localScale.x, true)
                && (tile.transform.position.x != this.transform.position.x || (tile.transform.position.z != this.transform.position.z))
                && !HasAirBagHere(tile,airBags)
                )

            {
                AirBag airBag = Resources.Load<AirBag>("Prefabs/AirBag");
                if (airBag)
                {
                    AirBag obj = Instantiate(airBag, new Vector3(0, 1, 0), Quaternion.identity);
                    obj.transform.SetParent(tile.transform, false);
                    

                }
                else
                {
                    print("Não achado");
                }


            }
        }
        this.stamina -= 5;
        waitForNextFrame = true;

    }

    private bool HasAirBagHere(Tile tile, AirBag[] airBags)
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


    public override void UseSkillW()
    {
        this.dilmaStage = DilmaStage.CASTINGW;
    }

}
