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
    public BaseMapRole role1;

    int time;

    public int shengyuTime;
    public Text info;
    /// <summary>
    /// 目标速率
    /// </summary>
    public int targetRate;

    public int targetdamege;
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
            if (role.warehouse[i].damage <targetdamege)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }
        for (int i = 0; i < role1.warehouse.Count; i++)
        {
            if (role1.warehouse[i].damage <targetdamege)
            {
                role1.warehouse.Remove(role1.warehouse[i]);
            }
        }

        info.text = "剩余时间 : "+(shengyuTime- (StageGoal.My.timeCount-time)) ;
        if ((StageGoal.My.timeCount - time) % shengyuTime == 0)
        {
            role.warehouse.Clear();
            role1.warehouse.Clear();
            time = StageGoal.My.timeCount;
        }

        missiondatas.data[0].currentNum = role.warehouse.Count;  
        missiondatas.data[1].currentNum = role1.warehouse.Count;  

        if (missiondatas.data[0].currentNum > missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true; 
           
        }
        if (missiondatas.data[1].currentNum > missiondatas.data[1].maxNum)
        {
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