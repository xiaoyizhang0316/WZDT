using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Awards : BaseGuideStep
{
    public GameObject awards;

    public override IEnumerator StepStart()
    {
        PlayerData.My.GetNewWorker(90001);
        PlayerData.My.GetNewWorker(90002);
        PlayerData.My.GetNewWorker(90003);
        awards.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        awards.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
