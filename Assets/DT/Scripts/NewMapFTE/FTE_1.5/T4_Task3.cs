using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T4_Task3 : BaseGuideStep
{
    public int needQualty = 0;
    private int startCost = 0;
    public override IEnumerator StepStart()
    {
        T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().checkQuality = needQualty;
        T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck = true;
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
        missiondatas.data[1].currentNum = GetCost();
        if (missiondatas.data[1].currentNum > missiondatas.data[1].maxNum)
        {
            T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().donotAdd = true;
        }
        if (!missiondatas.data[0].isFinish && missiondatas.data[1].currentNum<=missiondatas.data[1].maxNum)
        {
            T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().donotAdd = false;
            missiondatas.data[0].currentNum = T4_Manager.My.QualityMerchant.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
                if (missiondatas.data[1].currentNum<=missiondatas.data[1].maxNum)
                {
                    missiondatas.data[1].isFinish = true;
                }
            }
        }
    }

    int GetCost()
    {
        int cost = 0;
        for (int i= 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].isNpc)
            {
                cost += PlayerData.My.MapRole[i].baseRoleData.cost;
            }
        }

        return cost;
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&& missiondatas.data[1].isFinish;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().needCheck = false;
        T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().donotAdd = false;
        T4_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        yield return new WaitForSeconds(3);
    }
}
