using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T7_Task2 : BaseGuideStep
{
    public GameObject roleEnco;
    public List<GameObject> npcEncos;

    public GameObject rect;
    public override IEnumerator StepStart()
    {
        roleEnco.SetActive(true);
        for (int i = 0; i < npcEncos.Count; i++)
        {
            npcEncos[i].SetActive(true);
        }
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    private bool showRect = false;
    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            if (NewCanvasUI.My.Panel_TradeSetting.activeInHierarchy)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (!missiondatas.data[1].isFinish)
        {
            if (NewCanvasUI.My.Panel_Update.activeInHierarchy)
            {
                rect.SetActive(true);
                showRect = true;
            }
            else
            {
                rect.SetActive(false);
                showRect = false;
            }
        }

        if (showRect)
        {
            if (!rect.activeInHierarchy)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
