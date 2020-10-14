using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// 玩家技能解锁状态
    /// </summary>
    public Dictionary<int, bool> tradeSkillLock = new Dictionary<int, bool>();

    public bool cheatIndex1 = false;

    public bool cheatIndex2 = false;

    public bool cheatIndex3 = false;

    public Client Client;

    public Server server;

    public bool isServer = true;

    /// <summary>
    /// 单人模式
    /// </summary>
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
        foreach (var v in target.EquipList)
        {
            SetGearStatus(v.Key, false);
        }
        foreach (var v in target.peoPleList)
        {
            SetWorkerStatus(v.Key, false);
        }
        TradeManager.My.DeleteRoleAllTrade(roleId);
        BaseMapRole mapRole = GetMapRoleById(roleId);
        if (StageGoal.My.timeCount - mapRole.putTime <= 5)
            StageGoal.My.GetTechPoint(target.baseRoleData.costTech);
        RoleData.Remove(target);
        MapRole.Remove(mapRole);
        MapManager.My.ReleaseLand(mapRole.posX, mapRole.posY);
        DeleleRoleOperationRecord(mapRole);
        Destroy(mapRole.gameObject);
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
        //playerConsumables.Clear();
    }

    /// <summary>
    /// 成功时重置调用
    /// </summary>
    public void WinReset()
    {
        MapRole.Clear();
    }

    private void Start()
    {
        NetManager.My.Init();
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
}
