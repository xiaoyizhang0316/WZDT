using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Task6 : BaseGuideStep
{
    public override IEnumerator StepStart()
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
    }
}
