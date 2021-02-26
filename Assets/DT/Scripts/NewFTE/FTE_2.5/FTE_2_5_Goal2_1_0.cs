using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_5_Goal2_1_0 : BaseGuideStep
{
    public GameObject nongBox;
    public override IEnumerator StepStart()
    {
        //NewCanvasUI.My.GameNormal();
        NewCanvasUI.My.GamePause(false);
        //FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitPeasantCount = 3;
        /*FTE_2_5_Manager.My.taste1.SetActive(true);
        FTE_2_5_Manager.My.taste2.SetActive(true);
        FTE_2_5_Manager.My.taste3.SetActive(true);*/
        //SkipButton();
        PlayerData.My.ClearAllRoleWarehouse();
        TradeManager.My.ResetAllTrade();
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
        //SeedBuildRise();
    }
    
    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            if (NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().npcInfo.activeInHierarchy)
            {
                if (NPCListInfo.My.currentNpc.baseRoleData.ID.ToString().StartsWith("25011"))
                {
                    nongBox.SetActive(true);
                    if (nongBox.GetComponent<MouseOnThis>().isOn)
                    {
                        missiondatas.data[0].isFinish = true;
                        nongBox.SetActive(false);
                    }
                }
            }

            
        }
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        NewCanvasUI.My.GamePause(false);
        yield return new WaitForSeconds(2f);
    }
}
