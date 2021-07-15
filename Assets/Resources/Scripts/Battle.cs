using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;


public class Battle : MonoBehaviour
{

    public enum AttackType
    {
        MELEE, RANGED, SKILL
    }

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTarget(Player currentPlayer)
    {

        if (currentPlayer.crowdControl == Player.CrowdControl.TAUNT)
        {
            if (GameUtils.Distance.IsEnoughDistance(currentPlayer.gameObject, currentPlayer.tauntedTarget.gameObject, currentPlayer.GetAttackRangeValue(), true) && currentPlayer != currentPlayer.tauntedTarget)
                   {
                        currentPlayer.tauntedTarget.SetTargable();

                    }
           
        }

        else
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (GameUtils.Distance.IsEnoughDistance(currentPlayer.gameObject, player.gameObject, currentPlayer.GetAttackRangeValue(), true) && currentPlayer != player)
                {
                    player.SetTargable();

                }
            }
        }

       

    }

    public void DoDamage(int damage, Player targetPlayer, AttackType attackType)
    {
        int appliedDamage;
       
        //Bolsonaro
        if (targetPlayer is Bolsonaro && attackType == AttackType.MELEE)
        {
            print("Melee Bolsonaro");
            appliedDamage = Mathf.FloorToInt(damage * (100f / (100 + targetPlayer.defense)) * 0.8f);
            targetPlayer.health -= appliedDamage;
            UIController.ShowDamagePopUp(appliedDamage.ToString(), targetPlayer.transform);
            //Todo UI da Passiva
        }
        else
        {
            print("Dano Comum");
            //Player normal
            appliedDamage = Mathf.FloorToInt(damage * (100f / (100 + targetPlayer.defense)));
            targetPlayer.health -= appliedDamage;
            UIController.ShowDamagePopUp(appliedDamage.ToString(), targetPlayer.transform);
        }


        print(targetPlayer.health);
    }

    public void DoHeal(int value, Player targetPlayer)
    {
        targetPlayer.health += value;
        print(targetPlayer.health);
    }

}
