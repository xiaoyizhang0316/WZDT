using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工商技能
/// </summary>
public class ServiceTradeOffice : BaseServiceSkill
{
    public int lastTurnIncome;

    public override void OnEndTurn()
    {
        base.OnEndTurn();
        // TODO 激励等级加成，世界交易成本等级加成
    }
}
