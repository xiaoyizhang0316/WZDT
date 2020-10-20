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
        SceneManager.LoadScene(str);
        PlayerData.My.Reset();
    }

    #region 角色

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
        role.GetComponent<BaseMapRole>().MonthlyCost();
        role.GetComponent<BaseMapRole>().AddTechPoint();
        role.GetComponent<BaseMapRole>().roleSprite = role.GetComponentInChildren<RoleSprite>();
        role.GetComponent<BaseMapRole>().roleSprite.ActiveRoleEffect(0);
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

    /// 升级角色
    /// </summary>
    /// <param name="str"></param>
    public void UpdateRole(string str)
    {
        double roleId = double.Parse(str.Split(',')[0]);
        Role target = PlayerData.My.GetRoleById(roleId);
        StageGoal.My.CostPlayerGold(target.baseRoleData.upgradeCost);
        StageGoal.My.Expend(target.baseRoleData.upgradeCost, ExpendType.AdditionalCosts, null, "升级");
        target.baseRoleData = GameDataMgr.My.GetModelData(
            target.baseRoleData.roleType,
            target.baseRoleData.level + 1);
        target.CalculateAllAttribute();
        target.baseRoleData.roleName = str.Split(',')[1];
        PlayerData.My.GetMapRoleById(target.ID).RecalculateEncourageLevel();
        PlayerData.My.GetMapRoleById(target.ID).ResetAllBuff();
        PlayerData.My.GetMapRoleById(target.ID).roleSprite.ActiveRoleEffect(2);
    }

    /// <summary>
    /// 修改装备和工人
    /// </summary>
    public void UpdateRoleEquipAndWorker(string str)
    {
        double roleId = double.Parse(str.Split(',')[0]);
        List<string> qeuip = str.Split(',')[1].Split('&')[0].Split('_').ToList();
        List<string> worker = str.Split(',')[1].Split('&')[1].Split('_').ToList();
        Role target = PlayerData.My.GetRoleById(roleId);
        target.EquipList.Clear();
        target.peoPleList.Clear();
        for (int i = 0; i < qeuip.Count; i++)
        {
            if (qeuip[i].Length == 0)
            {
                break;
            }
            target.EquipList.Add(int.Parse(qeuip[i]), new Vector3());
        }

        for (int i = 0; i < worker.Count; i++)
        {
            if (worker[i].Length == 0)
            {
                break;
            }
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
        target.effect = finalEffect;
        target.efficiency = finalEfficiency;
        target.riskResistance = finalRiskResistance;
        target.range = finalRange;
        target.cost = finalCost;
        target.tradeCost = finalTradeCost;
        target.bulletCapacity = finalBulletCapacity;
        target.techAdd = finalTechAdd;
        PlayerData.My.GetMapRoleById(target.ID).ResetAllBuff();
        PlayerData.My.GetMapRoleById(target.ID).roleSprite.ActiveRoleEffect(1);
    }

    /// <summary>
    /// 解锁角色时调用
    /// </summary>
    /// <param name="str"></param>
    public void OnUnlockRole(string str)
    {
        double id = double.Parse(str);
        BaseMapRole role = PlayerData.My.GetMapRoleById(id);
        role.npcScript.isLock = false;
        StageGoal.My.CostTp(role.npcScript.lockNumber, CostTpType.Unlock);
        FloatInfoManager.My.TechChange(0 - role.npcScript.lockNumber);
        StageGoal.My.playerTechPoint -= role.npcScript.lockNumber;
        StageGoal.My.SetInfo();
    }

    #endregion

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

    public void OnOpenGuide(string str)
    {
        bool active = bool.Parse(str);
        NewCanvasUI.My.watchGuidePanel.SetActive(active);
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

    #region 消费者

    /// <summary>
    /// 消费者死亡回调
    /// </summary>
    /// <param name="str"></param>
    public void OnConsumerDead(string str)
    {
        int buildingId = int.Parse(str.Split(',')[0]);
        int consumerIndex = int.Parse(str.Split(',')[1]);
        int score = int.Parse(str.Split(',')[2]);
        int gold = int.Parse(str.Split(',')[3]);
        Building building = BuildingManager.My.GetBuildingByIndex(buildingId);
        building.consumeSigns[consumerIndex].Stop();
        building.consumeSigns[consumerIndex].spriteLogo.GetComponent<SpriteRenderer>().color = Color.green;
        Destroy(building.consumeSigns[consumerIndex].gameObject, 0.5f);
        StageGoal.My.GetSatisfy(score);
        StageGoal.My.GetPlayerGold(gold);
        StageGoal.My.Income(gold, IncomeType.Consume);
        StageGoal.My.killNumber++;
    }

    /// <summary>
    /// 消费者移动速度变化回调
    /// </summary>
    /// <param name="str"></param>
    public void OnConsumerChangeSpeed(string str)
    {
        int id = int.Parse(str.Split(',')[0]);
        float speedAdd = float.Parse(str.Split(',')[1]);
        ConsumeSign[] signs = FindObjectsOfType<ConsumeSign>();
        for (int i = 0; i < signs.Length; i++)
        {
            if (signs[i].gameObject.GetInstanceID() == id)
            {
                signs[i].tweener.timeScale += speedAdd;
                break;
            }
        }
    }

    #endregion

    /// <summary>
    /// 改变金钱
    /// </summary>
    /// <param name="str"></param>
    public void OnGoldChange(string str)
    {
        int gold = int.Parse(str);
        if (gold >= 0)
        {
            StageGoal.My.GetPlayerGold(gold);
        }
        else
        {
            StageGoal.My.CostPlayerGold(0 - gold);
        }
    }
    /// <summary>
    /// 改变生命值
    /// </summary>
    /// <param name="str"></param>
    public void OnHealthChange(string str)
    {
        int  health = int.Parse(str);
        StageGoal.My.LostHealth(health);
    }

    /// <summary>
    /// 改变满意度
    /// </summary>
    /// <param name="str"></param>
    public void OnPlayerSatisfyChange(string str)
    {
        int  Satisfy = int.Parse(str);
        StageGoal.My.GetSatisfy(Satisfy);
    }

    /// <summary>
    /// 改变科技
    /// </summary>
    /// <param name="str"></param>
    public void OnTechPointChange(string str)
    {
        int  Tech = int.Parse(str);
        if (Tech >= 0)
        {
            StageGoal.My.GetTechPoint(Tech);
        }
        else
        {
            StageGoal.My.CostTechPoint(0 - Tech);
        }
    }

    public void OnGameReady(string str)
    {
        BaseLevelController.My.isAllReady = true;
        BaseLevelController.My.CheckGameStart();
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


    /// <summary>
    /// 确定职责
    /// </summary>
    public void ConfirmDuty(string str)
    {
       PlayerData.My.InitPlayerRightControl(str.Split(',')[0],str.Split(',')[1],str.Split(',')[2],str.Split(',')[3],str.Split(',')[4],str.Split(',')[5],str.Split(',')[6],str.Split(',')[7]);
    }


    /// <summary>
    /// 获得玩家名称
    /// </summary>
    /// <param name="str"></param>
    public void GetUserName(string str)
    {
        MissionConfirm.My.subName = str;
     
    }

    /// <summary>
    /// 打开职责确认UI
    /// </summary>
    public void OpenDutyConfirmUI(string str)
    {
        LevelInfoManager.My.missionConfirm.SetActive(true);
    }

    public void UpdateDutyUI(string str)
    {
        MissionConfirm.My.InitName();
    }

    public void OnClientReady(string str)
    {
        if (str == "1")
        {
            MissionConfirm.My.subReady = true;
        }
        else
        {
            MissionConfirm.My.subReady = false;
        }
    }

    /// <summary>
    /// 发送表情函数回调
    /// </summary>
    /// <param name="str"></param>
    public void OnGenerateEmoji(string str)
    {
        float x = float.Parse(str.Split(',')[0]);
        float y = float.Parse(str.Split(',')[1]);
        float z = float.Parse(str.Split(',')[2]);
        BaseLevelController.My.GenerateEmoji(new Vector3(x, y, z));
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
        listeners.Add("DeleteRole", OnDeleteRole);
        listeners.Add("CreateTrade", OnCreateTrade);
        listeners.Add("DeleteTrade",OnDeleteTrade);
        listeners.Add("ChangeTrade", OnChangeTrade);
        listeners.Add("UseThreeMirror", OnUseThreeMirror);
        listeners.Add("UpdateRole", UpdateRole);
        listeners.Add("UpdateRoleEquipAndWorker", UpdateRoleEquipAndWorker);
        listeners.Add("OnGoldChange", OnGoldChange);
        listeners.Add("OnPlayerSatisfyChange", OnPlayerSatisfyChange);
        listeners.Add("OnHealthChange", OnHealthChange);
        listeners.Add("OnTechPointChange", OnTechPointChange);
        listeners.Add("UnlockRole", OnUnlockRole); 
        listeners.Add("ConfirmDuty", ConfirmDuty); 
        listeners.Add("ConsumerDead", OnConsumerDead);
        listeners.Add("GetUserName",GetUserName); 
        listeners.Add("OpenDutyConfirmUI",OpenDutyConfirmUI); 
        listeners.Add("UpdateDutyUI",UpdateDutyUI); 
        listeners.Add("OnReady",OnClientReady);
        listeners.Add("OnGameReady", OnGameReady);
        listeners.Add("Emoji", OnGenerateEmoji);
        listeners.Add("OpenGuide", OnOpenGuide);
    }

    private void Update()
    {
        if (listNoDelayActions.Count > 0)
        {
            //try
            {
                listNoDelayActions[0].action(listNoDelayActions[0].param);
                listNoDelayActions.RemoveAt(0);
            }
            //catch (Exception ex)
            //{
            //    Debug.Log(ex.Message);
            //}
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