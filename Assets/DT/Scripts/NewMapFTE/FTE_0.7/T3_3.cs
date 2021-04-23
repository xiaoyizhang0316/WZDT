using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3_3 : BaseGuideStep
{
    public BaseMapRole role;
 
    public int targetdamege; 

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        FTE_0_6Manager.My.UpRole( FTE_0_6Manager.My.dealerJC1);
        FTE_0_6Manager.My.SetRoleInfoAddEquip(true);
        RoleListManager.My.OutButton();
        NewCanvasUI.My.GameNormal();
        role.warehouse.Clear();
        role.OnMoved += ChangeColor;

        yield return null;
    }

    public override IEnumerator StepEnd()
    {
        FTE_0_6Manager.My.SetClearWHButton(true);
        yield return new WaitForSeconds(2);
    }

    public void ChangeColor(ProductData data)
    {
        if (data.damage >= targetdamege)
        {
            FTE_0_6Manager.My.ChangeColor(FTE_0_6Manager.My.dealerJC1_ran, FTE_0_6Manager.My.sg);
        }

        else
        {
            FTE_0_6Manager.My.ChangeColor(FTE_0_6Manager.My.dealerJC1_ran, FTE_0_6Manager.My.sr);
        }
    }



    private int daojishi = 0;

    public override bool ChenkEnd()
    {
  
        StageGoal.My.maxRoleLevel = 3;

        TradeManager.My.ShowAllIcon();
        for (int i = 0; i < role.warehouse.Count; i++)
        {
            if (role.warehouse[i].damage < targetdamege)
            {
                role.warehouse.Remove(role.warehouse[i]);
            }
        }

        missiondatas.data[0].currentNum = role.warehouse.Count;
        if (role.warehouse.Count >= missiondatas.data[0].maxNum)
        {
            missiondatas.data[0].isFinish = true;
            return true;
        }

        return false;
    }
}
