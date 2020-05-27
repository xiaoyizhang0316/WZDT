using System;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    /// <summary>
    /// 创建角色Panel
    /// </summary>
    public GameObject Panel_AssemblyRole;

    public GameObject Panel_ChoseRole;

    /// <summary>
    /// 交易配置Panel
    /// </summary>
    public GameObject Panel_CreateTrade;


    /// <summary>
    /// 角色列表物体创建预制体
    /// </summary>
    public GameObject roleListSignOBJ;

    public Transform Panel_MapRoleUI;

    public Transform MapRoleUI;
    public Transform roleListCreatPos;

    public Role CurrentClickRole;
    public BaseMapRole currentMapRole;
    public Transform Panel_RoleDetalInfo;

    /// <summary>
    /// 胜利
    /// </summary>
    public GameObject Panel_Win;
    /// <summary>
    /// 失败
    /// </summary>
    public GameObject Panel_Lose;
    /// <summary>
    /// 确认窗口GameObject
    /// </summary>
    public GameObject Panel_Confirm;

    public GameObject Button_Pause;

    public GameObject Button_Normal;

    public GameObject Button_Accelerate;

    public GameObject Panel_exchangeExecution;

    public GameObject Panel_SeedChange;
    public GameObject Panel_POPUI;
    public GameObject Panel_CreatRole;

    public GameObject Panel_POPInfo;
    public GameObject LandCube;
    public GameObject LandOBJ;
    #region 交易相关变量
    /// <summary>
    /// 是否处于设置交易状态
    /// </summary>
    public bool isSetTrade;

    /// <summary>
    /// 交易发起方
    /// </summary>
    public Role startRole;

    /// <summary>
    /// 交易承受方
    /// </summary>
    public Role endRole;

    public GameObject CreateTradeLineGo;

    #endregion

    /// <summary>
    /// 需要遮挡的UI
    /// </summary>
    public List<GameObject> needReycastTargetPanel;
    // Start is called before the first frame update
    void Start()
    {
       
        GameNormal();
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (!PlayerData.My.RoleData[i].isNpc)
                PlayerData.My.RoleData[i].inMap = false;
        }
        UpdateRoleList();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 创建玩家角色列表  刷新玩家角色列表
    /// </summary>
    public void UpdateRoleList()
    {

        for (int i = 0; i < PlayerData.My.RoleManager.Count; i++)
        {
            Destroy(PlayerData.My.RoleManager[i]);
        }
        PlayerData.My.RoleManager.Clear();
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].isNpc)
            {
                continue;
            }
            GameObject roleListSign = Instantiate(roleListSignOBJ, roleListCreatPos);
            roleListSign.GetComponent<CreatRole_Button>().RolePrb =
                Resources.Load<GameObject>(PlayerData.My.RoleData[i].baseRoleData.PrePath);
            roleListSign.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(PlayerData.My.RoleData[i].baseRoleData.SpritePath);
            roleListSign.name = PlayerData.My.RoleData[i].baseRoleData.roleType.ToString() + "_" + PlayerData.My.RoleData[i].ID;
            PlayerData.My.RoleManager.Add(roleListSign);
            if (PlayerData.My.RoleData[i].inMap)
            {
                roleListSign.GetComponent<Button>().interactable = false;
                roleListSign.GetComponent<Image>().raycastTarget = false;
            }
        }
    }

    /// <summary>
    /// 退出按钮
    /// </summary>
    public void ExitButton()
    {
        Panel_MapRoleUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 退出按钮
    /// </summary>
    public void ExitCreatRoleButton()
    {
        Panel_CreatRole.gameObject.SetActive(false);
    }
    public void ExitRoleDetalInfo()
    {

        Panel_RoleDetalInfo.gameObject.SetActive(false);
        ExitButton();
    }

    /// <summary>
    /// 编辑角色
    /// </summary>
    public void EditRole()
    {
        ExitButton();

        Panel_AssemblyRole.gameObject.SetActive(true);
        CreatRoleManager.My.Open(CurrentClickRole);
    }

    /// <summary>
    /// 显示角色详细信息
    /// </summary>
    public void ShowRoleDetalInfo()
    {
        Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().currentMapRole = currentMapRole;
      

        Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
        Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalData();
        Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalUpdate();
        Panel_RoleDetalInfo.gameObject.SetActive(true);
    }

    public void UpdateDetalInfo()
    {

    }

    /// <summary>
    /// 删除地图中的角色
    /// </summary>
    public void DeleteMapRole()
    {
        ExitButton();
        double roleId = CurrentClickRole.ID;

        POPUIManager.My.POPNormalUI("", "是否要删除当前角色", () =>
         {
             if (ExecutionManager.My.SubExecution(ExecutionManager.My.removeRole))
             {
                 TradeManager.My.DeleteRoleAllTrade(roleId);
                 PlayerData.My.GetRoleById(roleId).inMap = false;
                 int index = PlayerData.My.MapRole.IndexOf(PlayerData.My.GetMapRoleById(roleId));
                 Destroy(PlayerData.My.GetMapRoleById(roleId).gameObject, 0.01f);
                 PlayerData.My.MapRole.RemoveAt(index);
                 UpdateRoleList();
             }
         }, () => { });

    }

    /// <summary>
    /// 点击创建交易生成交易线
    /// </summary>
    public void CreateTrade()
    {
        startRole = CurrentClickRole;
        isSetTrade = true;
        BaseMapRole start = PlayerData.My.GetMapRoleById(startRole.ID);
        CreateTradeLineGo.gameObject.SetActive(true);
        CreateTradeLineGo.GetComponent<CreateTradeLine>().InitPos(start.transform);
        ExitButton();
    }

    /// <summary>
    /// 创建交易面板
    /// </summary>
    public void InitCreateTradePanel()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(TradeManager.My.transform);
        go.GetComponent<TradeSign>().Init(startRole.ID.ToString(), endRole.ID.ToString());
        //Panel_CreateTrade.SetActive(true);
        //CreateTradeManager.My.Open(go);
        isSetTrade = false;
        //CreateTradeLineGo.SetActive(false);
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void GamePause()
    {
        //Time.timeScale = 0.01f;
        //Time.fixedDeltaTime = 0.0005f;
        DOTween.PauseAll();
        Button_Pause.GetComponent<Button>().interactable = false;
        Button_Normal.GetComponent<Button>().interactable = true;
        Button_Accelerate.GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// 游戏正常速度
    /// </summary>
    public void GameNormal()
    {
        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = 0.02f;
        DOTween.PlayAll();
        DOTween.timeScale = 1f;
        Button_Pause.GetComponent<Button>().interactable = true;
        Button_Normal.GetComponent<Button>().interactable = false;
        Button_Accelerate.GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// 游戏加速
    /// </summary>
    public void GameAccelerate()
    {
        DOTween.PlayAll();
        DOTween.timeScale = 2f;
        //Time.timeScale = 2f;
        //Time.fixedDeltaTime = 0.02f;
        Button_Pause.GetComponent<Button>().interactable = true;
        Button_Normal.GetComponent<Button>().interactable = true;
        Button_Accelerate.GetComponent<Button>().interactable = false;
    }

    public void ShowExecutionPanel()
    {
        Panel_exchangeExecution.SetActive(true);

    }

    public void CloseExcutionPanel()
    {
        Panel_exchangeExecution.SetActive(false);

    }

    public void Load2Scene()
    {
        //GameMain.My.LoadScene("FTE_2");
        PlayerData.My.WinReset();
        SceneManager.LoadScene("FTE_2");
    }
    public void Load3Scene()
    {
        PlayerData.My.WinReset();
        PlayerPrefs.SetInt("FTE_2", 1);
        SceneManager.LoadScene("FTE_3");
        //SceneManager.LoadScene("GameMain");
    }
    public void Load4Scene()
    {
        PlayerData.My.WinReset();
        PlayerPrefs.SetInt("FTE_3", 1);
        SceneManager.LoadScene("FTE_4");
        //SceneManager.LoadScene( "GameMain");
    }
    public void Destroyrole()
    {
        for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
        {
            TradeManager.My.DeleteRoleAllTrade(PlayerData.My.MapRole[i].baseRoleData.ID);
            PlayerData.My.MapRole.Remove(PlayerData.My.MapRole[i]);
            UpdateRoleList();
        }

    }
    /// <summary>
    /// 点击角色显示UI
    /// </summary>
    /// <param name="target"></param>
    public void UpdateUIPosition(Transform target)
    {
        CurrentClickRole = PlayerData.My.GetRoleById(Double.Parse(target.name));
        currentMapRole = PlayerData.My.GetMapRoleById(Double.Parse(target.name));
        Vector2 mouseDown = Camera.main.WorldToScreenPoint(target.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.My.transform as RectTransform,
            mouseDown, Camera.main, out mouseUGUIPos);
        if (isRect)
        {
            UIManager.My.Panel_MapRoleUI.gameObject.SetActive(true);

            MapRoleUI.GetComponent<RectTransform>().anchoredPosition = mouseUGUIPos;
        }
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
               
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 升级当前角色
    /// </summary>
    public void UpdateRole()
    {
        string name = CurrentClickRole.baseRoleData.roleName;
        CurrentClickRole.baseRoleData = GameDataMgr.My.GetModelData(CurrentClickRole.baseRoleData.roleType,++CurrentClickRole.baseRoleData.level );
        CurrentClickRole.baseRoleData.roleName = name;
        CurrentClickRole.CalculateAllAttribute();
     ExitButton();

    }

    public void LoadCurrentScene()
    {

        PlayerData.My.Reset();
        SceneManager.LoadScene("FTE_1");
    }
}