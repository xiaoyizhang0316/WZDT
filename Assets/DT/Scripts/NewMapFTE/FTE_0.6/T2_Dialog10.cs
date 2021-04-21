using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog10 : FTE_Dialog
{
    public BornType bornType;
    public override void BeforeDialog()
    {
        // delete 20210419
        /*T2_Manager.My.DeleteAllRole();
        T2_Manager.My.StopBornConsumer();
        T2_Manager.My.DeleteAllConsumer();
        T2_Manager.My.BornConsumer((int)bornType.type, bornType.count);*/
        
        T2_Manager.My.DoMoveRoleDown(T2_Manager.My.QualityMerchant);
        T2_Manager.My.DoMoveRoleUp(T2_Manager.My.dealer_NPC, 2.2f);
        T2_Manager.My.BornConsumer((int)bornType.type, bornType.count);
    }
}
