using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_7 : BaseGuideStep
{
    public MapSign a;
    public MapSign b;
    public bool addFT = false;
    public TradeConstraint.TradeConstraintItem item;
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
        if (addFT)
        {
            TradeConstraint.My.forbiddenTrade.Add(item);
        }
        NewCanvasUI.My.ShowAllTradeButton();
        yield return new WaitForSeconds(0.2f); 
    }

    public override IEnumerator StepEnd()
    {
        if (addFT)
        {
            TradeConstraint.My.forbiddenTrade.Remove(item);
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
