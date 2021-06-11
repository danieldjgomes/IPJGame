﻿using System.Collections;
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

    public void DoAttack(Player currentPlayer)
    {
        Player[] players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            if (GameUtils.Distance.IsEnoughDistance(currentPlayer.gameObject, player.gameObject, 2) && currentPlayer != player)
            {
                print(player.name);
            }
        }

    }
}