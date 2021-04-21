using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog7 : FTE_Dialog
{
    public override void BeforeDialog()
    {
        // delete 20210419
        /*T2_Manager.My.ShowTradeButton(GetSeed());*/
        
        // TODO 交易按钮显示
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
