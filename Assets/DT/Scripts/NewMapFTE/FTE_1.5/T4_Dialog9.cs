using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Dialog9 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T4_Manager.My.BornConsumer(0);
    }
}
