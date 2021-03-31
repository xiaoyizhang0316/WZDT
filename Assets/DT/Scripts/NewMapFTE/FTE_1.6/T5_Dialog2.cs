using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Dialog2 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T5_Manager.My.DoMoveRoleUp(T5_Manager.My.softFactory, 1.98f);
        T5_Manager.My.DoMoveRoleUp(T5_Manager.My.QualityMerchant);
    }
}
