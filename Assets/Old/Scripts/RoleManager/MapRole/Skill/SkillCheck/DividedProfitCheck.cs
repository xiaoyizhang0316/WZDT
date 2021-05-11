using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedProfitCheck : SkillCheckBase
{
    public int startDividedProfit;

    protected override void InitCheck()
    {
        if (StageGoal.My.isTurnStart)
        {
            currentText.text = "本回合不计入检测！";
            InvokeRepeating("CheckStartInNextTurn", 1, 1);
            return;
        }

        SkillCheckManager.My.checkDivide=true;
        SkillCheckManager.My.proportion=detail.proportion;
        currentText.text = isPercent ? "当前：0%" : "当前：0";
        isTurnEnd = true;
        checkedTurn = 0;
        startDividedProfit = SkillCheckManager.My.dividedProfit;
        InvokeRepeating("Check", 1f, 1f);
    }

    void CheckStartInNextTurn()
    {
        if (!StageGoal.My.isTurnStart)
        {
            CancelInvoke();
            isTurnEnd = true;
            startDividedProfit = SkillCheckManager.My.dividedProfit;
            currentText.text = isPercent ? "当前：0%" : "当前：0";

            checkedTurn = 0;
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
    }

    private int currentDividedProfit;
    protected override void Check()
    {
        if (StageGoal.My.isTurnStart)
        {
            if (isTurnEnd)
            {
                isTurnEnd = false;
            }
        }
        
        currentDividedProfit = SkillCheckManager.My.dividedProfit - startDividedProfit;
        currentText.text = "当前：" +currentDividedProfit;
        isSuccess = currentDividedProfit >= int.Parse(target);
        
        if (!StageGoal.My.isTurnStart && !isTurnEnd)
        {
            Debug.Log("check Turn end");
            isTurnEnd = true;
            checkedTurn += 1;
            if (checkedTurn >= checkTurn)
            {
                SkillCheckManager.My.checkDivide = false;
                NotifyRoleEnd();
            }
        }
    }
}
