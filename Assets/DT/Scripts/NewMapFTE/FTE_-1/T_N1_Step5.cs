using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N1_Step5 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        yield return null;
    }
    
    public override bool ChenkEnd()
    {
        return CreatRoleManager.My.EquipList.Count>0;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return null;
    }
}
