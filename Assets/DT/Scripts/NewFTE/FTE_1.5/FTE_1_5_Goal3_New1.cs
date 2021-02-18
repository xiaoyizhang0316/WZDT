using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using DG.Tweening;

public class FTE_1_5_Goal3_New1 : BaseGuideStep
{
    public GameObject border;
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GamePause(false);
        InvokeRepeating("CheckGoal", 0.2f, 0.2f);
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish==false )
        {
            if (NewCanvasUI.My.Panel_Update.activeInHierarchy)
            {
                border.SetActive(true);
                if (border.GetComponent<MouseOnThis>().isOn)
                {
                    missiondatas.data[0].isFinish = true;
                    border.SetActive(false);
                }
            }
            else
            {
                border.SetActive(false);
            }
        }
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2);
        RoleUpdateInfo.My.gameObject.SetActive(false);
    }
}
