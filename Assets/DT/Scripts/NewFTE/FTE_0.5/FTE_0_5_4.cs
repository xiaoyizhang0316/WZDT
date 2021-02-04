﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_4 : BaseGuideStep
{

    public GameObject roleInfo;
    public GameObject bulletInfo;

    public GameObject roleinfoTip;
    public GameObject buildTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        buildTip.SetActive(true);
        yield return new WaitForSeconds(1f);
     
    }

    public override IEnumerator StepEnd()
    {

        roleinfoTip.SetActive(false);
        buildTip.SetActive(false);

   
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        TradeManager.My.HideAllIcon();

       
        if (roleInfo.activeInHierarchy)
        {
         //   roleinfoTip.SetActive(true);
            buildTip.SetActive(true);
            missiondatas.data[0].currentNum= 1;
            missiondatas.data[0].isFinish= true;
        }

        if (bulletInfo.activeInHierarchy)
        {
            missiondatas.data[1].currentNum= 1;
            missiondatas.data[1].isFinish= true;
        }

        if (missiondatas.data[0].isFinish &&missiondatas.data[1].isFinish)
        {
            return true;
        }
        else
        {
            return false;

        }


    }

 
}
