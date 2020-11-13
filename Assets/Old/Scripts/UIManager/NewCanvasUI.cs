using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEnum;
using static UnityEngine.UIElements.VisualElement;

public class NewCanvasUI : MonoSingleton<NewCanvasUI>
{

    public GameObject Panel_ChoseRole;
    public Role CurrentClickRole;
    public BaseMapRole currentMapRole;
    public GameObject Panel_AssemblyRole;
    public GameObject Panel_TradeSetting;
    public Transform RoleTF;
    public GameObject lose;
    
    
    /// <summary>
    /// 需要遮挡的UI
    /// </summary>
    public List<GameObject> needReycastTargetPanel;

    #region 交易相关变量
    /// <summary>
    /// 是否处于设置交易状态
    /// </summary>
    public bool isSetTrade;

    /// <summary>
    /// 交易发起方
    /// </summary>
    public BaseMapRole startRole;

    /// <summary>
    /// 交易承受方
    /// </summary>
    public BaseMapRole endRole;

    public GameObject CreateTradeLineGo;

    public GameObject Panel_Delete;

    public GameObject Panel_Update;

    public GameObject Panel_RoleInfo;

    public GameObject Panel_Review;

    public GameObject consumerInfoFloatWindow;

    public GameObject Panel_Option;

    public GameObject Panel_Stat;

    public GameObject Panel_NPC;

    public Button Button_Pause;

    public Button Button_Normal;

    public Button Button_Accelerate;

    public Button statBtn;
    public Button OptionsBtn;
    public Button showHideTradeButton;

    #endregion

    public Transform hidePanel;

    public List<GameObject> highLight = new List<GameObject>();

    public List<GameObject> panelList = new List<GameObject>();

    public GameObject watchGuidePanel;

    // Start is called before the first frame update
    void Start()
    {
        CreateTradeLineGo = FindObjectOfType<CreateTradeLine>().gameObject;
        CreateTradeLineGo.SetActive(false);
        RoleTF = GameObject.FindGameObjectWithTag("RoleTF").transform;
        GetComponent<Canvas>().worldCamera = Camera.main;
        //Button_Pause = transform.Find("TimeScale/GamePause").GetComponent<Button>();
        //Button_Normal = transform.Find("TimeScale/GameNormal").GetComponent<Button>();
        //Button_Accelerate = transform.Find("TimeScale/GameAccelerate").GetComponent<Button>();
        statBtn.onClick.AddListener(() => {
            Panel_Stat.SetActive(true);
            DataStatPanel.My.ShowStat();
        });
        OptionsBtn.onClick.AddListener(()=> {
            Panel_Option.SetActive(true);
            OptionsPanel.My.ShowOPtionsPanel();
        });
        showHideTradeButton.onClick.AddListener(ToggleHidePanelShow);
        InitTimeButton();
        Panel_Delete.SetActive(false);
        lose.SetActive(false);
        isTradeButtonActive = true;
        isProductLineActive = true;
        isInfoLineActive = true;
        Init1_4UI();
    }



