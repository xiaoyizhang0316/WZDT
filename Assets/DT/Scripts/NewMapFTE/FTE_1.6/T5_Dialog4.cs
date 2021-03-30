using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5_Dialog4 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T5_Manager.My.DoMoveRoleUp(T5_Manager.My.peasant, 1.98f);
    }
}
