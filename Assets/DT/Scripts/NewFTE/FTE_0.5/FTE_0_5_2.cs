using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_0_5_2 : BaseGuideStep
{
    public List<GameObject> land;
    public List<GameObject> Seedtesting;

    public GameObject roleImage;

    public GameObject red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override IEnumerator StepStart()
    {
        StageGoal.My.maxRoleLevel = 1;
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 1, 0, 15, 20, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 2, 0, 20, 25, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 3, 0, 25, 30, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 4, 0, 30, 35, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 5, 0, 35, 40, 0, 0, 0, 0, 0); 
            
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 1, 0, 20, 10, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 2, 0, 25, 15, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 3, 0, 30, 20, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 4, 0, 40, 25, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 5, 0, 50, 30, 0, 0, 0, 0, 0); 

        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 1, 0, 20, 20, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 2, 0, 25, 28, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 3, 0, 30, 35, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 4, 0, 35, 42, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 5, 0, 40, 50, 0, 0, 0, 0, 0); 

        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 1, 0, 0, 30, 28, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 2, 0, 0, 35, 32, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 3, 0, 0, 40, 36, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 4, 0, 0, 45, 40, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 5, 0, 0, 50, 44, 0, 0, 0, 0);
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().delete.interactable = false;
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
        red.SetActive(true);
    }

    public override IEnumerator StepEnd()
    {
     
        

        yield return new WaitForSeconds(2); 
        red.SetActive(false);
    }

    public override bool ChenkEnd()
    {
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                PlayerData.My.MapRole[i].tradeButton.SetActive(false);
                missiondatas.data[0].currentNum = 1; 
                missiondatas.data[0].isFinish= true; 
                return true;
            }
        }

        return false;
    }

 
}
