using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog9 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T2_Manager.My.DoMoveRoleDown(T2_Manager.My.QualityMerchant);
        // TODO
        T2_Manager.My.BornConsumer(0);
    }
}
