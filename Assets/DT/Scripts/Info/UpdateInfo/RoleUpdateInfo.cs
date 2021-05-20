using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleUpdateInfo : MonoSingleton<RoleUpdateInfo>
{
    /// <summary>
    /// 种子商模板
    /// </summary>
    public GameObject seed;

    /// <summary>
    /// 农民模板
    /// </summary>
    public GameObject peasant;

    /// <summary>
    /// 贸易商模板
    /// </summary>
    public GameObject merchant;

    /// <summary>
    /// 零售商
    /// </summary>
    public GameObject dealer;


    /// <summary>
    /// 服务
    /// </summary>
    public GameObject service;

    /// <summary>
    /// 改弹种
    /// </summary>
    public GameObject changeBulletType;
    /// <summary>
    /// 当前传入的角色
    /// </summary>
    public Role currentRole;

    /// <summary>
    /// 详细信息名字
    /// </summary>
    public Text name;

    /// <summary>
    /// 当前等级
    /// </summary>
    public int currentLevel;


    /// <summary>
    /// 关闭按钮
    /// </summary>
    public Button close;

    /// <summary>
    /// 删除角色按钮
    /// </summary>
    public Button delete;

    /// <summary>
    /// 等级
    /// </summary>
    public Text level;

    /// <summary>
    /// 技能描述
    /// </summary>
    public Text skillDesc;

    /// <summary>
    /// 下一个等级
    /// </summary>
    public int nextLevel;

    /// <summary>
    /// 升级按钮
    /// </summary>
    public Button update;

    /// <summary>
    /// 角色名称
    /// </summary>
    public string roleName;

    /// <summary>
    /// 弹药UI
    /// </summary>
    public Sprite AOE;

    public Sprite normallpp;
    public Sprite lightning;
    public Sprite tow;

    /// <summary>
    /// 种子商速度UI
    /// </summary>
    public Sprite seedSpeed;


    /// <summary>
    /// 角色信息
    /// </summary>
    public Image roleImg;

    /// <summary>
    /// 角色BuffUI列表
    /// </summary>
    public List<Image> roleBuff;

    /// <summary>
    /// Buff生成位置
    /// </summary>
    public Transform buffTF;

    /// <summary>
    /// buff预制体
    /// </summary>
    public GameObject buffPrb;

    /// <summary>
    /// 空buffUI
    /// </summary>
    public Sprite buffNull;

    /// <summary>
    /// Buff内容;
    /// </summary>
    public GameObject buffcontent;

    /// <summary>
    /// Buff文本
    /// </summary>
    public Text buffcontentText;

    /// <summary>
    /// 更改角色装备按钮
    /// </summary>
    public Button changeRoleButton;

    /// <summary>
    /// 创建交易按钮
    /// </summary>
    public Button createTradeButton;

    /// <summary>
    /// 清空仓库
    /// </summary>
    public Button clearWarehouse;

    /// <summary>
    /// 售卖角色
    /// </summary>
    public Button sellRole;

    /// <summary>
    /// 激励等级UI
    /// </summary>
    public EncourageLevel encourageLevel;

    /// <summary>
    /// 没有使用装备
    /// </summary>
   // public GameObject emptyEquip;


    /// <summary>
    /// 激励等级文本
    /// </summary>
    public Text jiliLevel;


    public GameObject self;

    public GameObject npc;

    public Transform RequiContent;
    public GameObject requiresign;
    public GameObject requireOBJ;

    // Start is called before the first frame update
    void Start()
    {
        SetDependency();
        InitRequire();
        close.onClick.AddListener(() => { gameObject.SetActive(false); });
        delete.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要删除" + currentRole.baseRoleData.roleName + "吗？";

            DeleteUIManager.My.Init(str, () =>
            {
                PlayerData.My.DeleteRole(currentRole.ID);
                if (!PlayerData.My.isSOLO)
                {
                    string str1 = "DeleteRole|";
                    str1 += currentRole.ID.ToString();
                    if (PlayerData.My.isServer)
                    {
                        PlayerData.My.server.SendToClientMsg(str1);
                    }
                    else
                    {
                        PlayerData.My.client.SendToServerMsg(str1);
                    }
                }
            });
        });
        changeRoleButton.onClick.AddListener(() =>
        {
            NewCanvasUI.My.Panel_AssemblyRole.gameObject.SetActive(true);
            CreatRoleManager.My.Open(currentRole);
            gameObject.SetActive(false);
        });
        createTradeButton.onClick.AddListener(() =>
        {
            NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentRole.ID));
            gameObject.SetActive(false);
        });
        clearWarehouse.onClick.AddListener(() =>
        {
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要清空仓库吗？";
            DeleteUIManager.My.Init(str, () =>
            {
                if (!PlayerData.My.isSOLO)
                {
                    string str1 = "ClearWarehouse|";
                    str1 += currentRole.ID.ToString();


                    if (PlayerData.My.isServer)
                    {
                        PlayerData.My.server.SendToClientMsg(str1);
                    }
                    else
                    {
                        PlayerData.My.client.SendToServerMsg(str1);
                    }
                }

                if (SceneManager.GetActiveScene().name.Equals("FTE_0.7"))
                {
                    if (currentRole.baseRoleData.roleType == GameEnum.RoleType.Peasant &&
                        FTE_0_6Manager.My.clearWarehouse == 1)
                    {
                        FTE_0_6Manager.My.clearWarehouse = 2;
                    }
                }

                PlayerData.My.GetMapRoleById(currentRole.ID).ClearWarehouse();
                ReInit(currentRole);
            });
        });
        sellRole.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要出售" + currentRole.baseRoleData.roleName + "吗？";

            DeleteUIManager.My.Init(str, () =>
            {
                PlayerData.My.SellRole(currentRole.ID);
                if (!PlayerData.My.isSOLO)
                {
                    string str1 = "DeleteRole|";
                    str1 += currentRole.ID.ToString();
                    if (PlayerData.My.isServer)
                    {
                        PlayerData.My.server.SendToClientMsg(str1);
                    }
                    else
                    {
                        PlayerData.My.client.SendToServerMsg(str1);
                    }
                }
            });
        });
        buffcontent.SetActive(false);
    }

    List<string> sceneName = new List<string> {"FTE_1", "FTE_0-1", "FTE_0-2"};

    private float interval = 1f;

    // Update is called once per frame
 //   void Update()
 //   {
 //       interval += Time.deltaTime;
 //       if (interval >= 1f)
 //       {
 //           if (!sceneName.Contains(SceneManager.GetActiveScene().name))
 //           {
 //               if (currentRole != null && currentRole.EquipList.Count == 0 && currentRole.peoPleList.Count == 0
 //                   && (PlayerData.My.GetAvailableWorkerNumber() > 0 || PlayerData.My.GetAvailableEquipNumber() > 0))
 //               {
 //                   //emptyEquip.SetActive(true);
 //               }
 //               else
 //               {
 //                  // emptyEquip.SetActive(false);
 //               }
 //           }
 //           else
 //           {
 //              // emptyEquip.SetActive(false);
 //           }
