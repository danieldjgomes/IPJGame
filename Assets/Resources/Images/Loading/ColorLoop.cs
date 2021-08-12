using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLoop : MonoBehaviour
{
    public Gradient myGradient;
    public float strobeDuration = 2f;

    void Update()
    {
        //this.GetComponent<Image>().color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
        this.GetComponent<Image>().color = myGradient.Evaluate(t);
    }
}
