using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : Skill
{

    public Ultimate(int costValue)
    {
        this.InitialValue = 0;
        this.CurrrentValue = 0;
        this.CostValue = costValue;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