//
 //           interval = 0f;
 //       }
 //   }

    public void SetDependency()
    {
        delete = transform.Find("Delete").GetComponent<Button>();
    }

    public void Init(Role role)
    {
        if (role.isNpc)
        {
            npc.SetActive(true);
            self.SetActive(false);
            update.gameObject.SetActive(false);
            changeRoleButton.gameObject.SetActive(false);
            delete.gameObject.SetActive(false);
            
        }
        else
        {
            npc.SetActive(false);
            self.SetActive(true);
            update.gameObject.SetActive(true);
            changeRoleButton.gameObject.SetActive(true);
            delete.gameObject.SetActive(true);
        }

        jiliLevel.text = PlayerData.My.GetMapRoleById(role.ID).encourageLevel.ToString();
        name.text = role.baseRoleData.roleName;
        roleName = role.baseRoleData.roleName;
        roleImg.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + role.baseRoleData.roleType.ToString() +
                                                role.baseRoleData.level.ToString());
        //roleImg.SetNativeSize();
        skillDesc.text = PlayerData.My.GetMapRoleById(role.ID).transform.GetComponent<BaseSkill>().skillDesc;
        encourageLevel.Init(PlayerData.My.GetMapRoleById(role.ID));
        currentRole = role;
        seed.SetActive(false);
        peasant.SetActive(false);
        merchant.SetActive(false);
        dealer.SetActive(false);
        service.SetActive(false);
        changeBulletType.SetActive(false);
        nextLevel = role.baseRoleData.level + 1;
        currentLevel = role.baseRoleData.level;
        if (GetComponentInChildren<UpdateRole>())
        {
            GetComponentInChildren<UpdateRole>().Init();
        }

        sellRole.gameObject.SetActive(false);
        ReInit(role);
        if (currentLevel >= StageGoal.My.maxRoleLevel)
        {
            update.interactable = false;
        }
        else
        {
            update.interactable = true;
        }

        InitBuff();
        DataUploadManager.My.AddData(DataEnum.角色_查看自己属性);
        switch (role.baseRoleData.roleType)
        {
            case GameEnum.RoleType.Seed:
                DataUploadManager.My.AddData(DataEnum.角色_查看自己种子商属性);
                break;
            case GameEnum.RoleType.Peasant:
                DataUploadManager.My.AddData(DataEnum.角色_查看自己农民属性);
                break;
            case GameEnum.RoleType.Merchant:
                DataUploadManager.My.AddData(DataEnum.角色_查看自己贸易商属性);
                break;
            case GameEnum.RoleType.Dealer:
                DataUploadManager.My.AddData(DataEnum.角色_查看自己零售商属性);
                break;
            default:
                break;
        }
    }

    public void InitBuff()
    {
        BaseMapRole baseMapRole = PlayerData.My.GetMapRoleById(currentRole.ID);

        for (int i = 0; i < roleBuff.Count; i++)
        {
            roleBuff[i].sprite = buffNull;
            roleBuff[i].GetComponent<ShowBuffText>().currentbuffData = null;
        }

        for (int i = 0; i < baseMapRole.GetEquipBuffList().Count; i++)
        {
            roleBuff[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.GetEquipBuffList()[i]);
            roleBuff[i].GetComponent<ShowBuffText>().currentbuffData =
                GameDataMgr.My.GetBuffDataByID(baseMapRole.GetEquipBuffList()[i]);
        }

        for (int i = 0; i < buffTF.childCount; i++)
        {
            Destroy(buffTF.GetChild(i).gameObject);
        }

        for (int i = 0; i < baseMapRole.buffList.Count; i++)
        {
            GameObject game = Instantiate(buffPrb, buffTF);
            game.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.buffList[i].buffId.ToString());
            game.GetComponent<ShowBuffText>().currentbuffData =
                baseMapRole.buffList[i].buffData;
            game.GetComponent<ShowBuffText>().role = baseMapRole.buffList[i].castRole;
        }
    }

    public void ReInit(Role role)
    {
        level.text = role.baseRoleData.level.ToString();
        if (role.baseRoleData.level >= 5)
        {
            level.color = Color.yellow;
        }
        else
        {
            level.color = Color.white;
        }

        if (role.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Product)
        {
            if (role.baseRoleData.roleType == GameEnum.RoleType.Seed)
            {
                seed.SetActive(true);
                seed.GetComponent<BaseRoleListInfo>().Init(role);
            }

           else if (role.baseRoleData.roleType == GameEnum.RoleType.Peasant)
            {
                peasant.SetActive(true);
                peasant.GetComponent<BaseRoleListInfo>().Init(role);
            }

            else  if (role.baseRoleData.roleType == GameEnum.RoleType.Merchant)
            {
                merchant.SetActive(true);
                merchant.GetComponent<BaseRoleListInfo>().Init(role);
            }

            else   if (role.baseRoleData.roleType == GameEnum.RoleType.Dealer)
            {
                dealer.SetActive(true);
                dealer.GetComponent<BaseRoleListInfo>().Init(role);
            }
            else
            {
                changeBulletType.SetActive(true);

                changeBulletType.GetComponent<BaseRoleListInfo>().Init(role);
            }
        }

        if (role.baseRoleData.roleSkillType == GameEnum.RoleSkillType.Service)
        {
            service.SetActive(true);

            service.GetComponent<BaseRoleListInfo>().Init(role);
        }
    }

    public void InitRequire()
    {
        BaseMapRole role = PlayerData.My.GetMapRoleById(currentRole.ID);
        if (role.roleRequirement.Count == 0)
        {
            requireOBJ.SetActive(false);
        }

        for (int i = 0; i <role.roleRequirement.Count; i++)
        {
           GameObject game =  Instantiate(requiresign, RequiContent);
           game.GetComponent<ReqireMentSign>().Init(false,role.roleRequirement[i]);
        }
    }

    public void ShowBuffText(string text)
    {
        buffcontent.SetActive(true);
        buffcontentText.text = text;
    }

    public void closeBuffContent()
    {
        buffcontent.SetActive(false);
        buffcontentText.text = null;
    }
}