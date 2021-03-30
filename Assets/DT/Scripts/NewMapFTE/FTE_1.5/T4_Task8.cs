using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task8 : BaseGuideStep
{
    public GameObject statPanel;
    public GameObject rect;
    public override IEnumerator StepStart()
    {
        
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
        if (!missiondatas.data[0].isFinish )
        {
            if (statPanel.activeInHierarchy)
            {
                if (showRect)
                {
                    if (!rect.activeInHierarchy)
                    {
                        missiondatas.data[0].isFinish = true;
                    }
                }
                else
                {
                    rect.SetActive(true);
                    showRect = true;
                }
            }
            else
            {
                showRect = false;
                rect.SetActive(false);
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
