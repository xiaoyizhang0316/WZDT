using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreCheck : SkillCheckBase
{
    public int startScore;

    protected override void InitCheck()
    {
        if (StageGoal.My.isTurnStart)
        {
            currentText.text = "本回合不计入检测！";
            InvokeRepeating("CheckStartInNextTurn", 1, 1);
            return;
        }
        
        currentText.text = isPercent ? "当前：0%" : "当前：0";
        isTurnEnd = true;
        checkedTurn = 0;
        startScore = SkillCheckManager.My.playerScore;
        InvokeRepeating("Check", 1f, 1f);
    }

    void CheckStartInNextTurn()
    {
        if (!StageGoal.My.isTurnStart)
        {
            CancelInvoke();
            isTurnEnd = true;
            startScore = SkillCheckManager.My.playerScore;
            currentText.text = isPercent ? "当前：0%" : "当前：0";

            checkedTurn = 0;
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
    }

    private int currentScore;
    protected override void Check()
    {
        if (StageGoal.My.isTurnStart)
        {
            if (isTurnEnd)
            {
                isTurnEnd = false;
            }
        }
        
        currentScore = SkillCheckManager.My.playerScore - startScore;
        currentText.text = "当前：" +currentScore;
        isSuccess = currentScore >= int.Parse(target);
        
        if (!StageGoal.My.isTurnStart && !isTurnEnd)
        {
            Debug.Log("check Turn end");
            isTurnEnd = true;
            checkedTurn += 1;
            if (checkedTurn >= checkTurn)
            {
                NotifyRoleEnd();
                return;
            }
        }
    }
}
