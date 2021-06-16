using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilma : Player
{

    bool waitForNextFrame = false;
    public int WCounter = 3;
    public int QCounter = 1;


    public enum DilmaStage
    {
        IDLE, CASTINGQ, CASTINGW
    }

    public DilmaStage dilmaStage;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            dilmaStage = DilmaStage.IDLE;
        }

        castingQTrigger();
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
            Player[] players = FindObjectsOfType<Player>();

            if (Physics.Raycast(ray, out hit))
            {
                foreach (Tile tile in tiles)
                {
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, 2 * tile.transform.localScale.x, true)
                        && (hit.transform.position.x != this.transform.position.x || (hit.transform.position.z != this.transform.position.z))
                        && (hit.transform.position.x == tile.transform.position.x && hit.transform.position.z == tile.transform.position.z)
                        && !HasSomeHere(tile, airBags) && !HasSomePlayerHere(hit, players)
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

    public void castingQTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.dilmaStage == DilmaStage.CASTINGQ && Input.GetMouseButtonUp(0) && QCounter > 0)
        {
            Tile[] tiles = FindObjectsOfType<Tile>();
            Player[] players = FindObjectsOfType<Player>();

            if (Physics.Raycast(ray, out hit))
            {
                foreach (Tile tile in tiles)
                {
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, hit.transform.gameObject, 5 * tile.transform.localScale.x, true))
                    {
                        print(tile.transform.position.x);
                        foreach (Player player in players)
                        {
                            if (hit.transform.position.x == player.transform.position.x && hit.transform.position.z == player.transform.position.z)
                            {
                                print(player.name);
                                battle.DoDamage(5 * this.phisicalDamage, player);
                                QCounter -= 1;
                                
                            }
                        }
                        break;

                    }
                }


                if (QCounter <= 0)
                {
                   
                    this.stamina -= 5;
                    this.dilmaStage = DilmaStage.IDLE;
                    QCounter = 1;
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
        this.dilmaStage = DilmaStage.CASTINGW;
    }

    public override void UseSkillQ()
    {
        this.dilmaStage = DilmaStage.CASTINGQ;
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
