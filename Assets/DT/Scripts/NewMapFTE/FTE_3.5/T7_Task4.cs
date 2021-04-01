using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T7_Task4 : BaseGuideStep
{
    public int needQuality = 0;
    public int timeLimit = 0;
    private int startCost = 0;
    public override IEnumerator StepStart()
    {
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck=true;
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkQuality=needQuality;
        T7_Manager.My.ResetTimeCountDown(timeLimit);
        startCost = StageGoal.My.totalCost;
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = T7_Manager.My.QualityMerchant.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[1].currentNum = StageGoal.My.totalCost - startCost;
                if (missiondatas.data[1].currentNum > missiondatas.data[1].maxNum)
                {
                    Rest();
                    HttpManager.My.ShowTip("超出成本限制，任务重置！");
                }
                else
                {
                    missiondatas.data[0].isFinish = true;
                    missiondatas.data[1].isFinish = true;
                }
            }
        }

        missiondatas.data[1].currentNum = StageGoal.My.totalCost - startCost;
        if (missiondatas.data[1].currentNum > missiondatas.data[1].maxNum)
        {
            Rest();
            HttpManager.My.ShowTip("超出成本限制，任务重置！");
        }

        if (T7_Manager.My.time_remain <= 0)
        {
            Rest();
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
        }
    }

    void Rest()
    {
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        T7_Manager.My.ResetTimeCountDown(timeLimit);
        startCost = StageGoal.My.totalCost;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish && missiondatas.data[1].isFinish ;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().CheckEnd();
        T7_Manager.My.StopTimeCountDown();
        yield return new WaitForSeconds(3);
    }
}
