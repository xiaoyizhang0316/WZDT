using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedProfitCheck : SkillCheckBase
{
    public int startDividedProfit;

    protected override void InitCheck()
    {
        startDividedProfit = SkillCheckManager.My.dividedProfit;
        SkillCheckManager.My.checkDivide=true;
    }

    private int currentDividedProfit;
    protected override void Check()
    {
        currentDividedProfit = SkillCheckManager.My.dividedProfit - startDividedProfit;
        currentText.text = "当前：" +currentDividedProfit;
        isSuccess = currentDividedProfit >= int.Parse(target);
        base.Check();
    }

    protected override void EndCheck()
    {
        SkillCheckManager.My.checkDivide=false;
    }
}
