using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDLJ : BaseGuideStep
{
    public Transform targetObj;

    public override IEnumerator StepEnd()
    {
        NPC[] temp = FindObjectsOfType<NPC>();
        foreach (var item in temp)
        {
            item.GetComponent<BoxCollider>().enabled = true;
        }
        highLight2DObjList[0].SetActive(true);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        highLight2DObjList[0].SetActive(false);
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (targetObj.GetComponent<NPC>().isCanSeeEquip)
            return true;
        return false;
    }
}