    /// <summary>
    /// 将1-4关未开启的UI关闭、开启
    /// </summary>
    public void Init1_4UI()
    {
        switch (NetworkMgr.My.levelProgressList.Count  )
        //switch (1  )
        {
            case 0:
                Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(false);
                Panel_TradeSetting.transform.Find("MoneyFirst").GetComponent<Button>(). enabled = false;
                Panel_TradeSetting.transform.Find("MoneyLast").GetComponent<Button>(). enabled = false;
                Panel_TradeSetting.transform.Find("MoneyFirst").GetChild(0).GetComponent<Text>().text = "???";
                Panel_TradeSetting.transform.Find("MoneyLast").GetChild(0).GetComponent<Text>().text = "???";
                Panel_Update.GetComponent<RoleUpdateInfo>().seed.GetComponent<RoleListInfoSeed>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().peasant.GetComponent<RoleListInfoPeasant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().merchant.GetComponent<RoleListInfoMerchant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().dealer.GetComponent<RoleListInfoDealer >().productTF.gameObject.SetActive(false);
            break;
            case 1:
                Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(false);
                Panel_TradeSetting.transform.Find("MoneyFirst").GetComponent<Button>(). enabled= false;
                Panel_TradeSetting.transform.Find("MoneyLast").GetComponent<Button>(). enabled = false;
                Panel_TradeSetting.transform.Find("MoneyFirst").GetChild(0).GetComponent<Text>().text = "???";
                Panel_TradeSetting.transform.Find("MoneyLast").GetChild(0).GetComponent<Text>().text = "???";
                Panel_Update.GetComponent<RoleUpdateInfo>().seed.GetComponent<RoleListInfoSeed>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().peasant.GetComponent<RoleListInfoPeasant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().merchant.GetComponent<RoleListInfoMerchant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().dealer.GetComponent<RoleListInfoDealer >().productTF.gameObject.SetActive(false);

           break;     
            case 2:
                //装备
                Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(true);
                //先后前
                Panel_TradeSetting.transform.Find("MoneyFirst").GetComponent<Button>(). enabled= false;
                Panel_TradeSetting.transform.Find("MoneyLast").GetComponent<Button>(). enabled = false;
                Panel_TradeSetting.transform.Find("MoneyFirst").GetChild(0).GetComponent<Text>().text = "???";
                Panel_TradeSetting.transform.Find("MoneyLast").GetChild(0).GetComponent<Text>().text = "???";
               // 风险交易成本
                Panel_Update.GetComponent<RoleUpdateInfo>().seed.GetComponent<RoleListInfoSeed>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().peasant.GetComponent<RoleListInfoPeasant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().merchant.GetComponent<RoleListInfoMerchant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().dealer.GetComponent<RoleListInfoDealer >().productTF.gameObject.SetActive(false);
                break;     
            case 3:
                //装备
                Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(true);
                //先后前
                Panel_TradeSetting.transform.Find("MoneyFirst").GetComponent<Button>(). enabled = false;
                Panel_TradeSetting.transform.Find("MoneyLast").GetComponent<Button>(). enabled = false;
                Panel_TradeSetting.transform.Find("MoneyFirst").GetChild(0).GetComponent<Text>().text = "???";
                Panel_TradeSetting.transform.Find("MoneyLast").GetChild(0).GetComponent<Text>().text = "???";
                // 风险交易成本
                Panel_Update.GetComponent<RoleUpdateInfo>().seed.GetComponent<RoleListInfoSeed>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().peasant.GetComponent<RoleListInfoPeasant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().merchant.GetComponent<RoleListInfoMerchant>().productTF.gameObject.SetActive(false);
                Panel_Update.GetComponent<RoleUpdateInfo>().dealer.GetComponent<RoleListInfoDealer >().productTF.gameObject.SetActive(false);
                break;     
            case 4:
                //装备
                Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(true);
                //先后前
                Panel_TradeSetting.transform.Find("MoneyFirst").GetComponent<Button>(). enabled = true;
                Panel_TradeSetting.transform.Find("MoneyLast").GetComponent<Button>(). enabled = true;
                Panel_TradeSetting.transform.Find("MoneyFirst").GetChild(0).GetComponent<Text>().text = "先钱";
                Panel_TradeSetting.transform.Find("MoneyLast").GetChild(0).GetComponent<Text>().text = "后钱";
                // 风险交易成本
                Panel_Update.GetComponent<RoleUpdateInfo>().seed.GetComponent<RoleListInfoSeed>().productTF.gameObject.SetActive(true);
                Panel_Update.GetComponent<RoleUpdateInfo>().peasant.GetComponent<RoleListInfoPeasant>().productTF.gameObject.SetActive(true);
                Panel_Update.GetComponent<RoleUpdateInfo>().merchant.GetComponent<RoleListInfoMerchant>().productTF.gameObject.SetActive(true);
                Panel_Update.GetComponent<RoleUpdateInfo>().dealer.GetComponent<RoleListInfoDealer >().productTF.gameObject.SetActive(true);
                break;    
            default:
                //装备
                Panel_Update.GetComponent<RoleUpdateInfo>().changeRoleButton.gameObject.SetActive(true);
                //先后前
                Panel_TradeSetting.transform.Find("MoneyFirst").GetComponent<Button>(). enabled = true;
                Panel_TradeSetting.transform.Find("MoneyLast").GetComponent<Button>(). enabled= true;
                Panel_TradeSetting.transform.Find("MoneyFirst").GetChild(0).GetComponent<Text>().text = "先钱";
                Panel_TradeSetting.transform.Find("MoneyLast").GetChild(0).GetComponent<Text>().text = "后钱";
                // 风险交易成本
                Panel_Update.GetComponent<RoleUpdateInfo>().seed.GetComponent<RoleListInfoSeed>().productTF.gameObject.SetActive(true);
                Panel_Update.GetComponent<RoleUpdateInfo>().peasant.GetComponent<RoleListInfoPeasant>().productTF.gameObject.SetActive(true);
                Panel_Update.GetComponent<RoleUpdateInfo>().merchant.GetComponent<RoleListInfoMerchant>().productTF.gameObject.SetActive(true);
                Panel_Update.GetComponent<RoleUpdateInfo>().dealer.GetComponent<RoleListInfoDealer >().productTF.gameObject.SetActive(true);
                break;    

                
        }
          
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("FTE_0-1") && !SceneManager.GetActiveScene().name.Equals("FTE_0-2"))
        {
            if(GuideManager.My.currentGuideIndex != -1)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (DOTween.defaultAutoPlay != AutoPlay.None)
                {
                    GamePause();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bool isOpenSetting = true;
                foreach (GameObject go in panelList)
                {
                    if (go.activeSelf)
                    {
                        go.SetActive(false);
                        isOpenSetting = false;
                    }
                }
                if (isOpenSetting)
                {
                    Panel_Option.SetActive(true);
                    OptionsPanel.My.ShowOPtionsPanel();
                }
            }
        }
    }

    /// <summary>
    /// 初始化三个时间控制按钮
    /// </summary>
    public void InitTimeButton()
    {
        Button_Pause.onClick.AddListener(()=>{
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.TimeScaleChange);
            DataUploadManager.My.AddData(DataEnum.时间_暂停次数);
            //GamePause();
        });
        Button_Normal.onClick.AddListener(()=> {
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.TimeScaleChange);
            //GameNormal();
        });
        Button_Accelerate.onClick.AddListener(()=> {
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.TimeScaleChange);
            //GameAccelerate();
        });
        Button_Normal.interactable = false;
        //if (!PlayerData.My.isSOLO && PlayerData.My.creatRole != PlayerData.My.playerDutyID)
        //    return;
        //GameNormal();
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void GamePause(bool isCount = true)
    {
        Debug.Log("game pause");
        DOTween.PauseAll();
        DOTween.defaultAutoPlay = AutoPlay.None;
        Button_Pause.interactable = false;
        Button_Normal.interactable = true;
        Button_Accelerate.interactable = true;
        //MessageManager.my.RpcGamePause();
        if (isCount)
            InvokeRepeating("CountPauseTime", 1f, 1f);
        else
            CancelInvoke("CountPauseTime");
        if (!PlayerData.My.isSOLO)
        {
            string str = "ChangeTimeScale|";
            str += "0";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str);
            }
        }
    }

    /// <summary>
    /// 游戏正常速度
    /// </summary>
    public void GameNormal()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 1f;
        DOTween.defaultAutoPlay = AutoPlay.All;
        //MessageManager.my.RpcGameNormal();
        Button_Pause.interactable = true;
        Button_Normal.interactable = false;
        Button_Accelerate.interactable = true;
        CancelInvoke("CountPauseTime");
        Debug.Log("Pause");
        if (!PlayerData.My.isSOLO)
        {
            string str = "ChangeTimeScale|";
            str += "1";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str);
            }
        }
    }

    /// <summary>
    /// 游戏加速
    /// </summary>
    public void GameAccelerate()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 2f;
        DOTween.defaultAutoPlay = AutoPlay.All;
        //MessageManager.my.RpcGameAccerlarate();
        Button_Pause.interactable = true;
        Button_Normal.interactable = true;
        Button_Accelerate.interactable = false;
        CancelInvoke("CountPauseTime");
        if (!PlayerData.My.isSOLO)
        {
            string str = "ChangeTimeScale|";
            str += "2";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str);
            }
        }
    }

    /// <summary>
    /// 统计暂停时间
    /// </summary>
    public void CountPauseTime()
    {
        StageGoal.My.totalPauseTime++;
    }

    /// <summary>
    /// 检测当前界面是否可以穿透panel
    /// </summary>
    public bool NeedRayCastPanel()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0" && GuideMgr.My.isOnGuide)
        {
            return true;
        }
        for (int i = 0; i <needReycastTargetPanel.Count; i++)
        {
            if (needReycastTargetPanel[i].activeSelf)
            {
                //print(needReycastTargetPanel[i].name);
                return true;
            }
        }
        return false;
    }

    [HideInInspector]
    public bool isChange = false;

    /// <summary>
    /// 发起交易
    /// </summary>
    /// <param name="_startRole"></param>
    public void CreateTrade(BaseMapRole _startRole)
    {
        startRole = _startRole;
        isSetTrade = true;
        CreateTradeLineGo.gameObject.SetActive(true);
        CreateTradeLineGo.GetComponent<CreateTradeLine>().InitPos(startRole.tradePoint);
        AudioManager.My.PlaySelectType(GameEnum.AudioClipType.StartTrade);
        if (isTradeButtonActive)
        {
            isChange = true;
            HideAllTradeButton();
        }
        foreach (BaseMapRole role in PlayerData.My.MapRole)
        {
            if (role.baseRoleData.ID != startRole.baseRoleData.ID)
            {
                role.LightOn(startRole);
            }
        }
    }

    /// <summary>
    /// 创建交易面板
    /// </summary>
    public void InitCreateTradePanel()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(TradeManager.My.transform);
        go.GetComponent<TradeSign>().Init(startRole.baseRoleData.ID.ToString(), endRole.baseRoleData.ID.ToString());
        TradeManager.My.CreateTradeRecord(go.GetComponent<TradeSign>());
        if (!PlayerData.My.isSOLO)
        {
            string str1 = "CreateTrade|";
            str1 += startRole.baseRoleData.ID.ToString() + "," + endRole.baseRoleData.ID.ToString();
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
        }
        //Panel_CreateTrade.SetActive(true);
        //CreateTradeManager.My.Open(go);
        isSetTrade = false;
        DataUploadManager.My.AddData(DataEnum.交易_建交易);
        //CreateTradeLineGo.SetActive(false);
    }

    public void OpenDeletePanel()
    {
        Panel_Delete.SetActive(true);
       // Panel_Delete.GetComponent<DeleteUIManager>().Init();
    }

    public void LostConfirm()
    {
        lose.SetActive(false);
        PlayerData.My.Reset();
        SceneManager.LoadScene("Map");
        if (PlayerData.My.isSOLO)
        {
            NetworkMgr.My.SetPlayerStatus("Map", "");
        }
        else
        {
            if (PlayerData.My.isServer)
            {
                NetworkMgr.My.SetPlayerStatus("Map", NetworkMgr.My.currentBattleTeamAcount.teamID);
            }
        }
    }

    public bool isTradeButtonActive = true;

    public bool isProductLineActive = true;

    public bool isInfoLineActive = true;

    /// <summary>
    /// 隐藏所有角色头上的创建交易按钮
    /// </summary>
    public void HideAllTradeButton()
    {
        isTradeButtonActive = !isTradeButtonActive;
        foreach (BaseMapRole role in PlayerData.My.MapRole)
        {
            if (isSetTrade)
            {
                if (role == startRole)
                    continue;
            }
            if (role.isNpc)
            {
                if (!role.npcScript.isCanSee)
                {
                    continue;
                }
            }
            role.LightOff();
            role.HideTradeButton(isTradeButtonActive);
        }
    }

    public void ShowAllTradeButton()
    {
        foreach (BaseMapRole role in PlayerData.My.MapRole)
        {
            if (isSetTrade)
            {
                if (role == startRole)
                    continue;
            }
            if (role.isNpc)
            {
                if (!role.npcScript.isCanSee)
                {
                    continue;
                }
            }
            role.LightOff();
            role.HideTradeButton(true);
        }

        foreach (TradeSign sign in TradeManager.My.tradeList.Values)
        {
            if (sign.GetComponentInChildren<TradeLineItem>() != null)
                sign.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏所有物流线
    /// </summary>
    public void HideAllProductLine()
    {
        isProductLineActive = !isProductLineActive;
        foreach (TradeSign sign in TradeManager.My.tradeList.Values)
        {
            if (sign.GetComponentInChildren<TradeLineItem>() != null)
                sign.gameObject.SetActive(isProductLineActive);
        }
    }

    /// <summary>
    /// 隐藏所有信息流线
    /// </summary>
    public void HideAllInfoLine()
    {
        isInfoLineActive = !isInfoLineActive;
        foreach (TradeSign sign in TradeManager.My.tradeList.Values)
        {
            if (sign.GetComponentInChildren<LineRenderer>() != null)
                sign.gameObject.SetActive(isInfoLineActive);
        }
    }

    private bool isPlaying = false;

    public List<GameObject> hideList = new List<GameObject>();

    /// <summary>
    /// 开关显示3个隐藏按钮
    /// </summary>
    public void ToggleHidePanelShow(bool isRapid = false)
    {
        if (isPlaying)
        {
            return;
        }
        if (GuideManager.My.currentGuideIndex != -1)
        {
            return;
        }
        if (hidePanel.GetComponent<Image>().fillAmount >= 0.99f)
        {
            isPlaying = true;
            hidePanel.GetComponent<Image>().DOFillAmount(0.25f, 0.2f).Play().SetEase(Ease.Linear).OnComplete(() => {
                isPlaying = false;
                foreach (GameObject go in hideList)
                {
                    go.SetActive(false);
                }
            });
        }
        else
        {
            isPlaying = true;

            hidePanel.GetComponent<Image>().DOFillAmount(1f, 0.2f).Play().SetEase(Ease.Linear).OnComplete(() => {
                isPlaying = false;
            });
            foreach (GameObject go in hideList)
            {
                go.SetActive(true);
            }
        }
    }

    public void ToggleHidePanelShow()
    {
        if (GuideManager.My.currentGuideIndex != -1)
        {
            
            return;
        }
        if (isPlaying)
        {
            return;
        }
        if (hidePanel.GetComponent<Image>().fillAmount >= 0.99f)
        {
            isPlaying = true;
            hidePanel.GetComponent<Image>().DOFillAmount(0.25f, 0.2f).Play().SetEase(Ease.Linear).OnComplete(() => {
                isPlaying = false;
                foreach (GameObject go in hideList)
                {
                    go.SetActive(false);
                }
            });
        }
        else
        {
            isPlaying = true;

            hidePanel.GetComponent<Image>().DOFillAmount(1f, 0.2f).Play().SetEase(Ease.Linear).OnComplete(() => {
                isPlaying = false;
            });
            foreach (GameObject go in hideList)
            {
                go.SetActive(true);
            }
        }
    }

    private bool isStart = false;

    public Color lowHealthColor;

    public void StartLowHealth()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "FTE_Record" || sceneName == "FTE_0-1" || sceneName == "FTE_0-2")
        {
            return;
        }
        if (!isStart)
        {
            InvokeRepeating("ShowLowHealthTip", 1f, 2f);
            isStart = true;
        }
    }

    public void ShowLowHealthTip()
    {
        CameraPlay.Hit(new Color(1f, 0.1367925f, 0.1367925f,0f), 2f);
    }

    public void EndLowHealth()
    {
        CancelInvoke("ShowLowHealthTip");
        isStart = false;
    }
}
