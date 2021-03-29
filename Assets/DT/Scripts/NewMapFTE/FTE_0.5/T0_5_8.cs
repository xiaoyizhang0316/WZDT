using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class T0_5_8 : BaseGuideStep
{

  
    public BaseMapRole seed;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
       
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(2);
    }
 

    public override bool ChenkEnd()
    {
        if (seed == null)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType. Seed&&
                    !PlayerData.My.MapRole[i].isNpc)
                {
                    seed = PlayerData.My.MapRole[i];
                }
            }
            return false;

        }

        if (seed.tradeList.Count ==1)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }
        else
        {
            return false;
        }

      // if (dealer1.startTradeList.Count == 0 && dealer1.endTradeList.Count == 0&&
      //     dealer2.startTradeList.Count == 0 && dealer2.endTradeList.Count == 0
      //     )
      // {
      //     missiondatas.data[0].isFinish = true;
      //     return true;
      // }
      // 
      // return false;
    }
}

// if (missiondatas.data[0].isFinish && missiondatas.data[1].isFinish)
     // {
     //     return true;
     // }
     // else
     // {
     //     return false;
     // } 
