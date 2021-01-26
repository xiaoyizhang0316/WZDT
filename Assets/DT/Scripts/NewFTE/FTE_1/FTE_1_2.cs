using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FTE_1_2 : BaseGuideStep
{
    // Start is called before the first frame update
   

    public override IEnumerator StepStart()
    {

        for (int i = 0; i <   MapManager.My._mapSigns.Count; i++)
        {
            MapManager.My._mapSigns[i].isCanPlace = false;
        }

        MapManager.My.GetMapSignByXY(8, 24).isCanPlace = true;
        
        yield return new WaitForSeconds(0.5f); 
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        if (PlayerData.My.dealerCount >= 1)
        {
            missiondatas.data[0].currentNum = PlayerData.My.dealerCount;
            missiondatas.data[0].isFinish = true;
            return true;
        }

        else
        {
            return false;
        }
    }
}