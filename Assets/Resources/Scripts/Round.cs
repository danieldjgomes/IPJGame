using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{

    public GameObject[] chars;
    bool waitForNextFrame = false;





    // Start is called before the first frame update
    void Start()
    {
        chars = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject charActual in chars)
        {
            //print(charActual.transform.name);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        waitForNextFrame = false;
    }

    public void finishTurn()
    {
        if (waitForNextFrame)
            return;

        Player player = chars[0].GetComponent<Player>();
        SetCounterCC(player);
        RestureStamina(chars[0]);
        RunCountSkills();
        chars = firstPlayerToLast(chars);
        


        
        waitForNextFrame = true;
    }

    public void RunCountSkills()
    {
        Aecio aecio = chars[1].transform.GetComponent<Aecio>();
        Player player = chars[0].transform.GetComponent<Player>();
        Prepare[] prepares = FindObjectsOfType<Prepare>();
        //print(chars[1].transform.name);
        //print(prepares.Length);
        if (aecio != null)
        {
            foreach(Prepare prepare in prepares)
            {
                //print(prepare.transform.position.x);
                Destroy(prepare.gameObject);
            }
        }
        player.AddCooldown();

        
    }

    public void SetCounterCC(Player player)
    {
        if (player && player.crowdControl == Player.CrowdControl.CONFUSE)
        {
            if (player.confuseCount > 0)
            {
                player.confuseCount -= 1;
            }
            if (player.confuseCount <= 0)
            {
                player.confuseCount = 0;
                player.crowdControl = Player.CrowdControl.NONE;
            }

        }

        if (player && player.crowdControl == Player.CrowdControl.TAUNT)
        {
            if (player.tauntCount > 0)
            {
                player.tauntCount -= 1;
            }
            if (player.tauntCount <= 0)
            {
                player.tauntCount = 0;
                player.crowdControl = Player.CrowdControl.NONE;
            }

        }

        if (player && player.crowdControl == Player.CrowdControl.ROOTED)
        {
            if (player.rootCount > 0)
            {
                player.rootCount -= 1;
            }
            if (player.rootCount <= 0)
            {
                player.rootCount = 0;
                player.crowdControl = Player.CrowdControl.NONE;
            }

        }

    }



    public void RestureStamina(GameObject character)
    {
        Player player = character.GetComponent<Player>();
        if (player)
        {
            player.stamina += 5;
        }
    }

   

    GameObject[] firstPlayerToLast(GameObject[] gos)
    {
        var tmp = gos[0];
        for (var i = 1; i < gos.Length; i++)
        {
            gos[i - 1] = gos[i];
        }
        gos[gos.Length - 1] = tmp;

        return gos;
    }

    public GameObject getActualPlayer()
    {   
        if (chars[0].activeSelf)
        {
            return chars[0];
        }
        else
        {
        firstPlayerToLast(chars);
            return chars[0];
        }
        
    }

    public void SetIdleAllPlayers()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            player.playerStage = Player.PlayerStage.IDLE;
        }

    }
}
