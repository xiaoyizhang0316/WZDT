using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task5 : BaseGuideStep
{
    public int taskTime = 0;
    public override IEnumerator StepStart()
    {
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().QualityReset();
        T2_Manager.My.ResetTimeCountDown(taskTime);
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
            missiondatas.data[0].currentNum = T2_Manager.My.QualitySeed.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (T2_Manager.My.time_remain <= 0)
        {
            T2_Manager.My.ResetTimeCountDown(taskTime);
            T2_Manager.My.QualitySeed.GetComponent<QualityRole>().QualityReset();
        }
    }


    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        T2_Manager.My.StopTimeCountDown();
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
