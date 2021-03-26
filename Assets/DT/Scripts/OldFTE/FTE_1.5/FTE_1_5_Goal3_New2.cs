using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using DG.Tweening;

public class FTE_1_5_Goal3_New2 : BaseGuideStep
{
    public GameObject tapPanel;
    public GameObject tapPanelChild;
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GamePause(false);
        tapPanel.SetActive(true);
        InvokeRepeating("CheckGoal", 0.2f, 0.2f);
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false && tapPanelChild.activeInHierarchy)
        {
            missiondatas.data[0].isFinish = true;
        }
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2);
    }
}
