using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcSpecialInfo : MonoBehaviour
{
    public GameObject seedProp;
    public GameObject peasantProp;
    public GameObject merchantProp;
    public GameObject dealerProp;

    public Text npcName;
    public Text skillDes;
    public Text efficiency;
    public Text effect;
    public Text cost;
    public Text risk;

    public void SetInfo(Role npc)
    {
        npcName.text = npc.baseRoleData.roleName;
        //skillDes.text = 
        efficiency.text = npc.efficiency.ToString();
        effect.text = npc.effect.ToString() ;
        cost.text = npc.cost.ToString();
        risk.text = npc.riskResistance.ToString();

        switch (npc.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                seedProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.efficiency / 20f).ToString() + " /s";
                seedProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.effect*10).ToString();
                seedProp.SetActive(true);
                break;
            case GameEnum.RoleType.Peasant:
                peasantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.efficiency / 10f).ToString() + " /s";
                peasantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.effect).ToString()+"%";
                peasantProp.SetActive(true);
                break;
            case GameEnum.RoleType.Merchant:
                merchantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (((npc.efficiency * 0.3f) / 100f) * npc.tradeCost).ToString() + " /s";
                merchantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.effect).ToString() + "%";
                merchantProp.SetActive(true);
                break;
            case GameEnum.RoleType.Dealer:
                dealerProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.efficiency).ToString() + "%";
                dealerProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.range).ToString();

                dealerProp.SetActive(true);
                break;
        }
    }

    void HideAll()
    {
        seedProp.SetActive(false);
        peasantProp.SetActive(false);
        merchantProp.SetActive(false);
        dealerProp.SetActive(false);
    }
}
