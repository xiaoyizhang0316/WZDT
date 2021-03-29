using System.Collections;
using System.Collections.Generic;
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

    public int roleTargetCount;
    public int role1TargetCount;
    
    public int targetdamege;
    public int targetdamege1;
    /// <summary>
    /// 当前速率
    /// </summary>
    public int currentRate;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
                                    role.OnMoved += ChangeColor;
                                    role1.OnMoved += ChangeColor1;
        time = StageGoal.My.timeCount;
        role.warehouse.Clear();
        role1.warehouse.Clear();
        role.baseRoleData.bulletCapacity = roleTargetCount;
        role1.baseRoleData.bulletCapacity = role1TargetCount;
        
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
    }

    public void ChangeColor(ProductData data)
    {
        if (data.damage >=targetdamege)
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC1_ran,FTE_0_5Manager.My.sg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC1_ran,FTE_0_5Manager.My.sr ); 
        }
    }
    public void ChangeColor1(ProductData data)
    {
        if (data.damage >=targetdamege1)
        {
//            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC2_ran,FTE_0_5Manager.My.bg );
        }

        else
        {
          //  FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC2_ran,FTE_0_5Manager.My.br ); 
        }
    }
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
        for (int i = 0; i < role.warehouse.Count; i++)
        {
            if (role.warehouse[i].damage <targetdamege)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }
        for (int i = 0; i < role1.warehouse.Count; i++)
        {
            if (role1.warehouse[i].damage <targetdamege1)
            {
                role1.warehouse.Remove(role1.warehouse[i]);
            }
        }

        info.text = "剩余时间 : "+(shengyuTime- (StageGoal.My.timeCount-time)) +"  质监站1剩余："+(roleTargetCount-role.warehouse.Count)
                    +"           质监站2剩余："+(role1TargetCount-role1.warehouse.Count)
            ;
        if ((StageGoal.My.timeCount - time) % shengyuTime == 0)
        {
            role.warehouse.Clear();
            role1.warehouse.Clear();
            time = StageGoal.My.timeCount;
            missiondatas.data[0].isFinish = false;
        }
 
        if ( role.warehouse.Count >=roleTargetCount&& role1.warehouse.Count >=role1TargetCount)
        {
            missiondatas.data[0].isFinish = true;
            return true; 
        }

        return false;

    }
}