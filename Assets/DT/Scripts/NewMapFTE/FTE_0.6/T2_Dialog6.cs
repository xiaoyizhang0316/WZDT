using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog6 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T2_Manager.My.SetDeleteButton(true);
        T2_Manager.My.SetUpdateButton(true);
        T2_Manager.My.SetRoleMaxLevel(2);
    }
}
