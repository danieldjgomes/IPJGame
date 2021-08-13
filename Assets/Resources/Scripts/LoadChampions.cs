using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChampions : MonoBehaviour
{
    //public Player[] champions = new Player[6];
    public Round round;
    public GameObject go;

    // Start is called before the first frame update
    void Awake()
    {
        summonChampion("p0a");
        summonChampion("p0b");
        summonChampion("p0c");
        summonChampion("p1a");
        summonChampion("p1b");
        summonChampion("p1c");
        round.chars.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    private void Update()
    {
        go = round.chars[0];
    }




    public void summonChampion(string memorySlot)
    {

        //print(PlayerPrefs.GetString(memorySlot));
        
        if(memorySlot.Contains("0")){
            summonTeamHome(memorySlot);
        }

        if (memorySlot.Contains("1"))
        {
            summonTeamAway(memorySlot);
        }
    }

    public void summonTeamHome(string memorySlot)
    {
        Player p;
        if (memorySlot.Contains("a"))
        {
            p = Instantiate(slotToPlayer(memorySlot), new Vector3(3,1,3), Quaternion.identity);
            p.gameObject.SetActive(true);
            p.name = PlayerPrefs.GetString(memorySlot);
            p.team = new Team("Time1");
        }
        if (memorySlot.Contains("b"))
        {
            p = Instantiate(slotToPlayer(memorySlot), new Vector3(3, 1, 6), Quaternion.identity);
            p.gameObject.SetActive(true);
            p.name = PlayerPrefs.GetString(memorySlot);
            p.team = new Team("Time1");
        }
        if (memorySlot.Contains("c"))
        {
            p = Instantiate(slotToPlayer(memorySlot), new Vector3(3, 1, 9), Quaternion.identity);
            p.gameObject.SetActive(true);
            p.name = PlayerPrefs.GetString(memorySlot);
            p.team = new Team("Time1");
        }


    }

    public void summonTeamAway(string memorySlot)
    {
        Player p;
        if (memorySlot.Contains("a"))
        {
            p = Instantiate(slotToPlayer(memorySlot), new Vector3(30, 1, 30), Quaternion.identity);
            p.gameObject.SetActive(true);
            p.name = PlayerPrefs.GetString(memorySlot);
            p.team = new Team("Time2");
        }
        if (memorySlot.Contains("b"))
        {
            p = Instantiate(slotToPlayer(memorySlot), new Vector3(30, 1, 24), Quaternion.identity);
            p.gameObject.SetActive(true);
            p.name = PlayerPrefs.GetString(memorySlot);
            p.team = new Team("Time2");
        }
        if (memorySlot.Contains("c"))
        {
            p = Instantiate(slotToPlayer(memorySlot), new Vector3(30, 1, 27), Quaternion.identity);
            p.gameObject.SetActive(true);
            p.name = PlayerPrefs.GetString(memorySlot);
            p.team = new Team("Time2");
        }
    }

    public Player slotToPlayer(string memorySlot)
    {
        string championName = PlayerPrefs.GetString(memorySlot);
        //Player[] players = FindObjectsOfType<Player>();
        //foreach(Player p in players)
        //{
        //    if(p.name == championName)
        //    {
        //        return p;
        //    }
        //}
        string s = "Prefabs/" + championName;
        Player p = Resources.Load<Player>(s);
        return p;
    }
}
