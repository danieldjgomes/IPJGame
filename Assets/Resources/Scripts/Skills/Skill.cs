﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int InitialValue;
    public int CurrrentValue;
    public int CostValue;
    public float Range;
    public bool friendlyFire;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetCooldown()
    {
        this.CurrrentValue = 0;
    }
}
