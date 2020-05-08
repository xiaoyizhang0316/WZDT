using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

[Serializable]
public class BaseRoleData 
{
    public string roleName;
    /// <summary>
    /// 角色类型
    /// </summary>
    public RoleType roleType;

    /// <summary>
    /// 等级
    /// </summary>
    public int level;

    /// <summary>
    /// 是否解锁
    /// </summary>
    public int unlock;

    /// <summary>
    /// 活动ID
    /// </summary>
    public int activityId;

    /// <summary>
    /// 活动列表
    /// </summary>
    public List<int> activityList;

    /// <summary>
    /// 需要的产能值
    /// </summary>
    public int needCapacity;

    /// <summary>
    /// 需要的效率值
    /// </summary>
    public int needEfficiency;

    /// <summary>
    /// 需要的质量值
    /// </summary>
    public int needQuality;

    /// <summary>
    /// 需要的品牌值
    /// </summary>
    public int needBrand;

    /// <summary>
    /// 基础固定成本
    /// </summary>
    public int baseFixedCost;

    /// <summary>
    /// 基础每月成本
    /// </summary>
    public int baseCostMonth;

    /// <summary>
    /// 交易范围
    /// </summary>
    public int tradingRange;

    /// <summary>
    /// 基础风险
    /// </summary>
    public int baseRisk;

    /// <summary>
    /// 基础搜寻
    /// </summary>
    public int baseSearch;

    /// <summary>
    /// 基础议价
    /// </summary>
    public int baseBargain;

    /// <summary>
    /// 基础交付
    /// </summary>
    public int baseDelivery;

    /// <summary>
    /// 活动时长
    /// </summary>
    public int activityTime;

    /// <summary>
    /// 柜台栏位
    /// </summary>
    public int counter;

    /// <summary>
    /// Icon路径地址
    /// </summary>
    public string SpritePath;

    /// <summary>
    /// 模型路径地址
    /// </summary>
    public string PrePath;

    /// <summary>
    /// 模型空间2D路径地址
    /// </summary>
    public string RoleSpacePath;

}
