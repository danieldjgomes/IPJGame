using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text text;
    public Round round;
   

   private static FloatingText floatingText;
   private static GameObject canvas;


    // Update is called once per frame]

    private static void Initialize()
    {
        floatingText = Resources.Load<FloatingText>("Prefabs/DamagePopUpParent");
        canvas = GameObject.Find("UiController");
    }
    private void Start()
    {
        Initialize();
        
    }
    void Update()
    {
        text.text = round.getActualPlayer().name;
        
    }

    public static void ShowDamagePopUp(string damage, Transform location) {

        FloatingText instance = Instantiate(floatingText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(location.position.x, location.position.y,location.position.z));
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(damage);
    }


    public void SetOutLineColor(Player player)
    {
        if (player != round.getActualPlayer() && player.playerStage == Player.PlayerStage.IDLE)
        {
            player.outline.OutlineWidth = 0f;
        }

        if (player.name == round.getActualPlayer().name && player.playerStage == Player.PlayerStage.IDLE)
        {
            player.outline.OutlineWidth = 5f;
            player.outline.OutlineColor = Color.white;
        }
        if (player.playerStage == Player.PlayerStage.MOVING)
        {
            player.outline.OutlineWidth = 5f;
            player.outline.OutlineColor = Color.green;
        }
        if (player.playerStage == Player.PlayerStage.PREPARINGATTACK)
        {
            player.outline.OutlineWidth = 5f;
            player.outline.OutlineColor = Color.yellow;
        }
        if (player.playerStage == Player.PlayerStage.TARGETABLE)
        {
            player.outline.OutlineWidth = 5f;
            player.outline.OutlineColor = Color.red;
        }




    }
}
