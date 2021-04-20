using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_5Manager : MonoSingleton<FTE_0_5Manager>
{

  
    public GameObject dealerJC1; 
    public GameObject seed; 
    public GameObject dealer; 

    public Material sn;
    public Material sr;
    public Material sg;
    public Material bn;
    public Material br;
    public Material bg;
    
    public GameObject consumerSpot;
    public GameObject endPoint;
    public Renderer seerJC1_ran; 
    public Renderer dealerJC1_ran; 

    /// <summary>
    /// 清空仓库
    /// </summary>
    public int clearWarehouse = 0;
    public void Update()
    {
        
        if (StageGoal.My.playerGold <= 10000)
        {
            StageGoal.My.playerGold = 1000000;
        }
    }

    public void Start()
    {
        consumerSpot.SetActive(false);
        endPoint.SetActive(false);
        SetRoleMageZero();
        InitRoleStartActive(false);
        SetRoleInfoUp();
        dealer.SetActive(false);
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        SetDeleteButton(false);
        InitRoleTradeButton();
        StageGoal.My.maxRoleLevel = 3;
    }

    public void UpRole(GameObject role)
    {
        role.SetActive(true);
        role.transform.DOLocalMoveY(3,1).Play();
        
    }

    public void DownRole(GameObject role)
    {
        role.transform.DOLocalMoveY(-5,1).Play().OnComplete(() =>
        {
            role.SetActive(false);
        });
        
    }

    public void InitRoleTradeButton()
    {
        seed.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
        dealerJC1.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
    }

    public void InitRoleStartActive(bool isActive)
    {
        seed.SetActive(isActive);
        dealerJC1.SetActive(isActive);
    }

    public void SetRoleInfoUp()
    {
        NewCanvasUI.My.Panel_Update.transform.localPosition += new Vector3(0,5000,0);
    }

    public void SetRoleInfoDown()
    {
        NewCanvasUI.My.Panel_Update.SetActive(false);
        NewCanvasUI.My.Panel_Update.transform.localPosition -= new Vector3(0,5000,0);
        
    }

    public void SetUpdateButton(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().update.gameObject.SetActive(setActive);
    }

    public void SetClearWHButton(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().clearWarehouse.gameObject.SetActive(setActive);

    }

    public void SetDeleteButton(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().delete.gameObject.SetActive(setActive);
        
    }

    public void SetRoleMageZero()
    {
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 1, 0, 25, 15, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 2, 0, 20, 25, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 3, 0, 25, 30, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 4, 0, 30, 35, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 5, 0, 35, 40, 0, 0, 0, 0, 0); 
            
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 1, 0, 24, 10, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 2, 0, 25, 15, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 3, 0, 30, 20, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 4, 0, 40, 25, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 5, 0, 50, 30, 0, 0, 0, 0, 0); 

        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 1, 0, 25, 26, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 2, 0, 25, 28, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 3, 0, 30, 35, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 4, 0, 35, 42, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 5, 0, 40, 50, 0, 0, 0, 0, 0); 

        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 1, 0, 0, 42, 32, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 2, 0, 0, 35, 32, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 3, 0, 0, 40, 36, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 4, 0, 0, 45, 40, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 5, 0, 0, 50, 44, 0, 0, 0, 0);

    }

    public void ChangeColor(Renderer role,Material mat)
    {
        role.material = mat;
    }
}
