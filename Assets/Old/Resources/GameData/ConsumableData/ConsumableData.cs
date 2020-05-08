using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableData
{
    /// <summary>
    /// 消耗品ID
    /// </summary>
    public int consumableId;

    /// <summary>
    /// 消耗品名字
    /// </summary>
    public string consumableName;

    /// <summary>
    /// 消耗品描述
    /// </summary>
    public string consumableDesc;

    /// <summary>
    /// 消耗品作用
    /// </summary>
    public List<int> targetBuffList;
}
