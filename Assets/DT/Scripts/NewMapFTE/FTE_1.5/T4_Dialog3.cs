using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Dialog3 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        foreach (BaseMapRole role in PlayerData.My.MapRole)
        {
            if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                Destroy(role.gameObject);
            }
        }
    }
}
