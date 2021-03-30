using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Task2 : BaseGuideStep
{
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
        if (!missiondatas.data[0].isFinish)
        {
            if (NewCanvasUI.My.Panel_Update.activeInHierarchy)
            {
                missiondatas.data[0].isFinish = true;
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
                rect.SetActive(false);
                showRect = false;
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
