using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowRoleText : RoleText,IPointerEnterHandler,IPointerExitHandler
{
  


    public Text text;
    public GameObject bg;
    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.SetActive(true);
        //todo
        if (CreatRoleManager.My.CurrentRole.baseRoleData.roleType == GameEnum.RoleType.Seed)
        {
            if (name.Equals("band"))
            {
              //  text.text = Seedband();
            }
            if (name.Equals("capacity"))
            {
               // text.text = Seedcapacity ();
            }
            if (name.Equals("efficiency"))
            {
               // text.text = Seedefficiency ();
            }
            if (name.Equals("quality"))
            {
               // text.text = SeedQulity  ();
            }
        }
        if (CreatRoleManager.My.CurrentRole.baseRoleData.roleType == GameEnum.RoleType.Peasant)
        {
            if (name.Equals("band"))
            {
               // text.text = PeasantBand();
            }
            if (name.Equals("capacity"))
            {
               // text.text = Peasantcapacity ();
            }
            if (name.Equals("efficiency"))
            {
               // text.text = Peasantefficiency ();
            }
            if (name.Equals("quality"))
            {
               // text.text = PeasantQulity  ();
            }
        }
        if (CreatRoleManager.My.CurrentRole.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            if (name.Equals("band"))
            {
                //text.text = MerchantBand();
            }
            if (name.Equals("capacity"))
            {
               // text.text = Merchantcapacity ();
            }
            if (name.Equals("efficiency"))
            {
              //  text.text =  Merchantefficiency ();
            }
            if (name.Equals("quality"))
            {
              //  text.text =  MerchantQulity  ();
            }
        }
        if (CreatRoleManager.My.CurrentRole.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            if (name.Equals("band"))
            {
                //text.text = DealerBand();
            }
            if (name.Equals("capacity"))
            {
               // text.text = Dealercapacity ();
            }
            if (name.Equals("efficiency"))
            {
              //  text.text = Dealerefficiency ();
            }
            if (name.Equals("quality"))
            {
              //  text.text = DealerQulity  ();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.SetActive(false);
    }

    public void Start()
    {
        bg.SetActive(false);
        
    }
}
