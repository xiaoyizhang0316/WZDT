using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using static GameEnum;

public class PlayerData : MonoSingletonDontDestroy<PlayerData>
{
    /// <summary>
    /// 角色数据——持久化
    /// </summary>
    public List<Role> RoleData;

    /// <summary>
    /// 当前角色列表管理
    /// </summary>
    public List<GameObject> RoleManager;

    /// <summary>
    /// 当前创建的地图的角色
    /// </summary>
    public List<BaseMapRole> MapRole;

    /// <summary>
    /// 当前玩家的物品栏里的装备
    /// </summary>
    public List<PlayerGear> playerGears = new List<PlayerGear>();

    /// <summary>
    /// 当前玩家的物品栏里的工人
    /// </summary>
    public List<PlayerWorker> playerWorkers = new List<PlayerWorker>();

    /// <summary>
    /// 当前玩家拥有的消耗品
    /// </summary>
    public List<PlayerConsumable> playerConsumables = new List<PlayerConsumable>();

    public List<bool> dingWei = new List<bool>{ false, false, false, false, false, false };

    public List<bool> yeWuXiTong = new List<bool> { false, false, false, false, false, false };

    public List<bool> guanJianZiYuanNengLi = new List<bool> { false, false, false, false, false, false };

    public List<bool> yingLiMoShi = new List<bool> { false, false, false, false, false, false };

    public List<bool> xianJinLiu = new List<bool> { false, false, false, false, false, false };

    public List<bool> qiYeJiaZhi = new List<bool> { false, false, false, false, false, false };

    public List<bool> isOneFinish = new List<bool> { false, false, false, false, false, false };

    public bool cheatIndex1 = false;

    public bool cheatIndex2 = false;

    public bool cheatIndex3 = false;

    public Client client;

    public Server server;

    //是否是主机
    public bool isServer = true;

