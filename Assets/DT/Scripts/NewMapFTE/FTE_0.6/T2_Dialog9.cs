using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog9 : FTE_Dialog
{
    //public BornType bornType;
    public override void BeforeDialog()
    {
        // delete 20210419
        /*T2_Manager.My.DoMoveRoleDown(T2_Manager.My.QualityMerchant);
        // TODO
        T2_Manager.My.BornConsumer((int)bornType.type, bornType.count);
        
        
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                PlayerData.My.MapRole[i].ClearWarehouse();
            }
        }*/
        
        T2_Manager.My.DoMoveRoleDown(T2_Manager.My.QualitySeed);
        T2_Manager.My.DoMoveRoleUp(T2_Manager.My.QualityMerchant, 2.4f);
    }
}
