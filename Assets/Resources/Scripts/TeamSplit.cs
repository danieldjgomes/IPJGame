using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSplit : MonoBehaviour
{
    Team team1 = null;
    Team team2 = null;
    public UIController ui;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Team team1 = new Team("Time1");
        Team team2 = new Team("Time2");
        int i = 0;
        Player[] players = FindObjectsOfType<Player>();
        foreach(Player player in players)
        {
            if (i % 2 == 0)
            {
                player.team = team1;
            }
            else
            {
                player.team = team2;
            }
            //print(player.team.name);
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        int team1count = 0;
        int team2count = 0;
        Player[] players = FindObjectsOfType<Player>();

        foreach (Player player in players)
        {
            //print(player.name + " : " + player.team.name);
            if(player.team.name == "Time1")
            {
                team1count++;

            }
            if (player.team.name == "Time2")
            {
                team2count++;
            }

        }
        //print(team1count);
        //print(team2count);
        if (team1count == 0)
        {
            ui.restartGame.SetActive(true);
            //GameObject gameObject = GameObject.Find("FinalMessage");
            //gameObject.GetComponent<TextMesh>().text = "A chapa 2 ganhou a eleição! \n Te vejo daqui 4 anos.";
            text.text = "A chapa 2 ganhou a eleição! \n Te vejo daqui 4 anos.";
            print("team 2 ganhou");
        }

        if (team2count == 0)
        {
            ui.restartGame.SetActive(true);
            //GameObject gameObject = GameObject.Find("FinalMessage");
            //gameObject.GetComponent<TextMesh>().text = "A chapa 1 ganhou a eleição! \n Te vejo daqui 4 anos.";
            text.text = "A chapa 1 ganhou a eleição! \n Te vejo daqui 4 anos.";
            print("team 1 ganhou");
        }



    }
}
