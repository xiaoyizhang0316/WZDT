using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_3_11 : BaseGuideStep
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
        NewCanvasUI.My.GamePause(); 

        yield return new WaitForSeconds(1);
         
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(1);

        
    }

    public override bool ChenkEnd()
    {
        return PlayerData.My.playerConsumables.Count > 1;
    }
}
