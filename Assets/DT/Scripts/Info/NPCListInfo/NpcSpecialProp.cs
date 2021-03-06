using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NpcSpecialProp : MonoBehaviour
{
    public Text effect;
    public Text effeciency;

    public GameObject effectBar;
    public GameObject effecencyBar;

    public Text prop1;
    public Text prop2;

    public void SetInfo(BaseMapRole npc)
    {
        effect.text = npc.baseRoleData.effect.ToString();
        effeciency.text = npc.baseRoleData.efficiency.ToString();

        switch (npc.baseRoleData.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                prop1.text = (npc.baseRoleData.efficiency / 20f).ToString("F2") + "/s";
                prop2.text = (npc.baseRoleData.effect * 10).ToString();
                break;
            case GameEnum.RoleType.Peasant:
                prop1.text= (npc.baseRoleData.efficiency / 20f).ToString("F2") + "/s";
                prop2.text = (npc.baseRoleData.effect).ToString() + "%";
                break;
            case GameEnum.RoleType.Merchant:
                prop1.text = npc.baseRoleData.efficiency.ToString() + "%";
                prop2.text = (npc.baseRoleData.effect * 0.3f + 24f).ToString() + "%";
                break;
            case GameEnum.RoleType.Dealer:
                prop1.text = (npc.baseRoleData.efficiency * -0.01f + 1.5f).ToString() + "s";
                prop2.text = (npc.baseRoleData.range).ToString();
                break;
        }
        SetBar(npc.baseRoleData.effect, npc.baseRoleData.efficiency);
    }

    void SetBar(int effct,int effcy)
    {
        effecencyBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(effct / 120f * 150f,
                effecencyBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play();
        effectBar.GetComponent<RectTransform>().DOSizeDelta(
            new Vector2(effcy / 120f * 150f,
                effectBar.GetComponent<RectTransform>().sizeDelta.y), 0.2f).Play() ;
    }
}
