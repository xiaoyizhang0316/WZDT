using System.Collections;
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

    public Role currentNpc;
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
    public void ShowNpcInfo(Role npc)
    {
        HideAll();
        currentNpc = npc;
        closeBtn.gameObject.SetActive(true);
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
        npcInfo.SetActive(true);
        specialTrade.onClick.RemoveAllListeners();
        specialTrade.onClick.AddListener(() =>
        {
            NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.ID));
            npcInfo.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        });
        specialInfo.GetComponent<NpcSpecialInfo>().SetInfo(currentNpc);
        specialInfo.SetActive(true);
    }

    void ShowCommonNpc()
    {
        npcInfo.SetActive(true);
        if (currentNpc.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product)
        {
            commonProductInfo.GetComponent<NpcProductInfo>().SetInfo(currentNpc);
            commonProductInfo.SetActive(true);
            productTrade.onClick.RemoveAllListeners();
            productTrade.onClick.AddListener(() =>
            {
                NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.ID));
                npcInfo.SetActive(false);
                closeBtn.gameObject.SetActive(false);
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
        pop.transform.GetChild(0).GetComponent<Text>().DOFade(0, 1f).OnComplete(()=> {
            //npcInfo.SetActive(false);
            pop.transform.GetChild(0).GetComponent<Text>().DOFade(1, 0.01f).Play();
            pop.SetActive(false);
            closeBtn.interactable = true;
            closeBtn.gameObject.SetActive(false);
        }).Play();
    }

    public void ShowUnlckPop(Role npc, NPC n)
    {
        currentNpc = npc;
        npcInfo.SetActive(true);
        closeBtn.gameObject.SetActive(true);
        lockedInfo.GetComponent<NpcLockedInfo>().SetInfo(npc, n.lockNumber);
        lockedInfo.SetActive(true);



        unlockBtn.onClick.RemoveAllListeners();
        unlockBtn.onClick.AddListener(()=> {
            Unlock(n);
        });

        cancelBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.AddListener(() => {
            npcInfo.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        });
    }

    private void Unlock(NPC npc)
    {
        if (npc.UnlockNPCRole())
        {
            lockedInfo.SetActive(false);
            ShowNpcInfo(currentNpc);
        }
        else
        {
            // unlock fail
            lockedInfo.SetActive(false);
            ShowHideTipPop("解锁失败");
        }
    }
}
