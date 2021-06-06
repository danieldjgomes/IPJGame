using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

   
{
    public float cameraSpeed;
    public GameObject objects;
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        //if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftControl))
        //{
        //    objects.transform.Rotate(new Vector3(0, 1, 0), 1);

        //}

        //if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftControl))
        //{
        //    objects.transform.Rotate(new Vector3(0, 1, 0), 0.005f);

        //}


        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed, Space.World);
            //this.transform.Translate(new Vector3(1,0,0) * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
           this.transform.Translate(Vector3.left * Time.deltaTime * cameraSpeed, Space.World);
          
           
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * cameraSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * cameraSpeed, Space.World);
        }
    }


}
