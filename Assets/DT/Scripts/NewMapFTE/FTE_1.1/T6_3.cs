using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T6_3: BaseGuideStep
{
 
    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        T6Manager.My.npcnong.SetActive(true);
        T6Manager.My.npcnong.GetComponent<NPC>().isCanSee = true;
        yield return new WaitForSeconds(1); 

     
     
    }

    public override IEnumerator StepEnd()
    { 
        
        yield return new WaitForSeconds(2); 
      
    }

    public override bool ChenkEnd()
    {
        if (!T6Manager.My.npcnong.GetComponent<NPC>().isLock)
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
