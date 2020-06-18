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
