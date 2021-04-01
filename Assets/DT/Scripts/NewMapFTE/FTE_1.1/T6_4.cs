using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T6_4: BaseGuideStep
{
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        T6Manager.My.npcDealer.SetActive(true); 
        T6Manager.My.gjj.SetActive(true); 
        yield return new WaitForSeconds(1); 

     
     
    }

    public override IEnumerator StepEnd()
    { 
        
        yield return new WaitForSeconds(2); 
      
    }

    public override bool ChenkEnd()
    {
        if ( T6Manager.My.npcDealer.GetComponent<NPC>().isCanSee)
        {
            missiondatas.data[0].isFinish= true; 

            return true;
        }
        else
        {
            return false;
        }
    }

}
