using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_2_8 : BaseGuideStep
{

   
 

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
       
        yield return new WaitForSeconds(1);

    }

    public override IEnumerator StepEnd()
    {
  
      yield return new WaitForSeconds(1); 
    }

    public override bool ChenkEnd()
    {
        return WinManager.My.gameObject.activeSelf;
    }
}
