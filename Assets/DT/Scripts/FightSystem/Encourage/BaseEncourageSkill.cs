using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEncourageSkill
{
    public EncourageSkillData skillData;

    public bool isSkillOpen;

    public BaseMapRole role;
    
    public virtual void SkillOn()
    {
        isSkillOpen = true;
        BaseLevelController.My.AddBuff(role,skillData.skillType,skillData.targetBuff,CalculateNumber());
    }

    public virtual void SkillOff()
    {
        BaseLevelController.My.RemoveBuff(skillData.skillType,skillData.targetBuff);
        isSkillOpen = false;
    }


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

    public BaseEncourageSkill(int id,BaseMapRole _role)
    {
        skillData = GameDataMgr.My.GetEncourageSkillDataById(id);
        role = _role;
    }
    
}
