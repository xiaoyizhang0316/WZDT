using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_1 : BaseGuideStep
{
    
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
       
       Debug.Log("开始教学 "+currentStepIndex);
        yield break;
    }

    public override IEnumerator StepEnd()
    {

        Debug.Log("结束教学 "+currentStepIndex);
        yield break;
    }

  
}
