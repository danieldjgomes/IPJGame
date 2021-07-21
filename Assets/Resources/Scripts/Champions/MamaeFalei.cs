using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaeFalei : Player
{

    bool waitForNextFrame = false;
    


    private void LateUpdate()
    {

        CastingQTrigger();
        waitForNextFrame = false;

    }

    private void Start()
    {
        this.Q = new Basic(2);
        this.W = new Basic(3);
        this.E = new Ultimate(5);
    }


    public override void UseSkillQ()
    {
        this.playerStage = PlayerStage.CASTINGQ;
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

                    if (player && !this.IsMyTeammate(player))
                    {
                        battle.SetCrowdControl(CrowdControl.TAUNT, player);
                        player.tauntedTarget = this;
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
        Player[] players = FindObjectsOfType<Player>();

        foreach (Player player in players)
        {
            battle.SetCrowdControl(CrowdControl.CONFUSE, player);
        }
        this.stamina -= 5;
        E.ResetCooldown();

    }

    public override void UseSkillW()
    {
        base.UseSkillW();
        this.stamina -= 5;
        W.ResetCooldown();
    }

}
