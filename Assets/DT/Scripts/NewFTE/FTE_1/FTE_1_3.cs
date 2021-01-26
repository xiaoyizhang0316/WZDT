using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FTE_1_3 : BaseGuideStep
{
    // Start is called before the first frame update
   

    public override IEnumerator StepStart()
    {

    
        yield return new WaitForSeconds(0.5f); 
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(1f);
        MissionManager.My.gameObject.SetActive(false);
     //   FTE_1Manager.My.blood.transform.DOLocalMoveY(FTE_1Manager.My.blood.transform.localPosition.y -200, 1f)
     //       .Play();
    }
    BaseMapRole seed;
    BaseMapRole Peasant;
    BaseMapRole dealer;
    public override bool ChenkEnd()
    {
        bool canseedhasTrad=false;
        bool candealerhasTrad=false;
        if (seed == null)
        {
            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
                {
                    seed = PlayerData.My.MapRole[i];
                }
            }
        }
        
        if (Peasant == null)
        {
            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant)
                {
                    Peasant = PlayerData.My.MapRole[i];
                }
            }
        }
        if (dealer == null)
        {
            for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
                {
                    dealer = PlayerData.My.MapRole[i];
                }
            }
        }

        if (seed != null && Peasant != null && dealer != null)
        {
            canseedhasTrad=  TradeManager.My.CheckTwoRoleHasTrade(seed.baseRoleData, Peasant.baseRoleData);
            candealerhasTrad=  TradeManager.My.CheckTwoRoleHasTrade(Peasant.baseRoleData, dealer.baseRoleData);

        }

        if (canseedhasTrad && candealerhasTrad)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}