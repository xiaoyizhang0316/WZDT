using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T7_Task1 : BaseGuideStep
{
    public int needQuality = 0;
    public int timeLimit = 0;
    public MissionData hideMissionData;
    private int startCost = 0;
    public override IEnumerator StepStart()
    {
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck=true;
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkQuality=needQuality;
        T7_Manager.My.ResetTimeCountDown(timeLimit);
        NewCanvasUI.My.GameNormal();
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
        if (!missiondatas.data[0].isFinish)
        {
            missiondatas.data[0].currentNum = T7_Manager.My.QualityMerchant.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
                hideMissionData.currentNum = StageGoal.My.totalCost - startCost;
                hideMissionData.isFail = true;
                MissionManager.My.AddMission(hideMissionData, missionTitle);
                HttpManager.My.ShowTip("超出成本限制，任务失败！");
            }
        }

        if (T7_Manager.My.time_remain <= 0)
        {
            T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
            T7_Manager.My.ResetTimeCountDown(timeLimit);
            startCost = StageGoal.My.totalCost;
            HttpManager.My.ShowTip("超出时间限制，任务重置！");
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        T7_Manager.My.QualityMerchant.GetComponent<QualityRole>().CheckEnd();
        T7_Manager.My.StopTimeCountDown();
        yield return new WaitForSeconds(3);
    }
}
