using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkIncomeCheck : SkillCheckBase
{
    public int startIncome;

    protected override void InitCheck()
    {
        startIncome = SkillCheckManager.My.drinkIncome;
    }

    private int currentIncome;
    protected override void Check()
    {
        currentIncome = SkillCheckManager.My.drinkIncome - startIncome;
        currentText.text = "当前：" +currentIncome;
        isSuccess = currentIncome >= int.Parse(target);
        base.Check();
    }
}
