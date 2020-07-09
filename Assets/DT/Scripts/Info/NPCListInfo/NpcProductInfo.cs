using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NpcProductInfo : MonoBehaviour
{
    public Text npcName;
    public Text des;
    public Text timeInv;
    public Text cost;
    public Text risk;

    public Text effect;
    public Text efficiency;

    public GameObject effectBar;
    public GameObject efficiencyBar;

    public Text prop1;
    public Text prop2;

    public Image icon;
    public List<Image> buffs;




    public void SetInfo(Transform npc, BaseSkill baseSkill)
    {
        npcName.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleName;
        des.text = baseSkill.skillDesc;
        //timeInv
        cost.text = npc.GetComponent<BaseMapRole>().baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.riskResistance.ToString();

        effect.text = npc.GetComponent<BaseMapRole>().baseRoleData.effect.ToString();
        efficiency.text = npc.GetComponent<BaseMapRole>().baseRoleData.efficiency.ToString();

        SetBar(npc.GetComponent<BaseMapRole>().baseRoleData.effect, npc.GetComponent<BaseMapRole>().baseRoleData.efficiency);
        prop1.text = (npc.GetComponent<BaseMapRole>().baseRoleData.efficiency / 10f).ToString("#.#") + "/s";
        prop2.text = (npc.GetComponent<BaseMapRole>().baseRoleData.effect).ToString() + "%";

        timeInv.text = (1.0f / npc.GetComponent<BaseMapRole>().baseRoleData.efficiency).ToString("#.##");
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

    void SetBar(int effct, int effcy)
    {
        efficiencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(effcy / 120f * 150f,
                efficiencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
        effectBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(effct / 120f * 150f,
                effectBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f);
    }
}
