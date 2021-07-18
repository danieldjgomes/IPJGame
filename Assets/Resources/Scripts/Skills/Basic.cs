using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Skill
{
    // Start is called before the first frame update

    public Basic(int costValue)
    {
        this.InitialValue = costValue;
        this.CurrrentValue = costValue;
        this.CostValue = costValue;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
