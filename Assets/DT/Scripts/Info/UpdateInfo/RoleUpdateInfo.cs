using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoleUpdateInfo : MonoSingleton<RoleUpdateInfo>
{ 
    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;
    public GameObject dealer;
    public Role currentRole;

    public Text name;
    public int nextLevel;
    public int currentLevel;
    public Button close;
    public Button delete;
    public Text level;

    public Text skillDesc;

    public Button hammer;
    public Button update;
    public string roleName;

    public Sprite AOE;
    public Sprite normallpp;
    public Sprite lightning;
    public Sprite tow;
    public Sprite seedSpeed;
    public Sprite melonSpeed;

    public Image roleImg;

    public List<Image> roleBuff;

    public Transform buffTF;

    public GameObject buffPrb;

    public Sprite buffNull;

    public GameObject buffcontent;
    public Text buffcontentText;

    public Button changeRoleButton;

    public Button createTradeButton;

    public Button clearWarehouse;

    public Button sellRole;

    public EncourageLevel encourageLevel;

    public GameObject emptyEquip;
    
    // Start is called before the first frame update
    void Start()
    {
        SetDependency();
        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        delete.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要删除" + currentRole.baseRoleData.roleName + "吗？";
          
            DeleteUIManager.My.Init(str, () => {
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
            if (PlayerData.My.guanJianZiYuanNengLi[5])
            {
                str = "确定要将仓库中的产品低价处理吗?";
            }

            DeleteUIManager.My.Init(str, () => {
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
                PlayerData.My.GetMapRoleById(currentRole.ID).ClearWarehouse();
                ReInit(currentRole);
            });
        });
        sellRole.onClick.AddListener(() => {
            gameObject.SetActive(false);
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要出售" + currentRole.baseRoleData.roleName + "吗？";

            DeleteUIManager.My.Init(str, () => {
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

    List<string> sceneName = new List<string> { "FTE_1", "FTE_0-1", "FTE_0-2" };

    private float interval = 1f;

    // Update is called once per frame
    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 1f)
        {
            if (!sceneName.Contains(SceneManager.GetActiveScene().name))
            {
                if (currentRole != null && currentRole.EquipList.Count == 0 && currentRole.peoPleList.Count == 0
                    && (PlayerData.My.GetAvailableWorkerNumber() > 0 || PlayerData.My.GetAvailableEquipNumber() > 0))
                {
                    emptyEquip.SetActive(true);
                }
                else
                {
                    emptyEquip.SetActive(false);
                }
            }
            else
            {
                emptyEquip.SetActive(false);
            }
            interval = 0f;
        }
    }

    public void SetDependency()
    {
        delete = transform.Find("Delete").GetComponent<Button>();
    }

    public void Init(Role role )
    {     
        name.text = role.baseRoleData.roleName;
        roleName = role.baseRoleData.roleName;
        roleImg.sprite = Resources.Load<Sprite>("Sprite/RoleLogo/" + role.baseRoleData.roleType.ToString()+role.baseRoleData.level.ToString());
        roleImg.SetNativeSize();
        skillDesc.text = PlayerData.My.GetMapRoleById(role.ID).transform.GetComponent<BaseSkill>().skillDesc;
        encourageLevel.Init(PlayerData.My.GetMapRoleById(role.ID));
        currentRole = role; 
        seed.SetActive(false);
        peasant.SetActive(false);
        merchant.SetActive(false);
        dealer.SetActive(false);
        nextLevel = role.baseRoleData.level+1;
        currentLevel = role.baseRoleData.level;
        if (GetComponentInChildren<UpdateRole>())
        {
            GetComponentInChildren<UpdateRole>().Init();

        }
        sellRole.gameObject.SetActive(false);
        if (PlayerData.My.yeWuXiTong[5] && role.baseRoleData.level >= 3)
        {
            sellRole.gameObject.SetActive(true);
        }
        if (PlayerData.My.guanJianZiYuanNengLi[5])
        {
            clearWarehouse.GetComponentInChildren<Text>().text = "清仓(" + PlayerData.My.GetMapRoleById(role.ID).CountWarehouseIncome() + ")";
            clearWarehouse.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Talent/Warehouse");
        }
        ReInit(role);
        if (currentLevel >= StageGoal.My.maxRoleLevel)
        {
            update.interactable = false;
            hammer.interactable = false;
        }
        else
        {
            update.interactable = true;
            hammer.interactable = true;
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
        BaseMapRole baseMapRole =    PlayerData.My.GetMapRoleById(currentRole.ID);
        
        for (int i = 0; i <roleBuff.Count; i++)
        {
            roleBuff[i].sprite = buffNull;
            roleBuff[i].GetComponent<ShowBuffText>().currentbuffData = null;
        }
        for (int i = 0; i <baseMapRole.GetEquipBuffList().Count; i++)
        {
            roleBuff[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.GetEquipBuffList()[i]);
            roleBuff[i].GetComponent<ShowBuffText>().currentbuffData =
                GameDataMgr.My.GetBuffDataByID(baseMapRole.GetEquipBuffList()[i]);
        }

        for (int i = 0; i <buffTF.childCount; i++)
        {
            Destroy(buffTF.GetChild(i).gameObject);
        }
        for (int i = 0; i <baseMapRole.buffList.Count; i++)
        {
           GameObject game= Instantiate(buffPrb, buffTF);
           game.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.buffList[i].buffId.ToString());
           game.GetComponent<ShowBuffText>().currentbuffData =
               baseMapRole.buffList[i].buffData;
            game.GetComponent<ShowBuffText>().role = baseMapRole.buffList[i].castRole;
        }
    }

    public void ReInit(Role role )
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
        if (role.baseRoleData.roleType == GameEnum.RoleType.Seed)
        {
            seed.SetActive(true);
            seed.GetComponent<BaseRoleListInfo>().Init(role);
        }
        if (role.baseRoleData.roleType == GameEnum.RoleType.Peasant)
        {
            peasant.SetActive(true);
            peasant.GetComponent<BaseRoleListInfo>().Init(role);
        }
        if (role.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            merchant.SetActive(true);
            merchant.GetComponent<BaseRoleListInfo>().Init(role);
        }
        if (role.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            dealer.SetActive(true);
            dealer.GetComponent<BaseRoleListInfo>().Init(role);
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
