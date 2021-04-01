using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Dialog6 : FTE_Dialog
{
    public BornType bornTypePoint1;
    public BornType bornTypePoint2;
    public override void BeforeDialog()
    {
        T5_Manager.My.DoMoveRoleUp(T5_Manager.My.sweetFactory, 1.98f);
        T5_Manager.My.DoMoveRoleDown(T5_Manager.My.QualityMerchant);
        T5_Manager.My.BornConsumer(T5_Manager.My.bornPoint1, (int) bornTypePoint1.type,bornTypePoint1.count);
        T5_Manager.My.BornConsumer(T5_Manager.My.bornPoint2, (int) bornTypePoint2.type,bornTypePoint2.count);
    }
}
