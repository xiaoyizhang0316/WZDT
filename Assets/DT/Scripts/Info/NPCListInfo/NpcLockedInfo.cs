using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcLockedInfo : MonoBehaviour
{
    public Text skillDes;
    public Text timeInterval;
    public Text tradeCost;
    public Text risk;

    public Text npcName;

    public Text unlockCost;

    public void SetInfo(Role npc, int unlockNumber)
    {
        //skillDes.text = 
        //timeInterval.text =
        npcName.text = npc.baseRoleData.roleName;
        tradeCost.text = npc.baseRoleData.tradeCost.ToString();
        risk.text = npc.baseRoleData.riskResistance.ToString();

        unlockCost.text = unlockNumber.ToString();
    }
}
