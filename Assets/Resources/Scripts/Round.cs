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

        print("Turno de : " + chars[0].name + " Finalizado. Indo para turno de: " + chars[1].name);
        RestureStamina(chars[0]);
        chars = firstPlayerToLast(chars);


        waitForNextFrame = true;
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







//public GameObject[] chars;
//public GameObject actual;
//public int index;



// Start is called before the first frame update
//void Start()
//{
//    chars = GameObject.FindGameObjectsWithTag("Player");
//    foreach (GameObject charActual in chars)
//    {
//        print(charActual.transform.name);
//    }
//    actual = chars[0];
//    index = 0;


//}

// Update is called once per frame
//void Update()
//{

//}

//public void finishTurn()
//{
//    if (index == chars.Length - 1)
//    {
//        index = 0;
//    }
//    else
//    {
//        index += 1;
//    }
//    print(index);
//    change(index);

//}

//public void change(int i)
//{
//    actual = chars[i];
//}