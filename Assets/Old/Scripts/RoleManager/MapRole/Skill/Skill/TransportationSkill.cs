using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 贸易商运送技能
/// </summary>
public class TransportationSkill : BaseSkill
{
    /// <summary>
    /// 当前汽车数量
    /// </summary>
    public int CarMaxCount = 20;

    public int CarCurrentCount;
    // Start is called before the first frame update
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
       PlayerData.My.GetMapRoleById( double.Parse(tradeData.targetRole)).UnLockPassivitySkill("运输_被动");
       PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)).GetPassivitySkill(("运输_被动")).targetMapRole =baseMapRole ;
       
       return true; 
    }

    public override bool DetectionCanRelease(BaseMapRole target)
    {
        return CarMaxCount > 0;
    }

  
}
