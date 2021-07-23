using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;

public class Basic : Skill
{
    // Start is called before the first frame update

    public Basic(int costValue, float range, bool friendlyFire)
    {
        this.InitialValue = costValue;
        this.CurrrentValue = costValue;
        this.CostValue = costValue;
        this.Range = range * GameUtils.Distance.GetBlockSize();
        this.friendlyFire = friendlyFire;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
