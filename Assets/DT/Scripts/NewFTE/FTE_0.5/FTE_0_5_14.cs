using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_14 : BaseGuideStep
{
    public GameObject land;
    public GameObject roleImage;
    public GameObject red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_5Manager.My.DownRole(FTE_0_5Manager.My.dealerJC1);
        FTE_0_5Manager.My.DownRole(FTE_0_5Manager.My.dealerJC2);
        FTE_0_5Manager.My.DownRole(FTE_0_5Manager.My.dealerJC3);
        FTE_0_5Manager.My.DownRole(FTE_0_5Manager.My.dealerJC4);
       red.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayerData.My.DeleteRole(90002);
        PlayerData.My.DeleteRole(90003);
        PlayerData.My.DeleteRole(90004);
        PlayerData.My.DeleteRole(90005);
         
        land.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack).Play();

        yield return new WaitForSeconds(1f);

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