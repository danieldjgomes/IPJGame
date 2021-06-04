using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGuide : MonoBehaviour
{
    public int columnLength, rowLength;
    public float x_Space, z_Space;
    public GameObject prefab;

 
    // Start is called before the first frame update
    void Start()
    {
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
