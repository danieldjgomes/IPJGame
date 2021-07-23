using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSplit : MonoBehaviour
{
    Team team1 = null;
    Team team2 = null;
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
            if(player.team == team1)
            {
                team1count++;

            }
            if (player.team == team2)
            {
                team2count++;
            }

        }
        if(team1count == 0)
        {
            print("team 2 ganhou");
        }

        if (team2count == 0)
        {
            print("team 1 ganhou");
        }



    }
}
