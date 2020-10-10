using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_01_Step_12 : BaseGuideStep
{

    bool isEnd = false;
    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
        //NewCanvasUI.My.GamePause(false);
        //RoleEditor.My.HideAllRoleSet();
    }

    public override IEnumerator StepStart()
    {
        RoleListManager.My.InButton();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(OwnStep());
    }

    IEnumerator OwnStep()
    {
        TradeManager.My.AutoCreateTrade("1001", "1002");
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("1002", "1003");
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("1003", "1004");
        yield return new WaitForSeconds(1);
        
        RoleEditor.My.ShowAllRoleSet();
        isEnd = true;
    }

    public override bool ChenkEnd()
    {
        
        return isEnd;
    }
}
