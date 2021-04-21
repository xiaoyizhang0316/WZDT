﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N1_Step7 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        yield return null;
    }
    
    public override bool ChenkEnd()
    {
        return StageGoal.My.isTurnStart;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(0.5f);
    }
}
