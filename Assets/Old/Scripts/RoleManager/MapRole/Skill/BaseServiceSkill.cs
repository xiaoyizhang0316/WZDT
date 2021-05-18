using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseServiceSkill : BaseSkill
{
    public int addAttributeType;

    public int addStartValue;

    public int addPerSP;

    public int buffId;

    /// <summary>
    /// 释放技能时调用函数
    /// </summary>
    /// <param name="data"></param>
    public virtual void Skill(TradeData data)
    {
        IsOpen = true;
        AddRoleBuff(data);
    }

    /// <summary>
    /// 取消技能时调用函数
    /// </summary>
    /// <param name="data"></param>
    public virtual void SkillOff(TradeData data)
    {
        DeteleRoleBuff(data);
    }

    /// <summary>
    /// 添加增益Buff
    /// </summary>
    public virtual void AddRoleBuff(TradeData tradeData)
    {
        if (!IsOpen)
        {
            return;
        }

        for (int i = 0; i < buffList.Count; i++)
        {
            var buff = GameDataMgr.My.GetBuffDataByID(buffList[i]);
            BaseBuff baseb = new BaseBuff();
            baseb.Init(buff);
            baseb.SetRoleBuff(PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
        }
        if (role.isNpc)
        {
            if (role.npcScript.isCanSeeEquip)
            {
                for (int i = 0; i < role.npcScript.NPCBuffList.Count; i++)
                {
                    var buff = GameDataMgr.My.GetBuffDataByID(role.npcScript.NPCBuffList[i]);
                    BaseBuff baseb = new BaseBuff();
                    baseb.Init(buff);
                    baseb.SetRoleBuff(PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
                }
            }
        }
    }
    
    /// <summary>
    /// 移除增益buff
    /// </summary>
    /// <param name="tradeData"></param>
    public virtual void DeteleRoleBuff(TradeData tradeData)
    {
        BaseMapRole targetRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        foreach (int i in buffList)
        {
            targetRole.RemoveBuffById(i);
        }
        if (role.isNpc)
        {
            if (role.npcScript.isCanSeeEquip)
            {
                foreach (int i in role.npcScript.NPCBuffList)
                {
                    targetRole.RemoveBuffById(i);
                }
            }
        }
    }

    /// <summary>
    /// 增益型技能回合结束调用
    /// </summary>
    public virtual void OnEndTurn()
    {
        
    }

    /// <summary>
    /// 计算buff加成的具体数值
    /// </summary>
    /// <returns></returns>
    public int CalculateNumber()
    {
        int result = addStartValue;
        result = addStartValue + role.skillPower * addPerSP;
        return result;
    }

    /// <summary>
    /// 增益型角色重启技能（对所有承受者重新施放一次技能，但不重新发起交易）
    /// </summary>
    public override void ReUnleashSkills()
    {
        IsOpen = true;
        for (int i = 0; i < role.tradeList.Count; i++)
        {
            Skill(role.tradeList[i].tradeData); 
        }
    }

    /// <summary>
    /// 增益型角色关闭技能（对所有承受者取消技能，但不删除交易）
    /// </summary>
    public override void CancelSkill()
    {
        base.CancelSkill();
        for (int i = 0; i < role.tradeList.Count; i++)
        {
            SkillOff(role.tradeList[i].tradeData); 
        }
    }
}
