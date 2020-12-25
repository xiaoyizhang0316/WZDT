using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;

public class TradeManager : MonoSingleton<TradeManager>
{
    public int index;

    /// <summary>
    /// 所有交易的列表
    /// </summary>
    public Dictionary<int, TradeSign> tradeList=new Dictionary<int, TradeSign>();

    /// <summary>
    /// 删除指定ID的交易
    /// </summary>
    /// <param name="ID"></param>
    public void DeleteTrade(int ID)
    {
        if (tradeList.ContainsKey(ID))
        {
            TradeSign temp = tradeList[ID];
            tradeList.Remove(ID);
            temp.ClearAllLine();
            DeleteTradeRecord(ID, temp);
            CheckDeleteNpcRole(temp);
            Destroy(temp.gameObject, 0f);
            if (NewCanvasUI.My.Panel_TradeSetting.activeSelf)
                CreateTradeManager.My.Close();
        }
    }

    public void CheckDeleteNpcRole(TradeSign sign)
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.endRole));
        if (start.baseRoleData.isNpc && start.baseRoleData.baseRoleData.roleType != RoleType.Bank && !start.isSell)
        {
            if (CheckTradeCount(sign.tradeData.startRole) < 1)
            {
                List<string> param = new List<string>();
                param.Add(start.baseRoleData.ID.ToString());
                StageGoal.My.RecordOperation(OperationType.DeleteRole, param);
            }
            start.GetComponent<NPC>().AnimatorCtr(CheckNpcInTrade(sign.tradeData.startRole));
        }
        if (end.baseRoleData.isNpc && end.baseRoleData.baseRoleData.roleType != RoleType.Bank && !end.isSell)
        {
            if (CheckTradeCount(sign.tradeData.endRole) < 1)
            {
                List<string> param = new List<string>();
                param.Add(end.baseRoleData.ID.ToString());
                StageGoal.My.RecordOperation(OperationType.DeleteRole, param);
            }
            end.GetComponent<NPC>().AnimatorCtr(CheckNpcInTrade(sign.tradeData.endRole));
        }
    }

    public int CheckTradeCount(string roleId)
    {
        int result = 0;
        foreach (TradeSign sign in tradeList.Values)
        {
            if (sign.tradeData.startRole.Equals(roleId) || sign.tradeData.endRole.Equals(roleId))
                result++;
        }
        return result;
    }

    /// <summary>
    /// 删除交易记录操作
    /// </summary>
    /// <param name="ID"></param>
    public void DeleteTradeRecord(int ID,TradeSign sign)
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.castRole));
        if (start.baseRoleData.baseRoleData.roleType == RoleType.Bank)
            return;
        List<string> param = new List<string>();
        param.Add(ID.ToString());
        StageGoal.My.RecordOperation(OperationType.DeleteTrade,param);
    }

    /// <summary>
    /// 删除某个角色相关的所有交易
    /// </summary>
    /// <param name="roleID"></param>
    public void DeleteRoleAllTrade(double roleID)
    {
        if (tradeList.Count == 0)
            return;
        List<int> temp = new List<int>(tradeList.Keys);
        for (int i = 0; i < temp.Count; i++)
        {
            if (Mathf.Abs((float)(double.Parse(tradeList[temp[i]].tradeData.startRole) - roleID)) < 0.1f || Mathf.Abs((float)(double.Parse(tradeList[temp[i]].tradeData.endRole) - roleID)) < 0.1f)
            {
                DeleteTrade(temp[i]);
            }
        }
    }

    /// <summary>
    /// 检测发起者承受者之间是否已经存在交易
    /// </summary>
    /// <returns></returns>
    public bool CheckDuplicateTrade()
    {
        foreach (TradeSign sign in tradeList.Values)
        {
            if (sign.tradeData.startRole.Equals(NewCanvasUI.My.startRole.baseRoleData.ID.ToString()) && sign.tradeData.endRole.Equals(NewCanvasUI.My.endRole.baseRoleData.ID.ToString()))
            {
                HttpManager.My.ShowTip("双方同时只能发生一笔交易！");
                return false;
            }

            if (sign.tradeData.startRole.Equals(NewCanvasUI.My.endRole.baseRoleData.ID.ToString()) && sign.tradeData.endRole.Equals(NewCanvasUI.My.startRole.baseRoleData.ID.ToString()))
            {
                HttpManager.My.ShowTip("双方同时只能发生一笔交易！");
                return false;
            }

        }
        return true;
    }

    /// <summary>
    /// 检测npc是否激活
    /// </summary>
    /// <returns></returns>
    public bool CheckNpcActive()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(NewCanvasUI.My.startRole.baseRoleData.ID);
        BaseMapRole end = PlayerData.My.GetMapRoleById(NewCanvasUI.My.endRole.baseRoleData.ID);
        if (start.isNpc)
        {
            if (start.GetComponent<BaseNpc>().isLock)
            {
                HttpManager.My.ShowTip("角色未解锁！");
                return false;
            }
        }
        if (end.isNpc)
        {
            if (end.GetComponent<BaseNpc>().isLock)
            {
                HttpManager.My.ShowTip("角色未解锁！");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 检测发起者和承受者技能类型
    /// </summary>
    /// <returns></returns>
    public bool CheckStartAndEnd()
    {
        if (NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service &&
            NewCanvasUI.My.endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            HttpManager.My.ShowTip("双方不能都为增益型角色！");
            return false;
        }

        else
            return true;
    }

    /// <summary>
    /// 检测发起方和承受方是否可以交易
    /// </summary>
    /// <returns></returns>
    public bool CheckTradeConstraint()
    {
        if (NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product && NewCanvasUI.My.endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            return TradeConstraint.My.CheckTradeConstraint(NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleType, NewCanvasUI.My.endRole.baseRoleData.baseRoleData.roleType,true);
        else
            return true;
    }

    /// <summary>
    /// 检测技能可施放数量
    /// </summary>
    /// <returns></returns>
    public bool CheckSkillCountLimit()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(NewCanvasUI.My.startRole.baseRoleData.ID);
        BaseMapRole end = PlayerData.My.GetMapRoleById(NewCanvasUI.My.endRole.baseRoleData.ID);
        if (start.extraSkill != null)
        {
            if (start.tradeList.Count == start.extraSkill.maxTradeLimit && start.extraSkill.maxTradeLimit != 0)
            {
                HttpManager.My.ShowTip("发起方无法进行更多交易！");
                return false;
            }
        }
        if (end.extraSkill != null)
        {
            if (end.tradeList.Count == end.extraSkill.maxTradeLimit && end.extraSkill.maxTradeLimit != 0)
            {
                HttpManager.My.ShowTip("承受方无法进行更多交易！");
                return false;
            }
        }
        return true;  
    }

    /// <summary>
    /// 检测玩家金钱是否
    /// </summary>
    /// <returns></returns>
    public bool CheckMoneyCondition()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(NewCanvasUI.My.startRole.baseRoleData.ID);
        BaseMapRole end = PlayerData.My.GetMapRoleById(NewCanvasUI.My.endRole.baseRoleData.ID);
        if (start.baseRoleData.baseRoleData.roleType == RoleType.Bank || end.baseRoleData.baseRoleData.roleType == RoleType.Bank)
            return true;
        if (StageGoal.My.playerGold >= 0)
            return true;
        else
        {
            if (PlayerData.My.yeWuXiTong[4])
            {
                if (!start.isNpc && !end.isNpc)
                    return true;
            }
            HttpManager.My.ShowTip("玩家金钱已达负数！无法发起新交易！");
            return false;
        }
    }

    /// <summary>
    /// 检测交易的所有条件
    /// </summary>
    /// <returns></returns>
    public bool CheckTradeCondition()
    {
        return CheckStartAndEnd() && CheckNpcActive() && CheckDuplicateTrade() && CheckSkillCountLimit() && CheckTradeConstraint() && CheckMoneyCondition();
    }

    /// <summary>
    /// 自动创建交易
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void AutoCreateTrade(string start,string end)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(transform);
        go.GetComponent<TradeSign>().Init(start,end);
        CreateTradeRecord(go.GetComponent<TradeSign>());
    }

    /// <summary>
    /// 创建交易记录操作
    /// </summary>
    public void CreateTradeRecord(TradeSign sign)
    {
        List<string> param = new List<string>();
        param.Add(sign.tradeData.ID.ToString());
        param.Add(sign.tradeData.castRole);
        param.Add(sign.tradeData.targetRole);
        param.Add(sign.tradeData.selectCashFlow.ToString());
        StageGoal.My.RecordOperation(GameEnum.OperationType.CreateTrade, param);
        CheckNpcRole(sign);
    }

    /// <summary>
    /// 生成交易时检测NPC角色是否添加
    /// </summary>
    /// <param name="sign"></param>
    public void CheckNpcRole(TradeSign sign)
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.endRole));
        if (start.baseRoleData.isNpc && !start.isSell)
        {
            if (CheckTradeCount(sign.tradeData.startRole) <= 1)
            {
                List<string> param = new List<string>();
                param.Add(start.baseRoleData.ID.ToString());
                param.Add("TRUE");
                param.Add(start.baseRoleData.baseRoleData.roleName);
                param.Add(start.baseRoleData.baseRoleData.roleType.ToString());
                param.Add(start.baseRoleData.baseRoleData.level.ToString());
                StageGoal.My.RecordOperation(OperationType.PutRole, param);
                ChangeNPCRoleRecord(start);
            }
            start.GetComponent<NPC>().AnimatorCtr(true);
        }
        if (end.baseRoleData.isNpc && !end.isSell)
        {
            if (CheckTradeCount(sign.tradeData.endRole) <= 1)
            {
                List<string> param = new List<string>();
                param.Add(end.baseRoleData.ID.ToString());
                param.Add("TRUE");
                param.Add(end.baseRoleData.baseRoleData.roleName);
                param.Add(end.baseRoleData.baseRoleData.roleType.ToString());
                param.Add(end.baseRoleData.baseRoleData.level.ToString());
                StageGoal.My.RecordOperation(OperationType.PutRole, param);
                ChangeNPCRoleRecord(end);
            }
            end.GetComponent<NPC>().AnimatorCtr(true);
        }
    }

    /// <summary>
    /// 记录修改角色操作
    /// </summary>
    /// <param name="role"></param>
    public void ChangeNPCRoleRecord(BaseMapRole role)
    {
        List<string> param = new List<string>();
        param.Add(role.baseRoleData.ID.ToString());
        BaseMapRole mapRole = PlayerData.My.GetMapRoleById(role.baseRoleData.ID);
        List<int> buffList = new List<int>();
        buffList.AddRange(mapRole.GetComponent<BaseSkill>().buffList);
        if (role.npcScript.isCanSeeEquip)
        {
            buffList.AddRange(role.npcScript.NPCBuffList);
        }
        for (int i = 0; i < buffList.Count; i++)
        {
            param.Add(buffList[i].ToString());
        }
        StageGoal.My.RecordOperation(OperationType.ChangeRole, param);
    }

    /// <summary>
    /// 检测两个角色是否存在一笔交易
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public bool CheckTwoRoleHasTrade(Role start,Role end)
    {
        foreach (TradeSign t in tradeList.Values)
        {
            if (t.tradeData.startRole.Equals(start.ID.ToString()) && t.tradeData.endRole.Equals(end.ID.ToString()))
            return true;    
        }
        return false;
    }

    /// <summary>
    /// 显示所有交易图标
    /// </summary>
    public void ShowAllIcon()
    {
        for (int i = 0; i < tradeList.Count; i++)
        {
            tradeList[i].icon.ShowIcon();
        }
    }

    /// <summary>
    /// 隐藏所有交易图标
    /// </summary>
    public void HideAllIcon()
    {
        for (int i = 0; i < tradeList.Count; i++)
        {
            tradeList[i].icon.HideIcon();
        }
    }

    private void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        tradeList = new Dictionary<int, TradeSign>();
    }

    public bool CheckNpcInTrade(string npcID)
    {
        if (tradeList.Count <= 0)
        {
            return false;
        }
        foreach(var sign in tradeList.Values)
        {
            if(sign.tradeData.startRole==npcID || sign.tradeData.endRole == npcID)
            {
                return true;
            }
        }
        return false;
    }
}
