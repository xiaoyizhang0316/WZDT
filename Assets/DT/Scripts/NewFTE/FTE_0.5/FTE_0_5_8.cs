﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_8 : BaseGuideStep
{
    public BaseMapRole role1;

    public BaseMapRole role2;

    public int seed1targetdamege;
    public Text info;
    /// <summary>
    /// 目标速率
    /// </summary>
    public int targetRate;
    int time;

    /// <summary>
    /// 当前速率
    /// </summary>
    public int currentRate;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        time = StageGoal.My.timeCount;
        role1.warehouse.Clear();
        role2.warehouse.Clear();
  yield return new WaitForSeconds(1f);
 //  for (int i = 0; i <land.Count; i++)
 //  {
 //      land[i].transform.DOLocalMoveY(-5, 1f).Play();
 //  }
 //  yield return new WaitForSeconds(1f);
 //  for (int i = 0; i <land.Count; i++)
 //  {
 //      land[i].transform.DOLocalMoveY(0, 1f).Play();
 // 
 //  }

 //  for (int i = 0; i < Seedtesting.Count; i++)
 //  {
 //      Seedtesting[i].SetActive(true);
 //      Seedtesting[i].transform.DOLocalMoveY(0.3f, 1f).Play();

 //  }
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }


    public override bool ChenkEnd()
    {
        if (role1.warehouse.Count == role1.baseRoleData.bulletCapacity)
        {
            role1.warehouse.Clear();
        }
        if (role2.warehouse.Count == role2.baseRoleData.bulletCapacity)
        {
            role2.warehouse.Clear();
        }
        for (int j = 0; j < role1.warehouse.Count; j++)
        {
            if (role1.warehouse[j].damage < seed1targetdamege)
            {
                role1.warehouse.Remove(role1.warehouse[j]);
            }
        }
       
        missiondatas.data[0].currentNum = role1.warehouse.Count;
     
        if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
           
        }
        info.text = "目标效率为："+targetRate+"/60s                  当前效率为："+currentRate+"/60s";
        if ((StageGoal.My.timeCount - time) % 60 == 0)
        {
            role2.warehouse.Clear();
            time = StageGoal.My.timeCount;
        }

        missiondatas.data[1].currentNum = role2.warehouse.Count; 
        currentRate =(int)( (float)(role2.warehouse.Count)/ (float)(StageGoal.My.timeCount - time)  * 60);

        if (currentRate >= targetRate&&role2.warehouse.Count > missiondatas.data[1].maxNum)
        {
            missiondatas.data[1].isFinish = true;
         
        }

        if (  missiondatas.data[1].isFinish &&  missiondatas.data[0].isFinish )
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}