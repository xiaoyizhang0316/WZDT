using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_5_Dialog3 : BaseGuideStep
{
    public GameObject openCG;
    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }

}
