using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_01_Step_12 : BaseGuideStep
{
    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
        NewCanvasUI.My.GamePause(false);
        RoleEditor.My.HideAllRoleSet();
    }

    public override IEnumerator StepStart()
    {
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
    }

    //public override bool ChenkEnd()
    //{
    //    if(StageGoal.My.killNumber >= 3)
    //    {
    //        NewCanvasUI.My.GamePause(false);
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
