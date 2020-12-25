using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_DialogADDDo : FTE_DialogDoBase
{
    public GameObject soft;
    public GameObject crisp;
    public GameObject sweet;

    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;
    public override void DoStart()
    {
        soft.GetComponent<BaseMapRole>().encourageLevel = -3;
        soft.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().encourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().encourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            if (!PlayerData.My.MapRole[i].isNpc)
            {
                PlayerData.My.DeleteRole(PlayerData.My.MapRole[i].baseRoleData.ID);
            }
        }
    }

    public override void DoEnd()
    {
        seed.SetActive(true);
        peasant.SetActive(true);
        merchant.SetActive(true);
    }
}
