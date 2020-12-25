using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5_1 : BaseGuideStep
{
    public GameObject dailog;


 
 

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.Panel_Update.SetActive(false);
        dailog.SetActive(true);
 
        yield return new WaitForSeconds(1f);
      
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(1f);

    }

 
    public override bool ChenkEnd()
    {

        if (dailog.activeSelf)
        {
            return false;
        }

        else{
            return true;

        }

    }

 
}
