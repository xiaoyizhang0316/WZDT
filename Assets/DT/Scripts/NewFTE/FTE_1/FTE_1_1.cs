using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FTE_1_1 : BaseGuideStep
{
    // Start is called before the first frame update
   

    public override IEnumerator StepStart()
    {
        PlayerData.My.seedCount = 0;
        PlayerData.My.peasantCount = 0;
        PlayerData.My.merchantCount = 0;
        PlayerData.My.dealerCount = 0;
        RoleListManager.My.OutButton();
        yield return new WaitForSeconds(0.5f); 
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        if (PlayerData.My.seedCount >= 1)
        {
            missiondatas.data[0].currentNum = PlayerData.My.seedCount;
            missiondatas.data[0].isFinish = true;
        }

        if (PlayerData.My.peasantCount >= 1)
        {
            missiondatas.data[1].currentNum = PlayerData.My.peasantCount;
            missiondatas.data[1].isFinish = true;
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}