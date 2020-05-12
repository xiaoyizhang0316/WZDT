using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;


[Serializable]
public class WorkerData
{
    public int ID;

    /// <summary>
    /// 工人名称
    /// </summary>
    public string workerName;

    /// <summary>
    /// 品阶
    /// </summary>
    public int ProductOrder;

    /// <summary>
    /// 效果值
    /// </summary>
    public int effect;

    /// <summary>
    /// 效率值
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 范围值
    /// </summary>
    public int range;

    /// <summary>
    /// 风险抗力
    /// </summary>
    public int riskResistance;

    /// <summary>
    /// 交易成本
    /// </summary>
    public int tradeCost;

    /// <summary>
    /// 成本
    /// </summary>
    public int cost;

    /// <summary>
    /// 弹药容量
    /// </summary>
    public int bulletCapacity;

    /// <summary>
    /// 当前是否正在使用
    /// </summary>
    public bool isUsed;

    /// <summary>
    /// PDP性格
    /// </summary>
    public PDPType PDP;

    /// <summary>
    /// Icon路径地址
    /// </summary>
    public string SpritePath;

    /// <summary>
    /// 模型空间2D路径地址
    /// </summary>
    public string WorkerSpacePath;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //SpritePath = CommonData.My.SpritePath + "Worker/" + ID.ToString();
        //WorkerSpacePath = CommonData.My.SpritePath + "WorkerSpace/" + ID.ToString();
        SpritePath = "Sprite/Worker/" + ID.ToString();
        WorkerSpacePath = "Sprite/WorkerSpace/" + ID.ToString();
    }
}