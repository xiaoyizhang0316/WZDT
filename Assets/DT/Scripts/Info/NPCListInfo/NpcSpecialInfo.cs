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

    public List<Image> buffs;

    public Text level;

    public void SetInfo(BaseMapRole npc, BaseSkill baseSkill)
    {
        npcName.text = npc.baseRoleData.baseRoleData.roleName;
        skillDes.text = baseSkill.skillDesc;
        efficiency.text = npc.baseRoleData.efficiency.ToString();
        effect.text = npc.baseRoleData.effect.ToString() ;
        cost.text = npc.baseRoleData.cost.ToString();
        risk.text = npc.baseRoleData.riskResistance.ToString();
        level.text = npc.baseRoleData.baseRoleData.level.ToString();
        HideAll();
        switch (npc.baseRoleData.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                seedProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.efficiency / 20f).ToString("#.#") + "/s";
                seedProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.effect*10).ToString();
                seedProp.SetActive(true);
                break;
            case GameEnum.RoleType.Peasant:
                peasantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.efficiency / 10f).ToString("#.#") + "/s";
                peasantProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.effect).ToString()+"%";
                peasantProp.SetActive(true);
                break;
            case GameEnum.RoleType.Merchant:
                merchantProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (((npc.baseRoleData.efficiency * 0.3f) / 100f) * npc.baseRoleData.tradeCost).ToString("#.#");
                merchantProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.effect).ToString() + "%";
                merchantProp.SetActive(true);
                break;
            case GameEnum.RoleType.Dealer:
                dealerProp.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.efficiency).ToString() + "%";
                dealerProp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = (npc.baseRoleData.range).ToString();

                dealerProp.SetActive(true);
                break;
        }

        if (npc.GetComponent<NPC>().isCanSeeEquip)
        {
            int i = 0;
            //foreach (var bf in baseSkill.buffList)
            //{
            //    buffs[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + bf);
            //    if (i == 2)
            //    {
            //        break;
            //    }
            //    i++;
            //}
            foreach(var sp in buffs)
            {
                if (i < baseSkill.buffList.Count)
                {
                    sp.sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseSkill.buffList[i]);
                    sp.GetComponent<BuffText>().InitBuff(GameDataMgr.My.GetBuffDataByID(baseSkill.buffList[i]));
                }
                else
                {
                    sp.sprite = NPCListInfo.My.buff;
                    sp.GetComponent<BuffText>().Reset();
                }
                i++;
            }
        }
        else
        {
            foreach(var sp in buffs)
            {
                sp.sprite = Resources.Load<Sprite>("Sprite/Buff/999");
                sp.GetComponent<BuffText>().Reset(); 
            }
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
