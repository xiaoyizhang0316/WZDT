using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_2_7 : BaseGuideStep
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
        return PlayerData.My.MapRole[0].baseRoleData.baseRoleData.level == 2;
    }
}
