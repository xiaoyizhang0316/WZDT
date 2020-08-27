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
            //if(roleID == 1001)
            //{
            //    land.GetComponent<MapSign>().baseMapRole.baseRoleData.effect = 100;
            //}else if(roleID == 1004)
            //{
            //    land.GetComponent<MapSign>().baseMapRole.baseRoleData.range = 50;
            //}
            //land.GetComponent<MapSign>().baseMapRole.baseRoleData.efficiency = 50;
            SetRoleDatas();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetRoleDatas()
    {
        BaseMapRole role = land.GetComponent<MapSign>().baseMapRole;
        switch (role.baseRoleData.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                role.baseRoleData.effect = RoleEditor.My.seed.effect[0];
                role.baseRoleData.efficiency = RoleEditor.My.seed.efficiency[0];
                role.baseRoleData.tradeCost = RoleEditor.My.seed.tradeCost;
                role.baseRoleData.riskResistance = RoleEditor.My.seed.risk;
                role.baseRoleData.cost = RoleEditor.My.seed.cost;
                break;
            case GameEnum.RoleType.Peasant:
                role.baseRoleData.effect = RoleEditor.My.peasant.effect[0];
                role.baseRoleData.efficiency = RoleEditor.My.peasant.efficiency[0];
                role.baseRoleData.tradeCost = RoleEditor.My.peasant.tradeCost;
                role.baseRoleData.riskResistance = RoleEditor.My.peasant.risk;
                role.baseRoleData.cost = RoleEditor.My.peasant.cost;
                break;
            case GameEnum.RoleType.Merchant:
                role.baseRoleData.effect = RoleEditor.My.merchant.effect[0];
                role.baseRoleData.efficiency = RoleEditor.My.merchant.efficiency[0];
                role.baseRoleData.tradeCost = RoleEditor.My.merchant.tradeCost;
                role.baseRoleData.riskResistance = RoleEditor.My.merchant.risk;
                role.baseRoleData.cost = RoleEditor.My.merchant.cost;
                role.transform.Find("BuffRange").localScale = new Vector3(RoleEditor.My.merchantBuffRange[0], 1, RoleEditor.My.merchantBuffRange[0]);
                break;
            case GameEnum.RoleType.Dealer:
                role.baseRoleData.range = RoleEditor.My.dealer.range[0];
                role.baseRoleData.efficiency = RoleEditor.My.dealer.efficiency[0];
                role.baseRoleData.tradeCost = RoleEditor.My.dealer.tradeCost;
                role.baseRoleData.riskResistance = RoleEditor.My.dealer.risk;
                role.baseRoleData.cost = RoleEditor.My.dealer.cost;
                break;
        }
    }
}
