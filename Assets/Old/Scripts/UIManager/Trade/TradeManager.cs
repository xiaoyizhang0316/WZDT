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
    public Dictionary<int, TradeSign> tradeList;

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
            Destroy(temp.gameObject, 0f);
            if (NewCanvasUI.My.Panel_TradeSetting.activeSelf)
                CreateTradeManager.My.Close();
        }
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
                return false;
            if (sign.tradeData.startRole.Equals(NewCanvasUI.My.endRole.baseRoleData.ID.ToString()) && sign.tradeData.endRole.Equals(NewCanvasUI.My.startRole.baseRoleData.ID.ToString()))
                return false;
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
                return false;
        }
        if (end.isNpc)
        {
            if (end.GetComponent<BaseNpc>().isLock)
                return false;
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
            return false;
        else
            return true;
    }

    public bool CheckTradeConstraint()
    {
        if (NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product && NewCanvasUI.My.endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            return TradeConstraint.My.CheckTradeConstraint(NewCanvasUI.My.startRole.baseRoleData.baseRoleData.roleType, NewCanvasUI.My.endRole.baseRoleData.baseRoleData.roleType);
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
            if (start.tradeList.Count == start.extraSkill.maxTradeLimit)
                return false;
        }
        if (end.extraSkill != null)
        {
            if (end.tradeList.Count == end.extraSkill.maxTradeLimit)
                return false;
        }
        return true;  
    }

    /// <summary>
    /// 检测交易的所有条件
    /// </summary>
    /// <returns></returns>
    public bool CheckTradeCondition()
    {
        return CheckStartAndEnd() && CheckNpcActive() && CheckDuplicateTrade() && CheckSkillCountLimit() && CheckTradeConstraint();
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

    private void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        tradeList = new Dictionary<int, TradeSign>();
    }
}
