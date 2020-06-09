﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoPeasant : BaseRoleListInfo
{
    public Text effect;
    public Text reload;

    public Text efficiency;

    public Text productTime;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;
    public GameObject effectyBar; 
    
    public Transform productTF;

    public bool isShowProduct;

    public GameObject productPrb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(Role role)
    {
        effect.text =role.effect.ToString();
        reload.text = role.effect  .ToString()+"%";
        efficiency.text = role.efficiency.ToString();
        productTime.text = (role.efficiency / 10f).ToString()+" /s";
        tradCost.text  =  role.tradeCost.ToString();
        risk .text = role.riskResistance.ToString();
        montyCost.text =   role.cost.ToString();
        technology.text =  role.techAdd.ToString();
        UpdateBar(role);
        if (isShowProduct)
        {
            ShowLastpruduct(role);
        }
    }
    public void UpdateBar(Role role)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2( role.efficiency/ 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2( role.effect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
    }
    
    public void ShowLastpruduct(Role role)
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetBaseMapRoleByName(role.baseRoleData.roleName);
        for (int i = 0; i <productTF.childCount; i++)
        {
            Destroy(productTF.GetChild(i).gameObject);
        }

        int count = 9;
        if (baseMapRole.GetComponent<PruductMelon>().productDatas.Count <9)
        {
            count = baseMapRole.GetComponent<PruductMelon>().productDatas.Count ;
        }
     

        for (int i = 1; i <=count; i++)
        { 
            Debug.Log(i+"||"+ baseMapRole.GetComponent<PruductMelon>().productDatas.Count);
            GameObject Pruductgame =  Instantiate(productPrb, productTF);
            Pruductgame.GetComponent<ProductSign>().currentProduct =
                baseMapRole.GetComponent<PruductMelon>().productDatas[    baseMapRole.GetComponent<PruductMelon>().productDatas.Count-i];
            Pruductgame.GetComponent<Image>().sprite = RoleUpdateInfo.My.normallpp;
        }
    }
}
