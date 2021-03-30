﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_0_5_1 : BaseGuideStep
{
    public GameObject dailog;


    public List<int> equipAdd;

    public GameObject info;

    public bool isUpLoad;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.Panel_Update.SetActive(false);
        dailog.SetActive(true);
        if(info!=null)
        info.SetActive(false); 

        yield return new WaitForSeconds(1f);
      
    }
    public override IEnumerator StepEnd()
    {
        for (int i = 0; i < equipAdd.Count; i++)
        {
              PlayerData.My.GetNewGear(equipAdd[i]);
              if (isUpLoad )
             {
                  NetworkMgr.My.AddEquip(equipAdd[i],0,1);
              }
        }

        if (!isUpLoad&&equipAdd.Count > 0)
        {
            info.SetActive(true); 
            yield return new WaitForSeconds(2f);

        }

        if (isUpLoad )
        {
         PlayerData.My.Reset();
            info.SetActive(true);
            yield return new WaitForSeconds(2f);
            NetworkMgr.My.AddTeachLevel(TimeStamp.GetCurrentTimeStamp()-StageGoal.My.startTime, SceneManager.GetActiveScene().name, 1);

           NetworkMgr.My.UpdatePlayerFTE(0.5.ToString(), () => { SceneManager.LoadScene("Map"); });
        }
        dailog.SetActive(false);
        yield return new WaitForSeconds(2f);

    }

 
    public override bool ChenkEnd()
    {

        if (dailog.activeSelf)
        {
            return false;
        }

        else{
            return true;

        }

    }

 
}