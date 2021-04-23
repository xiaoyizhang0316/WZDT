using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N1_Step4 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        yield return null;
    }
    
    public override bool ChenkEnd()
    {
        return NewCanvasUI.My.Panel_AssemblyRole.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(0.5f);
    }
}
