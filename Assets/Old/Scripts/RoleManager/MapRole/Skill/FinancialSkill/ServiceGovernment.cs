using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceGovernment : BaseFinancialSkill
{
    public override void Skill()
    {
        switch (index)
        {
            case 0:
                StageGoal.My.GetPlayerGold(50000,true,true);
                break;
            case 1:
                StageGoal.My.GetPlayerGold(70000,true,true);
                break;
            case 2:
                StageGoal.My.GetPlayerGold(100000,true,true);
                break;
        }
        base.Skill();
    }
}
