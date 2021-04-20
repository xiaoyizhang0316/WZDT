using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task4 : BaseGuideStep
{
    public int needQuality = 0;
    public override IEnumerator StepStart()
    {
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().CheckEnd();
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().checkQuality = needQuality;
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().needCheck = true;
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
            missiondatas.data[0].currentNum = T2_Manager.My.QualitySeed.GetComponent<QualityRole>().warehouse.Count;
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
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().CheckEnd();
        yield return new WaitForSeconds(2);
    }
    
    #region delete 20210419

    /*public GameObject redTipBuild;
    public override IEnumerator StepStart()
    {
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
            redTipBuild.SetActive(true);
            if (PlayerData.My.seedCount >= 2)
            {
                redTipBuild.SetActive(false);
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
