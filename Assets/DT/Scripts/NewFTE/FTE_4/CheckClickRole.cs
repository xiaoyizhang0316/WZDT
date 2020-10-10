using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClickRole : BaseGuideStep
{

    public Transform targetNPC;

    public override IEnumerator StepEnd()
    {
        NewCanvasUI.My.Panel_NPC.SetActive(true);
        if (targetNPC.GetComponent<NPC>().isLock)
        {
            NPCListInfo.My.ShowUnlckPop(targetNPC);
        }
        else
        {
            NPCListInfo.My.ShowNpcInfo(targetNPC);
        }
        yield break;
    }

    public override IEnumerator StepStart()
    {
        yield break;
    }
}
