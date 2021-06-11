using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    public Tile tile;

    public void SetMovableTile(Player player)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Tile");
        Vector3 currentPostion = player.transform.position;
        foreach (GameObject go in gos)
        {

            if (Vector3.Distance(new Vector3(go.transform.position.x, 1, go.transform.position.z), new Vector3(currentPostion.x, 1, currentPostion.z)) <= player.speed &&
                Vector3.Distance(new Vector3(go.transform.position.x, 1, go.transform.position.z), new Vector3(currentPostion.x, 1, currentPostion.z)) <= player.stamina)
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

    public void movePlayer(Transform hit, Player player)
    {
        Tile[] tileControllers = calculatePath(hit);
        StartCoroutine(runPath(hit, tileControllers, player));
    }

    public Tile[] calculatePath(Transform hit)
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
                tc.weight = Mathf.Floor(
                 Vector3.Distance(
                 new Vector3(
                     hit.transform.position.x,
                     hit.transform.position.z),
                 new Vector3(
                     tc.transform.position.x,
                     tc.transform.position.z)));
            }

          

        }
        return tileControllers;

    }

    IEnumerator runPath(Transform hit, Tile[] tileControllers, Player player)
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

                    (int)Mathf.RoundToInt(
                        Vector3.Distance(
                        new Vector3(
                            player.transform.position.x, 1,
                            player.transform.position.z),
                        new Vector3(
                            tc.transform.position.x, 1,
                            tc.transform.position.z))) <= 1 && (player.transform.position.x == tc.transform.position.x || (player.transform.position.z == tc.transform.position.z)) && tc.inUse == false)
                {


                    if (path is null)
                    {
                        path = tc;
                    }


                    if (path.weight > tc.weight)
                    {
                        path = tc;


                    }

                    if (path.weight == tc.weight)
                    {

                        if (tc.transform.position.x == hit.transform.position.x || (tc.transform.position.z == hit.transform.position.z))
                        {
                            path = tc;
                        }


                        //TODO: Arrumar erro na movimentação

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
            //print(path.transform.position.x + ":" + path.transform.position.z);

            if (path.transform.position.x == hit.transform.position.x && path.transform.position.z == hit.transform.position.z)
            {
                break;
            }

            yield return null;
        }


        yield return new WaitForSeconds(1f);

    }
}
