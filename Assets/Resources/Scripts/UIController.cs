using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text playerName;
    public Text playerStamina;
    public Text playersHealth;
    public Text roundOrder;
    public Round round;
    public Button attackButton;
    public Sprite[] staminaBar;
    public GameObject restartGame;
    public Button restartButton;


    public Image staminaImage;
    public Image healthImage;

    public Image QImage;
    public Image WImage;
    public Image EImage;

    public bool alreadyCastStamina;


    

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
        alreadyCastStamina = false;

    }
    void LateUpdate()
    {
        Sprite punchImage = Resources.Load<Sprite>("Images/punchButton");
        Sprite rockImage = Resources.Load<Sprite>("Images/rockButton");
        Sprite punchImageActive = Resources.Load<Sprite>("Images/punchButtonActive");
        Sprite rockImageActive = Resources.Load<Sprite>("Images/rockButtonActive");
        Player[] players = FindObjectsOfType<Player>();

        string playersList = "";
        string orderList = "Ordem dos personagens: \n";

        foreach (Player p in players)
        {
            if (p.name == round.getActualPlayer().name)
            {
                Skill q = p.getQ();
                Skill w = p.getW();
                Skill e = p.getE();

                string textPlayer = "";
                textPlayer += p.transform.name + "\n";
                //textPlayer += string.Format("Q: {0}/{1}\n", q.CurrrentValue, q.CostValue);
                //textPlayer += string.Format("W: {0}/{1}\n", w.CurrrentValue, w.CostValue);
                //textPlayer += string.Format("E: {0}/{1}\n", e.CurrrentValue, e.CostValue);


                playerName.text = textPlayer;
                //playerName.text = player.name;
                playerStamina.text = p.stamina + "/" + p.MaxStamina;
            }

            this.SetOutLineColor(p);

            playersList += string.Format("{0}: {1}/{2} \n", p.name, p.health, p.maxHealth);


        }
        playersHealth.text = playersList;

        foreach (GameObject gameObject in round.chars)
        {
            Player pl = gameObject.GetComponent<Player>();

            orderList += string.Format("{0}: ({1}) \n", pl.transform.name, pl.crowdControl); ;
        }
        roundOrder.text = orderList;

        if ((round.getActualPlayer().GetComponent<Player>().playerStage == Player.PlayerStage.IDLE))
        {
            UpdateSkillUI(round.getActualPlayer().GetComponent<Player>());
            if (round.getActualPlayer().GetComponent<Player>().attackRange == Player.AttackRange.MELEE)
            {
                attackButton.image.sprite = punchImage;
            }

            else
            {
                attackButton.image.sprite = rockImage;
            }
        }

        if ((round.getActualPlayer().GetComponent<Player>().playerStage == Player.PlayerStage.PREPARINGATTACK))
        {
            if (round.getActualPlayer().GetComponent<Player>().attackRange == Player.AttackRange.MELEE)
            {
                attackButton.image.sprite = punchImageActive;
            }

            else
            {
                attackButton.image.sprite = rockImageActive;
            }
        }



        //Player player = round.getActualPlayer().GetComponent<Player>();
        //for (int i = 0; i < 20; i++)
        //{
        //    GameObject sprite = Resources.Load<GameObject>("Images/StaminaPrefab");

        //    GameObject go = Instantiate(staminaImage.gameObject);
        //    go.name = i.ToString();
        //    go.transform.SetParent(staminaImage.transform);
        //    go.transform.parent = canvas.transform;
        //    go.GetComponent<RectTransform>().sizeDelta = staminaImage.GetComponent<RectTransform>().sizeDelta;
        //    go.GetComponent<RectTransform>().localScale = staminaImage.GetComponent<RectTransform>().localScale;
        //    go.GetComponent<RectTransform>().position = staminaImage.GetComponent<RectTransform>().position;
        //    go.gameObject.transform.position = new Vector3(go.gameObject.transform.position.x + i * go.GetComponent<RectTransform>().rect.width / 2, go.gameObject.transform.position.y, go.gameObject.transform.position.z);
        //    go.transform.parent = staminaImage.transform.parent;
        //    go.transform.tag = "Stamina";
        //    go.GetComponent<Image>().sprite = staminaBar[3];
        //    go.SetActive(true);


        //    if (int.Parse(go.name) <= player.stamina)
        //    {
        //        go.GetComponent<Image>().sprite = staminaBar[2];
        //    }
        //    if (int.Parse(go.name) > player.stamina && int.Parse(go.name) <= player.MaxStamina)
        //    {
        //        go.GetComponent<Image>().sprite = staminaBar[0];
        //    }

        //    //if(int.Parse(go.name) < player.MaxStamina && int.Parse(go.name) < 20)
        //    //{
        //    //    go.GetComponent<Image>().sprite = staminaBar[0];
        //    //}


        //}

      

    }

    public void UpdateStaminaUI(Player player)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Stamina");

        foreach (GameObject go in gos)
        {
            if (go.name != "Stamina")
            {
                DestroyImmediate(go.gameObject);
            }

        }
        this.alreadyCastStamina = false;

        if (!alreadyCastStamina)
        {
            for (int i = 1; i <= 20; i++)
            {
                GameObject sprite = Resources.Load<GameObject>("Images/StaminaPrefab");

                GameObject go = Instantiate(staminaImage.gameObject);
                go.name = i.ToString();
                go.transform.SetParent(staminaImage.transform);
                go.transform.parent = canvas.transform;
                go.GetComponent<RectTransform>().sizeDelta = staminaImage.GetComponent<RectTransform>().sizeDelta;
                go.GetComponent<RectTransform>().localScale = staminaImage.GetComponent<RectTransform>().localScale;
                go.GetComponent<RectTransform>().position = staminaImage.GetComponent<RectTransform>().position;
                go.gameObject.transform.position = new Vector3(go.gameObject.transform.position.x + i * go.GetComponent<RectTransform>().rect.width, go.gameObject.transform.position.y, go.gameObject.transform.position.z);
                go.transform.parent = staminaImage.transform.parent;
                go.transform.tag = "Stamina";
                go.GetComponent<Image>().sprite = staminaBar[3];
                go.SetActive(true);


                if (int.Parse(go.name) < player.stamina +1)
                {
                    go.GetComponent<Image>().sprite = staminaBar[2];
                }
                if (int.Parse(go.name) > player.stamina && int.Parse(go.name) <= player.MaxStamina)
                {
                    go.GetComponent<Image>().sprite = staminaBar[0];
                }

                if(player.playerStage == Player.PlayerStage.CASTINGQ)
                {
                    if (int.Parse(go.name) < player.stamina + 1 && int.Parse(go.name) > player.stamina-player.getQ().CostValue)
                    {
                        go.GetComponent<Image>().sprite = staminaBar[1];
                    }
                }

                if (player.playerStage == Player.PlayerStage.CASTINGW)
                {
                    if (int.Parse(go.name) < player.stamina + 1 && int.Parse(go.name) > player.stamina - player.getW().CostValue)
                    {
                        go.GetComponent<Image>().sprite = staminaBar[1];
                    }
                }

                if (player.playerStage == Player.PlayerStage.CASTINGE)
                {
                    if (int.Parse(go.name) < player.stamina + 1 && int.Parse(go.name) > player.stamina - player.getE().CostValue)
                    {
                        go.GetComponent<Image>().sprite = staminaBar[1];
                    }
                }

                

            }
            alreadyCastStamina = true;
        }
    }

    public void UpdateHealthUI(Player player)
    {
        healthImage.rectTransform.localScale = new Vector3(player.health/ (float)player.maxHealth,1f,1f);      
    }

    public void UpdateSkillUI(Player player)
    {
        Color grey = Color.gray;
        Color blue = new Color32(0, 168, 250, 255);
        Color red = new Color32(254, 12, 11, 255);
        if (player.getQ().CostValue > player.stamina)
        {
            QImage.color = grey;
        }
        else{
            QImage.color = Color.white;
        }

        if (player.getW().CostValue > player.stamina)
        {
            WImage.color = grey;
        }
        else
        {
            WImage.color = Color.white;
        }

        if (player.getE().CostValue > player.stamina)
        {
            EImage.color = grey;
        }

        if (!player.IsCostEnough(player.getQ()))
        {
            QImage.color = red;
        }
        else
        {
            QImage.color = Color.white;
        }

        if (!player.IsCostEnough(player.getW()))
        {
            WImage.color = red;
        }
        else
        {
            WImage.color = Color.white;
        }

        if (!player.IsCostEnough(player.getE()))
        {
            EImage.color = red;
        }


        else
        {
            EImage.color = Color.white;
        }

        QImage.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0}/{1}", player.getQ().CurrrentValue, player.getQ().CostValue);
        WImage.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0}/{1}", player.getW().CurrrentValue, player.getW().CostValue);
        EImage.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0}/{1}", player.getE().CurrrentValue, player.getE().CostValue);

        if(player.playerStage == Player.PlayerStage.CASTINGQ)
        {
            QImage.color = blue;
        }

        if (player.playerStage == Player.PlayerStage.CASTINGW)
        {
            WImage.color = blue;
        }
        if (player.playerStage == Player.PlayerStage.CASTINGE)
        {
            EImage.color = blue;
        }



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

    public void ResetOutLine()
    {
        Player[] players = FindObjectsOfType<Player>();


        foreach(Player p in players)
        {
            p.outline.OutlineWidth = 0;
        }
    }
}
