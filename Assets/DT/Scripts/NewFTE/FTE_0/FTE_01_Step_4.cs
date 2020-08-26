using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FTE_01_Step_4 : BaseGuideStep
{
    public GameObject land;
    public GameObject hand;

    public double roleID;


    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        //transform.GetChild(0).GetComponent
        Debug.Log("开始教学 " + currentStepIndex);
        land.GetComponent<MapSign>().isCanPlace = true;
        yield return new WaitForSeconds(0.8f);
        hand.SetActive(true);
        ShowInfos();
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }
    }

    

    public override bool ChenkEnd()
    {
        if (land.GetComponent<MapSign>().baseMapRole != null && land.GetComponent<MapSign>().baseMapRole.baseRoleData.inMap)
        {
            Destroy(transform.GetChild(0).gameObject);
            land.GetComponent<MapSign>().baseMapRole.baseRoleData.ID = roleID;
            if(roleID == 1001)
            {
                land.GetComponent<MapSign>().baseMapRole.baseRoleData.effect = 100;
            }else if(roleID == 1004)
            {
                land.GetComponent<MapSign>().baseMapRole.baseRoleData.range = 50;
            }
            land.GetComponent<MapSign>().baseMapRole.baseRoleData.efficiency = 50;
            return true;
        }
        else
        {
            return false;
        }
    }
}
