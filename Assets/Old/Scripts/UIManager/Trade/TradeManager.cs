﻿using System.Collections;
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
    /// 所有的交易图标列表
    /// </summary>
    public List<TradeIcon> tradeIcons;

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

    private void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        tradeList = new Dictionary<int, TradeSign>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
