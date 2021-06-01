using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceDrinksGroup : BaseFinancialSkill
{
    public override void Skill()
    {
        switch (index)
        {
            case 0:
                StageGoal.My.GetPlayerGold(30000, true, true);
                break;
            case 1:
                StageGoal.My.GetPlayerGold(50000, true, true);
                break;
            case 2:
                StageGoal.My.GetPlayerGold(70000, true, true);
                break;
        }
        base.Skill();
    }
    
    
}
