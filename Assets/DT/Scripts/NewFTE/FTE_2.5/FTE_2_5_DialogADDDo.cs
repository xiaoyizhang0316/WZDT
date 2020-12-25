using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_DialogADDDo : FTE_DialogDoBase
{
    public GameObject soft;
    public GameObject crisp;
    public GameObject sweet;
    public override void DoStart()
    {
        soft.GetComponent<BaseMapRole>().encourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().encourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().encourageLevel = -3;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].GetComponent<BaseMapRole>().isNpc)
            {
                PlayerData.My.DeleteRole(PlayerData.My.MapRole[i].GetComponent<BaseMapRole>().baseRoleData.ID);
            }
        }
    }

    public override void DoEnd()
    {
        
    }
}
