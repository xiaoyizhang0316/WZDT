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

    public float CalculateNumber()
    {
        float result;
        result = (role.encourageLevel - 3f) * skillData.add + skillData.startValue;
        return result;
    }

    private BaseEncourageSkill(int id,BaseMapRole _role)
    {
        skillData = GameDataMgr.My.GetEncourageSkillDataById(id);
        role = _role;
    }
    
}
