using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseServiceSkill : BaseSkill
{
    
    
    public override void Skill()
    {
        throw new System.NotImplementedException();
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
    
    
}
