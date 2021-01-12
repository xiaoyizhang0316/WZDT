using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_11_3 : BaseGuideStep
{

    public BaseMapRole role;
    public BaseMapRole role1;    
    public BaseMapRole role2;
    public BaseMapRole role3;

    int time;

    public int shengyuTime;
    public Text info; 

    public int roleTargetCount;
    public int role1TargetCount;
    public int role2TargetCount;
    public int role3TargetCount;
    
    public int targetdamege;
    public int targetdamege1;
    public int targetdamege2;
    public int targetdamege3;
    /// <summary>
    /// 当前速率
    /// </summary>
    public int currentRate;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
   FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC3);
   FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC4);

                                    role.OnMoved += ChangeColor;
                                    role1.OnMoved += ChangeColor1;
                                    role2.OnMoved += ChangeColor2;
                                    role3.OnMoved += ChangeColor3;
        time = StageGoal.My.timeCount;
        role.warehouse.Clear();
        
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
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC2_ran,FTE_0_5Manager.My.bg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC2_ran,FTE_0_5Manager.My.br ); 
        }
    }
    public void ChangeColor2(ProductData data)
    {
        if (data.damage >=targetdamege2)
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC3_ran,FTE_0_5Manager.My.bg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC3_ran,FTE_0_5Manager.My.br ); 
        }
    }
    
    public void ChangeColor3(ProductData data)
    {
        if (data.damage >=targetdamege3)
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC4_ran,FTE_0_5Manager.My.bg );
        }

        else
        {
            FTE_0_5Manager.My.ChangeColor( FTE_0_5Manager.My.dealerJC4_ran,FTE_0_5Manager.My.br ); 
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
        for (int i = 0; i < role2.warehouse.Count; i++)
        {
            if (role2.warehouse[i].damage <targetdamege1)
            {
                role2.warehouse.Remove(role2.warehouse[i]);
            }
        }
        for (int i = 0; i < role3.warehouse.Count; i++)
        {
            if (role3.warehouse[i].damage <targetdamege1)
            {
                role3.warehouse.Remove(role3.warehouse[i]);
            }
        }
        info.text = "剩余时间 : "+(shengyuTime- (StageGoal.My.timeCount-time)) +"  质监站1剩余："+(roleTargetCount-role.warehouse.Count)
                    +"           质监站2剩余："+(role1TargetCount-role1.warehouse.Count) +"           质监站3剩余："+(role2TargetCount-role2.warehouse.Count)
                    +"           质监站4剩余："+(role3TargetCount-role3.warehouse.Count)   ;
        if ((StageGoal.My.timeCount - time) % shengyuTime == 0)
        {
            role.warehouse.Clear();
            role1.warehouse.Clear();
            role2.warehouse.Clear();
            role3.warehouse.Clear();
            time = StageGoal.My.timeCount;
        }
 
        if ( role.warehouse.Count >roleTargetCount&& role3.warehouse.Count >=role3TargetCount&& role1.warehouse.Count >=role1TargetCount&& role2.warehouse.Count >=role2TargetCount)
        {
            missiondatas.data[0].isFinish = true;
            return true; 
        }

        return false;

    }
}