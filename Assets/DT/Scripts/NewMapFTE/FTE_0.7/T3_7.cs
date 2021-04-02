using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T3_7: BaseGuideStep
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
     
    /// <summary>
    /// 当前速率
    /// </summary>
    public int currentRate;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
  //FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC3);
  //FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC4);
   role.baseRoleData.bulletCapacity = roleTargetCount;
   role1.baseRoleData.bulletCapacity = role1TargetCount;
   role2.baseRoleData.bulletCapacity = role2TargetCount;
   role3.baseRoleData.bulletCapacity = role3TargetCount; 
        time = StageGoal.My.timeCount;
        role.warehouse.Clear();
        role1.warehouse.Clear();
        role2.warehouse.Clear();
        role3.warehouse.Clear();

        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
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
            missiondatas.data[0].isFinish = false;

        }
 
        if ( role.warehouse.Count >=roleTargetCount&& role3.warehouse.Count >=role3TargetCount&& role1.warehouse.Count >=role1TargetCount&& role2.warehouse.Count >=role2TargetCount)
        {
            missiondatas.data[0].isFinish = true;
            return true; 
        }

        return false;

    }
}
