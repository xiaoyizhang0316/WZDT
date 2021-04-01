using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Task2 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            if (NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().commonServiceInfo.activeInHierarchy 
                && NPCListInfo.My.currentNpc.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.SoftFactory)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
