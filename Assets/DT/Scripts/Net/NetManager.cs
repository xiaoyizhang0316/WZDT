﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnum;

public class NetManager : MonoSingleton<NetManager>
{
    struct NoDelayedQueueItem
    {
        public Action<string> action;
        public string param;
    }

    List<NoDelayedQueueItem> listNoDelayActions = new List<NoDelayedQueueItem>();

    public Action<string> msgAction;

    public Dictionary<string, Action<string>> listeners;

    /// <summary>
    /// 切换场景同步调用
    /// </summary>
    /// <param name="str"></param>
    public void OnLoadScene(string str)
    {
        SceneManager.LoadScene(str);
        PlayerData.My.Reset();
    }

    /// <summary>
    /// 创建角色同步调用
    /// </summary>
    /// <param name="str"></param>
    public void OnCreateRole(string str)
    {
        string[] args = str.Split(',');
        RoleType type = (RoleType)Enum.Parse(typeof(RoleType), args[0]);
        Role tempRole = new Role();
        int x = int.Parse(args[3]);
        int y = int.Parse(args[4]);
        tempRole.baseRoleData = GameDataMgr.My.GetModelData(type, 1);
        tempRole.ID = double.Parse(args[2]);
        tempRole.baseRoleData.roleName = args[1];
        GameObject role = Instantiate(Resources.Load<GameObject>("Prefabs/Role/" + args[0] + "_1"), NewCanvasUI.My.RoleTF.transform);
        role.name = tempRole.ID.ToString();
        role.GetComponent<BaseMapRole>().baseRoleData = new Role();
        role.GetComponent<BaseMapRole>().baseRoleData = tempRole;
        role.transform.position = MapManager.My.GetMapSignByXY(x, y).transform.position + new Vector3(0f, 0.3f, 0f);
        PlayerData.My.RoleData.Add(role.GetComponent<BaseMapRole>().baseRoleData);
        PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
        MapManager.My.SetLand(x, y, role.GetComponent<BaseMapRole>());
        role.GetComponent<BaseMapRole>().baseRoleData.inMap = true;
        StageGoal.My.CostTechPoint(role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.costTech);
        StageGoal.My.CostTp(role.GetComponent<BaseMapRole>().baseRoleData.baseRoleData.costTech,CostTpType.Build);
    }

    /// <summary>
    /// 删除角色调用
    /// </summary>
    /// <param name="str"></param>
    public void OnDeleteRole(string str)
    {
        double roleId = double.Parse(str);
        Role target = PlayerData.My.GetRoleById(roleId);
        foreach (var v in target.EquipList)
        {
            PlayerData.My.SetGearStatus(v.Key, false);
        }
        foreach (var v in target.peoPleList)
        {
            PlayerData.My.SetWorkerStatus(v.Key, false);
        }
        TradeManager.My.DeleteRoleAllTrade(roleId);
        BaseMapRole mapRole = PlayerData.My.GetMapRoleById(roleId);
        if (StageGoal.My.timeCount - mapRole.putTime <= 5)
            StageGoal.My.GetTechPoint(target.baseRoleData.costTech);
        PlayerData.My.RoleData.Remove(target);
        PlayerData.My.MapRole.Remove(mapRole);
        MapManager.My.ReleaseLand(mapRole.posX, mapRole.posY);
        Destroy(mapRole.gameObject);
    }

    /// <summary>
    /// 变更时间速率同步调用
    /// </summary>
    /// <param name="str"></param>
    public void OnChangeTimeScale(string str)
    {
        int select = int.Parse(str);
        switch(select)
        {
            case 0:
                DOTween.PauseAll();
                DOTween.defaultAutoPlay = AutoPlay.None;
                break;
            case 1:
                DOTween.PlayAll();
                DOTween.timeScale = 1f;
                DOTween.defaultAutoPlay = AutoPlay.All;
                break;
            case 2:
                DOTween.PlayAll();
                DOTween.timeScale = 2f;
                DOTween.defaultAutoPlay = AutoPlay.All;
                break;
            default:
                DOTween.PlayAll();
                DOTween.timeScale = 1f;
                DOTween.defaultAutoPlay = AutoPlay.All;
                break;
        }
    }

