using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

[Serializable]
public class BuffData
{
    /// <summary>
    /// Buff ID
    /// </summary>
    public int BuffID;

    /// <summary>
    /// 子弹buff类别
    /// </summary>
    public BulletBuffType bulletBuffType;

    /// <summary>
    /// Buff名称
    /// </summary>
    public string BuffName;

    /// <summary>
    /// BUFF描述
    /// </summary>
    public string BuffDesc;


    public ProductElementType elementType;

    public int attackEffect;

    /// <summary>
    /// Buff添加时
    /// </summary>
    public List<string> OnBuffAdd;

    /// <summary>
    /// Buff移除时
    /// </summary>
    public List<string> OnBuffRemove;

    /// <summary>
    /// 濒临破产时
    /// </summary>
    public List<string> OnBeforeDead;

    /// <summary>
    /// 周期性时 
    /// </summary>
    public List<string> OnTick;

    /// <summary>
    /// 弹药相关
    /// </summary>
    public List<string> OnProduct;

    /// <summary>
    /// 持续时间
    /// </summary>
    public int duration;

    /// <summary>
    /// 生效间隔
    /// </summary>
    public int interval;

    /// <summary>
    /// 时间点其他骚操作
    /// </summary>
    public Dictionary<int, List<string>> otherFunctions;

    public BuffData()
    {
        OnBuffAdd = new List<string>();
        OnBuffRemove = new List<string>();
        OnBeforeDead = new List<string>();
        OnTick = new List<string>();
        OnProduct = new List<string>();
        otherFunctions = new Dictionary<int, List<string>>();
    }
}

