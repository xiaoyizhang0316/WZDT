using System.Collections;
using System.Collections.Generic; 
using DG.Tweening;
using UnityEngine;
<<<<<<< HEAD:Assets/DT/Scripts/NewFTE/FTE_1/FTE_1_4.cs
using UnityEngine.Assertions.Must;
 
public class FTE_1_4 : BaseGuideStep
{  
=======

public class FTE_1_4_1 : BaseGuideStep
{
    public GameObject inBorder;
    public GameObject outBorder;
>>>>>>> origin/hwj:Assets/DT/Scripts/NewFTE/FTE_1/FTE_1_4_1.cs
    public override IEnumerator StepStart()
    { 
        yield return new WaitForSeconds(0.5f);

    }
    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }
  
} 
