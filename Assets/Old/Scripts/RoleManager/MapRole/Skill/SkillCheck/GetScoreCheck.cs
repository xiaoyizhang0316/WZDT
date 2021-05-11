using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreCheck : SkillCheckBase
{
    public int startScore;

    protected override void InitCheck()
    {
        startScore = SkillCheckManager.My.playerScore;
    }

    private int currentScore;
    protected override void Check()
    {
        currentScore = SkillCheckManager.My.playerScore - startScore;
        currentText.text = "当前：" +currentScore;
        isSuccess = currentScore >= int.Parse(target);
        base.Check();
    }
}
