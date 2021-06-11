using System.Collections;
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
            //outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            player.outline.OutlineWidth = 5f;

        }
        else
        {
            //outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            player.outline.OutlineWidth = 0;

        }

    }
}
