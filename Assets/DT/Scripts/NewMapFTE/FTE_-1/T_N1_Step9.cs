using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_N1_Step9 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        yield return null;
    }
    
    public override bool ChenkEnd()
    {
        return WinManager.My.transform.GetChild(1).gameObject.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(0.5f);
    }
}
