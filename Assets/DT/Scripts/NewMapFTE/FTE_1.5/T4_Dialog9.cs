using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Dialog9 : FTE_Dialog
{
    public BornType bornType;
    public override void BeforeDialog()
    {
        T4_Manager.My.BornConsumer((int) bornType.type, bornType.count);
        T4_Manager.My.DoMoveRoleDown(T4_Manager.My.QualityMerchant);
    }
}
