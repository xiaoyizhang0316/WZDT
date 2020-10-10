using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExtraSkill : MonoBehaviour
{
    public bool isOpen;

    public string extraSkillDesc;

    public int maxTradeLimit;

    public virtual void SkillOn(TradeSign sign)
    {
        isOpen = true;
    }

    public virtual void SkillOff(TradeSign sign)
    {
        
    }

    public virtual bool CheckSkillCondition()
    {
        return StageGoal.My.playerGold >= 0;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        isOpen = false;
        GetComponentInParent<BaseMapRole>().extraSkill = this;
    }
}
