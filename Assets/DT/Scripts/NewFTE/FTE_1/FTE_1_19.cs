using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_19 : BaseGuideStep
{

    public UpdateRole update;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
 

    public override IEnumerator StepStart()
    {
       
        yield return new WaitForSeconds(0.2f);     
    }

    public override IEnumerator StepEnd()
    {
         update.UpdateRole1(); 
        yield break;
    }

    
}
