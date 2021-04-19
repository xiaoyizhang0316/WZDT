using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task7 : BaseGuideStep
{
    private int tradeCount = 0;
    public override IEnumerator StepStart()
    {
        tradeCount = TradeManager.My.tradeList.Count;
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
            if (TradeManager.My.tradeList.Count < tradeCount)
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
    public override IEnumerator StepStart()
    {
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().QualityReset();
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().checkBuff = -1;
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().checkQuality = needQuality;
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().needCheck = true;
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
    }


    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().CheckEnd();
        yield return new WaitForSeconds(3);
    }*/

    #endregion
    
}
