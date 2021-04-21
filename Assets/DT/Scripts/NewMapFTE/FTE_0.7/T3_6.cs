using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T3_6: BaseGuideStep
{
 
    public GameObject red;
    public GameObject roleImage;

    public  BaseMapRole nong;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {

        red.SetActive(true);
        
         
        roleImage.gameObject.SetActive(false);


        yield return new WaitForSeconds(1f);

    }

    public override IEnumerator StepEnd()
    {
        red.SetActive(false);

        missiondatas.data[0].currentNum = 1; 
        missiondatas.data[0].isFinish= true; 
        yield return new WaitForSeconds(2);

         
      
            TradeManager.My.DeleteRoleAllTrade(nong.baseRoleData.ID);
    
    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Merchant)
            {
              
                return true;
            }
        }

        return false;
    
    }
}
