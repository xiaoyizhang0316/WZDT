using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
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

    public Button Button_Pause;

    public Button Button_Normal;

    public Button Button_Accelerate;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CreateTradeLineGo = FindObjectOfType<CreateTradeLine>().gameObject;
        RoleTF = GameObject.FindGameObjectWithTag("RoleTF").transform;
        Button_Pause = transform.Find("TimeScale/GamePause").GetComponent<Button>();
        Button_Normal = transform.Find("TimeScale/GameNormal").GetComponent<Button>();
        Button_Accelerate = transform.Find("TimeScale/GameAccelerate").GetComponent<Button>();
        InitTimeButton();
        Panel_Delete.SetActive(false);
        lose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitTimeButton()
    {
        Button_Pause.onClick.AddListener(GamePause);
        Button_Normal.onClick.AddListener(GameNormal);
        Button_Accelerate.onClick.AddListener(GameAccelerate);
        GameNormal();
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void GamePause()
    {
        //Time.timeScale = 0.01f;
        //Time.fixedDeltaTime = 0.0005f;
        DOTween.PauseAll();
        DOTween.defaultAutoPlay = AutoPlay.None;
        Button_Pause.interactable = false;
        Button_Normal.interactable = true;
        Button_Accelerate.interactable = true;
    }

    /// <summary>
    /// 游戏正常速度
    /// </summary>
    public void GameNormal()
    {
        //Time.timeScale = 1f;
        //Time.timeScale = 1f;
        DOTween.PlayAll();
        DOTween.timeScale = 1f;
        DOTween.defaultAutoPlay = AutoPlay.All;
        Button_Pause.interactable = true;
        Button_Normal.interactable = false;
        Button_Accelerate.interactable = true;
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
    }

    /// <summary>
    /// 检测当前界面是否可以穿透panel
    /// </summary>
    public bool NeedRayCastPanel()
    {
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

    public void CreateTrade(BaseMapRole _startRole)
    {
        startRole = _startRole;
        isSetTrade = true;
        CreateTradeLineGo.gameObject.SetActive(true);
        CreateTradeLineGo.GetComponent<CreateTradeLine>().InitPos(startRole.transform);
    }

    /// <summary>
    /// 创建交易面板
    /// </summary>
    public void InitCreateTradePanel()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(TradeManager.My.transform);
        go.GetComponent<TradeSign>().Init(startRole.baseRoleData.ID.ToString(), endRole.baseRoleData.ID.ToString());
        //Panel_CreateTrade.SetActive(true);
        //CreateTradeManager.My.Open(go);
        isSetTrade = false;
        //CreateTradeLineGo.SetActive(false);
    }

    /// <summary>
    /// 打开并初始化交易面板
    /// </summary>
    public void OpenTradePanel()
    {
        Panel_TradeSetting.SetActive(true);
        GameObject go = FindObjectOfType<TradeSign>().gameObject;
        Panel_TradeSetting.GetComponent<CreateTradeManager>().Open(go);
    }


    public void OpenDeletePanel()
    {
        Panel_Delete.SetActive(true);
       // Panel_Delete.GetComponent<DeleteUIManager>().Init();
    }

    public void LostConfirm()
    {
        lose.SetActive(false);
    }
}
