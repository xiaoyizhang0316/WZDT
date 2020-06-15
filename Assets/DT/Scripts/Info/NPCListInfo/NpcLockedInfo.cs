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

    public Text unlockCost;

    public void SetInfo(Transform npc, int unlockNumber)
    {
        skillDes.text = npc.GetComponent<BaseSkill>().skillDesc;
        //timeInterval.text = 
        tradeCost.text = npc.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.tradeCost.ToString();
        risk.text = npc.GetComponent<BaseMapRole>().baseRoleData.riskResistance.ToString();

        unlockCost.text = unlockNumber.ToString();
    }
}
