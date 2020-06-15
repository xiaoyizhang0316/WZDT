using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcServiceInfo : MonoBehaviour
{
    public Text des;
    public Text timeInv;
    public Text cost;
    public Text risk;

    public void SetInfo(BaseMapRole npc, BaseSkill baseSkill)
    {
        des.text = baseSkill.skillDesc;
        //timeInv
        cost.text = npc.baseRoleData.baseRoleData.tradeCost.ToString();
        risk.text = npc.baseRoleData.baseRoleData.riskResistance.ToString();
    }
}
