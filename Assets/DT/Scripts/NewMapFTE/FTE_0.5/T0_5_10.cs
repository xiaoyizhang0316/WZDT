using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class T0_5_10 : BaseGuideStep
{
 
    public GameObject roleImage;
    public GameObject red;
    public BaseMapRole Peasant;


    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_5Manager.My.DownRole(FTE_0_5Manager.My.dealerJC1);
       red.SetActive(true); 

        yield return new WaitForSeconds(1f);
        if (Peasant == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType. Peasant&&
                    !PlayerData.My.MapRole[i].isNpc)    
                {
                    Peasant = PlayerData.My.MapRole[i];
                }
            }

        }
   PlayerData.My.DeleteRole(FTE_0_5Manager.My.dealerJC1.GetComponent<BaseMapRole>().baseRoleData.ID);
        roleImage.gameObject.SetActive(false);
    }

    public override IEnumerator StepEnd()
    {
        red.SetActive(false);

        missiondatas.data[0].currentNum = 1; 
        missiondatas.data[0].isFinish= true; 
        
        yield return new WaitForSeconds(2);

    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Dealer&&!PlayerData.My.RoleData[i].isNpc)
            {
          
                return true;
            }
        }

        return false;
    
    }
}