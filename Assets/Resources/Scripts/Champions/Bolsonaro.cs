using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolsonaro : Player
{

    bool waitForNextFrame = false;
    public int WCounter = 1;
    public int QCounter = 2;

    private void Start()
    {
        this.Q = new Basic(2,2,false);
        this.W = new Basic(3, 3* Mathf.Sqrt(2),true);
        this.E = new Ultimate(5,10,false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //UIController.ShowDamagePopUp("X", hit.transform);
        CastingQTrigger();
        CastingWTrigger();
        waitForNextFrame = false;
    }

    private void CastingQTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.playerStage == PlayerStage.CASTINGQ && Input.GetMouseButtonUp(0) && QCounter > 0)
        {
            Bull[] bulls = FindObjectsOfType<Bull>();
            int layerMask = 1 << LayerMask.NameToLayer("Tile");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Tile tile = hit.transform.GetComponent<Tile>();

                if (tile)
                {
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, Q.Range, true)
                        && (hit.transform.position.x == tile.transform.position.x && hit.transform.position.z == tile.transform.position.z)
                        && !HasSomeHere(tile, bulls)
                        && tile.tileState != Tile.TileState.INUSE
                        )

                    {
                        Bull bull = Resources.Load<Bull>("Prefabs/Bull");
                        if (bull)
                        {
                            Bull obj = Instantiate(bull, new Vector3(0, 1, 0), Quaternion.identity);
                            obj.transform.SetParent(tile.transform, false);
                            this.QCounter -= 1;
                        }
                        else
                        {
                            print("Não achado");
                        }


                    }


                    if (QCounter <= 0)
                    {
                        QCounter = 2;
                        this.stamina -= 5;
                        this.playerStage = PlayerStage.IDLE;
                        Q.ResetCooldown();
                    }
                    waitForNextFrame = true;


                }




            }


        }
    }
    private void CastingWTrigger()
    {
        
        if (waitForNextFrame)
            return;

        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WCounter > 0)
        {

            Player[] players = FindObjectsOfType<Player>();
            int layerMask = 1 << LayerMask.NameToLayer("Tile");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                    if (GameUtils.Distance.IsEnoughDistance(this.gameObject, hit.transform.gameObject, W.Range, true))
                        {
                            foreach(Player player in players)
                            {
                                if (player != this && GameUtils.Distance.IsEnoughDistance(hit.transform.gameObject, player.transform.gameObject, 2* tile.transform.localScale.x * Mathf.Sqrt(2), true))
                                {
                                    UIController.ShowDamagePopUp("X", hit.transform);
                                    this.battle.DoDamage(this.phisicalDamage * 2, player, Battle.AttackType.SKILL);
                                    WCounter -= 1;
                                }
                            }

                        }
                if (WCounter <= 0)
                {
                    WCounter = 1;
                    this.stamina -= 5;
                    this.playerStage = PlayerStage.IDLE;
                    W.ResetCooldown();
                }
                waitForNextFrame = true;

            }
    }
       }

    //public override void UseSkillQ()
    //{
    //    this.playerStage = PlayerStage.CASTINGQ;
    //}

    //public override void UseSkillW()
    //{
    //    this.playerStage = PlayerStage.CASTINGW;
    //}

    public override void UseSkillE()
    {
        if (this.IsCostEnough(E))
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (GameUtils.Distance.IsEnoughDistance(this.gameObject, player.gameObject, E.Range, true) && player != this && !this.IsMyTeammate(player))
                {
                    battle.SetCrowdControl(CrowdControl.ZAPEFFECT, player);
                }
            }
            this.stamina -= 5;
            E.ResetCooldown();
        }
       

    }



}
