using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T8_4 : BaseGuideStep
{
    public  Building port;
    // Start is called before the first frame update
  

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
       T8Manager.My.tsg.SetActive(true);
       T8Manager.My.consumerPort2.SetActive(true);    
       T8Manager.My.endPointPort2.SetActive(true);
         yield return new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    {  
        yield return new WaitForSeconds(1); 
        T8Manager.My.npcmaoyi.SetActive(true);
    }
                  
    public override bool ChenkEnd()
    {
        
        return port.isUseTSJ ;
    }
 
}
