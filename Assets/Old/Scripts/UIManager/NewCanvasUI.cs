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

    public Button Button_Pause;

    public Button Button_Normal;

    public Button Button_Accelerate;

    public Button statBtn;
    public Button OptionsBtn;

    #endregion

    public Transform hidePanel;

    // Start is called before the first frame update
    void Start()
    {
        CreateTradeLineGo = FindObjectOfType<CreateTradeLine>().gameObject;
        CreateTradeLineGo.SetActive(false);
        RoleTF = GameObject.FindGameObjectWithTag("RoleTF").transform;
        GetComponent<Canvas>().worldCamera = Camera.main;
        Button_Pause = transform.Find("TimeScale/GamePause").GetComponent<Button>();
        Button_Normal = transform.Find("TimeScale/GameNormal").GetComponent<Button>();
        Button_Accelerate = transform.Find("TimeScale/GameAccelerate").GetComponent<Button>();
        statBtn.onClick.AddListener(() =>  DataStatPanel.My.ShowStat());
        OptionsBtn.onClick.AddListener(()=>OptionsPanel.My.ShowOPtionsPanel());
        InitTimeButton();
        ToggleHidePanelShow();
        Panel_Delete.SetActive(false);
        lose.SetActive(false);
        isTradeButtonActive = true;
        isProductLineActive = true;
        isInfoLineActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化三个时间控制按钮
    /// </summary>
    public void InitTimeButton()
    {
        Button_Pause.onClick.AddListener(()=>{
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.TimeScaleChange);
            GamePause();
        });
        Button_Normal.onClick.AddListener(()=> {
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.TimeScaleChange);
            GameNormal();
        });
        Button_Accelerate.onClick.AddListener(()=> {
            AudioManager.My.PlaySelectType(GameEnum.AudioClipType.TimeScaleChange);
            GameAccelerate();
        });
        GameNormal();
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void GamePause(bool isCount = true)
    {
        DOTween.PauseAll();
        DOTween.defaultAutoPlay = AutoPlay.None;
        Button_Pause.interactable = false;
        Button_Normal.interactable = true;
        Button_Accelerate.interactable = true;
        if (isCount)
            InvokeRepeating("CountPauseTime", 1f, 1f);
        else
            CancelInvoke("CountPauseTime");
    }

    /// <summary>
    /// 游戏正常速度
    /// </summary>
    public void GameNormal()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 1f;
        DOTween.defaultAutoPlay = AutoPlay.All;
        Button_Pause.interactable = true;
        Button_Normal.interactable = false;
        Button_Accelerate.interactable = true;
        CancelInvoke("CountPauseTime");
    }

    /// <summary>
    /// 游戏加速
    /// </summary>
    public void GameAccelerate()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 2f;
        DOTween.defaultAutoPlay = AutoPlay.All;
        Button_Pause.interactable = true;
        Button_Normal.interactable = true;
        Button_Accelerate.interactable = false;
        CancelInvoke("CountPauseTime");
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
        //Panel_CreateTrade.SetActive(true);
        //CreateTradeManager.My.Open(go);
        isSetTrade = false;

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
            if (role.isNpc)
            {
                if (!role.npcScript.isCanSee)
                {
                    continue;
                }
            }
            role.HideTradeButton(isTradeButtonActive);
        }
    }

    public void HideAllProductLine()
    {
        isProductLineActive = !isProductLineActive;
        foreach (TradeSign sign in TradeManager.My.tradeList.Values)
        {
            if (sign.GetComponentInChildren<TradeLineItem>() != null)
                sign.gameObject.SetActive(isProductLineActive);
        }
    }

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

    private List<GameObject> hideList = new List<GameObject>();

    public void ToggleHidePanelShow()
    {
        if (isPlaying)
            return;
        if (hideList.Count == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                hideList.Add(hidePanel.GetChild(i).gameObject);
            }
        }
        if (hidePanel.GetComponent<Image>().fillAmount >= 0.99f)
        {
            isPlaying = true;
            hidePanel.GetComponent<Image>().DOFillAmount(0.25f,0.25f).Play().OnComplete(()=> {
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
            hidePanel.GetComponent<Image>().DOFillAmount(1f, 0.25f).Play().OnComplete(() => {
                isPlaying = false;
            });
            foreach (GameObject go in hideList)
            {
                go.SetActive(true);
            }
        }
    }
}
