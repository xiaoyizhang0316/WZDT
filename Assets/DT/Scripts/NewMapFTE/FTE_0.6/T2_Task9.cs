using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task9 : BaseGuideStep
{
    public int needQuality = 0;
    public override IEnumerator StepStart()
    {
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().CheckEnd();
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkQuality = needQuality;
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck = true;
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
            missiondatas.data[0].currentNum = T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().warehouse.Count;
            if (missiondatas.data[0].CheckNumFinish())
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
        T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().CheckEnd();
        yield return new WaitForSeconds(2);
    }
    
    #region delete 20210419

    /*public override IEnumerator StepStart()
    {
        StageGoal.My.killNumber = 0;
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
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
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
        yield return new WaitForSeconds(3);
    }*/

    #endregion
    
}
