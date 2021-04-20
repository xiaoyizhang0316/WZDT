using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task6 : BaseGuideStep
{
    public int taskTime = 0;
    public override IEnumerator StepStart()
    {
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().CheckEnd();
        T2_Manager.My.QualitySeed.GetComponent<QualityRole>().needCheck = true;
        
        T2_Manager.My.ResetTimeCountDown(taskTime);
        
        yield return null;
        Check();
    }
    
    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.5f);
    }

    void CheckGoal()
    {
        if (T2_Manager.My.time_remain <= 0)
        {
            T2_Manager.My.ResetTimeCountDown(taskTime);
            T2_Manager.My.QualityMerchant.GetComponent<QualityRole>().QualityReset();
        }
        
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
        yield return new WaitForSeconds(2);
    }
    
    #region delete 20210419

    /*public override IEnumerator StepStart()
    {
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.5f);
    }

    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            if (CheckAllSeedLevelUp())
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }

    bool CheckAllSeedLevelUp()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed &&
                PlayerData.My.MapRole[i].baseRoleData.baseRoleData.level == 1)
            {
                return false;
            }
        }

        return true;
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
