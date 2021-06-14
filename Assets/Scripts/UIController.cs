﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text text;
    public Round round;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = round.getActualPlayer().name;
    }

    public void SetOutlineTurn(Player player)
    {
        if (round.getActualPlayer() == player.gameObject)
        {
            
            player.outline.OutlineWidth = 5f;

        }
        else
        {
            
            player.outline.OutlineWidth = 0;

        }



    }

    public void SetMovimentPlayer(Player player)
    {
        if (player.playerStage == Player.PlayerStage.IDLE)
        {
            player.outline.OutlineColor = Color.white;
        }
        if (player.playerStage == Player.PlayerStage.MOVING)
        {
            player.outline.OutlineColor = Color.green;
        }
        if (player.playerStage == Player.PlayerStage.PREPARINGATTACK)
        {
            player.outline.OutlineColor = Color.yellow;
        }
        if (player.playerStage == Player.PlayerStage.TARGETABLE)
        {
            player.outline.OutlineColor = Color.red;
        }




    }
}
