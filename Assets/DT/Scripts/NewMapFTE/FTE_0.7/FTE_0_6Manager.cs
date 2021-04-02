using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_0_6Manager : MonoSingleton<FTE_0_6Manager>
{


    public GameObject seerJC1; 
    public GameObject dealerJC1; 
    public GameObject dealerJC2; 
    public GameObject dealerJC3; 
    public GameObject dealerJC4; 

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
    public Renderer dealerJC2_ran; 
    public Renderer dealerJC3_ran; 
    public Renderer dealerJC4_ran; 

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
        PlayerData.My.playerGears.Clear();
        PlayerData.My.playerWorkers.Clear();
        SetDeleteButton(false);
        InitRoleTradeButton();
        SetRoleInfoAddEquip(false);
        RoleListManager.My.OutButton();
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
        seerJC1.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
        dealerJC1.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
        dealerJC3.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
        dealerJC2.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
        dealerJC4.GetComponent<BaseMapRole>().tradeButton.SetActive(false);
    }

    public void InitRoleStartActive(bool isActive)
    {
        seerJC1.SetActive(isActive);
        dealerJC2.SetActive(isActive);
        dealerJC3.SetActive(isActive);
        dealerJC4.SetActive(isActive);
    }

    public void SetRoleInfoAddEquip(bool setActive)
    {
        NewCanvasUI.My.Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(setActive);

    }

    public void SetRoleInfoUp()
    {
        NewCanvasUI.My.Panel_Update.transform.localPosition += new Vector3(0,5000,0);
    }

    public void SetRoleInfoDown()
    {
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
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 2, 0, 30, 19, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 3, 0, 38, 25, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 4, 0, 45, 31, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Seed, 5, 0, 51, 36, 0, 0, 0, 0, 0); 
            
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 1, 0, 24, 10, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 2, 0, 27, 16, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 3, 0, 32, 22, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 4, 0, 41, 30, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Peasant, 5, 0, 49, 38, 0, 0, 0, 0, 0); 

        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 1, 0, 25, 26, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 2, 0, 35, 33, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 3, 0, 45, 42, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 4, 0, 55, 48, 0, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Merchant, 5, 0, 60, 54, 0, 0, 0, 0, 0); 

        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 1, 0, 0, 42, 32, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 2, 0, 0, 46, 38, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 3, 0, 0, 53, 43, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 4, 0, 0, 63, 49, 0, 0, 0, 0); 
        GameDataMgr.My.SetModuleData(GameEnum.RoleType.Dealer, 5, 0, 0, 74, 54, 0, 0, 0, 0);

    }

    public void ChangeColor(Renderer role,Material mat)
    {
        role.material = mat;
    }
}
