using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T7_Dialog5 : FTE_Dialog
{
    public GameObject gjj;
    public BornType bornTypePoint1;
    public BornType bornTypePoint2;
    public override void BeforeDialog()
    {
        T7_Manager.My.DoMoveRoleDown(T7_Manager.My.QualityMerchant);
        T7_Manager.My.BornConsumer(T7_Manager.My.bornPoint1, (int) bornTypePoint1.type,bornTypePoint1.count);
        T7_Manager.My.BornConsumer(T7_Manager.My.bornPoint2, (int) bornTypePoint2.type,bornTypePoint2.count);
        gjj.SetActive(true);
    }
}
