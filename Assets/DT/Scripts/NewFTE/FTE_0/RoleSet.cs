using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class RoleSet : MonoBehaviour
{
    public RoleType roleType = RoleType.Seed;
    
    public int childRole;
    public Transform roles;

    public Text prop1;
    public Text prop2;
    public Text cost;

    public Transform role;

    int effect = 0;
    int efficiency = 0;
    int range = 0;
    int Cost = 0;

    int tet = 0;
    int tey = 0;
    int trg = 0;

    Color color0 = Color.red;
    Color color1 = Color.yellow;
    Color color2 = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        role = roles.GetChild(childRole);
        effect = role.GetComponent<BaseMapRole>().baseRoleData.effect;
        efficiency = role.GetComponent<BaseMapRole>().baseRoleData.efficiency;
        Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
        range = role.GetComponent<BaseMapRole>().baseRoleData.range;
        ShowValues();
    }

    void ShowValues()
    {
        SwitchTextColor();
        switch (roleType)
        {
            case RoleType.Seed:
                prop1.text = "效<color=#00000000>效率</color>率: " + (efficiency/20f).ToString("f2")+"/s";
                prop2.text = "效<color=#00000000>效果</color>果: " + effect * 10;
                cost.text =  "固定成本: " + Cost;
                break;
            case RoleType.Peasant:
                prop1.text = "效<color=#00000000>效率</color>率: " + (efficiency / 20f).ToString("f2") + "/s";
                prop2.text = "效<color=#00000000>效果</color>果: " + effect +"%";
                cost.text = "固定成本: " + Cost;
                break;
            case RoleType.Merchant:
                prop1.text = "效<color=#00000000>效率</color>率: " + efficiency+"%";
                prop2.text = "效<color=#00000000>效果</color>果: " + (effect *0.3f + 24).ToString("f2")+"%";
                cost.text = "固定成本: " + Cost;
                break;
            case RoleType.Dealer:
                prop1.text = "效<color=#00000000>效率</color>率: " + (1.5f-efficiency * 0.01f  ).ToString("f2")+"s";
                prop2.text = "范<color=#00000000>范围</color>围: " + range;
                cost.text = "固定成本: " + Cost;
                break;
        }
        //RoleEditor.My.ShowTradeCost();
    }

    void SwitchTextColor()
    {
        if (roleType == RoleType.Dealer)
        {
            if (trg == 0)
            {
                prop2.color = color0;
            }
            else if (trg == 1)
            {
                prop2.color = color1;
            }
            else
            {
                prop2.color = color2;
            }
            if (tey == 0)
            {
                prop1.color = color0;
            }
            else if (tey == 1)
            {
                prop1.color = color1;
            }
            else
            {
                prop1.color = color2;
            }
        }
        else
        {
            if (tet == 0)
            {
                prop2.color = color0;
            }
            else if (tet == 1)
            {
                prop2.color = color1;
            }
            else
            {
                prop2.color = color2;
            }
            if (tey == 0)
            {
                prop1.color = color0;
            }
            else if (tey == 1)
            {
                prop1.color = color1;
            }
            else
            {
                prop1.color = color2;
            }
        }
    }

    public void RefreshValues(int lva, int tva, ValueType valueType)
    {
        if (!RoleEditor.My.isDragEnd)
        {
            if (RoleEditor.My.hand != null)
            {
                RoleEditor.My.hand.SetActive(false);
            }
            RoleEditor.My.isDragEnd = true;
        }
        RoleEditor.My.isDragEnd = true;
        switch (roleType)
        {
            case RoleType.Seed:
                SetSeed(lva, tva, valueType);
                break;
            case RoleType.Peasant:
                SetPeasant(lva, tva, valueType);
                break;
            case RoleType.Merchant:
                SetMerchant(lva, tva, valueType);
                break;
            case RoleType.Dealer:
                SetDealer(lva, tva, valueType);
                break;
        }
    }

    private void SetSeed(int lva, int tva, ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.Effect:
                ClearBullets();
                role.GetComponent<BaseSkill>().CancelSkill();
                role.GetComponent<BaseMapRole>().baseRoleData.effect += (RoleEditor.My.seed.effect[tva]- RoleEditor.My.seed.effect[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.seed.effect[tva]- RoleEditor.My.seed.effect[lva])* RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.seed.effect[tva]- RoleEditor.My.seed.effect[lva])* RoleEditor.My.costOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.seed.effect[tva]- RoleEditor.My.seed.effect[lva])* RoleEditor.My.riskOffset);
                effect = role.GetComponent<BaseMapRole>().baseRoleData.effect;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                StartCoroutine(ContinueProductSeed());
                tet = tva;
                break;
            case ValueType.Efficiency:
                role.GetComponent<BaseMapRole>().baseRoleData.efficiency += (RoleEditor.My.seed.efficiency[tva] - RoleEditor.My.seed.efficiency[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.seed.efficiency[tva] - RoleEditor.My.seed.efficiency[lva])* RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.seed.efficiency[tva] - RoleEditor.My.seed.efficiency[lva])* RoleEditor.My.riskOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.seed.efficiency[tva] - RoleEditor.My.seed.efficiency[lva])* RoleEditor.My.costOffset);
                efficiency = role.GetComponent<BaseMapRole>().baseRoleData.efficiency;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                tey = tva;
                break;
            default:
                break;
        }

        ShowValues();
    }

    private void SetPeasant(int lva, int tva, ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.Effect:
                role.GetComponent<BaseMapRole>().baseRoleData.effect += (RoleEditor.My.peasant.effect[tva] - RoleEditor.My.peasant.effect[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.peasant.effect[tva] - RoleEditor.My.peasant.effect[lva]) * RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.peasant.effect[tva] - RoleEditor.My.peasant.effect[lva]) * RoleEditor.My.costOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.peasant.effect[tva] - RoleEditor.My.peasant.effect[lva]) * RoleEditor.My.riskOffset);
                effect = role.GetComponent<BaseMapRole>().baseRoleData.effect;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                tet = tva;
                break;
            case ValueType.Efficiency:
                role.GetComponent<BaseMapRole>().baseRoleData.efficiency += (RoleEditor.My.peasant.efficiency[tva] - RoleEditor.My.peasant.efficiency[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.peasant.efficiency[tva] - RoleEditor.My.peasant.efficiency[lva]) * RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.peasant.efficiency[tva] - RoleEditor.My.peasant.efficiency[lva]) * RoleEditor.My.riskOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.peasant.efficiency[tva] - RoleEditor.My.peasant.efficiency[lva]) * RoleEditor.My.costOffset);
                efficiency = role.GetComponent<BaseMapRole>().baseRoleData.efficiency;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                tey = tva;
                break;
            default:
                break;
        }

        ShowValues();
    }

    private void SetMerchant(int lva, int tva, ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.Effect:
                role.GetComponent<BaseMapRole>().baseRoleData.effect += (RoleEditor.My.merchant.effect[tva] - RoleEditor.My.merchant.effect[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.merchant.effect[tva] - RoleEditor.My.merchant.effect[lva]) * RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.merchant.effect[tva] - RoleEditor.My.merchant.effect[lva]) * RoleEditor.My.costOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.merchant.effect[tva] - RoleEditor.My.merchant.effect[lva]) * RoleEditor.My.riskOffset);
                effect = role.GetComponent<BaseMapRole>().baseRoleData.effect;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                tet = tva;
                SetMerchantRange(tva);
                break;
            case ValueType.Efficiency:
                role.GetComponent<BaseMapRole>().baseRoleData.efficiency += (RoleEditor.My.merchant.efficiency[tva] - RoleEditor.My.merchant.efficiency[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.merchant.efficiency[tva] - RoleEditor.My.merchant.efficiency[lva]) * RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.merchant.efficiency[tva] - RoleEditor.My.merchant.efficiency[lva]) * RoleEditor.My.riskOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.merchant.efficiency[tva] - RoleEditor.My.merchant.efficiency[lva]) * RoleEditor.My.costOffset);
                efficiency = role.GetComponent<BaseMapRole>().baseRoleData.efficiency;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                tey = tva;
                break;
            default:
                break;
        }

        ShowValues();
    }

    void SetMerchantRange(int tval)
    {
        role.Find("BuffRange").localScale = new Vector3(RoleEditor.My.merchantBuffRange[tval], 1, RoleEditor.My.merchantBuffRange[tval]);
    }

    private void SetDealer(int lva, int tva, ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.Range:
                role.GetComponent<BaseMapRole>().baseRoleData.range += (RoleEditor.My.dealer.range[tva] - RoleEditor.My.dealer.range[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.dealer.range[tva] - RoleEditor.My.dealer.range[lva]) * RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.dealer.range[tva] - RoleEditor.My.dealer.range[lva]) * RoleEditor.My.costOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.dealer.range[tva] - RoleEditor.My.dealer.range[lva]) * RoleEditor.My.riskOffset);
                range = role.GetComponent<BaseMapRole>().baseRoleData.range;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                trg = tva;
                break;
            case ValueType.Efficiency:
                role.GetComponent<BaseMapRole>().baseRoleData.efficiency += (RoleEditor.My.dealer.efficiency[tva] - RoleEditor.My.dealer.efficiency[lva]);
                role.GetComponent<BaseMapRole>().baseRoleData.tradeCost += (int)((RoleEditor.My.dealer.efficiency[tva] - RoleEditor.My.dealer.efficiency[lva]) * RoleEditor.My.tradeCostOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.riskResistance += (int)((RoleEditor.My.dealer.efficiency[tva] - RoleEditor.My.dealer.efficiency[lva]) * RoleEditor.My.riskOffset);
                role.GetComponent<BaseMapRole>().baseRoleData.cost += (int)((RoleEditor.My.dealer.efficiency[tva] - RoleEditor.My.dealer.efficiency[lva]) * RoleEditor.My.costOffset);
                efficiency = role.GetComponent<BaseMapRole>().baseRoleData.efficiency;
                Cost = role.GetComponent<BaseMapRole>().baseRoleData.cost;
                tey = tva;
                break;
            default:
                break;
        }

        ShowValues();
    }

    private void ClearBullets()
    {
        for(int i=0; i < roles.childCount; i++)
        {
            roles.GetChild(i).GetComponent<BaseMapRole>().ClearWarehouse();
        }
    }

    IEnumerator ContinueProductSeed()
    {
        RoleEditor.My.destroyBullets = true;
        yield return new WaitForSeconds(0.5f);
        RoleEditor.My.destroyBullets = false;
        role.GetComponent<BaseSkill>().ReUnleashSkills();
    }
}


public enum ValueType
{
    Effect,
    Efficiency,
    Range
}