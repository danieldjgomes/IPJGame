using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text playerName;
    public Text playerStamina;
    public Text playersHealth;
    public Text roundOrder;
    public Round round;
   

   private static FloatingText floatingText;
   private static GameObject canvas;


    // Update is called once per frame

    private static void Initialize()
    {
        floatingText = Resources.Load<FloatingText>("Prefabs/DamagePopUpParent");
        canvas = GameObject.Find("UiController");
    }
    private void Start()
    {
        Initialize();
        
    }
    void LateUpdate()
    {
        Player[] players = FindObjectsOfType<Player>();
        string playersList = "";
        string orderList = "Ordem dos personagens: \n";

        foreach(Player player in players)
        {
            if (player.name == round.getActualPlayer().name)
            {
                Skill q = player.getQ();
                Skill w = player.getW();
                Skill e = player.getE();

                string textPlayer = "";
                textPlayer += player.transform.name + "\n";
                textPlayer += string.Format("Q: {0}/{1}\n", q.CurrrentValue, q.CostValue);
                textPlayer += string.Format("W: {0}/{1}\n", w.CurrrentValue, w.CostValue);
                textPlayer += string.Format("E: {0}/{1}\n", e.CurrrentValue, e.CostValue);


                playerName.text = textPlayer;
                //playerName.text = player.name;
                playerStamina.text = player.stamina + "/" + player.MaxStamina;
            }

            this.SetOutLineColor(player);

            playersList += string.Format("{0}: {1}/{2} \n", player.name, player.health, player.maxHealth);

            
        }
        playersHealth.text = playersList;

        foreach(GameObject gameObject in round.chars)
        {
            Player pl = gameObject.GetComponent<Player>();
            
            orderList += string.Format("{0}: ({1}) \n", pl.transform.name, pl.crowdControl); ; 
        }
        roundOrder.text = orderList;

       
        

        
    }

    public static void ShowDamagePopUp(string damage, Transform location) {

        FloatingText instance = Instantiate(floatingText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(location.position.x, location.position.y+ 1.0f ,location.position.z));
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(damage);
    }


    public void SetOutLineColor(Player player)
    {
        if (player.gameObject != round.getActualPlayer() && player.playerStage == Player.PlayerStage.IDLE)
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
            //print(player.name + " : " + player.outline.OutlineWidth);
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
