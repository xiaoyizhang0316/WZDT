using System.Collections;
using System.Collections.Generic; 
using DG.Tweening;
using UnityEngine;
 
using UnityEngine.Assertions.Must;


public class FTE_1_4_2 : BaseGuideStep
{
    public GameObject inBorder;
    public GameObject outBorder;

    public override IEnumerator StepStart()
    {
        yield return new WaitForSeconds(0.5f);

    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }
}