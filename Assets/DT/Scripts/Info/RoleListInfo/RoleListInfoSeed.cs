﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfoSeed : BaseRoleListInfo
{
    public Text effect;
    public Text Damage;

    public Text efficiency;

    public Text productTime;

    public Text tradCost;
    public Text risk;
    public Text montyCost;

    public Text technology;

    public GameObject efficiencyBar;
    public GameObject effectyBar; 
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
        effect.text = role.effect.ToString();
        Damage.text = ( role.effect* 10f).ToString();
        efficiency.text = role.efficiency.ToString();
        productTime.text = (role.efficiency / 10f).ToString()+" /s";
        tradCost.text  =  role.tradeCost.ToString();
        risk .text = role.riskResistance.ToString();
        montyCost.text =  role.cost.ToString();
        technology.text = role.techAdd.ToString();
        UpdateBar(role);
    }

    public void UpdateBar(Role role)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(role.efficiency/ 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        effectyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(role.effect / 120f * 150f,
                effectyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
    }
}