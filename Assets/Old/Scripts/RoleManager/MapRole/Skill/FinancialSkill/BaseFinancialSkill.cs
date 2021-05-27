using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BaseFinancialSkill : BaseSkill
{
    public string condition_1;
    public string condition_2;
    public string condition_3;

    public List<BaseMapRole> MapRoles;

    public GJJ gjj;
    //public bool isActive;
    
    public int index = -1;
    public virtual void Skill()
    {
        SkillCheckManager.My.ActiveRoleCheck(GetComponent<BaseMapRole>(),index);
        AutoStartGJJ();
    }

    public void AutoStartGJJ()
    {
        for (int i = 0; i <MapRoles.Count; i++)
        {
            gjj.AutoUseGJJ(MapRoles[i]);
            Debug.Log("打开广角镜"+MapRoles[i].baseRoleData.baseRoleData.roleName);
        }
    }

    public override void ReUnleashSkills()
    {
        
    }

    public override void Start()
    {
        base.Start();
    }
}
