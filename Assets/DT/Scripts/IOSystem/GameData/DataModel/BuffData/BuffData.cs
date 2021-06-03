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

    /// <summary>
    /// buff描述数字
    /// </summary>
    public List<int> buffParam;

    /// <summary>
    /// 子弹口味属性（没有的话写normal）
    /// </summary>
    public ProductElementType elementType;

    /// <summary>
    /// 攻击特效发生概率
    /// </summary>
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
    /// 回合结束相关
    /// </summary>
    public List<string> OnEndTurn;

    /// <summary>
    /// 持续时间
    /// </summary>
    public int duration;

    public int turnDuration;

    /// <summary>
    /// 生效间隔
    /// </summary>
    public int interval;

    /// <summary>
    /// buff价值
    /// </summary>
    public int buffValue;

    /// <summary>
    /// 构造函数
    /// </summary>
    public BuffData()
    {
        OnBuffAdd = new List<string>();
        OnBuffRemove = new List<string>();
        OnBeforeDead = new List<string>();
        OnTick = new List<string>();
        OnProduct = new List<string>();
        buffParam = new List<int>();
        OnEndTurn = new List<string>();
    }

    /// <summary>
    /// 复制成新的buffData实体
    /// </summary>
    /// <returns></returns>
    public BuffData CopyNew()
    {
        BuffData temp = new BuffData();
        temp.BuffID = BuffID;
        temp.bulletBuffType = bulletBuffType;
        temp.BuffName = BuffName;
        temp.BuffDesc = BuffDesc;
        temp.elementType = elementType;
        temp.attackEffect = attackEffect;
        temp.OnBuffAdd.AddRange(OnBuffAdd);
        temp.OnBuffRemove.AddRange(OnBuffRemove);
        temp.OnBeforeDead.AddRange(OnBeforeDead);
        temp.OnTick.AddRange(OnTick);
        temp.OnProduct.AddRange(OnProduct);
        temp.buffParam.AddRange(buffParam);
        temp.OnEndTurn.AddRange(OnEndTurn);
        temp.duration = duration;
        temp.turnDuration = turnDuration;
        temp.interval = interval;
        temp.buffValue = buffValue;
        return temp;
    }

    public string GenerateBuffDesc(BaseMapRole role)
    {
        string[] tempStr = BuffDesc.Split('*');
        string result = tempStr[0];
        float add = 1f;
        if (role == null)
        {
            for (int i = 0; i < buffParam.Count; i++)
            {
                result += buffParam[i].ToString() + tempStr[i + 1];
            }
            if (tempStr.Length > 1)
            {
                result += tempStr[tempStr.Length - 1];
            }
            return result;
        }
        if (role.baseRoleData.baseRoleData.roleSkillType != RoleSkillType.Service)
        {
            if (role.encourageLevel > 0)
            {
                add += role.encourageLevel * 0.05f;
            }
            else
            {
                add += role.encourageLevel * -0.1f;
            }
        }
        else
        {
            add += role.encourageLevel * 0.1f;
        }
        for (int i = 0; i < buffParam.Count; i++)
        {
            result += ((int)(buffParam[i] * add)).ToString() + tempStr[i + 1];
        }
        if (tempStr.Length > 1)
        {
            result += tempStr[tempStr.Length - 1];
        }
        return result;
    }

    public string GenerateBuffDesc()
    {
        string[] tempStr = BuffDesc.Split('*');
        string result = tempStr[0];
        for (int i = 0; i < buffParam.Count; i++)
        {
            result += buffParam[i].ToString() + tempStr[i + 1];
        }
        if (tempStr.Length > 1)
        {
            result += tempStr[tempStr.Length - 1];
        }
        return result;
    }
}

