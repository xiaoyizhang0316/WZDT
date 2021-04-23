﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N0_Step1 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        isStepEnd = false;
        InvokeRepeating("Check", 1,1);
        yield return null;
    }

    private bool isStepEnd = false;
    void Check()
    {
        if (StageGoal.My.currentWave > 1 && !StageGoal.My.isTurnStart )
        {
            isStepEnd = true;
        }
    }

    public override bool ChenkEnd()
    {
        return isStepEnd;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(0.5f);
    }
}