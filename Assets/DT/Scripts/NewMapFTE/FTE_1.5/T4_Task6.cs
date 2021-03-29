using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task6 : BaseGuideStep
{
    public GameObject panel_tab;
    public override IEnumerator StepStart()
    {
        panel_tab.SetActive(true);
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
            if (panel_tab.transform.GetChild(0).gameObject.activeInHierarchy)
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
