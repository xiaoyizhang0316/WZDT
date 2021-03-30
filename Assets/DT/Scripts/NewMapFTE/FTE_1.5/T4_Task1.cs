using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Task1 : BaseGuideStep
{
    public int limitCost = 0;
    public int startCost = 0;
    private bool isEnd;
    public override IEnumerator StepStart()
    {
        isEnd = false;
        if (StageGoal.My.killNumber != 0)
        {
            StageGoal.My.killNumber = 0;
        }

        startCost = StageGoal.My.totalCost;
        NewCanvasUI.My.GameNormal();
        Check();
        yield return null;
    }

    void Check()
    {
        InvokeRepeating("CheckGoal", 0.5f, 0.3f);
    }

    void CheckGoal()
    {
        if (!missiondatas.data[0].isFinish)
        {
            missiondatas.data[0].currentNum = StageGoal.My.killNumber;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
                MissionData md = new MissionData();
                md.content = "总成本<color=red>不超过"+limitCost+"</color>";
                md.currentNum = StageGoal.My.totalCost - startCost;
                md.maxNum = limitCost;
                if (md.currentNum > md.maxNum)
                {
                    md.isFail = true;
                }

                md.isMainmission = false;
                missiondatas.data.Add(md);
                MissionManager.My.AddMission(md,missionTitle);
                //rectBoard.SetActive(true);
                if (StageGoal.My.totalCost - startCost > limitCost)
                {
                    HttpManager.My.ShowTip("超出成本限制，本次任务失败！");
                }

                isEnd = true;
            }
        }
    }

    public override bool ChenkEnd()
    {
        return isEnd;
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3);
    }
}
