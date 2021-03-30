using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Task3 : BaseGuideStep
{
    public int checkBuffID = 0;
    
    public override IEnumerator StepStart()
    {
        T5_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        T5_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkBuff=checkBuffID;
        T5_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck=true;
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
            missiondatas.data[0].currentNum = T5_Manager.My.QualityMerchant.GetComponent<BaseMapRole>().warehouse.Count;
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
        T5_Manager.My.QualityMerchant.GetComponent<QualityRole>().CheckEnd();
        yield return new WaitForSeconds(3);
    }
}
