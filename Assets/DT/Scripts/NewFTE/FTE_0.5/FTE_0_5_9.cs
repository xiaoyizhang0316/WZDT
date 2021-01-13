using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FTE_0_5_9 : BaseGuideStep
{

    public GameObject nongminLock;

    public GameObject red;
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC1);
    //    FTE_0_5Manager.My.UpRole( FTE_0_5Manager.My.dealerJC2);
        red.SetActive(true);
        nongminLock.SetActive(false);
        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        red.SetActive(false);

        yield return new WaitForSeconds(2f);

    }


    public override bool ChenkEnd()
    {
        TradeManager.My.HideAllIcon();
        int count = 0;

       
   count = PlayerData.My.peasantCount;
            if (count >=GetComponent<UnlockRoleFTE>().peasant)
            {
                missiondatas.data[0].isFinish = true;
          
                return true;

            }
            

            return false;
        }
    } 