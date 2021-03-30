using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Dialog2 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T4_Manager.My.StopBornConsumer();
        T4_Manager.My.DeleteAllConsumer();
        T4_Manager.My.DoMoveRoleUp(T4_Manager.My.QualityMerchant);
    }
    
    
}
