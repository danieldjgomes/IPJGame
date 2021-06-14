using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGuide : MonoBehaviour
{
    public int columnLength, rowLength;
    private float x_Space, z_Space;
    public GameObject prefab;
    public Tile tile;

 
    // Start is called before the first frame update
    void Start()
    {
        x_Space = tile.transform.localScale.x;
        z_Space = tile.transform.localScale.z;
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            GameObject tile = Instantiate(prefab, new Vector3(
                x_Space + (x_Space * (i % columnLength)),
                0,
                z_Space + (z_Space * (i / rowLength))), Quaternion.identity
               );

            
            tile.transform.parent = GameObject.Find("Grid").transform;

        }

    }

    // Update is called once per frame
    void Update()
    {
       

    }

}
