﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BankLoan : BaseServiceSkill
{
    public int loanNumber;

    public int timeCount;

    public int eachReturn;

    public TradeSign sign;

    public override void Skill(TradeData data)
    {
        base.Skill(data);
        TradeManager.My.tradeList[data.ID].icon.gameObject.SetActive(false);
        //Debug.Log("开始借钱" + _sign.tradeData.ID);
        sign = TradeManager.My.tradeList[data.ID];
        StartLoan(sign);

    }

    public void StartLoan(TradeSign sign)
    {
        int actualLoan = loanNumber;
        count = 0;
        StageGoal.My.GetPlayerGold(actualLoan, true);
        StageGoal.My.Income(actualLoan, IncomeType.Npc, GetComponentInParent<BaseMapRole>());
        eachReturn = (int)(actualLoan * (1 + CalculateInterest(PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole)))) / timeCount);
        if (StageGoal.My.currentType != GameEnum.StageType.Normal)
        {
            StartCoroutine(KeepLoan(sign));
        }
    }

    private int count = 0;

    public IEnumerator KeepLoan(TradeSign sign)
    {
        while (count < timeCount)
        {
            Tweener twe = transform.DOScale(1f, 20f);
            yield return twe.WaitForCompletion();
            StageGoal.My.CostPlayerGold(eachReturn);
            StageGoal.My.Expend(eachReturn, ExpendType.AdditionalCosts, null, "还贷");
            count++;
        }
        TradeManager.My.DeleteTrade(sign.tradeData.ID);
    }

    public override void OnEndTurn()
    {
        StageGoal.My.CostPlayerGold(eachReturn);
        StageGoal.My.Expend(eachReturn, ExpendType.AdditionalCosts, null, "还贷");
        count++;
        if (count == timeCount)
        {
            TradeManager.My.DeleteTrade(sign.tradeData.ID);
        }
        base.OnEndTurn();
    }

    /// <summary>
    /// 计算利率
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public float CalculateInterest(BaseMapRole role)
    {
        int risk = role.baseRoleData.riskResistance;
        risk = Mathf.Min(risk, 180);
        risk = Mathf.Max(risk, 30);
        //print("利率：" + (risk * 0.1f - 3f) / 100f);
        return Mathf.Round((risk * 0.1333f - 4) * 10 )/ 1000f;
    }

    private new void Start()
    {
        base.Start();
    }
}
