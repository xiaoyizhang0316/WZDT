using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_DialogADDDo : FTE_DialogDoBase
{
    public GameObject soft;
    public GameObject crisp;
    public GameObject sweet;
    public GameObject softPlace;
    public GameObject crispPlace;
    public GameObject sweetPlace;

    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;

    public GameObject place1;
    public GameObject place2;
    public GameObject place3;

    public Transform roles;
    public override void DoStart()
    {
        soft.GetComponent<BaseMapRole>().encourageLevel = -3;
        soft.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().encourageLevel = -3;
        crisp.GetComponent<BaseMapRole>().startEncourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().encourageLevel = -3;
        sweet.GetComponent<BaseMapRole>().startEncourageLevel = -3;

        sweet.transform.DOMoveY(-8, 1f).OnComplete(() =>
        {
            sweet.SetActive(false);
        });
        sweetPlace.transform.DOMoveY(-8.32f, 1f);
        
        soft.transform.DOMoveY(-8, 1f).OnComplete(() =>
        {
            soft.SetActive(false);
        });
        softPlace.transform.DOMoveY(-8.32f, 1f);
        
        crisp.transform.DOMoveY(-8, 1f).OnComplete(() =>
        {
            crisp.SetActive(false);
        });
        crispPlace.transform.DOMoveY(-8.32f, 1f);
        
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

        seed.transform.DOMoveY(0.32f, 1f).Play();
        place1.transform.DOMoveY(0f, 1f).Play();
        peasant.transform.DOMoveY(0.32f, 1f).Play();
        place2.transform.DOMoveY(0f, 1f).Play();
        merchant.transform.DOMoveY(0.32f, 1f).Play();
        place3.transform.DOMoveY(0f, 1f).Play();
    }
}
