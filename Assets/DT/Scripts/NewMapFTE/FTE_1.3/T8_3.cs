using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T8_3 : BaseGuideStep
{
    public  GameObject buffPanel;
    // Start is called before the first frame update
  

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
        
        return buffPanel.activeSelf ;
    }
 
}
