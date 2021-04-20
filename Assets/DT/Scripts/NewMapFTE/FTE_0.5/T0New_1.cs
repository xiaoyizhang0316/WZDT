using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T0New_1 : BaseGuideStep
{
  public BaseMapRole role; 
    public Text info; 
    int time;

    public int shengyuTime;
    public int roleTargetCount; 
      
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        time = StageGoal.My.timeCount; 
   role.baseRoleData.bulletCapacity = roleTargetCount; 
        role.warehouse.Clear(); 

        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        
        yield return new WaitForSeconds(2);
        FTE_0_5Manager.My.dealer.SetActive(true);
        FTE_0_5Manager.My.UpRole(  FTE_0_5Manager.My.dealer);
        TradeManager.My.DeleteRoleAllTrade(90003);
        FTE_0_5Manager.My.dealerJC1.SetActive(false);
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
        

        if ((StageGoal.My.timeCount - time) % shengyuTime == 0)
        {
            role.warehouse.Clear(); 
            time = StageGoal.My.timeCount;
            missiondatas.data[0].isFinish = false;

        }
        info.text = "剩余时间 : "+(shengyuTime- (StageGoal.My.timeCount-time)) +"  质监站剩余："+(roleTargetCount-role.warehouse.Count)
                     ;
       
        if ( role.warehouse.Count >=roleTargetCount  )
        {
            missiondatas.data[0].isFinish = true;
            return true; 
        }

        return false;

    }
}
