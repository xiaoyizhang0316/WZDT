using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BuffItem
{
    /// <summary>
    /// 
    /// </summary>
    public string BuffID;

    public string BulletBuffType;
    /// <summary>
    /// 
    /// </summary>
    public string BuffName;

    public string ElementType;

    public string AttackEffect;

    public string BuffDesc;
    /// <summary>
    /// 
    /// </summary>
    public string OnBuffAdd;
    /// <summary>
    /// 
    /// </summary>
    public string OnBuffRemove;
    /// <summary>
    /// 
    /// </summary>
    public string OnTradeConduct;
    /// <summary>
    /// 
    /// </summary>
    public string OnBeforeDead;
    /// <summary>
    /// 
    /// </summary>
    public string OnProduct;
    /// <summary>
    /// 
    /// </summary>
    public string OnTick;

    public string Duration;

    public string Interval;
}

[Serializable]
public class BuffsData
{
    public List<BuffItem> buffSigns = new List<BuffItem>();
}

