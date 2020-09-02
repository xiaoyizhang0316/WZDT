using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;
using System;

public class RoleEditor : MonoSingleton<RoleEditor>
{
    public RoleArgs seed;
    public RoleArgs peasant;
    public RoleArgs merchant;
    public RoleArgs dealer;

    public int[] merchantBuffRange;
    public float tradeCostOffset;
    public float riskOffset;
    public float costOffset;

    public Transform roles;
    public Transform sets;
    public Text tc1;
    public Text tc2;
    public Text tc3;
    public bool destroyBullets = false;

    public bool isTrade = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrade)
            ShowTradeCost();
    }

    public void ShowAllRoleSet()
    {
        for(int i =0; i< sets.childCount; i++)
        {
            sets.GetChild(i).gameObject.SetActive(true);
        }
        isTrade = true;
    }

    public void HideAllRoleSet()
    {
        for (int i = 0; i < sets.childCount; i++)
        {
            sets.GetChild(i).gameObject.SetActive(false);
        }
        isTrade = false;
    }

    public void ShowTradeCost()
    {
        tc1.text = GetTradeCost(0).ToString();
        tc2.text = GetTradeCost(1).ToString();
        tc3.text = GetTradeCost(2).ToString();
    }

    public int  GetTradeCost(int index)
    {
        return (int)((roles.GetChild(index).GetComponent<BaseMapRole>().baseRoleData.tradeCost + roles.GetChild(index).GetComponent<BaseMapRole>().baseRoleData.riskResistance +
            roles.GetChild(index + 1).GetComponent<BaseMapRole>().baseRoleData.tradeCost + roles.GetChild(index + 1).GetComponent<BaseMapRole>().baseRoleData.riskResistance) * 0.3);
    }
}

[Serializable]
public struct RoleArgs
{
    public int[] effect;
    public int[] efficiency;
    public int[] range;
    public int tradeCost;
    public int risk;
    public int cost;
}