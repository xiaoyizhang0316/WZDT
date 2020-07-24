using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class TradeSign : MonoBehaviour
{
    /// <summary>
    /// 数据类
    /// </summary>
    public TradeData tradeData;

    /// <summary>
    /// 交易图标
    /// </summary>
    public TradeIcon icon;

    /// <summary>
    /// 单根管子
    /// </summary>
    public GameObject tradeCylinder;

    /// <summary>
    /// 信息流线
    /// </summary>
    public GameObject tradeBuffLine;

    public GameObject tradeIconPrb;

    /// <summary>
    /// buff线结算交易成本动画
    /// </summary>
    public Tweener tweener;

    private int countNumber = 0;

    public void Init(string start, string end)
    {
        tradeData = new TradeData();
        tradeData.startRole = start;
        tradeData.endRole = end;
        tradeData.isFree = false;
        tradeData.castRole = start;
        tradeData.targetRole = end;
        tradeData.selectSZFS = SZFSType.固定;
        tradeData.selectCashFlow = CashFlowType.先钱;
        tradeData.ID = TradeManager.My.index++;
        TradeManager.My.tradeList.Add(tradeData.ID, this);
        SetSkillTarget();
        GenerateTradeLine();
        GenerateTradeIcon();
        AddTradeToRole();
    }

    /// <summary>
    /// 设置技能施法者
    /// </summary>
    public void SetSkillTarget()
    {
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        if (startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            if (endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            {
                tradeData.castRole = tradeData.startRole;
                tradeData.targetRole = tradeData.endRole;
            }
            else if (endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
            {
                tradeData.castRole = tradeData.endRole;
                tradeData.targetRole = tradeData.startRole;
            }
        }
        else if (startRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            if (endRole.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
            {
                tradeData.castRole = tradeData.startRole;
                tradeData.targetRole = tradeData.endRole;
            }
        }
    }

    /// <summary>
    /// 为施法方添加技能
    /// </summary>
    public void AddTradeToRole()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        cast.tradeList.Add(this);
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            CheckBuffLineTradeCost();
            cast.GetComponent<BaseSkill>().AddRoleBuff(tradeData);
            if (cast.extraSkill != null)
            {
                cast.extraSkill.SkillOn(this);
            }
        }
    }

    /// <summary>
    /// 信息流每10秒结算交易成本
    /// </summary>
    public void CheckBuffLineTradeCost()
    {
        tweener = transform.DOScale(1f, 10f).OnComplete(()=> {
            BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
            cast.GetComponent<BaseSkill>().AddRoleBuff(tradeData);
            CalculateTC();
            CheckBuffLineTradeCost();
        });
    }

    /// <summary>
    /// 生成交易线
    /// </summary>
    public void GenerateTradeLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            GenerateProductLine();
        }
        else
        {
            GenerateMoneyLine();
        }
    }

    /// <summary>
    /// 生成交易物流线
    /// </summary>
    public void GenerateProductLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        int number = Mathf.FloorToInt(Vector3.Distance(cast.tradePoint.position, target.tradePoint.position));
        Vector3 offset = (target.tradePoint.position - cast.tradePoint.position) / number;
        float HalfLength = Vector3.Distance(target.tradePoint.position, cast.tradePoint.position) / number / 2f;
        Vector3 tempStart = cast.tradePoint.position - offset / 2f;
        for (int i = 0; i < number; i++)
        {
            GameObject go = Instantiate(tradeCylinder);
            go.transform.SetParent(transform);
            Vector3 rightRotation = target.tradePoint.position - cast.tradePoint.position;
            float LThickness = 0.1f;
            go.transform.position = tempStart + offset;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
            go.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
            tempStart = go.transform.position;
        }
    }

    /// <summary>
    /// 生成交易信息流线
    /// </summary>
    public void GenerateMoneyLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        GameObject go = Instantiate(tradeBuffLine);
        go.transform.SetParent(transform);
        go.GetComponent<DrawMoneyLine>().InitPos(cast.tradePoint.transform, target.tradePoint.transform, tradeData.ID);
    }

    /// <summary>
    /// 生成交易按钮图标
    /// </summary>
    public void GenerateTradeIcon()
    {
        GameObject go = Instantiate(tradeIconPrb, transform);
        go.GetComponent<TradeIcon>().Init(tradeData);
        icon = go.GetComponent<TradeIcon>();
    }

    /// <summary>
    /// 获取所有物流移动路径点
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetDeliverProductPath()
    {
        TradeLineItem[] list = GetComponentsInChildren<TradeLineItem>();
        List<Vector3> posList = new List<Vector3>();
        for (int i = 0; i < list.Length - 1; i++)
        {
            posList.Add(list[i].transform.Find("end").position);
        }
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        posList.Add(end.transform.position);
        countNumber++;
        if (countNumber == 10)
        {
            CalculateTC();
            countNumber = 0;
        }
        return posList;
    }

    /// <summary>
    /// 清理施法者目标
    /// </summary>
    public void ClearAllLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        cast.tradeList.Remove(this);
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            cast.GetComponent<BaseSkill>().DeteleRoleBuff(tradeData);
            if (cast.extraSkill != null)
            {
                cast.extraSkill.SkillOff(this);
            }
        }
    }

    /// <summary>
    /// 结算交易成本
    /// </summary>
    public void CalculateTC()
    {
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        int result = startRole.baseRoleData.tradeCost + endRole.baseRoleData.tradeCost;
        result += startRole.baseRoleData.riskResistance + endRole.baseRoleData.riskResistance;
        int result1, result2;
        if (startRole.isNpc || endRole.isNpc)
        {
            result1 = (int)(result * 0.6f);
            result2 = (int)(result * 0.3f);
        }
        else
        {
            result1 = (int)(result * 0.5f);
            result2 = (int)(result * 0.2f);
        }
        if (isTradeSettingBest())
        {
            result = result2;
        }
        else
        {
            result = result1;
        }
        StageGoal.My.CostPlayerGold(result);
        StageGoal.My.Expend(result, ExpendType.TradeCosts);
    }

    /// <summary>
    /// 判断交易成本是不是最优
    /// </summary>
    /// <returns></returns>
    public bool isTradeSettingBest()
    {
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        if (startRole.baseRoleData.riskResistance >= endRole.baseRoleData.riskResistance)
        {
            return tradeData.selectCashFlow == CashFlowType.后钱;
        }
        else
        {
            return tradeData.selectCashFlow == CashFlowType.先钱;
        }
    }
}
