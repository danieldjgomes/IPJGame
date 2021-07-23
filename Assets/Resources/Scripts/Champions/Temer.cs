using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temer : Player
{

   bool waitForNextFrame = false;
   public int QCounter = 1;

    private void LateUpdate()
    {

        CastingQTrigger();
        waitForNextFrame = false;

    }

    private void Start()
    {
        this.Q = new Basic(2,5,true);
        this.W = new Basic(3,4,true);
        this.E = new Ultimate(5,0,false);
    }

    public override void UseSkillW()
    {
        Player[] players = FindObjectsOfType<Player>();

        foreach (Player player in players)
        {
            if (GameUtils.Distance.IsEnoughDistance(this.gameObject, player.gameObject, W.Range, true) && player != this)
            {
                battle.DoDamage(3 * phisicalDamage, player, Battle.AttackType.SKILL);
            }
            
        }
        this.stamina -= 5;
        W.ResetCooldown();
    }

   

    //public override void UseSkillQ()
    //{
    //    this.playerStage = PlayerStage.CASTINGQ;
    //}

    public void CastingQTrigger(){
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
                    print(player.name);
                    if (player)
                    {
                        battle.DoDamage(5 * this.phisicalDamage, player, Battle.AttackType.SKILL);
                        battle.DoHeal(3* this.phisicalDamage, this);
                        this.playerStage = PlayerStage.IDLE;
                        this.stamina -= 5;
                        Q.ResetCooldown();
                    }




                }

                waitForNextFrame = true;


            }


        }
    }

    public override void UseSkillE()
    {
        if (this.IsCostEnough(E))
        {
            this.speed += 2;
            this.phisicalDamage += 5;
            
            if (this.attackCost > 1)
            {
                this.attackCost -= 1;
            }
            this.stamina -= 5;
            E.ResetCooldown();

        }
    }

}
