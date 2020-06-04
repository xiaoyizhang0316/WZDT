﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using DG.Tweening;
using UnityEngine.UI;

public class NPCListInfo : MonoSingleton<NPCListInfo>
{

    public Button closeBtn;
    public Button unlockBtn;
    public Button cancelBtn;

    public Button specialTrade;
    public Button serviceTrade;
    public Button productTrade;

    public GameObject specialInfo;
    public GameObject commonServiceInfo;
    public GameObject commonProductInfo;
    public GameObject pop;
    public GameObject lockedInfo;

    public Role currentNpc;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(()=> {
            gameObject.SetActive(false);
        });
        cancelBtn.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="npc">Seed,Peasant,Merchant,Dealer,Common(Service Product)</param>
    public void ShowNpcInfo(Role npc)
    {
        HideAll();
        currentNpc = npc;
        switch (npc.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                ShowSpecialNpc();
                break;
            case GameEnum.RoleType.Peasant:
                ShowSpecialNpc();
                break;
            case GameEnum.RoleType.Merchant:
                ShowSpecialNpc();
                break;
            case GameEnum.RoleType.Dealer:
                ShowSpecialNpc();
                break;
            default:
                ShowCommonNpc();
                break;
        }
    }

    void HideAll()
    {
        specialInfo.SetActive(false);
        commonProductInfo.SetActive(false);
        commonServiceInfo.SetActive(false);
        
        lockedInfo.SetActive(false);
    }

    void ShowSpecialNpc()
    {
        specialTrade.onClick.RemoveAllListeners();
        specialTrade.onClick.AddListener(() =>
        {
            NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.ID));
            gameObject.SetActive(false);
        });
        specialInfo.GetComponent<NpcSpecialInfo>().SetInfo(currentNpc);
        specialInfo.SetActive(true);
    }

    void ShowCommonNpc()
    {
        if(currentNpc.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product)
        {
            commonProductInfo.GetComponent<NpcProductInfo>().SetInfo(currentNpc);
            commonProductInfo.SetActive(true);
            productTrade.onClick.RemoveAllListeners();
            productTrade.onClick.AddListener(() =>
            {
                NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.ID));
                gameObject.SetActive(false);
            });
        }
        else if(currentNpc.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Service)
        {
            commonServiceInfo.GetComponent<NpcServiceInfo>().SetInfo(currentNpc);
            commonServiceInfo.SetActive(true);
            serviceTrade.onClick.RemoveAllListeners();
            serviceTrade.onClick.AddListener(() =>
            {
                NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.ID));
                gameObject.SetActive(false);
            });
        }
    }

    public void ShowHideTipPop()
    {
        //useItemPop.GetComponent<NpcPop>()
        pop.SetActive(true);
        closeBtn.interactable = false;
        pop.GetComponent<Image>().DOFade(0, 1f).OnComplete(()=> {
            gameObject.SetActive(false);
            closeBtn.interactable = true;
        });
    }

    public void ShowUnlckPop(Role npc, int unlockNumber)
    {
        currentNpc = npc;
        lockedInfo.GetComponent<NpcLockedInfo>().SetInfo(npc, unlockNumber);
        lockedInfo.SetActive(true);

        unlockBtn.onClick.RemoveAllListeners();
        unlockBtn.onClick.AddListener(()=> {
            Unlock(unlockNumber);
        });
    }

    private void Unlock(int unlockNum)
    {
        
    }
}
