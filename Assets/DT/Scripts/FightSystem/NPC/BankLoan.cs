using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BankLoan : BaseExtraSkill
{
    public int loanNumber;

    public int timeCount;

    public int eachReturn;

    public override void SkillOn()
    {
        base.SkillOn();
        int index = GetComponentInParent<BaseMapRole>().tradeList.Count - 1;
        GetComponentInParent<BaseMapRole>().tradeList[index].icon.gameObject.SetActive(false);
        StartCoroutine(StartLoan(index));
    }

    public IEnumerator StartLoan(int index)
    {
        int count = 0;
        StageGoal.My.GetPlayerGold(loanNumber);
        StageGoal.My.Income(loanNumber,IncomeType.Npc, GetComponentInParent<BaseMapRole>());
        while (count < timeCount)
        {
            Tweener twe = transform.DOScale(1f, 20f);
            yield return twe.WaitForCompletion();
            StageGoal.My.CostPlayerGold(eachReturn);
            StageGoal.My.Expend(eachReturn, ExpendType.AdditionalCosts);
            count++;
        }
        TradeManager.My.DeleteTrade(GetComponentInParent<BaseMapRole>().tradeList[index].tradeData.ID);
    }
}
