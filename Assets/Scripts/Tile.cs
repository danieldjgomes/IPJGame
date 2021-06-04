using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Material selectedMaterial;
    public Material defaultMaterial;
    public Material matAtlantica;
    public Material floresAmaz;
    public Material caatinga;
    public Material pantanal;
    public int weight;
    public TextMesh text;
    public string biome;
    public bool inUse;
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {

        Tile[] tileControllers = FindObjectsOfType<Tile>();
        foreach (Tile tc in tileControllers)
        {
            tc.biome = randomBiome();
        }

    }

    // Update is called once per frame
    void Update()
    {
        this.text.text = this.weight.ToString();
        
        if (this.biome == "matAtlantica")
        {
            transform.GetComponent<Renderer>().material = matAtlantica;
        }

        if (this.biome == "floresAmaz")
        {
            transform.GetComponent<Renderer>().material = floresAmaz;
        }

        if (this.biome == "caatinga")
        {
            transform.GetComponent<Renderer>().material = caatinga;
        }

        if (this.biome == "pantanal")
        {
            transform.GetComponent<Renderer>().material = pantanal;
        }




    }

    string randomBiome()
    {
        int r = rnd.Next(0, 101);

        if (r >= 0 && r < 4)
        {
            return "matAtlantica";
        }
            

        if (r >= 4 && r < 10)
        {
            return "floresAmaz";
        }
           

        if (r >= 10 && r < 20)
        {
            return "caatinga";
        }  

        if (r >= 20 && r < 40)
        {

            return "pantanal";
        }

        return "default";
        
    }

   
    //public void setInUse()
    //{
    //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


    //    foreach (GameObject player in players)
    //    {
    //        if (this.transform.position.x == player.transform.position.x && (this.transform.position.z == player.transform.position.z))
    //            {
    //            this.inUse = true;
               
    //        }

           

    //    }   
    //}

    public void setMaterialSelected(Component go)
    {
        go.transform.GetComponent<Renderer>().material = selectedMaterial;
    }

    public void setColorGuideActive(Component go)
    {
        go.GetComponentInChildren<TextMesh>().color = new Color(0, 1, 0);
    }

    public void setColorGuideUnactive(Component go)
    {
        go.GetComponentInChildren<TextMesh>().color = new Color(1, 1, 1);
    }

    public void setMaterialDefault(Component go)
    {
        go.transform.GetComponent<Renderer>().material = defaultMaterial;
    }
}
