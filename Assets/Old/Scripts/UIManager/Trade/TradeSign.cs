using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public float startPer;

    public float endPer;

    private int countNumber = 0;

    private int createTime;

    private bool isChecked = false;

    public List<string> bothIDs=new List<string>();

    public void Init(string start, string end)
    {
        tradeData = new TradeData();
        tradeData.startRole = start;
        tradeData.endRole = end;
        bothIDs.Clear();
        bothIDs.Add(start);
        bothIDs.Add(end);
        tradeData.isFree = false;
        tradeData.castRole = start;
        tradeData.targetRole = end;
        tradeData.selectSZFS = SZFSType.固定;
        tradeData.selectCashFlow = CashFlowType.先钱;
        tradeData.dividePercent = 0;
        startPer = 1f;
        endPer = 1f;
        if (PlayerData.My.xianJinLiu[3])
        {
            if (!isTradeSettingBest())
            {
                tradeData.selectCashFlow = CashFlowType.后钱;
            }
        }
        tradeData.ID = TradeManager.My.index++;
        createTime = StageGoal.My.timeCount;
        TradeManager.My.tradeList.Add(tradeData.ID, this);
        DataUploadManager.My.TradeOnNpc(start, end);
        SetSkillTarget();
        GenerateTradeLine();
        GenerateTradeIcon();
        AddTradeToRole();
        InvokeRepeating("CheckEncourageSetting", 0f, 1f);
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
                tradeData.dividePercent = 4;
                startPer = 0.6f;
                endPer = 1.4f;
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
    /// 检测交易先后钱对激励等级的影响 
    /// </summary>
    public void CheckEncourageSetting()
    {
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        start.RecalculateEncourageLevel(true);
        end.RecalculateEncourageLevel(true);
    }

    /// <summary>
    /// 为施法方添加技能
    /// </summary>
    public void AddTradeToRole()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        start.startTradeList.Add(this);
        end.endTradeList.Add(this);
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
        if (StageGoal.My.currentType == StageType.Normal)
        {
            return;
        }
        tweener = transform.DOScale(1f, 10f).OnComplete(() =>
        {
            BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
            cast.GetComponent<BaseSkill>().AddRoleBuff(tradeData);
            CalculateTC();
            CheckBuffLineTradeCost();
        });
    }

    /// <summary>
    /// 根据回合结算交易成本
    /// </summary>
    public void TurnTradeCost()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            int costNum = CalculateTC(true) * 4;
            StageGoal.My.CostPlayerGold(costNum);
            StageGoal.My.Expend(costNum, ExpendType.TradeCosts);
        }
        else if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
            GoodsSign[] goodsSigns = GetComponentsInChildren<GoodsSign>();
            int tradeCount = cast.tradeList.Count;
            int costNum = CalculateTC(true) * 4 / tradeCount;
            StageGoal.My.CostPlayerGold(costNum);
            StageGoal.My.Expend(costNum, ExpendType.TradeCosts);
            for (int i = 0; i < goodsSigns.Length; i++)
            {
                target.AddPruductToWareHouse(goodsSigns[i].productData);
                Destroy(goodsSigns[i].gameObject, 0f);
            }
        }
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
        gameObject.SetActive(NewCanvasUI.My.isProductLineActive);
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
        gameObject.SetActive(NewCanvasUI.My.isInfoLineActive);
    }

    /// <summary>
    /// 生成交易按钮图标
    /// </summary>
    public void GenerateTradeIcon()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2")
        {
            return;
        }
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
        if (countNumber == 10 && StageGoal.My.currentType != StageType.Normal)
        {
            if (PlayerData.My.yeWuXiTong[2])
            {
                int number = UnityEngine.Random.Range(0, 101);
                if (number > 10)
                {
                    int cost = CalculateTC();
                    countNumber = 0;
                    if (!PlayerData.My.isSOLO)
                    {
                        string str = "OnGoldChange|" + (0 - cost).ToString();
                        if (PlayerData.My.isServer)
                        {
                            PlayerData.My.server.SendToClientMsg(str);
                        }
                    }
                }
                else
                {
                    countNumber = 0;
                }
            }
            else
            {
                int cost = CalculateTC();
                countNumber = 0;
                if (!PlayerData.My.isSOLO)
                {
                    string str = "OnGoldChange|" + (0 - cost).ToString();
                    if (PlayerData.My.isServer)
                    {
                        PlayerData.My.server.SendToClientMsg(str);
                    }
                }
            }
        }
        return posList;
    }

    /// <summary>
    /// 清理施法者目标
    /// </summary>
    public void ClearAllLine()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        cast.tradeList.Remove(this);
        start.startTradeList.Remove(this);
        end.endTradeList.Remove(this);
        start.RecalculateEncourageLevel(true);
        end.RecalculateEncourageLevel(true);
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            cast.GetComponent<BaseSkill>().DeteleRoleBuff(tradeData);
            if (cast.extraSkill != null)
            {
                cast.extraSkill.SkillOff(this);
            }
        }
    }

    public void UpdateEncourageLevel()
    {
        //BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        BaseMapRole start = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole end = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        start.RecalculateEncourageLevel(true);
        end.RecalculateEncourageLevel(true);
        //cast.RecalculateEncourageLevel(true);
    }

    /// <summary>
    /// 结算交易成本
    /// </summary>
    public int CalculateTC(bool isShow = false)
    {
        if (SceneManager.GetActiveScene().name.Equals("FTE_1"))
        {
            return 0;
        }
        BaseMapRole startRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.startRole));
        BaseMapRole endRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole));
        int result = (int)((startRole.baseRoleData.tradeCost * startPer + startRole.baseRoleData.riskResistance));
        result += (int)((endRole.baseRoleData.tradeCost * endPer + endRole.baseRoleData.riskResistance));
        bool isOutTrade = false;
        if (startRole.isNpc || endRole.isNpc)
        {
            isOutTrade = true;
            result = (int)(result * 0.3f);
        }
        else
        {
            result = (int)(result * 0.2f);
        }
        if (!isTradeSettingBest())
        {
            if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2"
                                                           || SceneManager.GetActiveScene().name == "FTE_1.5"
                                                           || SceneManager.GetActiveScene().name == "FTE_2.5")
            {
                result *= 2;
            }
            else
            {
                if (startRole.baseRoleData.riskResistance == 0 && endRole.baseRoleData.riskResistance == 0)
                {
                    result *= 2;
                }
                else
                {
                    int diff = Mathf.Abs(startRole.baseRoleData.riskResistance - endRole.baseRoleData.riskResistance) / 2;
                    int ave = (startRole.baseRoleData.riskResistance + endRole.baseRoleData.riskResistance) / 2;
                    float per = 2f * diff / ave + 1f;
                    result = (int)(result * per);
                }
            }
        }
        if (!isShow)
        {
            StageGoal.My.CostPlayerGold(result);
            StageGoal.My.Expend(result, ExpendType.TradeCosts);
        }
        return result;
    }

    /// <summary>
    /// 预测回合交易成本扣除
    /// </summary>
    /// <returns></returns>
    public int PredictTurnTradeCost()
    {
        int result = CalculateTC(true);
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            result *= 4;
        }
        else if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Product)
        {
            int tradeCount = cast.tradeList.Count;
            result = result * 4 / tradeCount;
        }
        return result;
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

    private TradeLineItem[] list;

    private LineRenderer infoLine;

    /// <summary>
    /// 开关显示物流线
    /// </summary>
    /// <param name="isActive"></param>
    public void HideProductLine(bool isActive)
    {
        if (list.Length == 0)
        {
            list = GetComponentsInChildren<TradeLineItem>();
            if (list.Length == 0)
                return;
        }
        foreach (TradeLineItem t in list)
        {
            t.GetComponent<MeshRenderer>().enabled = isActive;
        }
    }

    /// <summary> 
    /// 开关显示信息流线
    /// </summary>
    /// <param name="isActive"></param>
    public void HideInfoLine(bool isActive)
    {
        if (infoLine == null)
        {
            infoLine = GetComponentInChildren<LineRenderer>();
            if (infoLine == null)
                return;
        }
        infoLine.enabled = isActive;
    }

    public void CheckClickTime()
    {
        if (StageGoal.My.timeCount - createTime <= 5 && !isChecked)
        {
            isChecked = true;
            DataUploadManager.My.AddData(DataEnum.交易_五秒内查看交易的次数);
        }
    }

    /// <summary>
    /// 重置交易（删除线上物品，重置交易计数）
    /// </summary>
    public void ResetThisTrade()
    {
        countNumber = 0;
        GoodsSign[] goodsSigns = GetComponentsInChildren<GoodsSign>();
        if (goodsSigns.Length > 0)
        {
            for (int i = 0; i < goodsSigns.Length; i++)
            {
                Destroy(goodsSigns[i].gameObject, 0f);
            }
        }
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole));
        if (cast.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            tweener.Restart();
        }
    }

    /// <summary>
    /// 检查角色在交易中的位置（true:star, false:end）
    /// </summary>
    /// <param name="roleID"></param>
    /// <returns></returns>
    public bool CheckRolePositionInTrade(string roleID)
    {
        if (tradeData.startRole.Equals(roleID))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 显示角色相关的交易图标（鼠标移动到角色上触发）
    /// </summary>
    /// <param name="roleID">相关角色ID</param>
    public void ShowTradeIcon(string roleID)
    {
        if (bothIDs.Contains(roleID))
        {
            if (CheckRolePositionInTrade(roleID))
            {
                icon.ShowRelateIcon(true);
            }
            else
            {
                icon.ShowRelateIcon();
            }
        }
    }
    
    /// <summary>
    /// 隐藏角色相关的交易图标（鼠标从角色上移出触发）
    /// </summary>
    public void HideTradeIcon()
    {
        icon.HideRelateIcon();
    }
}
