using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCheck : SkillCheckBase
{
    public int startKillNum;
    public int startTurn;

    protected override void InitCheck()
    {
        startTurn = StageGoal.My.currentWave;
        if (StageGoal.My.isTurnStart)
        {
            startTurn = StageGoal.My.currentWave + 1;
            // TODO 检测从下回合开始
            currentText.text = "本回合不计入检测！";
            InvokeRepeating("CheckStartInNextTurn", 1, 1);
            return;
        }
        
        if (isPercent)
        {
            currentText.text = "当前：0%";
        }
        else
        {
            currentText.text = "当前：0";
        }
        startKillNum = SkillCheckManager.My.killNum;
        InvokeRepeating("Check", 0.5f, 0.5f);
    }

    void CheckStartInNextTurn()
    {
        if (StageGoal.My.currentWave == startTurn)
        {
            startKillNum = SkillCheckManager.My.killNum;
            if (isPercent)
            {
                currentText.text = "当前：0%";
            }
            else
            {
                currentText.text = "当前：0";
            }
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
    }

    protected override void Check()
    {
        
    }
}
