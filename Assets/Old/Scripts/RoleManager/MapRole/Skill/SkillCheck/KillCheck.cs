using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCheck : SkillCheckBase
{
    public int startKillNum;
    public int startTurn;
    public int totalConsumerNum;
    public int checkedTurn;

    protected override void InitCheck()
    {
        startTurn = StageGoal.My.currentWave;
        if (StageGoal.My.isTurnStart)
        {
            startTurn = StageGoal.My.currentWave + 1;
            currentText.text = "本回合不计入检测！";
            InvokeRepeating("CheckStartInNextTurn", 1, 1);
            return;
        }
        
        currentText.text = isPercent ? "当前：0%" : "当前：0";

        checkedTurn = 0;
        startKillNum = SkillCheckManager.My.killNum;
        totalConsumerNum = BuildingManager.My.GetExtraConsumerNumber("100")+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave);
        InvokeRepeating("Check", 0.5f, 0.5f);
    }

    void CheckStartInNextTurn()
    {
        if (StageGoal.My.currentWave == startTurn && StageGoal.My.isTurnStart)
        {
            startKillNum = SkillCheckManager.My.killNum;
            currentText.text = isPercent ? "当前：0%" : "当前：0";

            checkedTurn = 0;
            totalConsumerNum = BuildingManager.My.GetExtraConsumerNumber("100")+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave);
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
    }

    private float currentPercent;
    protected override void Check()
    {
        if (!StageGoal.My.isTurnStart)
        {
            checkedTurn += 1;
            if (checkedTurn < checkTurn)
            {
                totalConsumerNum += BuildingManager.My.GetExtraConsumerNumber("100")+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave);
            }
            else
            {
                return;
            }
        }

        currentPercent = (SkillCheckManager.My.killNum - startKillNum) / (float)totalConsumerNum;
        currentText.text = "当前：" + currentPercent.ToString("F2") + "%";
        isSuccess = currentPercent >= float.Parse(target);
    }
}
