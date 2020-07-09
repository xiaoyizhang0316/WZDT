using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

public class TradeConstraint : MonoSingleton<TradeConstraint>
{
    /// <summary>
    /// 不可以进行交易的角色组合
    /// </summary>
    public List<TradeConstraintItem> forbiddenTrade = new List<TradeConstraintItem>();

    /// <summary>
    /// 判断两个角色是否可以进行交易
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public bool CheckTradeConstraint(RoleType start,RoleType end)
    {
        for (int i = 0; i < forbiddenTrade.Count; i++)
        {
            if (forbiddenTrade[i].startRole == start || forbiddenTrade[i].startRole == RoleType.All)
            {
                if (forbiddenTrade[i].endRole == end || forbiddenTrade[i].endRole == RoleType.All)
                {
                    HttpManager.My.ShowTip("双方无法形成交易！");
                    return false;
                }
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    public struct TradeConstraintItem
    {
        public RoleType startRole;

        public RoleType endRole;
    }
}
