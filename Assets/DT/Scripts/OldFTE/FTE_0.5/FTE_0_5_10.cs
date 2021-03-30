﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_10 : BaseGuideStep
{
    public BaseMapRole role;
    public BaseMapRole role2;
    int time;

    //public Text info;

    /// <summary>
    /// 目标速率
    /// </summary>
    public float targetRate;

    public int targetdamege;
    public int targetdamege2;

    /// <summary>
    /// 当前速率
    /// </summary>
    public float currentRate;

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        time = StageGoal.My.timeCount;
        role.warehouse.Clear();
       // role2.warehouse.Clear();
        role.OnMoved += ChangeColor;
       // role2.OnMoved += ChangeColor1;
      //  info.text = "目标效率为：" + targetRate + "个/s                  当前效率为：" + 0 + "个/s";

        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
    }

    public void ChangeColor(ProductData data)
    {
        if (data.damage >= targetdamege)
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.dealerJC1_ran, FTE_0_5Manager.My.sg);
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.dealerJC1_ran, FTE_0_5Manager.My.sr);
        }
    }

    public void ChangeColor1(ProductData data)
    {
        if (data.damage >= targetdamege2)
        {
      //      FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.dealerJC2_ran, FTE_0_5Manager.My.bg);
        }

        else
        {
         //   FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.dealerJC2_ran, FTE_0_5Manager.My.br);
        }
    }

    private int daojishi = 0;

    public override bool ChenkEnd()
    {
        if (NewCanvasUI.My.Panel_AssemblyRole.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        StageGoal.My.maxRoleLevel = 3;

        TradeManager.My.ShowAllIcon();
        for (int i = 0; i < role.warehouse.Count; i++)
        {
            if (role.warehouse[i].damage < targetdamege)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }

        missiondatas.data[0].currentNum = role.warehouse.Count;
        if (role.warehouse.Count >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }

        return false;
        //  for (int i = 0; i < role2.warehouse.Count; i++)
        //  {
        //      if (role2.warehouse[i].damage < targetdamege2)
        //      {
        //          role2.warehouse.Remove(role2.warehouse[i]);
        //      }
        //  }
//
        //  if ((StageGoal.My.timeCount - time) % 60 == 0)
        //  {
        //      role2.warehouse.Clear();
        //      time = StageGoal.My.timeCount;
        //  }
//
        //  missiondatas.data[1].currentNum = role2.warehouse.Count;
        //  if (role2.warehouse.Count <= 0)
        //  {
        //      return false;
        //  }
//
        //  currentRate = (float) (role2.warehouse.Count) / (float) (StageGoal.My.timeCount - time);
        //  info.text = "目标效率为：" + targetRate + "个/s                  当前效率为：" + currentRate.ToString("f2") + "个/s";
//
        //  if (currentRate >= targetRate && role2.warehouse.Count > missiondatas.data[1].maxNum)
        //  {
        //      if (daojishi == 0)
        //      {
        //          daojishi = timeCount;
        //      }
//
        //      if (daojishi != 0 && timeCount - daojishi >= 150)
        //      {
        //          missiondatas.data[1].isFinish = true; 
        //      }
        //      else
        //      {
        //          missiondatas.data[1].isFinish = true;
        //          return false;
        //      }
    }


     //   if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
     //   {
     //       return true;
     //   }
     //   else
     //   {
     //       return false;
     //   }
    }