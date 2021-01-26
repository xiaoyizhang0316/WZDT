using System.Collections;
using System.Collections.Generic; 
using DG.Tweening;
using UnityEngine;
 
using UnityEngine.Assertions.Must;


public class FTE_1_4_2 : BaseGuideStep
{
    public GameObject inBorder;
    public GameObject outBorder;

    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.5f);
        yield return null;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (!inBorder.activeInHierarchy)
        {
            missiondatas.data[0].isFinish = true;
        }

        if (!outBorder.activeInHierarchy)
        {
            missiondatas.data[1].isFinish = true;
        }
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
    }
}