using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RoleTemplateModelItem
{
    /// <summary>
    /// 角色类型
    /// </summary>
    public string roleType;

    /// <summary>
    /// 等级
    /// </summary>
    public string level;

    /// <summary>
    /// 是否解锁
    /// </summary>
    public string unlock;

    /// <summary>
    /// 活动ID
    /// </summary>
    public string activityId;

    /// <summary>
    /// 活动列表
    /// </summary>
    public string activityList;

    /// <summary>
    /// 需要的产能值
    /// </summary>
    public string needCapacity;

    /// <summary>
    /// 需要的效率值
    /// </summary>
    public string needEfficiency;

    /// <summary>
    /// 需要的质量值
    /// </summary>
    public string needQuality;

    /// <summary>
    /// 需要的品牌值
    /// </summary>
    public string needBrand;

    /// <summary>
    /// 基础固定成本
    /// </summary>
    public string baseFixedCost;

    /// <summary>
    /// 基础每月成本
    /// </summary>
    public string baseCostMonth;

    /// <summary>
    /// 交易范围
    /// </summary>
    public string tradingRange;

    /// <summary>
    /// 基础风险
    /// </summary>
    public string baseRisk;

    /// <summary>
    /// 基础搜寻
    /// </summary>
    public string baseSearch;

    /// <summary>
    /// 基础议价
    /// </summary>
    public string baseBargain;

    /// <summary>
    /// 基础交付
    /// </summary>
    public string baseDelivery;

    /// <summary>
    /// 活动时长
    /// </summary>
    public string activityTime;

    /// <summary>
    /// 柜台栏位
    /// </summary>
    public string counter;
}

[Serializable]
public class RoleTemplateModelsData
{
    public List<RoleTemplateModelItem> roleTemplateModelItems = new List<RoleTemplateModelItem>();
}
