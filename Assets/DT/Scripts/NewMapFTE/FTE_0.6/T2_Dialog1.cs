using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2_Dialog1 : FTE_Dialog
{
    public BornType bornType;
    public override void BeforeDialog()
    {
        RoleSet();
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        T2_Manager.My.SetRoleMaxLevel();
        T2_Manager.My.SetDeleteButton(true);
        T2_Manager.My.SetClearWHButton(false);
        T2_Manager.My.SetUpdateButton(false);
        T2_Manager.My.SetEquipButton(false);
        T2_Manager.My.SetRoleCost(0);
        T2_Manager.My.BornConsumer((int) bornType.type, bornType.count);
    }
    
    void RoleSet()
    {
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).upgradeCost =0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).upgradeCost =0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).upgradeCost = 0;
        //dealer_sign.GetComponent<CreatRole_Button>().ReadCostTech();
    }
}
