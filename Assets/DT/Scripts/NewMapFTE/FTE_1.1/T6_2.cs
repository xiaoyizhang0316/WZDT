﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T6_2: BaseGuideStep
{
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(1); 

     
     
    }

    public override IEnumerator StepEnd()
    { 
        
        yield return new WaitForSeconds(2); 
      
    }

    public override bool ChenkEnd()
    {
        return StageGoal.My.playerTechPoint > 40;
    }

}