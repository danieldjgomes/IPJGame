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
        
        Player[] players = FindObjectsOfType<Player>();
        foreach(Player player in players)
        {

            if(player.name == "MamaeFalei" || player.name == "Dilma" || player.name == "Aecio")
            {
                player.team = team1;
            }
            else
            {
                player.team = team2;
            }
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
            if(player.team.name == "Time1")
            {
                team1count++;

            }
            if (player.team.name == "Time2")
            {
                team2count++;
            }

        }
        print(team1count);
        print(team2count);
        if (team1count == 0)
        {
            ui.restartGame.SetActive(true);
            text.text = "A chapa 2 ganhou a eleição! \n Te vejo daqui 4 anos.";
            print("team 2 ganhou");
        }

        if (team2count == 0)
        {
            ui.restartGame.SetActive(true);
            text.text = "A chapa 1 ganhou a eleição! \n Te vejo daqui 4 anos.";
            print("team 1 ganhou");
        }



    }
}
