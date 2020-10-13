using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        loadSceneName = str;
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
        RoleType type = (RoleType) Enum.Parse(typeof(RoleType), args[0]);
        Role tempRole = new Role();
        int x = int.Parse(args[3]);
        int y = int.Parse(args[4]);
        tempRole.baseRoleData = GameDataMgr.My.GetModelData(type, 1);
        tempRole.ID = double.Parse(args[2]);
        tempRole.baseRoleData.roleName = args[1];

        GameObject role = Instantiate(Resources.Load<GameObject>("Prefabs/Role/" + args[0] + "_1"),
            NewCanvasUI.My.RoleTF.transform);
        role.name = tempRole.ID.ToString();
        role.GetComponent<BaseMapRole>().baseRoleData = new Role();
        role.GetComponent<BaseMapRole>().baseRoleData = tempRole;
        role.transform.position = MapManager.My.GetMapSignByXY(x, y).transform.position + new Vector3(0f, 0.3f, 0f);
        PlayerData.My.RoleData.Add(role.GetComponent<BaseMapRole>().baseRoleData);
        PlayerData.My.MapRole.Add(role.GetComponent<BaseMapRole>());
        MapManager.My.SetLand(x, y, role.GetComponent<BaseMapRole>());
        role.GetComponent<BaseMapRole>().baseRoleData.inMap = true;
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
        switch (select)
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
    /// 升级角色
    /// </summary>
    /// <param name="str"></param>
    public void UpdateRole(string str)
    {
        double roleId = double.Parse(str.Split(',')[0]);
        Role target = PlayerData.My.GetRoleById(roleId);
        target.baseRoleData = GameDataMgr.My.GetModelData(
            target.baseRoleData.roleType,
            target.baseRoleData.level + 1);
        target.CalculateAllAttribute();
        target.baseRoleData.roleName = str.Split(',')[1];
        PlayerData.My.GetMapRoleById(target.ID).RecalculateEncourageLevel();
        PlayerData.My.GetMapRoleById(target.ID).ResetAllBuff();
    }

    /// <summary>
    /// 修改装备和工人
    /// </summary>
    public void UpdateRoleEquipAndWorker(string str)
    {
        double roleId = double.Parse(str.Split(',')[0]);
        List<string> qeuip = str.Split(',')[0].Split('&')[0].Split('_').ToList();
        List<string> worker = str.Split(',')[0].Split('&')[1].Split('_').ToList();
        Role target = PlayerData.My.GetRoleById(roleId);
        for (int i = 0; i < qeuip.Count; i++)
        {
            target.EquipList.Add(int.Parse(qeuip[i]), new Vector3());
        }

        for (int i = 0; i < worker.Count; i++)
        {
            target.peoPleList.Add(int.Parse(worker[i]), new Vector3());
        }

        int finalEffect = target.baseRoleData.effect;
        int finalEfficiency = target.baseRoleData.efficiency;
        int finalRange = target.baseRoleData.range;
        int finalTradeCost = target.baseRoleData.tradeCost;
        int finalCost = target.baseRoleData.cost;
        int finalBulletCapacity = target.baseRoleData.bulletCapacity;
        int finalRiskResistance = target.baseRoleData.riskResistance;
        int finalTechAdd = 0;
        target.equipCost = 0;
        target.workerCost = 0;
        foreach (var i in target.EquipList)
        {
            GearData tempData = GameDataMgr.My.GetGearData(i.Key);
            finalEffect += tempData.effect;
            finalEfficiency += tempData.efficiency;
            finalRange += tempData.range;
            finalTradeCost += tempData.tradeCost;
            finalCost += tempData.cost;
            finalRiskResistance += tempData.riskResistance;
            finalBulletCapacity += tempData.bulletCapacity;
            target.equipCost += tempData.cost;
        } 
        foreach (var i in target.peoPleList)
        {
            WorkerData tempData = GameDataMgr.My.GetWorkerData(i.Key);
            finalEffect += tempData.effect;
            finalEfficiency += tempData.efficiency;
            finalRange += tempData.range;
            finalTradeCost += tempData.tradeCost;
            finalCost += tempData.cost;
            finalRiskResistance += tempData.riskResistance;
            finalBulletCapacity += tempData.bulletCapacity;
            finalTechAdd += tempData.techAdd;
            target.workerCost += tempData.cost;
        }
        
    }

    /// <summary>
    /// 改变金钱
    /// </summary>
    /// <param name="str"></param>
    public void OnGoldChange(string str)
    {
        int gold = int.Parse(str);
        StageGoal.My.playerGold = gold;
    }
    /// <summary>
    /// 改变生命值
    /// </summary>
    /// <param name="str"></param>
    public void OnHealthChange(string str)
    {
        int  health = int.Parse(str);
        StageGoal.My.playerHealth = health;
    }

    /// <summary>
    /// 改变满意度
    /// </summary>
    /// <param name="str"></param>
    public void OnPlayerSatisfyChange(string str)
    {
        int  Satisfy = int.Parse(str);
        StageGoal.My.playerSatisfy = Satisfy;
    }

    /// <summary>
    /// 改变科技
    /// </summary>
    /// <param name="str"></param>
    public void OnTechPointChange(string str)
    {
        int  Tech = int.Parse(str);
        StageGoal.My.playerTechPoint = Tech;
    }

    public void Receivemsg(string str)
    {
        string methodName = str.Split('|')[0];
        if (listeners.ContainsKey(methodName))
        {
            Debug.Log(methodName);
            string args = str.Split('|')[1];
            //actionActions.Add(listeners[methodName]);
            listNoDelayActions.Add(new NoDelayedQueueItem {action = listeners[methodName], param = args});
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

            if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) &&
                item.OperationalStatus == OperationalStatus.Up)
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
        listeners.Add("UpdateRole", UpdateRole);
        listeners.Add("UpdateRoleEquipAndWorker", UpdateRoleEquipAndWorker);
        listeners.Add("OnGoldChange", OnGoldChange);
        listeners.Add("OnPlayerSatisfyChange", OnPlayerSatisfyChange);
        listeners.Add("OnHealthChange", OnHealthChange);
        listeners.Add("OnTechPointChange", OnTechPointChange);
        
    }

    public string loadSceneName;

    public bool isLoadScene = false;

    public bool isCreateRole = false;

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
    IPv4,
    IPv6
}