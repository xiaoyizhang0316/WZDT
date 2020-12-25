﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_3 : BaseGuideStep
{

    public BaseMapRole maprole;
    // Start is called before the first frame update
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
      NewCanvasUI.My.GameNormal();
    
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].tradeButton.transform.localScale = Vector3.zero;
            
            PlayerData.My.MapRole[i].tradeButton.gameObject.SetActive(true);
            PlayerData.My.MapRole[i].tradeButton.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBounce).Play();
        }
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator StepEnd()
    {
       
        missiondatas.data[0].isFinish= true; 
        
      yield break; 
    }

    public override bool ChenkEnd()
    {
        if (maprole.warehouse.Count > 10)
        {
            return true;
        }
        else
        {
            missiondatas.data[0].currentNum = maprole.warehouse.Count; 
            return false;

        }

    }

 
}