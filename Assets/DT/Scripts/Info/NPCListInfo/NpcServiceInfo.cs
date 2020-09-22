using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcServiceInfo : MonoBehaviour
{
    public Text npcName;
    public Text des;
    public Text timeInv;
    public Text cost;
    public Text risk;

    public Image icon;
    public List<Image> buffs;
    public Text level;

    public void SetInfo(Transform npc, BaseSkill baseSkill)
    {
        npcName.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName;
        des.text = baseSkill.skillDesc;
        //timeInv
        cost.text = npc.GetComponent<BaseMapRole>().baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.riskResistance.ToString();
        icon.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType.ToString() +
            (npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level == 0?1: npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level));
        level.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.level.ToString();
        timeInv.text = (1.0f/ npc.GetComponent<BaseMapRole>().baseRoleData.efficiency).ToString("F2");
        int i = 0;
        foreach (var sp in buffs)
        {
            if (i < baseSkill.buffList.Count)
            {
                sp.sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseSkill.buffList[i]);
                sp.GetComponent<BuffText>().InitBuff(GameDataMgr.My.GetBuffDataByID(baseSkill.buffList[i]));
            }
            else
            {
                sp.sprite = Resources.Load<Sprite>("Sprite/Buff/999");
                sp.GetComponent<BuffText>().Reset();
            }
            i++;
        }

        if (npc.GetComponent<NPC>().isCanSeeEquip)
        {
            int a = 0;
            for (int j = baseSkill.buffList.Count; j < buffs.Count; j++)
            {
                if (a < npc.GetComponent<NPC>().NPCBuffList.Count)
                {
                    buffs[j].sprite = Resources.Load<Sprite>("Sprite/Buff/" + npc.GetComponent<NPC>().NPCBuffList[a]);
                    buffs[j].GetComponent<BuffText>().InitBuff(GameDataMgr.My.GetBuffDataByID(npc.GetComponent<NPC>().NPCBuffList[a]));
                }
                else
                {
                    buffs[j].sprite = NPCListInfo.My.buff;
                    buffs[j].GetComponent<BuffText>().Reset();
                }
                a++;
            }
        }
    }
}
