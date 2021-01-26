using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_23 : BaseGuideStep
{

    public GameObject panel;
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
     
        PlayAnim();
        yield return new WaitForSeconds(0.2f); 
    }

    public override IEnumerator StepEnd()
    {
     
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (panel.activeSelf)
        {
            Debug.Log("当前");
            return true;
        }

        else
        {
            return false;
        }
    }
}