    /// <summary>
    /// 使用三镜调用
    /// </summary>
    /// <param name="str"></param>
    public void OnUseThreeMirror(string str)
    {
        int option = int.Parse(str.Split(',')[0]);
        switch (option)
        {
            case 0:
                {
                    double id = double.Parse(str.Split(',')[1]);
                    BaseMapRole role = PlayerData.My.GetMapRoleById(id);
                    role.npcScript.DetectNPCRole();
                    int techNumber = int.Parse(str.Split(',')[2]);
                    StageGoal.My.CostTechPoint(techNumber);
                    StageGoal.My.CostTp(techNumber, CostTpType.Mirror);
                    break;
                }
            case 1:
                {
                    double id = double.Parse(str.Split(',')[1]);
                    BaseMapRole role = PlayerData.My.GetMapRoleById(id);
                    role.npcScript.isCanSeeEquip = true;
                    int techNumber = int.Parse(str.Split(',')[2]);
                    StageGoal.My.CostTechPoint(techNumber);
                    StageGoal.My.CostTp(techNumber, CostTpType.Mirror);
                    break;
                }
            case 2:
                {
                    int id = int.Parse(str.Split(',')[1]);
                    BuildingManager.My.GetBuildingByIndex(id).UseTSJ();
                    int techNumber = int.Parse(str.Split(',')[2]);
                    StageGoal.My.CostTechPoint(techNumber);
                    StageGoal.My.CostTp(techNumber, CostTpType.Mirror);
                    break;
                }
            default:
                break;
        }
    }

    #region 交易  

    /// <summary>
    /// 创建交易调用
    /// </summary>
    /// <param name="str"></param>
    public void OnCreateTrade(string str)
    {
        string start = str.Split(',')[0];
        string end = str.Split(',')[1];
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Trade/TradeSign"));
        go.transform.SetParent(TradeManager.My.transform);
        go.GetComponent<TradeSign>().Init(start, end);
    }

    /// <summary>
    /// 删除交易调用
    /// </summary>
    /// <param name="str"></param>
    public void OnDeleteTrade(string str)
    {
        int ID = int.Parse(str);
        if (TradeManager.My.tradeList.ContainsKey(ID))
        {
            TradeSign temp = TradeManager.My.tradeList[ID];
            TradeManager.My.tradeList.Remove(ID);
            temp.ClearAllLine();
            Destroy(temp.gameObject, 0f);
            if (NewCanvasUI.My.Panel_TradeSetting.activeSelf)
                CreateTradeManager.My.Close();
        }
    }

    /// <summary>
    /// 配置交易调用
    /// </summary>
    public void OnChangeTrade(string str)
    {
        int Id = int.Parse(str.Split(',')[0]);
        CashFlowType cashFlow = (CashFlowType)Enum.Parse(typeof(CashFlowType), str.Split(',')[1]);
        int dividePer = int.Parse(str.Split(',')[2]);
        float startP = float.Parse(str.Split(',')[3]);
        float endP = float.Parse(str.Split(',')[4]);
        if (TradeManager.My.tradeList.ContainsKey(Id))
        {
            TradeManager.My.tradeList[Id].tradeData.selectCashFlow = cashFlow;
            TradeManager.My.tradeList[Id].tradeData.dividePercent = dividePer;
            TradeManager.My.tradeList[Id].startPer = startP;
            TradeManager.My.tradeList[Id].endPer = endP;
            TradeManager.My.tradeList[Id].UpdateEncourageLevel();
        }
    }

    #endregion

    public void Receivemsg(string str)
    {
        string methodName = str.Split('|')[0];
        if (listeners.ContainsKey(methodName))
        {
            Debug.Log(methodName);
            string args = str.Split('|')[1];
            //actionActions.Add(listeners[methodName]);
            listNoDelayActions.Add(new NoDelayedQueueItem { action = listeners[methodName], param = args });
        }
    }

    public string GetIP(ADDRESSFAM Addfam)
    {
        //Return null if ADDRESSFAM is Ipv6 but Os does not support it
        if (Addfam == ADDRESSFAM.IPv6 && !Socket.OSSupportsIPv6)
        {
            return null;
        }

        string output = "";

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
            NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
#endif 
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    //IPv4
                    if (Addfam == ADDRESSFAM.IPv4)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }

                    //IPv6
                    else if (Addfam == ADDRESSFAM.IPv6)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
        }
        return output;
    }

    public void Init()
    {
        Debug.Log("construct");
        listeners = new Dictionary<string, Action<string>>();
        listeners.Add("LoadScene", OnLoadScene);
        listeners.Add("CreateRole", OnCreateRole);
        listeners.Add("ChangeTimeScale", OnChangeTimeScale);
        listeners.Add("DeleteRole", OnDeleteRole);
        listeners.Add("CreateTrade", OnCreateTrade);
        listeners.Add("DeleteTrade",OnDeleteTrade);
        listeners.Add("ChangeTrade", OnChangeTrade);
    }

    private void Update()
    {
        if (listNoDelayActions.Count > 0)
        {
            try
            {
                listNoDelayActions[0].action(listNoDelayActions[0].param);
                listNoDelayActions.RemoveAt(0);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

        }
    }

    //void OnGUI()
    //{
    //    if (GUILayout.Button("load"))
    //    {
    //        listeners["LoadScene"]("FTE_1");
    //    }
    //}
}


public enum ADDRESSFAM
{
    IPv4, IPv6
}

