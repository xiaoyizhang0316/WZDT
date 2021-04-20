using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_2_1 : BaseGuideStep
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        RoleListManager.My.OutButton(true);
        yield return new WaitForSeconds(1);
    }

    public override IEnumerator StepEnd()
    { 
        yield return new WaitForSeconds(1);
    }

    public override bool ChenkEnd()
    {
        return PlayerData.My.dealerCount==1;
    }

    void Update()
    {
        
    }
}
