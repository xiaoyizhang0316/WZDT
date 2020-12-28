using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Awards : BaseGuideStep
{
    public GameObject awards;

    public override IEnumerator StepStart()
    {
        //PlayerData.My.GetNewGear(99909);
        PlayerData.My.GetNewGear(22203);
        awards.SetActive(true);
        //Debug.LogWarning("add eq start");
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = -1;
        //Debug.LogWarning("add eq starting");
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        awards.SetActive(false);
        //Debug.LogWarning("add eq end");
        yield return new WaitForSeconds(0.5f);
    }
}
