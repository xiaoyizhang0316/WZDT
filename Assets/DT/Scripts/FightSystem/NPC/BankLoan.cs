using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BankLoan : BaseExtraSkill
{
    public int loanNumber;

    public int timeCount;

    public int eachReturn;

    public override void SkillOn(TradeSign sign)
    {
        base.SkillOn(sign);
        sign.icon.gameObject.SetActive(false);
        StartCoroutine(StartLoan(sign));
    }

    public IEnumerator StartLoan(TradeSign sign)
    {
        int count = 0;
        StageGoal.My.GetPlayerGold(loanNumber);
        StageGoal.My.Income(loanNumber,IncomeType.Npc, GetComponentInParent<BaseMapRole>());
        eachReturn = (int)(loanNumber * (1 + CalculateInterest(PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole)))) / timeCount);
        while (count < timeCount)
        {
            Tweener twe = transform.DOScale(1f, 20f);
            yield return twe.WaitForCompletion();
            StageGoal.My.CostPlayerGold(eachReturn);
            StageGoal.My.Expend(eachReturn, ExpendType.AdditionalCosts,null,"还贷");
            count++;
        }
        TradeManager.My.DeleteTrade(sign.tradeData.ID);
    }

    public float CalculateInterest(BaseMapRole role)
    {
        int risk = role.baseRoleData.riskResistance;
        risk = Mathf.Min(risk, 150);
        risk = Mathf.Max(risk, 50);
        //print("利率：" + (risk * 0.1f - 3f) / 100f);
        return (risk * 0.1f - 3f) / 100f;
    }
}
