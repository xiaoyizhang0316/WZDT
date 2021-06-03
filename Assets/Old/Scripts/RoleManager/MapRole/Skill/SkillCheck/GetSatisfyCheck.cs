using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSatisfyCheck : SkillCheckBase
{
    public int startSatisfy;

    protected override void InitCheck()
    {
        startSatisfy = StageGoal.My.playerHealth;
        currentSatisfy = 0;
    }

    private int currentSatisfy;
    protected override void Check()
    {
        currentSatisfy = StageGoal.My.playerHealth - startSatisfy;
        currentText.text = "当前：" +currentSatisfy;
        isSuccess = currentSatisfy >= int.Parse(target);
        base.Check();
    }
}
