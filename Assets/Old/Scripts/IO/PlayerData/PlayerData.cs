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
        //foreach (Role r in RoleData)
        //{
        //    if (Mathf.Abs((float)(r.ID - id)) < 0.1)
        //    {
        //        print("find role");
        //        Role tempRole = new Role();
        //        tempRole = r;
        //        return tempRole;
        //    }
        //}
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
    /// 将地图中的角色回收
    /// </summary>
    /// <param name="roleId"></param>
    public void UnistallRoleFromMap(double roleId)
    {

    }

    /// <summary>
    /// 将建立的角色删除
    /// </summary>
    /// <param name="roleId"></param>
    public void DeleteRole(double roleId)
    {
        Role target = GetRoleById(roleId);
        foreach (var v in target.EquipList)
        {
            SetGearStatus(v.Key, false);
        }
        foreach (var v in target.peoPleList)
        {
            SetWorkerStatus(v.Key, false);
        }

        for (int i = 0; i < RoleManager.Count; i++)
        {
            if (System.Math.Abs(double.Parse(RoleManager[i].name.Split('_')[1]) - target.ID) < 0.1f)
            {
                GameObject temp = RoleManager[i];
                RoleManager.Remove(temp);
                RoleData.Remove(target);
                Destroy(temp, 0.001f);
                break;
            }
        }
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
    /// 随机解锁一个技能
    /// </summary>
    /// <returns></returns>
    public TradeSkillData UnlockRandomSkill()
    {
        List<int> unlockList = new List<int>();
        foreach (var v in tradeSkillLock)
        {
            TradeSkillData data = GameDataMgr.My.GetTradeSkillDataByID(v.Key);
            if (IsRoleTypeInMap(data.startRole) && IsRoleTypeInMap(data.endRole))
            {
                if (v.Value)
                {
                    unlockList.Add(v.Key);
                }
            }
        }
        if (unlockList.Count == 0)
            return null;
        else
        {
            int num = Random.Range(0, unlockList.Count);
            if (ExecutionManager.My.SubExecution(ExecutionManager.My.unlockSkills))
            {
                tradeSkillLock[unlockList[num]] = false;
                return GameDataMgr.My.GetTradeSkillDataByID(unlockList[num]);
            }
        }
        return null;
    }

    /// <summary>
    /// 解锁被动技能
    /// </summary>
    /// <returns></returns>
    public string UnlockPassiveSkill()
    {
        string str = "";
        List<int> lockIndex = new List<int>();
        int number;
        string unlockName = "";
        for (int i = 0; i < MapRole.Count; i++)
        {
            if (MapRole[i].baseRoleData.baseRoleData.roleType == RoleType.Dealer && !MapRole[i].isNpc)
            {
                for (int j = 0; j < MapRole[i].AllPassivitySkills.Count; j++)
                {
                    if (MapRole[i].AllPassivitySkills[j].isLock && (MapRole[i].AllPassivitySkills[j].SkillName.Equals("精细运营") || MapRole[i].AllPassivitySkills[j].SkillName.Equals("在线支付")))
                    {
                        lockIndex.Add(j);
                    }
                    if (lockIndex.Count != 0)
                    {
                        number = Random.Range(0, lockIndex.Count);
                        unlockName = MapRole[i].AllPassivitySkills[lockIndex[number]].SkillName;
                        break;
                    }
                    else
                    {
                        str = "";
                        return str;
                    }
                }
                break;
            }
        }
        if (lockIndex.Count == 0)
        {
            return str;
        }
        if (ExecutionManager.My.SubExecution(ExecutionManager.My.unlockSkills))
        {
            for (int i = 0; i < MapRole.Count; i++)
            {
                if (MapRole[i].baseRoleData.baseRoleData.roleType == RoleType.Dealer && !MapRole[i].isNpc)
                {
                    MapRole[i].UnLockPassivitySkill(unlockName);
                }
            }
            str = "解锁了零售商的" + unlockName + "被动技能";
            return str;
        }
        else
        {
            str = "";
            return str;
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
    /// 解锁所有指定技能ID的交易
    /// </summary>
    /// <param name="skillId"></param>
    public void UnLockSelectTradeSkill(int skillId)
    {
        List<int> keyList = tradeSkillLock.Keys.ToList();
        for (int i = 0; i < keyList.Count; i++)
        {
            if (GameDataMgr.My.GetTradeSkillDataByID(keyList[i]).skillId == skillId)
            {
                tradeSkillLock[keyList[i]] = false;
            }
        }
    }

    /// <summary>
    /// 失败时重置调用
    /// </summary>
    public void Reset()
    {
        playerGears.Clear();
        playerWorkers.Clear();
        RoleData.Clear();
        RoleManager.Clear();
        MapRole.Clear();
        playerConsumables.Clear();
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

    }

}
