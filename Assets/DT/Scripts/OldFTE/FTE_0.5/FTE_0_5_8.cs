﻿using System.Collections;
using System.Collections.Generic;
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
    public float targetRate;

    int time;

    /// <summary>
    /// 当前速率
    /// </summary>
    public float currentRate;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        role1.OnMoved += ChangeColor;
        role2.OnMoved += ChangeColor1;
        StageGoal.My.maxRoleLevel = 2;
        time = StageGoal.My.timeCount;
        role1.warehouse.Clear();
        role2.warehouse.Clear();
        info.text = "目标效率为：" + targetRate + "个/s                  当前效率为：" + 0 + "个/s";

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

    public GameObject peasant1;
    public GameObject peasant2;
    public override IEnumerator StepEnd()
    {
     FTE_0_5Manager.My.DownRole(peasant1);
     FTE_0_5Manager.My.DownRole(peasant2);
        yield return new WaitForSeconds(1.1f);
        PlayerData.My.DeleteRole(90000);
        PlayerData.My.DeleteRole(90001);
        yield return new WaitForSeconds(0.8f);

    }

    public void ChangeColor(ProductData data)
    {
        if (data.damage > seed1targetdamege)
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.seerJC1_ran, FTE_0_5Manager.My.sg);
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.seerJC1_ran, FTE_0_5Manager.My.sr);
        }
    }

    public void ChangeColor1(ProductData data)
    {
        if (data.damage > 0)
        {
//            FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.seerJC2_ran, FTE_0_5Manager.My.sg);
        }

        else
        {
          //  FTE_0_5Manager.My.ChangeColor(FTE_0_5Manager.My.seerJC2_ran, FTE_0_5Manager.My.sr);
        }
        
    }

    public int daojishi = 0;

    public override bool ChenkEnd()
    {
        info.text = " ";
        bool ismission2 = false;
        if (NewCanvasUI.My.Panel_AssemblyRole.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

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

        if ((StageGoal.My.timeCount - time) % 60 == 0)
        {
            role2.warehouse.Clear();
            time = StageGoal.My.timeCount;
            
        }

        missiondatas.data[1].currentNum = role2.warehouse.Count;
        if (role2.warehouse.Count <= 0)
        {
            return false;
        }

        currentRate = (float) (role2.warehouse.Count) / (float) (StageGoal.My.timeCount - time);
        info.text = "目标效率为：" + targetRate + "个/s                  当前效率为：" + currentRate.ToString("f2") + "个/s";

        if (currentRate >= targetRate && role2.warehouse.Count > missiondatas.data[1].maxNum)
        {
            if (daojishi == 0)
            {
                daojishi = timeCount;
            }

            if (daojishi != 0 && timeCount - daojishi >= 150)
            {
            
                ismission2 = true;
            }
            else
            {
                missiondatas.data[1].isFinish = false;
                ismission2 =  false;
            }
        }

        else
        {
            missiondatas.data[1].isFinish = false;
        }

        if (ismission2 && missiondatas.data[0].isFinish)
        {
            missiondatas.data[1].isFinish = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}