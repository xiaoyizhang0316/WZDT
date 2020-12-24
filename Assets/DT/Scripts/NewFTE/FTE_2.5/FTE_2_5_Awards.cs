using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Awards : BaseGuideStep
{
    public GameObject awards;

    public override IEnumerator StepStart()
    {
        PlayerData.My.GetNewGear(99909);
        PlayerData.My.GetNewGear(22203);
        awards.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        awards.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
