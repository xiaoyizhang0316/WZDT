using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCheck : SkillCheckBase
{
    public int startKillNum;
    public int totalConsumerNum;

    protected override void InitCheck()
    {
        startKillNum = SkillCheckManager.My.killNum;
        totalConsumerNum = GetTotalConsumer();
        Debug.Log("total consumer "+totalConsumerNum);
    }
    

    private float currentPercent;
    protected override void Check()
    {
        currentPercent =100 * (SkillCheckManager.My.killNum - startKillNum) / (float)(totalConsumerNum==0?1: totalConsumerNum);
        currentText.text = "当前：" + currentPercent.ToString("F2") + "%";
        isSuccess = currentPercent >= float.Parse(target);
        base.Check();
    }

    protected override void TurnEnd()
    {
        totalConsumerNum += GetTotalConsumer();
    }

    int GetTotalConsumer()
    {
        return BuildingManager.My.GetExtraConsumerNumber("100") +
               BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave) /*+
               BuildingManager.My.GetExtraConsumerNumber("100") +
               BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave + 1) +
               BuildingManager.My.GetExtraConsumerNumber("100") +
               BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave + 2)*/;
    }
}
