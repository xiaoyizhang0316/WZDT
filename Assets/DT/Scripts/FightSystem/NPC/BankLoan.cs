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
        int actualLoan = loanNumber;
        if (PlayerData.My.yingLiMoShi[2])
        {
            actualLoan = actualLoan * 120 / 100;
        }
        StageGoal.My.GetPlayerGold(actualLoan);
        StageGoal.My.Income(actualLoan, IncomeType.Npc, GetComponentInParent<BaseMapRole>());
        eachReturn = (int)(actualLoan * (1 + CalculateInterest(PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.targetRole)))) / timeCount);
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
        risk = Mathf.Min(risk, 180);
        risk = Mathf.Max(risk, 30);
        //print("利率：" + (risk * 0.1f - 3f) / 100f);
        return Mathf.Round((risk * 0.1333f - 4) * 10 )/ 1000f;
    }
}
