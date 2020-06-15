using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcProductInfo : MonoBehaviour
{
    public Text des;
    public Text timeInv;
    public Text cost;
    public Text risk;

    public Text npcName;

    public void SetInfo(Role npc)
    {
        //des
        //timeInv
        npcName.text = npc.baseRoleData.roleName;
        cost.text = npc.baseRoleData.tradeCost.ToString();
        risk.text = npc.baseRoleData.riskResistance.ToString();
    }
}
