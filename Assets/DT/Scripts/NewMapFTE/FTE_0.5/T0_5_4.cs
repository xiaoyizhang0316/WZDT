﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class T0_5_4 : BaseGuideStep
{

    public BaseMapRole maprole;
    // Start is called before the first frame update
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
      NewCanvasUI.My.GameNormal();
      maprole.OnMoved += ChangeColor;
    
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
       
        yield return new WaitForSeconds(2);

    }

    public override bool ChenkEnd()
    {
     
        TradeManager.My.HideAllIcon();
        missiondatas.data[0].currentNum = maprole.warehouse.Count; 
        if (maprole.warehouse.Count >= 10)
        {
            return true;
        }
        else
        {
           
            return false;

        }

      
    }


    public void ChangeColor(ProductData data)
    {
        if (data.damage > 0)
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.seerJC1_ran,FTE_0_5Manager.My.sg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.seerJC1_ran,FTE_0_5Manager.My.sr ); 
        }
    }
}
