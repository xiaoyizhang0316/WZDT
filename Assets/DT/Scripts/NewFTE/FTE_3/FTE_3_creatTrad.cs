using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_3_creatTrad : BaseGuideStep
{
    public BaseMapRole gas;

    public BaseMapRole dealer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        yield return null;
    }

    public override bool ChenkEnd()
    {
      return   TradeManager.My.CheckTwoRoleHasTrade(gas.baseRoleData, dealer.baseRoleData);
    }

    public override IEnumerator StepEnd()
    {
        yield return null;
       
    }

    void Update()
    {
        
    }
}
