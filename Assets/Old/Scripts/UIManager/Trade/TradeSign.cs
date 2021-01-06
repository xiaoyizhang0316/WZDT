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
        tweener = transform.DOScale(1f, 10f).OnComplete(() =>
        {
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
        if (countNumber == 10)
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
    public int CalculateTC()
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
            //if (PlayerData.My.xianJinLiu[4])
            //{
            //    int count = 0;
            //    for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            //    {
            //        if (!PlayerData.My.MapRole[i].isNpc)
            //        {
            //            count++;
            //        }
            //    }
            //    if (count <= 3)
            //    {
            //        result1 = result1 * 90 / 100;
            //        result2 = result2 * 90 / 100;
            //    }
            //}
        }
        else
        {
            result = (int)(result * 0.2f);
        }
        if (!isTradeSettingBest())
        {
            if (startRole.baseRoleData.riskResistance == 0 || endRole.baseRoleData.riskResistance == 0)
            {
                return 0;
            }

            int diff = Mathf.Abs(startRole.baseRoleData.riskResistance - endRole.baseRoleData.riskResistance) / 2;
            int ave = (startRole.baseRoleData.riskResistance + endRole.baseRoleData.riskResistance) / 2;
            float per = 2f * diff / ave + 1f;
            result = (int)(result * per); 
            //if (PlayerData.My.xianJinLiu[1])
            //{
            //    result = result * 95 / 100;
            //}
        }
        //if (PlayerData.My.xianJinLiu[0])
        //{
        //    result = result * 95 / 100;
        //}
        //if (isOutTrade && PlayerData.My.qiYeJiaZhi[5])
        //{
        //    StageGoal.My.GetSatisfy((int)(result * 0.2f));
        //    StageGoal.My.ScoreGet(ScoreType.金钱得分, (int)(result * 0.2f));
        //}
        StageGoal.My.CostPlayerGold(result);
        StageGoal.My.Expend(result, ExpendType.TradeCosts);
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
}
