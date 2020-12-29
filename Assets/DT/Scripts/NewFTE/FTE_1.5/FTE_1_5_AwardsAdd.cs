using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_AwardsAdd : BaseGuideStep
{
    public GameObject awards;

    public override IEnumerator StepStart()
    {
        PlayerData.My.GetNewGear(90007);
        PlayerData.My.GetNewGear(90008);
        PlayerData.My.GetNewGear(90009);
        PlayerData.My.GetNewGear(90010);
        awards.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        awards.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
