using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T6_1 : BaseGuideStep
{
    
    public GameObject roleImage;

    public GameObject red; 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.maxRoleLevel = 1;
     
        RoleListManager.My.OutButton();

        yield return new WaitForSeconds(1f);

        roleImage.gameObject.SetActive(false);
        red.SetActive(true);
    }

    public override IEnumerator StepEnd()
    { 
        red.SetActive(false);
        yield return new WaitForSeconds(2); 
      
    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                PlayerData.My.MapRole[i].tradeButton.SetActive(false);
                missiondatas.data[0].currentNum = 1; 
                missiondatas.data[0].isFinish= true; 
                return true;
            }
        }

        return false;
    }

}
