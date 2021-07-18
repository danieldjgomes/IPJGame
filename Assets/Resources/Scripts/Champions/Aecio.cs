using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aecio : Player
{

    bool waitForNextFrame = false;
    public int WStage = 0;
    public int staminaValue = 4;
    // Start is called before the first frame update
    void Start()
    {
        this.Q = new Basic(2);
        this.W = new Basic(3);
        this.E = new Ultimate(5);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CastingWTrigger();
        waitForNextFrame = false;
    }



    public override void UseSkillQ()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        Prepare prepare = Resources.Load<Prepare>("Prefabs/Prepare");

        if (prepare)
        {
            foreach (Tile tile in tiles)
            {
                if (GameUtils.Distance.IsEnoughDistance(this.gameObject, tile.gameObject, tile.transform.localScale.x * Mathf.Sqrt(2), true))
                {
                    Prepare obj = Instantiate(prepare, new Vector3(0, 1, 0), Quaternion.identity);
                    obj.transform.SetParent(tile.transform, false);
                    this.crowdControl = CrowdControl.ROOTED;
                    this.rootCount = 1;
                }
            }
        }
        else
        {
            print("Não achou");
        }
    }

    public void CastingWTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WStage == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                if (GameUtils.Distance.IsEnoughDistance(this.gameObject, hit.transform.gameObject, 5 * tile.transform.localScale.x, true))
                {
                    Player player = hit.transform.GetComponent<Player>();

                    if (player)
                    {
                        player.stamina += this.staminaValue;
                        WStage = 0;
                        this.playerStage = PlayerStage.IDLE;
                        this.stamina -= 5;
                    }




                }




            }

            waitForNextFrame = true;
        }
        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WStage == 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                if (GameUtils.Distance.IsEnoughDistance(this.gameObject, hit.transform.gameObject, 5 * tile.transform.localScale.x, true))
                {
                    Player player = hit.transform.GetComponent<Player>();
                    print(player.transform.name);
                    if (player)
                    {
                        print("Stamina roubada");
                        player.stamina -= this.staminaValue;
                        WStage = 1;
                        //battle.DoDamage(5 * this.phisicalDamage, player, Battle.AttackType.SKILL);
                        //battle.DoHeal(3 * this.phisicalDamage, this);
                        //this.playerStage = PlayerStage.IDLE;
                        //this.stamina -= 5;
                    }




                }

                waitForNextFrame = true;


            }


        }
        
    }



    public override void UseSkillW()
    {
        this.playerStage = PlayerStage.CASTINGW;
    }
}
