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

    public void SetInfo(Role npc, int unlockNumber)
    {
        //skillDes.text = 
        //timeInterval.text = 
        tradeCost.text = npc.tradeCost.ToString();
        risk.text = npc.riskResistance.ToString();

        unlockCost.text = unlockNumber.ToString();
    }
}
