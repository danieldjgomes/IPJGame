using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float speed;
    public int height;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, 14) - 4, transform.position.z);
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, 7) + height, transform.position.z);


    }
}
