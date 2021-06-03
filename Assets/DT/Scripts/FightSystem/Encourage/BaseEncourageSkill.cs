using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色的激励等级技能类
/// 负责控制角色的激励等级技能效果
/// 包括激励等级技能效果的开关，数值计算
/// 激励等级技能目前有4类
/// 1 PlayerStatic类 回合类资源影响，通过baselevelcontroller里的buff正常结算
/// 2 ConsumerBUff类 影响消费者的属性，通过baselevelcontroller里的buff正常结算
/// 3 BuildingBuff类 影响每波生成消费者的数量，通过BuildingManager里的extraConsumer进行状态更新
/// 4 StageStatic类 即时类世界环境因素影戏那个，通过baselevelcontroller里的buff正常结算
/// </summary>
public class BaseEncourageSkill
{
    //激励等级技能数据存储实体
    public EncourageSkillData skillData;

    //激励等级技能开关状态
    public bool isSkillOpen;

    //激励等级技能的拥有者
    public BaseMapRole role;
    
    /// <summary>
    /// 激励等级技能激活时，将该激励等级效果注册到baselevelcontroller里
    /// </summary>
    public virtual void SkillOn()
    {
        isSkillOpen = true;
        if (skillData.skillType != GameEnum.EncourageSkillType.Self)
        {
            BaseLevelController.My.AddBuff(role,skillData.skillType,skillData.targetBuff,CalculateNumber());
        }

    }

    /// <summary>
    /// 激励等级技能关闭时，通知baselevelcontroller移除该激励等级效果
    /// </summary>
    public virtual void SkillOff()
    {
        BaseLevelController.My.RemoveBuff(skillData.skillType,skillData.targetBuff);
        isSkillOpen = false;
    }
    
    /// <summary>
    /// 激励等级发生变化时通知baselevelcontroller更改对应的加成数值
    /// </summary>
    public void OnEncourageValueChange()
    {
        if (role.encourageLevel >= 3)
        {
            if (!isSkillOpen)
            {
                SkillOn();
            }
            else
            {
                BaseLevelController.My.ChangeBuffNumber(skillData.skillType,skillData.targetBuff,CalculateNumber());
            }
        }
        else
        {
            if (isSkillOpen)
            {
                SkillOff();
            }
        }
    }

    /// <summary>
    /// 根据激励等级加成和指定特殊加成算出最终收益值
    /// </summary>
    /// <returns></returns>
    public float CalculateNumber()
    {
        float result;
        result = (role.encourageLevel - 3f) * skillData.add + skillData.startValue;
        float specialNumber = 0f;
        try
        {
            int choice = int.Parse(skillData.specialAddType);
            switch (choice)
            {
                case 0:
                    break;
                case 5:
                    specialNumber = BaseLevelController.My.riskControlLevel;
                    break;
                case 6:
                    specialNumber = BaseLevelController.My.tradeCostLevel;
                    break;
                case 7:
                    specialNumber = BaseLevelController.My.distanceLevel;
                    break;
                case 8:
                    specialNumber = BaseLevelController.My.monthCostLevel;
                    break;
                case 9:
                    specialNumber = BaseLevelController.My.encourageLevel;
                    break;
                case 100:
                    specialNumber = BuildingManager.My.GetExtraConsumerNumber("100");
                    break;
            }
        }
        catch (Exception e)
        {
            GameEnum.ConsumerType type = (GameEnum.ConsumerType)Enum.Parse(typeof(GameEnum.ConsumerType),skillData.specialAddType);
            specialNumber = BuildingManager.My.GetExtraConsumerNumber(type.ToString());
        }
        result += specialNumber * skillData.specialAdd;
        return result;
    }

    /// <summary>
    /// 生成激励等级描述字符串（暂时没用）
    /// </summary>
    /// <returns></returns>
    public string GenerateDesc()
    {
        string result = "";
        return result;
    }

    /// <summary>
    /// 构造函数，根据ID查找对应的激励等级技能数据并注册拥有者
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_role"></param>
    public BaseEncourageSkill(int id,BaseMapRole _role)
    {
        skillData = GameDataMgr.My.GetEncourageSkillDataById(id);
        role = _role;
    }
    
}
