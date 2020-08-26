using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_22 : BaseGuideStep
{
    public MapSign a;
    public MapSign b;
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
        foreach (var VARIABLE in MapManager.My._mapSigns)
        {
            if (VARIABLE.mapType == GameEnum.MapType.Grass && VARIABLE.baseMapRole == null)
            {
                VARIABLE.isCanPlace = true;

            }

        }
        
        yield break;
    }

    public override bool ChenkEnd()
    {
        if (TradeManager.My.CheckTwoRoleHasTrade(a.baseMapRole.baseRoleData,b.baseMapRole.baseRoleData ))
        {
            return true;
        }
        else
        {    
            return false;
        }
    }
}
