using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilma : Player
{
    public enum DilmaStage
    {
        CASTINGQ
    }

    public DilmaStage dilmaStage;

    private void Update()
    {
        



    }


    public override void UseSkillQ()
    {
        this.dilmaStage = DilmaStage.CASTINGQ;
    }




}
