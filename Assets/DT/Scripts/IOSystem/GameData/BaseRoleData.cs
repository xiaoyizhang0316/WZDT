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
    /// 角色技能类型
    /// </summary>
    public RoleSkillType roleSkillType;

    /// <summary>
    /// 等级
    /// </summary>
    public int level;

    /// <summary>
    /// 模板提供效果值
    /// </summary>
    public int effect;

    /// <summary>
    /// 模板提供效率值
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 模板提供范围值
    /// </summary>
    public int range;

    /// <summary>
    /// 模板提供交易成本
    /// </summary>
    public int tradeCost;

    /// <summary>
    /// 模板提供风险抗力
    /// </summary>
    public int riskResistance;

    /// <summary>
    /// 模板提供成本
    /// </summary>
    public int cost;

    /// <summary>
    /// 模板提供弹药装载量
    /// </summary>
    public int bulletCapacity;

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