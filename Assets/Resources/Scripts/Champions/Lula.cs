using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Lula : Player
{

    bool waitForNextFrame = false;
    int WCounter = 1;
    int direction = 0;


    public enum VectorDirection
    {
        UP, DOWN, LEFT, RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Q = new Basic(2,5,false);
        this.W = new Basic(3,1,false);
        this.E = new Ultimate(5,0,false);
        this.outline.OutlineWidth = 5;
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
      
        CastingQTrigger();
        CastingWTrigger();
        waitForNextFrame = false;

    }

    public override void UseSkillQ()
    {
        this.playerStage = PlayerStage.CASTINGQ;
    }

    public override void UseSkillW()
    {
        this.playerStage = PlayerStage.CASTINGW;
    }

    public void CastingQTrigger()
    {
        if (waitForNextFrame)
            return;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (this.playerStage == PlayerStage.CASTINGQ && Input.GetMouseButtonUp(0))
        {

            int layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, hit.transform.gameObject, Q.Range, true))
                {
                    Player player = hit.transform.GetComponent<Player>();

                    if (player && this.IsMyTeammate(player))
                    {
                        battle.DoHeal(3 * this.phisicalDamage, player);
                        this.playerStage = PlayerStage.IDLE;
                        Q.ResetCooldown();
                        this.stamina -= 5;
                    }




                }

                waitForNextFrame = true;


            }


        }
    }

    private void CastingWTrigger()
    {
        if (waitForNextFrame)
            return;

        if (this.playerStage == PlayerStage.CASTINGW && Input.GetMouseButtonUp(0) && WCounter > 0)
        {

            Tile[] tiles = FindObjectsOfType<Tile>();
            AirBag[] airBags = FindObjectsOfType<AirBag>();
            Player[] players = FindObjectsOfType<Player>();
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                Tile tile = hit.transform.GetComponent<Tile>();


                if (tile)
                {
                    if (GameUtils.Utility.IsEnoughDistance(this.gameObject, tile.gameObject,W.Range, false)
                        && (hit.transform.position.x == tile.transform.position.x 
                        && hit.transform.position.z == tile.transform.position.z)
                        )

                    {
                    
                     if (tile.transform.position.x == this.transform.position.x + tile.transform.localScale.x)
                        {
                            CastWToDirection(tile, VectorDirection.RIGHT);
                            WCounter -= 1;

                        }
                        if (tile.transform.position.x == this.transform.position.x - tile.transform.localScale.x)
                        {
                            CastWToDirection(tile, VectorDirection.LEFT);
                            WCounter -= 1;
                        }
                        if (tile.transform.position.z == this.transform.position.z + tile.transform.localScale.z)
                        {
                            CastWToDirection(tile, VectorDirection.UP);
                            WCounter -= 1;
                        }
                        if (tile.transform.position.z == this.transform.position.z - tile.transform.localScale.z)
                        {
                            CastWToDirection(tile, VectorDirection.DOWN);
                            WCounter -= 1;
                        }


                    }

                    if (WCounter <= 0)
                    {
                        WCounter = 1;
                        this.stamina -= 5;
                        W.ResetCooldown();
                        this.playerStage = PlayerStage.IDLE;
                    }
                    waitForNextFrame = true;
                }
            }
        }
    }

    private void CastWToDirection(Tile tile, VectorDirection vectorDirection)
    {
        int maskPlayer = 1 << LayerMask.NameToLayer("Player");
        List<Vector3> vectors = GetDirectionalVectors(tile, vectorDirection);
        RaycastHit[] hits = DetectPlayerinLine(vectors);
        ShowDebugLines(vectors);

        foreach (RaycastHit rc in hits)
        {
            print(rc.transform.name);
            Player player = rc.transform.GetComponent<Player>();
            if (!this.IsMyTeammate(player))
            {
                player.crowdControl = CrowdControl.ROOTED;
                player.rootCount = 1;
            }
            
        }
    }

    private void ShowDebugLines(List<Vector3> vectors)
    {
        Debug.DrawRay(vectors[0], vectors[1], Color.yellow, 10f, true);
        Debug.DrawRay(vectors[2], vectors[3], Color.yellow, 10f, true);
        Debug.DrawRay(vectors[4], vectors[5], Color.yellow, 10f, true);
    }

    public List<Vector3> GetDirectionalVectors(Tile tile, VectorDirection vectorDirection)
    {
        List<Vector3> vectors = new List<Vector3>();

            if (vectorDirection == VectorDirection.UP)
        {
            //Cima
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(0, 1, 10 * tile.transform.localScale.x));
            vectors.Add(new Vector3(this.transform.position.x + tile.transform.localScale.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(0, 1, 10 * tile.transform.localScale.x));
            vectors.Add(new Vector3(this.transform.position.x - tile.transform.localScale.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(0, 1, 10 * tile.transform.localScale.x));
            return vectors;
        }
        if (vectorDirection == VectorDirection.DOWN)
        {
            //Baixo
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(0, 1, -10 * tile.transform.localScale.x));
            vectors.Add(new Vector3(this.transform.position.x + tile.transform.localScale.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(0, 1, -10 * tile.transform.localScale.x));
            vectors.Add(new Vector3(this.transform.position.x - tile.transform.localScale.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(0, 1, -10 * tile.transform.localScale.x));
            return vectors;
        }

        if (vectorDirection == VectorDirection.LEFT)
        {
            //Esquerda
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(-10 * tile.transform.localScale.x, 1, 0));
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z + tile.transform.localScale.x));
            vectors.Add(new Vector3(-10 * tile.transform.localScale.x, 1, 0));
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z - tile.transform.localScale.x));
            vectors.Add(new Vector3(-10 * tile.transform.localScale.x, 1, 0));
            return vectors;
        }

        else
        {
            //Direita
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z));
            vectors.Add(new Vector3(10 * tile.transform.localScale.x, 1, 0));
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z + tile.transform.localScale.x));
            vectors.Add(new Vector3(10 * tile.transform.localScale.x, 1, 0));
            vectors.Add(new Vector3(this.transform.position.x, 1, this.transform.position.z - tile.transform.localScale.x));
            vectors.Add(new Vector3(10 * tile.transform.localScale.x, 1, 0));
            return vectors;
        }



    }

    public RaycastHit[] DetectPlayerinLine(List<Vector3> vectors)
    {
        int maskPlayer = 1 << LayerMask.NameToLayer("Player");
        return Physics.RaycastAll(
                                                     vectors[0], vectors[1], Mathf.Infinity, maskPlayer)
                                                    .Concat(Physics.RaycastAll(vectors[2], vectors[3], Mathf.Infinity, maskPlayer))
                                                    .ToArray()
                                                    .Concat(Physics.RaycastAll(vectors[4], vectors[5], Mathf.Infinity, maskPlayer))
                                                    .ToArray();
    }


    public override void UseSkillE()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        Triplex triplex = Resources.Load<Triplex>("Prefabs/Triplex/triplex");

        if (triplex)
        {
            foreach (Tile tile in tiles)
            {
                if (GameUtils.Utility.IsEnoughDistance(this.gameObject, tile.gameObject, 4.5f*Mathf.Sqrt(2), true))
                {
                    Triplex obj = Instantiate(triplex, new Vector3(tile.transform.position.x, 1, tile.transform.position.z), Quaternion.identity);
                    obj.owner = this;
                    //Triplex obj = Instantiate(triplex, new Vector3(0, 1, 0), Quaternion.identity);
                    //obj.transform.SetParent(tile.transform, false);
                }
            }
            E.ResetCooldown();
            this.stamina -= 5;
        }

       

        




    }



    


}
