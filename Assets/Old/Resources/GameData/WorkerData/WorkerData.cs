using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;
using System;

[Serializable]
public class WorkerData
{
    public int ID;

    public string workerName;

    /// <summary>
    /// 品阶
    /// </summary>
    public int ProductOrder;

    /// <summary>
    /// 产能
    /// </summary>
    public int capacity;

    /// <summary>
    /// 效率
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 质量
    /// </summary>
    public int quality;

    /// <summary>
    /// 品牌
    /// </summary>
    public int brand;

    /// <summary>
    /// 甜度加成
    /// </summary>
    public int sweetnessAdd;

    /// <summary>
    /// 脆度加成
    /// </summary>
    public int BrittlenessAdd;

    /// <summary>
    /// 固定成本
    /// </summary>
    public int fixedCost;

    /// <summary>
    /// 每月成本
    /// </summary>
    public int costMonth;

    /// <summary>
    /// 风险加成
    /// </summary>
    public int riskAdd;

    /// <summary>
    /// 搜寻加成
    /// </summary>
    public int searchAdd;

    /// <summary>
    /// 议价加成
    /// </summary>
    public int bargainAdd;

    /// <summary>
    /// 交付加成
    /// </summary>
    public int deliverAdd;

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
