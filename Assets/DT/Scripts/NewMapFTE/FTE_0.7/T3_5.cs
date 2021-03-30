using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T3_5: BaseGuideStep
{
 
    public BaseMapRole role;
    public BaseMapRole role1;    
    public BaseMapRole role2;
    public BaseMapRole role3;  
    public Text info; 

    public int roleTargetCount;
    public int role1TargetCount;
    public int role2TargetCount;
    public int role3TargetCount;
      
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        
         FTE_0_6Manager.My.UpRole( FTE_0_6Manager.My.dealerJC2);
        FTE_0_6Manager.My.UpRole( FTE_0_6Manager.My.dealerJC3);
        FTE_0_6Manager.My.UpRole( FTE_0_6Manager.My.dealerJC4);
   role.baseRoleData.bulletCapacity = roleTargetCount;
   role1.baseRoleData.bulletCapacity = role1TargetCount;
   role2.baseRoleData.bulletCapacity = role2TargetCount;
   role3.baseRoleData.bulletCapacity = role3TargetCount; 
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
        
        info.text = "  质监站1剩余："+(roleTargetCount-role.warehouse.Count)
                    +"           质监站2剩余："+(role1TargetCount-role1.warehouse.Count) +"           质监站3剩余："+(role2TargetCount-role2.warehouse.Count)
                    +"           质监站4剩余："+(role3TargetCount-role3.warehouse.Count)   ;
       
        if ( role.warehouse.Count >=roleTargetCount&& role3.warehouse.Count >=role3TargetCount&& role1.warehouse.Count >=role1TargetCount&& role2.warehouse.Count >=role2TargetCount)
        {
            missiondatas.data[0].isFinish = true;
            return true; 
        }

        return false;

    }
}
