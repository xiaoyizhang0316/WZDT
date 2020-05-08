using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class TradeIcon : MonoBehaviour
{
    /// <summary>
    /// 交易方式显示图标
    /// </summary>
    public GameObject JYFSgo;

    /// <summary>
    /// 收支来源显示图标
    /// </summary>
    public GameObject SZLYGo;

    /// <summary>
    /// 收支方式显示图标
    /// </summary>
    public GameObject SZFSGo;

    public Transform start;

    public Transform end;

    public int tradeId;

    public void SetTrasform(Transform s, Transform e)
    {
        start = s;
        end = e;
    }

    /// <summary>
    /// 为指定的交易结构生成交易图标
    /// </summary>
    /// <param name="szfs"></param>
    /// <param name="cashflow"></param>
    /// <param name="isfree"></param>
    /// <param name="skilltype"></param>
    public void SetTradeIcon(SZFSType szfs,CashFlowType cashflow, bool isfree,TradeData tradeData,int Id)
    {
        tradeId = Id;
        RoleSkillType skilltype = GameDataMgr.My.GetSkillDataByName(tradeData.selectJYFS).skillType;
        if (isfree)
        {
            SZLYGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/白给");
            SZFSGo.GetComponent<SpriteRenderer>().sprite = null;
            switch (skilltype)
            {
                case RoleSkillType.Product:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/产品黑");
                    break;
                case RoleSkillType.Service:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/服务黑");
                    break;
                case RoleSkillType.Solution:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/解决方案黑");
                    break;
            }
        }
        else if (szfs == SZFSType.固定)
        {
            SZFSGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/固定");
            switch (cashflow)
            {
                case CashFlowType.先钱:
                    SZLYGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/先给");
                    break;
                case CashFlowType.后钱:
                    SZLYGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/后给");
                    break;
            }
            switch(skilltype)
            {
                case RoleSkillType.Product:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/产品黑");
                    break;
                case RoleSkillType.Service:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/服务黑");
                    break;
                case RoleSkillType.Solution:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/解决方案黑");
                    break;
            }
        }
        else if (szfs == SZFSType.剩余)
        {
            SZFSGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/剩余");
            SZLYGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/后给");
            switch (skilltype)
            {
                case RoleSkillType.Product:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/产品白");
                    break;
                case RoleSkillType.Service:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/服务白");
                    break;
                case RoleSkillType.Solution:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/解决方案白");
                    break;
            }
        }
        else if (szfs == SZFSType.分成)
        {
            SZFSGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/分成");
            SZLYGo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/后给");
            switch (skilltype)
            {
                case RoleSkillType.Product:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/产品白");
                    break;
                case RoleSkillType.Service:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/服务白");
                    break;
                case RoleSkillType.Solution:
                    JYFSgo.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Trade/DealElement/解决方案白");
                    break;
            }
        }
    }

    public void OnMouseDown()
    {
        //UIManager.My.Panel_CreateTrade.SetActive(true);
        if (!UIManager.My.NeedRayCastPanel())
        {
            UIManager.My.Panel_CreateTrade.SetActive(true);
            CreateTradeManager.My.Open(TradeManager.My.tradeList[tradeId].gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TradeManager.My.tradeList.ContainsKey(tradeId))
        {
            DrawMoneyLine tep = TradeManager.My.tradeList[tradeId].tradeMoneyLineGo.GetComponent<DrawMoneyLine>();
            transform.position = tep.pointList[10];
        }
    }
}
