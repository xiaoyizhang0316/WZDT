using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExtraSkill : MonoBehaviour
{
    /// <summary>
    /// 是否开启
    /// </summary>
    public bool isOpen;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string extraSkillDesc;

    /// <summary>
    /// 最大技能
    /// </summary>
    public int maxTradeLimit;

    /// <summary>
    /// 开启技能
    /// </summary>
    /// <param name="sign"></param>
    public virtual void SkillOn(TradeSign sign=null)
    {
        isOpen = true;
    }

    /// <summary>
    /// 关闭技能
    /// </summary>
    /// <param name="sign"></param>
    public virtual void SkillOff(TradeSign sign)
    {

    }

    public virtual bool CheckSkillCondition()
    {
        return true;
    }

    /// <summary>
    /// 当回合结束时
    /// </summary>
    public virtual void OnEndTurn()
    {

    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        isOpen = false;
        GetComponentInParent<BaseMapRole>().extraSkill = this;
    }
}
