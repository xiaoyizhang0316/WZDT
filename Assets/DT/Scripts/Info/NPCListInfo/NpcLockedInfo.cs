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

    public void SetInfo(Transform npc, int unlockNumber)
    {
        npcName.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName;
        skillDes.text = npc.GetComponent<BaseSkill>().skillDesc;
        timeInterval.text = "";
        tradeCost.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.riskResistance.ToString();

        unlockCost.text = unlockNumber.ToString();

        if (npc.GetComponent<NPC>().isCanSeeEquip)
        {
            int i = 0;
            //foreach (var bf in npc.GetComponent<BaseSkill>().buffList)
            //{
             //   buffs[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + bf);
            //    if (i == 2)
             //   {
             //       break;
             //   }
             //   i++;
            //}

            foreach (var sp in buffs)
            {
                if (i < npc.GetComponent<BaseSkill>().buffList.Count)
                {
                    sp.sprite = Resources.Load<Sprite>("Sprite/Buff/" + npc.GetComponent<BaseSkill>().buffList[i]);
                }
                else
                {
                    sp.sprite = NPCListInfo.My.buff;
                }
                i++;
            }
        }


    }
}
