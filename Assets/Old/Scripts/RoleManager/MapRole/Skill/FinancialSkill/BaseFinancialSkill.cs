using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFinancialSkill : BaseSkill
{
    public int index = -1;
    public virtual void Skill()
    {
        SkillCheckManager.My.ActiveRoleCheck(GetComponent<BaseMapRole>(),index);
    }

    public override void ReUnleashSkills()
    {
        
    }
}
