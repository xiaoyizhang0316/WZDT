using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog2 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        // remove 20210419
        /*T2_Manager.My.StopBornConsumer();
        T2_Manager.My.DeleteAllConsumer();*/ 
        T2_Manager.My.DoMoveRoleUp(T2_Manager.My.QualitySeed, 3.2f);
    }
}
