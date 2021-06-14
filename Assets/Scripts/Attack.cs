using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;


public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTarget(Player currentPlayer)
    {
        Player[] players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            if (GameUtils.Distance.IsEnoughDistance(currentPlayer.gameObject, player.gameObject, player.GetAttackRangeValue(), true) && currentPlayer != player)
            {
               
                player.SetTargable();

                //DoDamage(currentPlayer.phisicalDamage, player);
            }
        }

    }

    public void DoDamage(int damage, Player targetPlayer)
    {
        targetPlayer.health -= Mathf.FloorToInt(damage * (100f/(100+targetPlayer.defense)));
        print(targetPlayer.health);
    }

     
}
