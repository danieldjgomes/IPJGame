using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public ChampionSelect championSelect;
    public string championName;
    public bool isSelected = false;
    private int index;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isSelected)
        {
            this.GetComponent<Image>().color = Color.green;
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }

    }

    public void addChampion()
    {
        if (isSelected)
        {
            index = championSelect.players.IndexOf(championName);
            championSelect.players.Remove(championSelect.players[index]);
            championSelect.texts[index].text = "";
            this.isSelected = false;
        }
        else
        {
            if(championSelect.players.Count < 3)
            {
                championSelect.players.Add(championName);
                this.isSelected = true;
            }
            
        }
 
    }






    



}
