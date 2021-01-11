using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_12 : BaseGuideStep
{

    public GameObject red;
    public GameObject roleImage;
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
        
        yield break;
        ;
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