using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCheck : SkillCheckBase
{
    public int startKillNum;
    public int totalConsumerNum;
    public int checkedTurn;

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
        startKillNum = SkillCheckManager.My.killNum;
        totalConsumerNum = BuildingManager.My.GetExtraConsumerNumber("100")+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave+1);
        Debug.Log("total consumer "+totalConsumerNum);
        InvokeRepeating("Check", 1f, 1f);
    }

    void CheckStartInNextTurn()
    {
        if (!StageGoal.My.isTurnStart)
        {
            CancelInvoke();
            isTurnEnd = true;
            startKillNum = SkillCheckManager.My.killNum;
            currentText.text = isPercent ? "当前：0%" : "当前：0";

            checkedTurn = 0;
            totalConsumerNum = BuildingManager.My.GetExtraConsumerNumber("100")+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave+1);
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
    }

    private float currentPercent;
    protected override void Check()
    {
        if (!StageGoal.My.isTurnStart && !isTurnEnd)
        {
            Debug.Log("check Turn end");
            isTurnEnd = true;
            checkedTurn += 1;
            if (checkedTurn < checkTurn)
            {
                totalConsumerNum += BuildingManager.My.GetExtraConsumerNumber("100")+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave+1);
            }
            else
            {
                CancelInvoke();
                if (detail.isMainTarget)
                {
                    SkillCheckManager.My.NotifyEnd(dependRole);
                }
                return;
            }
        }

        if (StageGoal.My.isTurnStart)
        {
            if (isTurnEnd)
            {
                isTurnEnd = false;
            }
        }
        
        Debug.Log(" check total consumer "+totalConsumerNum);

        currentPercent = (SkillCheckManager.My.killNum - startKillNum) / (float)(totalConsumerNum==0?1: totalConsumerNum);
        currentText.text = "当前：" + currentPercent.ToString("F2") + "%";
        isSuccess = currentPercent >= float.Parse(target);
    }
}