    //是否是单人模式
    public bool isSOLO = true;
    /// <summary>
    /// 通过名字获得Role信息
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public Role GetRoleByName(string roleName)
    {
        for (int i = 0; i < RoleData.Count; i++)
        {
            if (RoleData[i].baseRoleData.roleName.Equals(roleName))
            {

                return RoleData[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 通过名字获得BaseMapRole信息
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public BaseMapRole GetBaseMapRoleByName(string roleName)
    {
        for (int i = 0; i < MapRole.Count; i++)
        {
            if (MapRole[i].baseRoleData.baseRoleData.roleName.Equals(roleName))
            {
                return MapRole[i];
            }
        }
        Debug.Log("当前地图角色找不到！");
        return null;
    }

    /// <summary>
    /// 根据ID查找玩家的角色
    /// </summary>
    /// <returns></returns>
    public Role GetRoleById(double id)
    {
        for (int i = 0; i < RoleData.Count; i++)
        {
            if (Mathf.Abs((float)(RoleData[i].ID - id)) < 0.1)
            {
                return RoleData[i];
            }
        }
        Debug.Log("当前角色找不到");
        return null;
    }

    /// <summary>
    /// 根据ID查找MapRole的角色
    /// </summary>
    /// <returns></returns>
    public BaseMapRole GetMapRoleById(double id)
    {
        for (int i = 0; i < MapRole.Count; i++)
        {
            if (Mathf.Abs((float)(MapRole[i].baseRoleData.ID - id)) < 0.1)
            { 
                return MapRole[i];
            }
        }
        Debug.Log("当前角色找不到");
        return null;
    }

    /// <summary>
    /// 将建立的角色删除
    /// </summary>
    /// <param name="roleId"></param>
    public void DeleteRole(double roleId)
    {
        DataUploadManager.My.AddData(DataEnum.角色_删除角色);
        Role target = GetRoleById(roleId);

        if(!target.isNpc)
        {
            foreach (var v in target.EquipList)
            {
                SetGearStatus(v.Key, false);
            }

            foreach (var v in target.peoPleList)
            {
                SetWorkerStatus(v.Key, false);
            }
        }

        BaseMapRole mapRole = GetMapRoleById(roleId);
        TradeManager.My.DeleteRoleAllTrade(roleId);
        if (StageGoal.My.timeCount - mapRole.putTime <= 5)
            StageGoal.My.GetTechPoint(target.baseRoleData.costTech);
        if (guanJianZiYuanNengLi[2])
        {
            if (StageGoal.My.timeCount - mapRole.putTime > 5)
            {
                int returnTech = GameDataMgr.My.GetModelData(target.baseRoleData.roleType, 1).costTech * 50 / 100;
                StageGoal.My.GetTechPoint(returnTech);
            }
            int returnGold = mapRole.totalUpgradeCost * 50 / 100;
            StageGoal.My.GetPlayerGold(returnGold);
            StageGoal.My.Income(returnGold,IncomeType.Other,null,"删除返现");
        }
        RoleData.Remove(target);
        MapRole.Remove(mapRole);
        MapManager.My.ReleaseLand(mapRole.posX, mapRole.posY);
        DeleleRoleOperationRecord(mapRole);
        Destroy(mapRole.gameObject);
        if(!mapRole.isNpc)
            RoleCountStatic(mapRole,-1);
    }

    /// <summary>
    /// 卖角色
    /// </summary>
    public void SellRole(double roleId)
    {
        DataUploadManager.My.AddData(DataEnum.角色_删除角色);
        Role target = GetRoleById(roleId);
        TradeManager.My.DeleteRoleAllTrade(roleId);
        BaseMapRole mapRole = GetMapRoleById(roleId);
        MapManager.My.ReleaseLand(mapRole.posX, mapRole.posY);
        SetSellNPC(mapRole);

        RoleData.Remove(target);
        MapRole.Remove(mapRole);
        SellRoleOperationRecord(mapRole);
        Destroy(mapRole.gameObject);
        RoleCountStatic(mapRole,-1);
    }

    /// <summary>
    /// 卖角色（建立NPC角色）
    /// </summary>
    /// <param name="mapRole"></param>
    public void SetSellNPC(BaseMapRole mapRole)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/NPC/" + mapRole.baseRoleData.baseRoleData.roleType));
        go.transform.SetParent(GameObject.Find("Role").transform);
        go.transform.position = MapManager.My.GetMapSignByXY(mapRole.posX, mapRole.posY).transform.position + new Vector3(0f, 0.3f, 0f);
        BaseMapRole role = go.GetComponent<BaseMapRole>();
        role.baseRoleData.effect = mapRole.baseRoleData.effect;
        role.baseRoleData.efficiency = mapRole.baseRoleData.efficiency;
        role.baseRoleData.range = mapRole.baseRoleData.range;
        role.baseRoleData.tradeCost = mapRole.baseRoleData.tradeCost + mapRole.baseRoleData.cost * 40 / 100;
        role.baseRoleData.riskResistance = mapRole.baseRoleData.riskResistance;
        role.baseRoleData.baseRoleData.level = mapRole.baseRoleData.baseRoleData.level;
        role.baseRoleData.baseRoleData.roleName = mapRole.baseRoleData.baseRoleData.roleName;
        role.baseRoleData.bulletCapacity = mapRole.baseRoleData.bulletCapacity;
        role.baseRoleData.ID = mapRole.baseRoleData.ID;
        role.startEncourageLevel = mapRole.startEncourageLevel;
        role.encourageLevel = mapRole.startEncourageLevel;
        role.isSell = true;
        NPC npcScript = go.GetComponent<NPC>();
        npcScript.isCanSee = true;
        npcScript.isLock =false;
        npcScript.lockNumber = 0;
        npcScript.isCanSeeEquip = true;
        go.GetComponent<BaseSkill>().buffList.AddRange(mapRole.GetEquipBuffList());
        go.GetComponent<NPC>().NPCBuffList.Clear();
        go.name = mapRole.baseRoleData.baseRoleData.roleName;
        go.GetComponent<NPC>().BaseInit();
        go.GetComponent<NPC>().Init();
    }

    /// <summary>
    /// 记录删除角色的操作
    /// </summary>
    public void DeleleRoleOperationRecord(BaseMapRole mapRole)
    {
        List<string> param = new List<string>();
        param.Add(mapRole.baseRoleData.ID.ToString());
        StageGoal.My.RecordOperation(OperationType.DeleteRole, param);
    }

    /// <summary>
    /// 记录卖出角色的操作
    /// </summary>
    /// <param name="mapRole"></param>
    public void SellRoleOperationRecord(BaseMapRole mapRole)
    {
        List<string> param = new List<string>();
        param.Add(mapRole.baseRoleData.ID.ToString());
        StageGoal.My.RecordOperation(OperationType.SellRole, param);
    }

    /// <summary>
    /// 获得当前可用装备数量
    /// </summary>
    /// <returns></returns>
    public int GetAvailableEquipNumber()
    {
        int result = 0;
        for (int i = 0; i < playerGears.Count; i++)
        {
            if (!playerGears[i].isEquiped)
            {
                result++;
            }
        }
        return result;
    }

    /// <summary>
    /// 获得当前可用工人数量
    /// </summary>
    /// <returns></returns>
    public int GetAvailableWorkerNumber()
    {
        int result = 0;
        for (int i = 0; i < playerWorkers.Count; i++)
        {
            if (!playerWorkers[i].isEquiped)
            {
                result++;
            }
        }
        return result;
    }

    /// <summary>
    /// 获得新的装备
    /// </summary>
    /// <param name="id"></param>
    public void GetNewGear(int id)
    {
        foreach (PlayerGear p in playerGears)
        {
            if (p.GearId == id)
                return;
        }
        playerGears.Add(new PlayerGear(id));
    }

    /// <summary>
    /// 获得新的工人
    /// </summary>
    /// <param name="id"></param>
    public void GetNewWorker(int id)
    {
        foreach (PlayerWorker p in playerWorkers)
        {
            if (p.WorkerId == id)
                return;
        }
        playerWorkers.Add(new PlayerWorker(id));
    }

    /// <summary>
    /// 设置装备的占用状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isEquiped"></param>
    public void SetGearStatus(int id, bool isEquiped)
    {
        foreach (PlayerGear p in playerGears)
        {
            if (p.GearId == id)
            {
                p.isEquiped = isEquiped;
            }
        }
    }

    /// <summary>
    /// 设置工人的占用状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isEquiped"></param>
    public void SetWorkerStatus(int id, bool isEquiped)
    {
        foreach (PlayerWorker p in playerWorkers)
        {
            if (p.WorkerId == id)
            {
                p.isEquiped = isEquiped;
            }
        }
    }

    /// <summary>
    /// 获取消耗品时
    /// </summary>
    /// <param name="id"></param>
    /// <param name="_number"></param>
    public void GetNewConsumalbe(int id, int _number = 1)
    {
        for (int i = 0; i < playerConsumables.Count; i++)
        {
            if (playerConsumables[i].consumableId == id)
            {
                playerConsumables[i].number += _number;
                ConsumableListManager.My.Init();
                return;
            }
        }
        PlayerConsumable temp = new PlayerConsumable(id, _number);
        playerConsumables.Add(temp);
        //print("新建  " + id.ToString());
        ConsumableListManager.My.Init();
    }

    /// <summary>
    /// 获取消耗品数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public PlayerConsumable GetPlayerConsumableById(int id)
    {
        foreach (PlayerConsumable p in playerConsumables)
        {
            if (p.consumableId == id)
                return p;
        }
        return null;
    }

    /// <summary>
    /// 使用消耗品时
    /// </summary>
    /// <param name="id"></param>
    public void UseConsumable(int id)
    {
        for (int i = 0; i < playerConsumables.Count; i++)
        {
            if (playerConsumables[i].consumableId == id)
            {
                playerConsumables[i].number--;
                if (playerConsumables[i].number == 0)
                {
                    //print("移除 " + playerConsumables[i].consumableId.ToString());
                    //PlayerConsumable temp = playerConsumables[i];
                    playerConsumables.RemoveAt(i);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 初始化玩家装备
    /// </summary>
    /// <param name="data"></param>
    public void InitPlayerEquip(List<PlayerEquip> data)
    {
            playerGears.Clear();
            playerWorkers.Clear();
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].equipType == 0)
            {
                GetNewGear(data[i].equipID);
            }
            else if (data[i].equipType == 1)
            {
                GetNewWorker(data[i].equipID);
            }
        }
    }

    /// <summary>
    /// 判断角色种类是否在地图中
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool IsRoleTypeInMap(RoleType type)
    {
        if (MapRole.Count == 0)
            return false;
        foreach (BaseMapRole b in MapRole)
        {
            if (b.baseRoleData.baseRoleData.roleType == type)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 失败时重置调用
    /// </summary>
    public void Reset()
    {
        //playerGears.Clear();
        //playerWorkers.Clear();
        RoleData.Clear();
        RoleManager.Clear();
        MapRole.Clear();
        cheatIndex1 = false;
        cheatIndex2 = false;
        cheatIndex3 = false;

        seedCount = 0;
        peasantCount = 0;
        merchantCount = 0;
        dealerCount = 0;
        //playerConsumables.Clear();
    }

    /// <summary>
    /// 成功时重置调用
    /// </summary>
    public void WinReset()
    {
        MapRole.Clear();
    }

    /// <summary>
    /// 读取天赋配置字符串
    /// </summary>
    /// <param name="str"></param>
    public void ParsePlayerTalent(string str)
    {
        string[] talentList = str.Split('_');
        //if (talentList.Length != 7)
        //{
        //    Debug.Log(str);
        //    Debug.LogWarning("天赋读取错误！");
            for (int i = 0; i < 6; i++)
            {
                dingWei[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                guanJianZiYuanNengLi[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                yeWuXiTong[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                xianJinLiu[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                yingLiMoShi[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                qiYeJiaZhi[i] = false;
            }
            for (int i = 0; i < 6; i++)
            {
                isOneFinish[i] = false;
            }
       // }
        //else
        //{
        //    Debug.Log(str);
        //    char[] temp = talentList[0].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        dingWei[i] = temp[i].Equals('1');
        //    }
        //    temp = talentList[1].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        guanJianZiYuanNengLi[i] = temp[i].Equals('1');
        //    }
        //    temp = talentList[2].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        yeWuXiTong[i] = temp[i].Equals('1');
        //    }
        //    temp = talentList[3].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        xianJinLiu[i] = temp[i].Equals('1');
        //    }
        //    temp = talentList[4].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        yingLiMoShi[i] = temp[i].Equals('1');
        //    }
        //    temp = talentList[5].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        qiYeJiaZhi[i] = temp[i].Equals('1');
        //    }
        //    temp = talentList[6].ToCharArray();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        isOneFinish[i] = temp[i].Equals('1');
        //    }
        //}    
    }

    /// <summary>
    /// 生成天赋简化字符串
    /// </summary>
    /// <returns></returns>
    public string GeneratePlayerTalentReview()
    {
        string result = "";
        int count = 0;
        for (int i = 0; i < 6; i++)
        {
            if (dingWei[i])
            {
                count++;
            }
        }
        result += count.ToString();
        count = 0;
        for (int i = 0; i < 6; i++)
        {
            if (guanJianZiYuanNengLi[i])
            {
                count++;
            }
        }
        result += count.ToString();
        count = 0;
        for (int i = 0; i < 6; i++)
        {
            if (yeWuXiTong[i])
            {
                count++;
            }
        }
        result += count.ToString();
        count = 0;
        for (int i = 0; i < 6; i++)
        {
            if (xianJinLiu[i])
            {
                count++;
            }
        }
        result += count.ToString();
        count = 0;
        for (int i = 0; i < 6; i++)
        {
            if(yingLiMoShi[i])
            {
                count++;
            }
        }
        result += count.ToString();
        count = 0;
        for (int i = 0; i < 6; i++)
        {
            if(qiYeJiaZhi[i])
            {
                count++;
            }
        }
        result += count.ToString();
        return result;
    }

    /// <summary>
    /// 生成玩家天赋字符串
    /// </summary>
    /// <returns></returns>
    public string GeneratePlayerTalent()
    {
        string result = "";
        for (int i = 0; i < dingWei.Count; i++)
        {
            result += dingWei[i] ? '1' : '0';
        }
        result += '_';
        for (int i = 0; i < guanJianZiYuanNengLi.Count; i++)
        {
            result += guanJianZiYuanNengLi[i] ? '1' : '0';
        }
        result += '_';
        for (int i = 0; i < yeWuXiTong.Count; i++)
        {
            result += yeWuXiTong[i] ? '1' : '0';
        }
        result += '_';
        for (int i = 0; i < xianJinLiu.Count; i++)
        {
            result += xianJinLiu[i] ? '1' : '0';
        }
        result += '_';
        for (int i = 0; i < yingLiMoShi.Count; i++)
        {
            result += yingLiMoShi[i] ? '1' : '0';
        }
        result += '_';
        for (int i = 0; i < qiYeJiaZhi.Count; i++)
        {
            result += qiYeJiaZhi[i] ? '1' : '0';
        }
        result += '_';
        for (int i = 0; i < isOneFinish.Count; i++)
        {
            result += isOneFinish[i] ? '1' : '0';
        }
        return result;
    }

    private void Start()
    {
        NetManager.My.Init();
        //Application.targetFrameRate = 60;
        //Time.maximumDeltaTime = 0.02f;
    }
    
    #region 玩家权限

    public int playerDutyID= 0 ;
    /// <summary>
    /// 0---单人玩家 1- 主手   2 ---副手
    /// </summary>

    ///切换关卡
    public int SwitchLevel = 0;

    /// <summary>
    /// 改变时间
    /// </summary>
    public int changeTime = 0;

    /// <summary>
    /// 使用三镜
    /// </summary>
    /// <returns></returns>
    public int UseThreeMirror = 0;

    /// <summary>
    /// 创建角色
    /// </summary>
    public int creatRole = 0;

    /// <summary>
    /// 删除角儿
    /// </summary>
    public int deleteRole = 0;

    
    /// <summary>
    ///  更新角色
    /// </summary>
    public int updateRole = 0;

    /// <summary>
    ///  修改角色装备和人力
    /// </summary>
    public int changeEquipAndWorker = 0;

    /// <summary>
    /// 修改交易
    /// </summary>
    public int changeTrad = 0;

    /// <summary>
    /// 创建交易
    /// </summary>
    public int creatTrad = 0;


    /// <summary>
    /// 删除交易
    /// </summary>
    public int deleteTrad = 0;



    /// <summary>
    /// 初始化角色权限控制
    /// </summary>
    public void InitPlayerRightControl( string UseThreeMirror,string creatRole,string deleteRole,string updateRole,string changeEquipAndWorker
    ,string changeTrad,string creatTrad,string deleteTrad
    
    )
    {
     
            SwitchLevel =0;
            this.changeTime = int.Parse(creatRole);
            this.UseThreeMirror = int.Parse(UseThreeMirror);
            this.creatRole =int.Parse(creatRole);
            this.deleteRole = int.Parse(deleteRole);
            this.updateRole = int.Parse(updateRole);
            this.changeEquipAndWorker = int.Parse(changeEquipAndWorker);
            this.changeTrad = int.Parse(changeTrad);
            this.creatTrad = int.Parse(creatTrad);
            this.deleteTrad = int.Parse(deleteTrad);
      
    }

    #endregion

    public bool isAllReady = false;

    public bool isLocalReady = false;

    public void CheckGameStart()
    {
        Debug.Log("检测ready");
        if (PlayerPrefs.GetInt("isUseGuide") == 1)
        {
            return;
        }
        if (isSOLO)
        {
            DOTween.PlayAll();
            DOTween.timeScale = 1f;
            DOTween.defaultAutoPlay = AutoPlay.All;
        }
        else if (isAllReady && isLocalReady)
        { 
            if (isServer)
            {
                Debug.Log("server ready");
                NewCanvasUI.My.GameNormal();
            }
            else
            {
                DOTween.PlayAll();
                DOTween.timeScale = 1f;
                DOTween.defaultAutoPlay = AutoPlay.All;
            }
        }
    }

    public string GetEquipUseDetail()
    {
        int useCount = 0;
        for(int i=0; i < playerGears.Count; i++)
        {
            if (playerGears[i].isEquiped)
            {
                useCount++;
            }
        }
        return useCount + "_" + playerGears.Count;
    }

    public string GetWorkerUseDetail()
    {
        int useCount = 0;
        for (int i = 0; i < playerWorkers.Count; i++)
        {
            if (playerWorkers[i].isEquiped)
            {
                useCount++;
            }
        }
        return useCount + "_" + playerWorkers.Count;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Screen.SetResolution(1920, 1080, true);
            }
        }
    }

    public int seedCount;
    public int peasantCount;
    public int merchantCount;
    public int dealerCount;

    public void RoleCountStatic(BaseMapRole role, int count)
    {
        switch (role.baseRoleData.baseRoleData.roleType)
        {
            case RoleType.Seed:
                seedCount += count;
                break;
            case RoleType.Peasant:
                peasantCount += count;
                break;
            case RoleType.Merchant:
                merchantCount += count;
                break;
            case RoleType.Dealer:
                dealerCount += count;
                break;
        }
    }

    /*
     * 清空所有的仓库
     */
    public void ClearAllRoleWarehouse()
    {
        for (int i = 0; i < MapRole.Count; i++)
        {
            MapRole[i].ClearWarehouse();
        }
    }
}
