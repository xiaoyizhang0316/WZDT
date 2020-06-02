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
