using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5_6 : BaseGuideStep
{
  
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
     
        yield return new WaitForSeconds(1f);
      
    }

    public override IEnumerator StepEnd()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                PlayerData.My.MapRole[i].warehouse.Clear();
            }
        }
       yield break; 
    }


   
    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                if (PlayerData.My.RoleData[i].baseRoleData.level == 2)
                {
                    missiondatas.data[0].isFinish = true;
                    missiondatas.data[0].currentNum = 1;
                }
            }
        }
        
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.EquipList.Count>0)
                {
                    missiondatas.data[1].isFinish = true;
                    missiondatas.data[1].currentNum = 1;
                }
            }
        }

        if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
        {
            return true;
        }

        return false;
    }
 
}
