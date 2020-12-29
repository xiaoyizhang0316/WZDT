using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Awards : BaseGuideStep
{
    public GameObject awards;

    public override IEnumerator StepStart()
    {
        PlayerData.My.GetNewGear(90011);
        PlayerData.My.GetNewGear(90012);
        awards.SetActive(true);
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitDealerCount = -1;
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        awards.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
