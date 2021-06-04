using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{

    public int stamina;
    public int moviment;
    public Tile tile;
    public string playerStage;
    RaycastHit hit;
    Ray ray;
    public Round round;

    // Start is called before the first frame update
    void Start()
    {
        playerStage = "idle";
    }

    // Update is called once per frame
    void Update()
    {
        //if (round.getActualPlayerTransform() == this.transform)
        if (round.getActualPlayerTransform() == this.gameObject.transform)
            {
            if (Input.GetMouseButtonDown(0) && playerStage == "idle")

            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform == this.transform)
                    {
                        setMovableTile();
                        playerStage = "moving";

                    }

                }
            }

            if (Input.GetMouseButtonDown(0) && playerStage == "moving")
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.Find("movable") != null)
                    {
                        movePlayer(hit.transform);
                        removeMovableTile();
                        playerStage = "idle";
                    }







                }
            }

            if (playerStage == "moving" && stamina == 0)
            {
                playerStage = "idle";
            }

            if (playerStage == "moving")
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    removeMovableTile();
                    playerStage = "idle";
                }
            }

            //if (playerStage == "idle")
            //{
            if (Input.GetKeyDown(KeyCode.Tab))
                {
                    round.finishTurn();
                }
            //}

        }

        

        


            
            
        


    }

    public void movePlayer(Transform hit)
    {
        Tile[] tileControllers = calculatePath();
        
        StartCoroutine(runPath(hit, tileControllers));
        //tile.setInUse();


    }

    public Tile[] calculatePath()
    {
       Tile[] tileControllers = FindObjectsOfType<Tile>();
        foreach (Tile tc in tileControllers)
        {
            tc.weight = (int)Mathf.Round(
                Vector3.Distance(
                new Vector3(
                    hit.transform.position.x,
                    hit.transform.position.z),
                new Vector3(
                    tc.transform.position.x, 
                    tc.transform.position.z)));

        }
        return tileControllers;

    }


        IEnumerator runPath(Transform hit, Tile[] tileControllers)
    {
        Tile path = null;
        //print("Coordenadas -  x: " + hit.transform.position.x +", y: " + hit.transform.position.z);

        foreach (Tile tc in tileControllers)
        {
            tc.setMaterialDefault(tc);
        }

            while(Vector3.Distance(
                new Vector3 (this.transform.position.x,1,this.transform.position.z),
                new Vector3(hit.transform.position.x, 1, hit.transform.position.z)

                ) != 0 )
        {
            foreach (Tile tc in tileControllers)
            {
               
                if(
                    
                    (int)Mathf.RoundToInt(
                        Vector3.Distance(
                        new Vector3(
                            this.transform.position.x,1,
                            this.transform.position.z),
                        new Vector3(
                            tc.transform.position.x,1,
                            tc.transform.position.z))) <= 1 && (this.transform.position.x == tc.transform.position.x || (this.transform.position.z == tc.transform.position.z)) && tc.inUse == false)
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

                        if(tc.transform.position.x == hit.transform.position.x || (tc.transform.position.z == hit.transform.position.z))
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
            if(stamina > 0)
            {
                this.transform.position = Vector3.MoveTowards(new Vector3(path.transform.position.x, 1, path.transform.position.z), new Vector3(hit.transform.position.x, 1, hit.transform.position.z), 0.000001f * Time.deltaTime);
                stamina -= 1;
               
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

    
 
 

    public void setMovableTile()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Tile");
        Vector3 currentPostion = transform.position;
        foreach (GameObject go in gos)
        {
            
            if (Vector3.Distance(new Vector3(go.transform.position.x,1,go.transform.position.z), new Vector3(currentPostion.x, 1, currentPostion.z)) <= moviment &&
                Vector3.Distance(new Vector3(go.transform.position.x, 1, go.transform.position.z), new Vector3(currentPostion.x, 1, currentPostion.z)) <= stamina)
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

    public void removeMovableTile()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("movableTile");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject tileGo in tiles)
        {
            tile.setColorGuideUnactive(tileGo.transform);
        }
    }

}
