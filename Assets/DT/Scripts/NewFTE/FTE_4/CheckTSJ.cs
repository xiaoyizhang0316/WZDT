using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTSJ : BaseGuideStep
{
    public Transform targetBuilding;

    public override IEnumerator StepEnd()
    {
        Building[] TEMP = FindObjectsOfType<Building>();
        for (int i = 0; i < TEMP.Length; i++)
        {
            TEMP[i].GetComponent<BoxCollider>().enabled = true;
        }
        highLight2DObjList[0].SetActive(true);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Building[] TEMP = FindObjectsOfType<Building>();
        for (int i = 0; i < TEMP.Length; i++)
        {
            TEMP[i].GetComponent<BoxCollider>().enabled = false;
        }
        targetBuilding.GetComponent<BoxCollider>().enabled = true;
        highLight2DObjList[0].SetActive(false);
        yield break;
    }

    public override bool ChenkEnd()
    {
        return targetBuilding.GetComponent<Building>().isUseTSJ;
    }
}
