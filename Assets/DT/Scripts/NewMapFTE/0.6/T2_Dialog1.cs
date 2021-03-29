using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog1 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T2_Manager.My.SetRoleMaxLevel();
        T2_Manager.My.SetDeleteButton(false);
        T2_Manager.My.SetClearWHButton(false);
        T2_Manager.My.SetUpdateButton(false);
        T2_Manager.My.SetRoleCost(0);
    }
}
