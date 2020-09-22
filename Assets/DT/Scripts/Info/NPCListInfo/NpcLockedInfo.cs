using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcLockedInfo : MonoBehaviour
{
    public Text npcName;
    public Text skillDes;
    public Text timeInterval;
    public Text tradeCost;
    public Text risk;

    public List<Image> buffs;

    public Text unlockCost;

    public Image skillTypeImg;

    public List<Sprite> spriteList;

    public void SetInfo(Transform npc, int unlockNumber)
    {
        BaseSkill baseSkill = npc.GetComponent<BaseSkill>();
        npcName.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName;
        skillDes.text = npc.GetComponent<BaseSkill>().skillDesc;
        
        tradeCost.text = npc.GetComponent<BaseMapRole>().baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.riskResistance.ToString();

        unlockCost.text = unlockNumber.ToString();

        timeInterval.text = (1.0f / npc.GetComponent<BaseMapRole>().baseRoleData.efficiency).ToString("F2");

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
        if(npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product)
        {
            skillTypeImg.sprite = spriteList[0];
        }
        else
        {
            skillTypeImg.sprite = spriteList[1];
        }

    }
}
