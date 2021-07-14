using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;

public class Moviment : MonoBehaviour
{
    public Tile tile;

    public void SetMovableTile(Player player)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Tile");
        Vector3 currentPostion = player.transform.position;
        foreach (GameObject go in gos)
        {

            if (Vector3.Distance(new Vector3(go.transform.position.x, 1, go.transform.position.z), new Vector3(currentPostion.x, 1, currentPostion.z)) <= player.speed * tile.transform.localScale.x &&
                Vector3.Distance(new Vector3(go.transform.position.x, 1, go.transform.position.z), new Vector3(currentPostion.x, 1, currentPostion.z)) <= player.stamina * tile.transform.localScale.x)
            {
                tile.setColorGuideActive(go.transform);
                GameObject obj = new GameObject("movable");
                obj.transform.position = go.transform.position;
                obj.transform.tag = "movableTile";
                obj.transform.parent = go.transform;


            }
            else
            {
                tile.setColorGuideUnactive(go.transform);
            }
        }
    }

    public void RemoveMovableTile()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("movableTile");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tileGo in tiles)
        {
            tile.setColorGuideUnactive(tileGo.transform);
        }
    }

    public void MovePlayer(Transform hit, Player player)
    {
        Tile[] tileControllers = CalculateWeight(hit);
        StartCoroutine(RunPath(hit, tileControllers, player));
    }

    public Tile[] CalculateWeight(Transform hit)
    {
        Tile[] tileControllers = FindObjectsOfType<Tile>();
        foreach (Tile tc in tileControllers)
        {
            tc.weight = Mathf.Floor(
                Vector3.Distance(
                new Vector3(
                    hit.transform.position.x,
                    hit.transform.position.z),
                new Vector3(
                    tc.transform.position.x,
                    tc.transform.position.z)));

            if (tc.transform.position.x != hit.transform.position.x && tc.transform.position.z != hit.transform.position.z)
            {
                tc.weight = 
                    Mathf.Floor(
                 Vector3.Distance(
                 new Vector3(
                     hit.transform.position.x,
                     hit.transform.position.z),
                 new Vector3(
                     tc.transform.position.x,
                     tc.transform.position.z))
                    )
                    ;
            }

            UpdateInUseTile(tc);



        }
        return tileControllers;

    }


    IEnumerator RunPath(Transform hit, Tile[] tileControllers, Player player)
    {
        Tile path = null;

        foreach (Tile tc in tileControllers)
        {
            tc.setMaterialDefault(tc);
        }

        while (Vector3.Distance(
            new Vector3(player.transform.position.x, 1, player.transform.position.z),
            new Vector3(hit.transform.position.x, 1, hit.transform.position.z)

            ) != 0)
        {
            foreach (Tile tc in tileControllers)
            {
               

                if (
                    GameUtils.Distance.IsEnoughDistance(player.gameObject, tc.gameObject, tile.transform.localScale.x, false)
                  
                    )
                {
                   
                    if (path is null)
                    {
                        path = tc;
                    }

                    if (path.weight > tc.weight)
                    {
                        path = tc;
                    }

                    else
                    {
                        if (path.weight == tc.weight)
                        {

                            if (tc.transform.position.x == hit.transform.position.x || (tc.transform.position.z == hit.transform.position.z))
                            {
                                path = tc;
                            }

                           

                        }
                    }
                    

                    if (tc.weight == 0)
                    {
                        path = tc;

                    }

                }


            }
            if (player.stamina > 0)
            {    

                player.transform.position = Vector3.MoveTowards(new Vector3(path.transform.position.x, 1, path.transform.position.z), new Vector3(hit.transform.position.x, 1, hit.transform.position.z), 0.000001f * Time.deltaTime);
                player.stamina -= 1;
                

            }
            

            if (path.transform.position.x == hit.transform.position.x && path.transform.position.z == hit.transform.position.z)
            {
                break;
            }

            path = null;
            yield return null;
        }


        yield return new WaitForSeconds(1f);

    }

    void UpdateInUseTile(Tile tile)
    {
        Player[] players = FindObjectsOfType<Player>();

        foreach(Player p in players)
        {
            if(tile.transform.position.x == p.transform.position.x && tile.transform.position.z == p.transform.position.z)
            {
                tile.weight *= 100;
            }
        }
    }
}
