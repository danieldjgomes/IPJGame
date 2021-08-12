using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameUtils
{
    public class Utility : MonoBehaviour
    {
        public static bool IsEnoughDistance(GameObject ob1, GameObject ob2, float distance, bool useDiagonals)
        {
            if (!useDiagonals)
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

            return
                Vector3.Distance(
                          new Vector3(
                              ob1.transform.position.x, 1,
                              ob1.transform.position.z),
                          new Vector3(
                              ob2.transform.position.x, 1,
                              ob2.transform.position.z)) <= distance;


        }
        public static float GetBlockSize()
        {
            return 3f;
        }

        public static void SetParent(Transform child, Transform parent)
        {
            child.parent = parent;
            child.localScale = Vector3.zero;
            child.localRotation = Quaternion.identity;
            child.localScale = Vector3.one;
        }


    }
}