using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_6_2 : BaseGuideStep
{

    public BaseMapRole role; 
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
        time -=1 ;
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
     
        info.text = "剩余时间 : "+(shengyuTime- (StageGoal.My.timeCount-time))   
                
            ;

        missiondatas.data[0].currentNum = role.warehouse.Count;
        if ((StageGoal.My.timeCount - time) % shengyuTime == 0)
        {
            role.warehouse.Clear();
            time = StageGoal.My.timeCount;
        }
  
        if ( role.warehouse.Count >= missiondatas.data[0].maxNum )
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