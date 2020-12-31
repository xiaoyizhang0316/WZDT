using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_AwardsAdd : BaseGuideStep
{
    public GameObject awards;

    public override IEnumerator StepStart()
    {
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        PlayerData.My.GetNewGear(90013);
        PlayerData.My.GetNewGear(90014);
        PlayerData.My.GetNewGear(90015);
        awards.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        awards.SetActive(false);
        //Debug.LogWarning("add eq end");
        yield return new WaitForSeconds(0.5f);
    }
}
