using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_5_Goal1_1 : BaseGuideStep
{
    public GameObject softBox;
    public GameObject crispBox;
    public GameObject sweetBox;
    public override IEnumerator StepStart()
    {
        //NewCanvasUI.My.GameNormal();
        NewCanvasUI.My.GamePause(false);
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitPeasantCount = 3;
        /*FTE_2_5_Manager.My.taste1.SetActive(true);
        FTE_2_5_Manager.My.taste2.SetActive(true);
        FTE_2_5_Manager.My.taste3.SetActive(true);*/
        //SkipButton();
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
        yield return new WaitForSeconds(0.5f);
        //SeedBuildRise();
    }
    
    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            if (NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().npcInfo.activeInHierarchy)
            {
                if (NPCListInfo.My.currentNpc.baseRoleData.ID.ToString().StartsWith("25004"))
                {
                    sweetBox.SetActive(true);
                    if (sweetBox.GetComponent<MouseOnThis>().isOn)
                    {
                        missiondatas.data[0].isFinish = true;
                        sweetBox.SetActive(false);
                    }
                }
            }

            
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            if (NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().npcInfo.activeInHierarchy)
            {
                if (NPCListInfo.My.currentNpc.baseRoleData.ID.ToString().StartsWith("25005"))
                {
                    crispBox.SetActive(true);
                    if (crispBox.GetComponent<MouseOnThis>().isOn)
                    {
                        missiondatas.data[1].isFinish = true;
                        crispBox.SetActive(false);
                    }
                }
            }

            
        }
        
        if (missiondatas.data[2].isFinish == false)
        {
            if (NewCanvasUI.My.Panel_NPC.GetComponent<NPCListInfo>().npcInfo.activeInHierarchy)
            {
                if (NPCListInfo.My.currentNpc.baseRoleData.ID.ToString().StartsWith("25006"))
                {
                    softBox.SetActive(true);
                    if (softBox.GetComponent<MouseOnThis>().isOn)
                    {
                        missiondatas.data[2].isFinish = true;
                        softBox.SetActive(false);
                    }
                }
            }

            
        }
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2f);
        NewCanvasUI.My.GamePause(false);
    }
}
