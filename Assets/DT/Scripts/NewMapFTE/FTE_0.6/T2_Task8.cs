using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task8 : BaseGuideStep
{
    private int seedCount = 0;
    public override IEnumerator StepStart()
    {
        seedCount = PlayerData.My.seedCount;
        yield return null;
        Check();
    }
    
    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.5f);
    }

    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            if (PlayerData.My.seedCount < seedCount)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return missiondatas.CheckEnd();
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
    }
    
    #region delete 20210419

    /*public int needQuality = 0;
    public int taskTime = 0;
    public override IEnumerator StepStart()
    {
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkBuff = -1;
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkQuality = needQuality;
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck = true;
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
            missiondatas.data[0].currentNum = T2_Manager.My.QualityMerchant.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }

        if (T2_Manager.My.time_remain <= 0)
        {
            T2_Manager.My.ResetTimeCountDown(taskTime);
            T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        }
    }


    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().CheckEnd();
        T2_Manager.My.StopTimeCountDown();
        yield return new WaitForSeconds(3);
    }*/

    #endregion
    
}
