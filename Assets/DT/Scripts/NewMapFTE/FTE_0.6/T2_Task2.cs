using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task2 : BaseGuideStep
{
    public GameObject redTipBullet;
    public GameObject seedDetail_panel;
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
            if (NewCanvasUI.My.Panel_Update.activeInHierarchy 
                && NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().currentRole.baseRoleData.roleType== GameEnum.RoleType.Seed)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (!missiondatas.data[1].isFinish)
        {
            if (NewCanvasUI.My.Panel_Update.activeInHierarchy)
            {
                redTipBullet.SetActive(true);
                if (seedDetail_panel.activeInHierarchy)
                {
                    redTipBullet.SetActive(false);
                    missiondatas.data[1].isFinish = true;
                }
            }
        }
    }


    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&& missiondatas.data[1].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
