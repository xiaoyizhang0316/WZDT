using System;
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
    public Dictionary<int, TradeSign> tradeList = new Dictionary<int, TradeSign>();
    
    public Dictionary<int,int> deleteTradeList = new Dictionary<int, int>();   

    /// <summary>
    /// 删除指定ID的交易
    /// </summary>
    /// <param name="ID"></param>
    public void DeleteTrade(int ID)
    {
        if (tradeList.ContainsKey(ID))
        {
            TradeSign temp = tradeList[ID];
            if (StageGoal.My.isTurnStart)
            {
                deleteTradeList.Add(StageGoal.My.timeCount,temp.CalculateTC(true));
            }
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
    /// 检测玩家金钱是否满足条件
    /// </summary>
    /// <returns></returns>
    public bool CheckMoneyCondition()
    {
        return true;
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
    /// 预测回合交易成本扣除
    /// </summary>
    public int PredictTurnTradeCost()
    {
        int result = 0;
        foreach (TradeSign item in tradeList.Values)
        {
            result += item.PredictTurnTradeCost();
        }
        return result;
    }

    /// <summary>
    /// 回合结束时结算所有交易的交易成本
    /// </summary>
    public void TurnTradeCost()
    {
        foreach (var item in tradeList)
        {
            item.Value.TurnTradeCost();
        }
        foreach (var item in deleteTradeList)
        {
            int costNum = item.Value * (StageGoal.My.timeCount - item.Key) / (StageGoal.My.timeCount - StageGoal.My.turnStartTime) * 4;
            StageGoal.My.CostPlayerGold(costNum);
            StageGoal.My.Expend(costNum, ExpendType.TradeCosts);
        }
        deleteTradeList.Clear();
    }

    /// <summary>
    /// 显示所有交易图标
    /// </summary>
    public void ShowAllIcon()
    {
        foreach (var item in tradeList)
        {
            item.Value.icon.ShowIcon();
        }
        //for (int i = 0; i < tradeList.Count; i++)
       // {
           // tradeList[i].icon.ShowIcon();
       // }
    }

    /// <summary>
    /// 隐藏所有交易图标
    /// </summary>
    public void HideAllIcon()
    {
        foreach (var item in tradeList)
        {
            item.Value.icon.HideIcon();
        }
    }

    private void OnDestroy()
    {
        
    }

    /// <summary>
    /// 重置所有交易（删除线上的物品，重置交易计数）
    /// </summary>
    public void ResetAllTrade()
    {
        foreach (var item in tradeList)
        {
            item.Value.ResetThisTrade();
        }
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

    /// <summary>
    /// 判断角色的使用是否正确
    /// </summary>
    /// <param name="npcID"></param>
    /// <returns></returns>
    public bool CheckProductNpcTradeWrong(string npcID)
    {
        bool isWrong = false;
        try
        {
            if (tradeList.Count <= 0)
            {
                return false;
            }
            foreach(var sign in tradeList.Values)
            {
                if(sign.tradeData.startRole==npcID)
                {
                    if (PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.endRole)).baseRoleData.baseRoleData
                        .roleSkillType == RoleSkillType.Product)
                    {
                        isWrong = true;
                    }
                }
                if (sign.tradeData.endRole == npcID)
                {
                    if (PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.startRole)).baseRoleData.baseRoleData
                        .roleSkillType == RoleSkillType.Product)
                    {
                        isWrong = false;
                        break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        return isWrong;
    }

    /// <summary>
    /// 显示角色相关的交易的图标（鼠标移动到角色上触发）
    /// </summary>
    /// <param name="roleID">相关角色的ID</param>
    public void ShowAllRelateTradeIcon(string roleID)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TradeSign>().ShowTradeIcon(roleID);
        }
    }

    /// <summary>
    /// 隐藏角色相关的交易的图标（鼠标从角色上移出）
    /// </summary>
    public void HideRelateTradeIcon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<TradeSign>().HideTradeIcon();
        }
    }

    public void ClearAllGoods()
    {
        foreach (var item in tradeList)
        {
            foreach (Transform child in item.Value.transform)
            {
                if (child.GetComponent<GoodsSign>())
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public void RecycleAllGoods()
    {
        foreach (var item in tradeList)
        {
            foreach (Transform child in item.Value.transform)
            {
                if (child.GetComponent<GoodsSign>())
                {
                    child.GetComponent<GoodsSign>().role.AddPruductToWareHouse(child.GetComponent<GoodsSign>().productData);
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
