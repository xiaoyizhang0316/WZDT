using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RubishProcess : BaseExtraSkill
{
    public List<BaseMapRole> clearRoleList= new List<BaseMapRole>();

    public int eachClearNumber;

    public RubishProcessItem item;

    public override void SkillOn(TradeSign sign)
    {
        base.SkillOn(sign);
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole));
        clearRoleList.Add(target);
    }

    public override void SkillOff(TradeSign sign)
    {
        base.SkillOff(sign);
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole));
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
