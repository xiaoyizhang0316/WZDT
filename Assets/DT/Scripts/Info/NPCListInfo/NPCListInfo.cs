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

    public Button closeUnlock;
    public Button closeProductAndSpecial;
    public Button closeService;


    public Button specialTrade;
    public Button serviceTrade;
    public Button productTrade;

    public GameObject npcInfo;

    public GameObject specialInfo;
    public GameObject commonServiceInfo;
    public GameObject commonProductInfo;
    public GameObject pop;
    public GameObject lockedInfo;
    public GameObject bulletWareHouse;

    public BaseMapRole currentNpc;
    public BaseSkill currentSkill;
    public GameObject buffTextContent;
    public Text buffText;

    public Sprite buff;

    public Transform buffTF;
    public GameObject buffPrb;

    public NpcBulletDetail npcBulletDetail;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(()=> {
            npcInfo.SetActive(false);
            specialInfo.GetComponent<NpcSpecialInfo>().Reset();
            commonProductInfo.GetComponent<NpcProductInfo>().Reset();
            closeBtn.gameObject.SetActive(false);
        });

        closeUnlock.onClick.AddListener(()=> {
            npcInfo.SetActive(false);
            specialInfo.GetComponent<NpcSpecialInfo>().Reset();
            commonProductInfo.GetComponent<NpcProductInfo>().Reset();
            closeBtn.gameObject.SetActive(false);
        });
        closeProductAndSpecial.onClick.AddListener(() => {
            npcInfo.SetActive(false);
            specialInfo.GetComponent<NpcSpecialInfo>().Reset();
            commonProductInfo.GetComponent<NpcProductInfo>().Reset();
            closeBtn.gameObject.SetActive(false);
        });
        closeService.onClick.AddListener(() => {
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
        gameObject.SetActive(true);
        HideAll();
        currentNpc = npc.GetComponent<BaseMapRole>();
        currentSkill = npc.GetComponent<BaseSkill>();
        closeBtn.gameObject.SetActive(true);
        switch (currentNpc.baseRoleData.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                ShowSpecialNpc(npc);
                InitBuff(currentNpc);
                break;
            case GameEnum.RoleType.Peasant:
                ShowSpecialNpc(npc);
                InitBuff(currentNpc);
                break;
            case GameEnum.RoleType.Merchant:
                ShowSpecialNpc(npc);
                InitBuff(currentNpc);
                break;
            case GameEnum.RoleType.Dealer:
                ShowSpecialNpc(npc);
                InitBuff(currentNpc);
                break;
            default:
                ShowCommonNpc( npc);
                break;
        }
    }

    public void InitBuff(BaseMapRole baseMapRole)
    {
        //BaseMapRole baseMapRole = PlayerData.My.GetBaseMapRoleByName(currentRole.baseRoleData.roleName);

        for (int i = 0; i < buffTF.childCount; i++)
        {
            Destroy(buffTF.GetChild(i).gameObject);
        }
        for (int i = 0; i < baseMapRole.buffList.Count; i++)
        {
            GameObject game = Instantiate(buffPrb, buffTF);
            game.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.buffList[i].buffId.ToString());
            game.GetComponent<BuffText>().buff =
                baseMapRole.buffList[i].buffData;
        }
    }

    void HideAll()
    {
        specialInfo.SetActive(false);
        commonProductInfo.SetActive(false);
        commonServiceInfo.SetActive(false);
        bulletWareHouse.SetActive(false);
        lockedInfo.SetActive(false);
    }

    void ShowSpecialNpc(Transform npc)
    {
        npcInfo.SetActive(true);
        specialTrade.onClick.RemoveAllListeners();
        specialTrade.onClick.AddListener(() =>
        {
            NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentNpc.baseRoleData.ID));
            npcInfo.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        });
        specialInfo.GetComponent<NpcSpecialInfo>().SetInfo(currentNpc, currentSkill, npc);
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
            InitBuff(currentNpc);
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
    bool isPopShow = false;
    public void ShowHideTipPop(string tip)
    {
        //useItemPop.GetComponent<NpcPop>()
        npcInfo.SetActive(true);
        HideAll();
        pop.transform.GetChild(0).GetComponent<Text>().text = tip;
        if (isPopShow)
        {
            DOTween.Kill("NPCPOP");
            pop.transform.GetChild(0).GetComponent<Text>().DOFade(1, 0.01f).Play();
        }
        pop.SetActive(true);
        isPopShow = true;
        closeBtn.interactable = false;
        pop.transform.GetChild(0).GetComponent<Text>().DOFade(0, 2f).SetId("NPCPOP").OnComplete(()=> {
            //npcInfo.SetActive(false);
            pop.transform.GetChild(0).GetComponent<Text>().DOFade(1, 0.01f).Play();
            pop.SetActive(false);
            closeBtn.interactable = true;
            closeBtn.gameObject.SetActive(false);
            isPopShow = false;
        }).Play();
    }

    public void ShowUnlckPop(Transform npc)
    {
        HideAll();
        currentNpc = npc.GetComponent<BaseMapRole>();
        npcInfo.SetActive(true);
        closeBtn.gameObject.SetActive(true);
        lockedInfo.SetActive(true);
        lockedInfo.GetComponent<NpcLockedInfo>().SetInfo(npc, npc.GetComponent<NPC>().lockNumber);




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

    public void ShowBuffInfo(string info)
    {
        buffText.text = info;
        buffTextContent.SetActive(true);
    }

    public void HideBuffInfo()
    {
        buffTextContent.SetActive(false);
    }
}
