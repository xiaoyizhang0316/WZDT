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

    public GameObject npcInfo;

    public GameObject specialInfo;
    public GameObject commonServiceInfo;
    public GameObject commonProductInfo;
    public GameObject pop;
    public GameObject lockedInfo;

    public BaseMapRole currentNpc;
    public BaseSkill currentSkill;

    public Sprite buff;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(()=> {
            npcInfo.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        });

        closeBtn.gameObject.SetActive(false);
        npcInfo.SetActive(false);
        //ShowHideTipPop("解锁失败");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="npc">Seed,Peasant,Merchant,Dealer,Common(Service Product)</param>
    public void ShowNpcInfo(Transform npc)
    {
        HideAll();
        currentNpc = npc.GetComponent<BaseMapRole>();
        currentSkill = npc.GetComponent<BaseSkill>();
        closeBtn.gameObject.SetActive(true);
        switch (currentNpc.baseRoleData.baseRoleData.roleType)
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
                ShowCommonNpc( npc);
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
        npcInfo.SetActive(true);
        specialTrade.onClick.RemoveAllListeners();
        specialTrade.onClick.AddListener(() =>
        {
            NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.baseRoleData.ID));
            npcInfo.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        });
        specialInfo.GetComponent<NpcSpecialInfo>().SetInfo(currentNpc, currentSkill);
        specialInfo.SetActive(true);
    }

    void ShowCommonNpc(Transform npc)
    {
        npcInfo.SetActive(true);
        if (currentNpc.baseRoleData.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product)
        {
            commonProductInfo.GetComponent<NpcProductInfo>().SetInfo(npc,currentSkill);
            commonProductInfo.SetActive(true);
            productTrade.onClick.RemoveAllListeners();
            productTrade.onClick.AddListener(() =>
            {
                NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.baseRoleData.ID));
                npcInfo.SetActive(false);
                closeBtn.gameObject.SetActive(false);
            });
        }
        else if(currentNpc.baseRoleData.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Service)
        {
            commonServiceInfo.GetComponent<NpcServiceInfo>().SetInfo(npc,currentSkill);
            commonServiceInfo.SetActive(true);
            serviceTrade.onClick.RemoveAllListeners();
            serviceTrade.onClick.AddListener(() =>
            {
                NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.baseRoleData.ID));
                npcInfo.SetActive(false);
                closeBtn.gameObject.SetActive(false);
            });
        }
    }

    public void ShowHideTipPop(string tip)
    {
        //useItemPop.GetComponent<NpcPop>()
        npcInfo.SetActive(true);
        HideAll();
        pop.transform.GetChild(0).GetComponent<Text>().text = tip;
        pop.SetActive(true);
        closeBtn.interactable = false;
        pop.transform.GetChild(0).GetComponent<Text>().DOFade(0, 2f).OnComplete(()=> {
            //npcInfo.SetActive(false);
            pop.transform.GetChild(0).GetComponent<Text>().DOFade(1, 0.01f).Play();
            pop.SetActive(false);
            closeBtn.interactable = true;
            closeBtn.gameObject.SetActive(false);
        }).Play();
    }

    public void ShowUnlckPop(Transform npc)
    {
        HideAll();
        currentNpc = npc.GetComponent<BaseMapRole>();
        npcInfo.SetActive(true);
        closeBtn.gameObject.SetActive(true);
        lockedInfo.GetComponent<NpcLockedInfo>().SetInfo(npc, npc.GetComponent<NPC>().lockNumber);
        lockedInfo.SetActive(true);



        unlockBtn.onClick.RemoveAllListeners();
        unlockBtn.onClick.AddListener(()=> {
            Unlock(npc);
        });

        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(() => {
            npcInfo.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        });
    }

    private void Unlock(Transform npc)
    {
        if (npc.GetComponent<NPC>().UnlockNPCRole())
        {
            lockedInfo.SetActive(false);
            ShowNpcInfo(npc);
        }
        else
        {
            // unlock fail
            lockedInfo.SetActive(false);
            ShowHideTipPop("解锁失败");
        }
    }
}
