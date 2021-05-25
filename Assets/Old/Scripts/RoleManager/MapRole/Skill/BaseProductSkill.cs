using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseProductSkill : BaseSkill
{
    public override void Start()
    {
        base.Start();
        if (IsOpen)
        {
            UnleashSkills();
        }
    }
    
    /// <summary>
    /// 释放技能（每一类生产型角色需重新override）
    /// </summary>
    public abstract void Skill();

    /// <summary>
    /// 根据效率进行生产（若效率公式有差别，则需重新override）
    /// </summary>
    public virtual void UnleashSkills()
    {
        isPlay = true;
        float d = Mathf.Min( 4f,1f / (role.baseRoleData.efficiency * 0.05f));
        transform.DORotate(transform.eulerAngles, d).OnComplete(() =>
        {
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });
    }

    public virtual void OnEndTurn()
    {
        
    }

    /// <summary>
    /// 重启释放技能
    /// </summary>
    public override void ReUnleashSkills()
    {
        IsOpen = true;
        Debug.Log("重启技能" + role.baseRoleData.ID);
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            if (!isPlay)
                UnleashSkills();
        }
        else
            UnleashSkills();
    }

    public override void Init()
    {
        IsOpen = true;
        Start();
    }
}
