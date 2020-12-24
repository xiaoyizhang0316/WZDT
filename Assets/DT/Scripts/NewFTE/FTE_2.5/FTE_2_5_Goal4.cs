using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FTE_2_5_Goal4 : BaseGuideStep
{
    public GameObject bornPoint4;
    public GameObject bornPoint5;
    private int lastMoney = 0;
    private int lastSatisfy = 0;
    private int lastTimeCount = 0;
    public override IEnumerator StepStart()
    {
        lastMoney = StageGoal.My.playerGold;
        lastSatisfy = StageGoal.My.playerSatisfy;
        lastTimeCount = StageGoal.My.timeCount;
        StartCoroutine(bornPoint4.GetComponent<Building>().BornEnemyForFTE_2_5(302));
        StartCoroutine(bornPoint5.GetComponent<Building>().BornEnemyForFTE_2_5(301));
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish&&missiondatas.data[3].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = FTE_2_5_Manager.My.packageKillNum;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            missiondatas.data[1].currentNum = FTE_2_5_Manager.My.saleKillNum;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            if (missiondatas.data[2].isFinish == false)
            {
                missiondatas.data[2].currentNum = (StageGoal.My.playerGold - lastMoney) * 5 /
                                                  ((StageGoal.My.timeCount - lastTimeCount)==0?1:(StageGoal.My.timeCount - lastTimeCount));

                if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
                {
                    missiondatas.data[2].isFinish = true;
                }
            }

            if (missiondatas.data[3].isFinish == false)
            {
                missiondatas.data[3].currentNum = (StageGoal.My.playerSatisfy - lastSatisfy) * 5 /
                                                  ((StageGoal.My.timeCount - lastTimeCount) <= 0
                                                      ? 1
                                                      : (StageGoal.My.timeCount - lastTimeCount));
                if (missiondatas.data[3].currentNum >= missiondatas.data[3].maxNum)
                {
                    missiondatas.data[3].isFinish = true;
                }
            }
        }
    }
}
