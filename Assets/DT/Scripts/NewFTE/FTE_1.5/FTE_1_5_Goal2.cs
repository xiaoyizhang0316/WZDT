using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Goal2 : BaseGuideStep
{
    public Transform tapPanel;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal",0, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield break;
    }

    public override bool ChenkEnd()
    {
        
        return missiondatas.data[0].isFinish &&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false && RoleUpdateInfo.My.gameObject.activeInHierarchy)
        {
            missiondatas.data[0].isFinish = true;
        }

        if (missiondatas.data[1].isFinish == false && CreateTradeManager.My.gameObject.activeInHierarchy)
        {
            missiondatas.data[1].isFinish = true;
        }

        if (missiondatas.data[2].isFinish == false && tapPanel.childCount > 0)
        {
            missiondatas.data[2].isFinish = true;
        }
    }
}
