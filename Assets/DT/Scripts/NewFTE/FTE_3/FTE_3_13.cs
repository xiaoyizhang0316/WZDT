using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_3_13: BaseGuideStep
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
        NewCanvasUI.My.Panel_NPC.SetActive(false);

        yield return new WaitForSeconds(1);
         
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(1);

        
    }

    public override bool ChenkEnd()
    {
        return ConsumableListManager.My.currentSign != null;
    }
}
