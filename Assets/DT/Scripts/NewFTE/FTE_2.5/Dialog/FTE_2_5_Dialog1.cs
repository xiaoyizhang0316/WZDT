using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_2_5_Dialog1 : BaseGuideStep
{
    public GameObject openCG;


    public override IEnumerator StepStart()
    {
        openCG.SetActive(true);
        RoleSet();
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return !openCG.activeInHierarchy;
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void RoleSet()
    {
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).effect = 15;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).efficiency = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).tradeCost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).riskResistance = 420;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).effect = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).efficiency = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).tradeCost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).riskResistance = 540;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).effect = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).efficiency = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).tradeCost = 1200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).riskResistance = 660;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).cost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).upgradeCost = 3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).effect = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).efficiency = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).tradeCost = 1400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).riskResistance = 780;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).cost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).upgradeCost = 4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).effect = 45;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).efficiency = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).tradeCost = 1600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).riskResistance = 900;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).cost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).upgradeCost = 5000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).effect = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).efficiency = 10;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).tradeCost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).riskResistance = 410;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).effect = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).efficiency = 15;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).tradeCost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).riskResistance = 520;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).effect = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).efficiency = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).tradeCost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).riskResistance = 630;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).cost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).upgradeCost = 3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).effect = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).efficiency = 25;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).tradeCost = 1200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).riskResistance = 740;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).cost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).upgradeCost = 4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).effect = 50;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).efficiency = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).tradeCost = 1400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).riskResistance = 850;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).cost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).upgradeCost = 5000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).effect = 60;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).efficiency = 20;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).tradeCost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).riskResistance = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).cost = 100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).effect = 75;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).efficiency = 28;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).tradeCost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).riskResistance = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).cost = 200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).effect = 90;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).efficiency = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).tradeCost = 700;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).riskResistance = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).cost = 300;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 3).upgradeCost =3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).effect = 105;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).efficiency = 42;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).tradeCost = 900;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).riskResistance = 700;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 4).upgradeCost =4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).effect = 120;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).efficiency = 50;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).range = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).tradeCost = 1100;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).riskResistance = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).cost = 500;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Merchant, 5).upgradeCost = 5000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).efficiency = 30;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).range = 28;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).tradeCost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).riskResistance = 450;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).cost = 400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 1).upgradeCost = 1000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).efficiency = 35;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).range = 32;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).tradeCost = 1200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).riskResistance = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).cost = 600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 2).upgradeCost = 2000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).efficiency = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).range = 36;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).tradeCost = 1400;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).riskResistance = 750;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).cost = 800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 3).upgradeCost = 3000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).efficiency = 45;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).range = 40;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).tradeCost = 1600;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).riskResistance = 900;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).cost = 1000;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 4).upgradeCost = 4000;
        
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).effect = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).efficiency = 50;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).range = 44;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).tradeCost = 1800;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).riskResistance = 1050;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).cost = 1200;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Dealer, 5).upgradeCost = 5000;
    }
}
