using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog8 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T2_Manager.My.DoMoveRoleDown(T2_Manager.My.QualitySeed);
        T2_Manager.My.DoMoveRoleUp(T2_Manager.My.QualityMerchant);
    }
}
