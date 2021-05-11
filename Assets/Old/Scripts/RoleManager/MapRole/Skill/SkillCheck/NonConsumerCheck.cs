using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonConsumerCheck : SkillCheckBase
{
    public int startIncome;

    protected override void InitCheck()
    {
        startIncome = SkillCheckManager.My.nonConsumerIncome;
    }

    private int currentNonIncome;
    protected override void Check()
    {
        currentNonIncome = SkillCheckManager.My.nonConsumerIncome - startIncome;
        currentText.text = "当前：" +currentNonIncome;
        isSuccess = currentNonIncome >= int.Parse(target);
        base.Check();
    }
}
