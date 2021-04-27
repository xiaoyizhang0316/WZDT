using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T8_2 : BaseGuideStep
{
    // Start is called before the first frame update
  

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        //T8Manager.My.dlg.SetActive(true);
        T5_Manager.My.dlj.SetActive(true);
        yield return new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {  
        yield return new WaitForSeconds(1); 
    }
                  
    public override bool ChenkEnd()
    {
        
        //return T8Manager.My.npcnong.GetComponent<NPC>().isCanSeeEquip;
        return T5_Manager.My.peasant.GetComponent<NPC>().isCanSeeEquip;
    }
 
}
