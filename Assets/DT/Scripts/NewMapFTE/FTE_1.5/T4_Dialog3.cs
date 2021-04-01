using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Dialog3 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                PlayerData.My.DeleteRole(PlayerData.My.MapRole[i].baseRoleData.ID);
            }
        }
    }
}
