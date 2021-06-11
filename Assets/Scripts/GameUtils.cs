using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameUtils
{
    public class Distance : MonoBehaviour
    {
        public static bool IsEnoughDistance(GameObject ob1, GameObject ob2, int distance)
        {
           return 
                        Vector3.Distance(
                        new Vector3(
                            ob1.transform.position.x, 1,
                            ob1.transform.position.z),
                        new Vector3(
                            ob2.transform.position.x, 1,
                            ob2.transform.position.z)) <= distance
                            && (ob1.transform.position.x == ob2.transform.position.x || (ob1.transform.position.z == ob2.transform.position.z));
                            
          
        }
    }
}