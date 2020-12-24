using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_11 : BaseGuideStep
{

    public BaseMapRole role;

    int time;

    public Text info;
    /// <summary>
    /// 目标速率
    /// </summary>
    public int targetRate;

    /// <summary>
    /// 当前速率
    /// </summary>
    public int currentRate;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        time = StageGoal.My.timeCount;
        role.warehouse.Clear();
        
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield break;
    }


    public override bool ChenkEnd()
    {

        for (int i = 0; i < role.warehouse.Count; i++)
        {
            if (role.warehouse[i].damage < 80)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }

        info.text = "目标效率为："+targetRate+"/60s                  当前效率为："+currentRate+"/60s";
        if ((StageGoal.My.timeCount - time) % 60 == 0)
        {
            role.warehouse.Clear();
        }

        missiondatas.data[0].currentNum = role.warehouse.Count; 
        currentRate =(int)( (float)(role.warehouse.Count)/ (float)(StageGoal.My.timeCount - time)  * 60);

        if (currentRate >= targetRate&&role.warehouse.Count > missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }

        else
        {
            return false;
        }
 
    }
}