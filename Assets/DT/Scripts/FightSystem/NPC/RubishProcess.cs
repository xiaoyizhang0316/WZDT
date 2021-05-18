using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RubishProcess : BaseServiceSkill
{
    public List<BaseMapRole> clearRoleList= new List<BaseMapRole>();

    public int eachClearNumber;

    public RubishProcessItem item;

    public override void Skill(TradeData data)
    {
        base.Skill(data);
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        clearRoleList.Add(target);
    }

    public override void SkillOff(TradeData data)
    {
        base.SkillOff(data);
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(data.targetRole));
        clearRoleList.Remove(target);
    }

    public override void Start()
    {
        base.Start();
        AutoClear();
    }

    public void AutoClear()
    {
        transform.DOScale(1f, 5f).OnComplete(() =>
        {
            int clearNumber = 0;
            for (int i = 0; i < clearRoleList.Count; i++)
            {
                clearNumber += clearRoleList[i].ClearTrash(eachClearNumber);
            }
            item.countDownNumber += clearNumber;
            AutoClear();
        });
        
    }
}
