using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog7 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        T2_Manager.My.ShowTradeButton(GetSeed());
    }
    
    BaseMapRole GetSeed()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                return PlayerData.My.MapRole[i];
            }
        }

        return null;
    }
}
