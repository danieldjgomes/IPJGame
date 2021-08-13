using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChampionSelect : MonoBehaviour
{
    public int selectIndex;
    public Team team;
    public List<string> players;
    public List<Text> texts;
    

    // Start is called before the first frame update
    void Start()
    {
        team = new Team("Home");
        selectIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<=3; i++)
        {
            try
            {
                if (players[i] != null)
                {
                    texts[i].text = players[i];
                }
            }
            catch
            {

            }
            
            
           
        }

        
       
    
    }

    public void SetChampion(Team team, List<string> players)
    {
        if(team.name == "Home")
        {
            PlayerPrefs.SetString("p0a", players[0]);
            print(PlayerPrefs.GetString("p0a"));
            PlayerPrefs.SetString("p0b", players[1]);
            print(PlayerPrefs.GetString("p0b"));
            PlayerPrefs.SetString("p0c", players[2]);
            print(PlayerPrefs.GetString("p0c"));
        }

        if (team.name == "Away")
        {
            PlayerPrefs.SetString("p1a", players[0]);
            PlayerPrefs.SetString("p1b", players[1]);
            PlayerPrefs.SetString("p1c", players[2]);
        }


    }

    public void ConfirmChoice()
    {
        if(selectIndex == 0 && players.Count == 3)
        {
            SetChampion(team, players);
            
            
            selectIndex = 1;
            team.name = "Away";
            players.Clear();
            ButtonSelect[] buttons = FindObjectsOfType<ButtonSelect>();
            foreach(ButtonSelect b in buttons)
            {
                b.isSelected = false;
            }

            foreach(Text t in texts)
            {
                t.text = "";
            }

        }

        if (selectIndex == 1 && team.name == "Away")
        {
            SetChampion(team, players);
            SceneManager.LoadScene("Game");
        }
    }

    //public void removeChampion()
    //{

    //}






}
