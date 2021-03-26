using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_01_Step_13 : BaseGuideStep
{
    public GameObject hand;
    bool exec = false;
    public override IEnumerator StepEnd()
    {
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.5f);
        Invoke("HandMove", 5);
    }

    void HandMove()
    {
        exec = true;
        hand.SetActive(true);
    }

    public override bool ChenkEnd()
    {
        if (RoleEditor.My.isDragEnd)
        {
            if (exec)
            {
                hand.SetActive(false);
            }
            else
            {
                CancelInvoke("HandMove");
            }
            return true;
        }
        return false;
    }
}
