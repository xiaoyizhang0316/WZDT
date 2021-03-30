using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog2 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T2_Manager.My.StopBornConsumer();
        T2_Manager.My.DeleteAllConsumer();
    }
}
