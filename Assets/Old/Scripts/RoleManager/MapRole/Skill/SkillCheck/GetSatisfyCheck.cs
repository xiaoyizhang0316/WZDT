using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSatisfyCheck : SkillCheckBase
{
    public int startSatisfy;

    protected override void InitCheck()
    {
        startSatisfy = StageGoal.My.playerHealth;
    }

    private int currentSatisy;
    protected override void Check()
    {
        currentSatisy = StageGoal.My.playerHealth - startSatisfy;
        currentText.text = "当前：" +currentSatisy;
        isSuccess = currentSatisy >= int.Parse(target);
        base.Check();
    }
}
