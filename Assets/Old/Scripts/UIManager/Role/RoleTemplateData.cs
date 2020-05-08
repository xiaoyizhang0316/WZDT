using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTemplateData : MonoBehaviour
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public int roleId;
    /// <summary>
    /// 角色名字
    /// </summary>
    public string roleName;
    /// <summary>
    /// 产能
    /// </summary>
    public int capacity;

    /// <summary>
    /// 效率
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 风险值
    /// </summary>
    public int risk;

    /// <summary>
    /// 交易成本
    /// </summary>
    public int tradeCost;

    /// <summary>
    /// 产品质量加成
    /// </summary>
    public int productsQualityAdd;

    /// <summary>
    /// 产品外观加成
    /// </summary>
    public int productsFacadeAdd;

    /// <summary>
    /// 产品品牌加成
    /// </summary>
    public int productsBrandAdd;

    /// <summary>
    /// 产品甜度加成
    /// </summary>
    public int productsSweetnessAdd;

    /// <summary>
    /// 产品脆度加成
    /// </summary>
    public int productsBrittlenessAdd;

    /// <summary>
    /// 固定成本
    /// </summary>
    public int fixedCost;

    /// <summary>
    /// 每月成本
    /// </summary>
    public int costMonth;

    /// <summary>
    /// 职业类型
    /// </summary>
    public int vocationalType;

    /// <summary>
    /// 产出时间
    /// </summary>
    public float ProductionTime;
    /// <summary>
    /// 品牌
    /// </summary>
    public int Brand;
    /// <summary>
    /// 搜寻值
    /// </summary>
    public int search;

    /// <summary>
    /// 搜寻成本
    /// </summary>
    public int searchCost;

    /// <summary>
    /// 议价成本
    /// </summary>
    public int bargainCost;

    /// <summary>
    /// 交付成本
    /// </summary>
    public int deliveryCost;

    /// <summary>
    /// 议价价值
    /// </summary>
    public float bargainValue;

    /// <summary>
    /// 交付价值
    /// </summary>
    public float deliveryValue;

    public List<PlotSign> PlotSigns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
