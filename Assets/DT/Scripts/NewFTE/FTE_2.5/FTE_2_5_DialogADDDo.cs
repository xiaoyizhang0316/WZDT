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

    public Transform roles;
    public override void DoStart()
    {
        soft.GetComponent<BaseMapRole>().encourageLevel = -3;
        soft.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().encourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().encourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        foreach (Transform role in roles)
        {
            if (!role.GetComponent<BaseMapRole>().isNpc && role.gameObject.activeInHierarchy)
            {
                PlayerData.My.DeleteRole(role.GetComponent<BaseMapRole>().baseRoleData.ID);
            }
            else
            {
                role.GetComponent<BaseMapRole>().ClearWarehouse();
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
