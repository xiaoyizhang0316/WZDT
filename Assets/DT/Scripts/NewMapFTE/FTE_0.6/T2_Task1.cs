using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class T2_Task1 : BaseGuideStep
{
    public override IEnumerator StepStart()
    {
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
            if (T2_Manager.My.CheckHasRole(GameEnum.RoleType.Seed))
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
        yield return new WaitForSeconds(2);
    }
    
    #region MyRegion 20210419 remove

    /*public override IEnumerator StepStart()
    {
        if (StageGoal.My.killNumber != 0)
        {
            StageGoal.My.killNumber = 0;
        }
        NewCanvasUI.My.GameNormal();
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
