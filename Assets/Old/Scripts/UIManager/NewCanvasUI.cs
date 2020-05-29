using System;
using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class NewCanvasUI : MonoSingleton<NewCanvasUI>
{
 
    public GameObject Panel_ChoseRole;
    public Role CurrentClickRole;
    public BaseMapRole currentMapRole;
    public GameObject Panel_AssemblyRole;
    public GameObject Panel_TradeSetting;
    public Transform RoleTF;
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

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CreateTradeLineGo = FindObjectOfType<CreateTradeLine>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void OpenTradePanel()
    {
        Panel_TradeSetting.SetActive(true);
        GameObject go = FindObjectOfType<TradeSign>().gameObject;
        Panel_TradeSetting.GetComponent<CreateTradeManager>().Open(go);
    }

}
