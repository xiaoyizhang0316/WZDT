using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcProductInfo : MonoBehaviour
{
    public Text npcName;
    public Text des;
    public Text timeInv;
    public Text cost;
    public Text risk;

    public Image icon;
    public List<Image> buffs;




    public void SetInfo(Transform npc, BaseSkill baseSkill)
    {
        npcName.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName;
        des.text = baseSkill.skillDesc;
        //timeInv
        cost.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.riskResistance.ToString();
        string timeiva = "";
        switch (npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                timeiva = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.efficiency / 20f + "/s";
                break;
            case GameEnum.RoleType.Merchant:
                timeiva = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.efficiency + "%";
                break;
            case GameEnum.RoleType.Dealer:
                timeiva = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.efficiency + "%";
                break;
            case GameEnum.RoleType.Peasant:
                timeiva = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.efficiency / 10f + "/s";
                break;
        }
        timeInv.text = timeiva;
        if (npc.GetComponent<NPC>().isCanSeeEquip)
        {
            int i = 0;
            //foreach(var bf in baseSkill.buffList)
            //{
            //    buffs[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + bf);
            //    if (i == 2)
             //   {
            //        break;
             //   }
            //    i++;
            //}
            foreach (var sp in buffs)
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
            foreach (var sp in buffs)
            {
                sp.sprite = Resources.Load<Sprite>("Sprite/Buff/999");
                sp.GetComponent<BuffText>().Reset();
            }
        }
    }
}
