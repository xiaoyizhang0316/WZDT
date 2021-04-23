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


    public float OnEncourageValueChange()
    {
        float result = 0f;
        if (role.encourageLevel <= -3)
        {
            
        }
        if (role.encourageLevel < 0)
        {
            
        }

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
        return result;
    }

    public float CalculateNumber()
    {
        float result;
        result = (role.encourageLevel - 3f) * skillData.add + skillData.startValue;
        return result;
    }

    private BaseEncourageSkill(string name,BaseMapRole _role)
    {
        skillData = GameDataMgr.My.GetEncourageSkillDataByName(name);
        role = _role;
    }
    
}
