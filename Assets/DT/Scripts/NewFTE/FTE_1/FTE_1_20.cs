using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_20 : BaseGuideStep
{
    public float waitTime;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isover;
 
    
    public override IEnumerator StepStart()
    {
        isover = false;
        
        NewCanvasUI.My.Panel_Update.gameObject.SetActive(false);
        
     
        yield return new WaitForSeconds(waitTime);
       isover = true;
    }

    public override IEnumerator StepEnd()
    {
  
        yield break;
    }


    public override bool ChenkEnd()
    {
        return isover;
    }
}
