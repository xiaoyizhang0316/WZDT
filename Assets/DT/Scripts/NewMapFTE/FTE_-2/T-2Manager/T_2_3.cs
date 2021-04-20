using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_2_3 : BaseGuideStep
{

    public BaseMapRole wareHouse;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        return TradeManager.My.CheckTwoRoleHasTrade(wareHouse.baseRoleData ,PlayerData.My.MapRole[0].baseRoleData);
    }
}
