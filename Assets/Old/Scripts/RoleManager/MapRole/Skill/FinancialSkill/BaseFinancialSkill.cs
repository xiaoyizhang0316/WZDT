using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFinancialSkill : BaseSkill
{
    public string condition_1;
    public string condition_2;
    public string condition_3;

    //public bool isActive;
    
    public int index = -1;
    public virtual void Skill()
    {
        SkillCheckManager.My.ActiveRoleCheck(GetComponent<BaseMapRole>(),index);
    }

    public override void ReUnleashSkills()
    {
        
    }

    public override void Start()
    {
        base.Start();
    }
}
