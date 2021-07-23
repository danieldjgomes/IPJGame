using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : Skill
{

    public Ultimate(int costValue, float range, bool friendlyFire)
    {
        this.InitialValue = 0;
        this.CurrrentValue = 0;
        this.CostValue = costValue;
        this.Range = range;
        this.friendlyFire = friendlyFire;
            }


    // Update is called once per frame
    void Update()
    {
        
    }
}
