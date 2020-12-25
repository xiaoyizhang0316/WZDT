using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_2 : BaseGuideStep
{
    public List<GameObject> land;
    public List<GameObject> Seedtesting;

    public GameObject roleImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).upgradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).tradeCost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).tradeCost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).tradeCost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).tradeCost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).tradeCost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).tradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).tradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).tradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).tradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).tradeCost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).riskResistance = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).riskResistance = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).riskResistance = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).riskResistance = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).riskResistance = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).riskResistance= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).riskResistance= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).riskResistance= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).riskResistance= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).riskResistance= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 2).cost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 1).cost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 3).cost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 4).cost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Seed, 5).cost = 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 2).cost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 1).cost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 3).cost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 4).cost= 0;
        GameDataMgr.My.GetModelDataFTE(GameEnum.RoleType.Peasant, 5).cost= 0;
        yield return new WaitForSeconds(1f);
      // for (int i = 0; i <land.Count; i++)
      // {
      //     land[i].transform.DOLocalMoveY(-5, 1f).Play();
      // }
      // yield return new WaitForSeconds(1f);
      // for (int i = 0; i <land.Count; i++)
      // {
      //     land[i].transform.DOLocalMoveY(0, 1f).Play();
      //
      // }

      // for (int i = 0; i < Seedtesting.Count; i++)
      // {
      //     Seedtesting[i].SetActive(true);
      //     Seedtesting[i].transform.DOLocalMoveY(0.3f, 1f).Play();

      // }
         
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
           
            PlayerData.My.MapRole[i].tradeButton.SetActive(false);
             
        }
        RoleListManager.My.OutButton();

        yield return new WaitForSeconds(1f);

        roleImage.gameObject.SetActive(false);
    }

    public override IEnumerator StepEnd()
    {
        missiondatas.data[0].currentNum = 1; 
        missiondatas.data[0].isFinish= true; 
        
      yield break;
      ;
    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                PlayerData.My.MapRole[i].tradeButton.SetActive(false);
                return true;
            }
        }

        return false;
    }

 
}
