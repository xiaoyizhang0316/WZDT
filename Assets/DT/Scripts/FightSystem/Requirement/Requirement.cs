using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DT.Fight.Bullet;
using Fungus;
using Debug = UnityEngine.Debug;

public class Requirement
{
    public RequirementData requirementData;

    public BaseMapRole role;

    public bool isOpen;

    /// <summary>
    /// 需求从不满足到满足时获得增益效果
    /// </summary>
    public void OnMeetRequire()
    {
        BaseBuff buff = new BaseBuff();
        BuffData data = GameDataMgr.My.GetBuffDataByID(requirementData.targetBuffId);
        buff.Init(data);
        buff.SetRoleBuff(role,role,role);
        isOpen = true;
    }

    /// <summary>
    /// 需求从满足到不满足时取消增益效果
    /// </summary>
    public void CancelRequire()
    {
        role.RemoveBuffById(requirementData.targetBuffId);
        isOpen = false;
    }
    

    /// <summary>
    /// 检测需求是否被满足
    /// </summary>
    public void CheckCondition()
    {
        for (int i = 0; i < requirementData.requireList.Count; i++)
        {
            if (!CheckSingleCondition(requirementData.requireList[i]))
            {
                if (isOpen && requirementData.isRealTime == 0)
                {
                    CancelRequire();
                }
                return;
            }
        }
        if (!isOpen)
        {
            OnMeetRequire();
        }
    }

    /// <summary>
    /// 检测单个需求是否被满足
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool CheckSingleCondition(string str)
    {
        string[] strList = str.Split('_');
        if (strList.Length < 3)
        {
            Debug.Log("------------------需求格式配置错误！-------------");
            return false;
        }
        int prefix = int.Parse(strList[0]);
        int condition = int.Parse(strList[1]);
        string targetNumber = strList[2];
        int target = 0;
        switch (prefix)
        {
            case 1:
                target = BaseLevelController.My.tradeCostLevel;
                break;
            case 2:
                target = BaseLevelController.My.distanceLevel;
                break;
            case 3:
                target = BuildingManager.My.GetExtraConsumerNumber("100");
                break;
            case 4:
            {
                foreach (TradeSign sign in TradeManager.My.tradeList.Values)
                {
                    if (sign.tradeData.targetRole.Equals(role.baseRoleData.ID.ToString()))
                    {
                        target++;
                    }
                }
                break;
            }
            case 5:
            {
                int maxLevel = 0;
                foreach (TradeSign sign in TradeManager.My.tradeList.Values)
                {
                    if (sign.tradeData.targetRole.Equals(role.baseRoleData.ID.ToString()))
                    {
                        BaseMapRole castRole = PlayerData.My.GetMapRoleById(double.Parse(sign.tradeData.startRole));
                        if (castRole.baseRoleData.baseRoleData.level > maxLevel)
                        {
                            maxLevel = castRole.baseRoleData.baseRoleData.level;
                        }
                    }
                }
                target = maxLevel;
                break;
            }
            case 6:
                target = role.baseRoleData.EquipList.Count;
                break;
            case 7:
                target = role.baseRoleData.baseRoleData.level;
                break;
            case 8:
                //TODO 高地条件检测
                break;
            case 9:
                target = StageGoal.My.timeCount - role.putTime;
                break;
            case 10:
                target = role.producedNumber;
                break;
            case 11:
                return TradeManager.My.CheckAllTradeRight();
            case 12:
                target = role.baseRoleData.riskResistance;
                break;
            case 13:
                target = role.baseRoleData.tradeCost;
                break;
            case 14:
                BulletType targetType = (BulletType)Enum.Parse(typeof(BulletType), targetNumber);
                for (int i = 0; i < role.warehouse.Count; i++)
                {
                    if (role.warehouse[i].bulletType.Equals(targetType))
                    {
                        return true;
                    }
                }
                return false;
            case 15:
                target = BaseLevelController.My.totalKillNumber;
                break;
            case 16:
                //TODO baselevelcontrooler消费者击杀数据
                break;
            case 17:
            {
                List<int> tasteBufflist = new List<int>(){303,304,305};
                for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                {
                    if (PlayerData.My.MapRole[i].CheckContainAddon())
                    {
                        return false;
                    }
                }
                return true;
            }
            case 18:
            {
                int tempMaxRisk = 0;
                for (int i = 0; i < role.startTradeList.Count; i++)
                {
                    BaseMapRole mapRole =
                        PlayerData.My.GetMapRoleById(double.Parse(role.startTradeList[i].tradeData.endRole));
                    if (mapRole.baseRoleData.riskResistance > tempMaxRisk)
                    {
                        tempMaxRisk = mapRole.baseRoleData.riskResistance;
                    }
                }
                for (int i = 0; i < role.endTradeList.Count; i++)
                {
                    BaseMapRole mapRole =
                        PlayerData.My.GetMapRoleById(double.Parse(role.endTradeList[i].tradeData.startRole));
                    if (mapRole.baseRoleData.riskResistance > tempMaxRisk)
                    {
                        tempMaxRisk = mapRole.baseRoleData.riskResistance;
                    }
                }
                target = tempMaxRisk;
                break;
            }
            case 19:
            {
                int count = 0;
                for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                {
                    if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Advertisment)
                    {
                        count++;
                    }
                }
                target = count;
                break;
            }
            case 20:
            {
                int targetBuff = int.Parse(targetNumber);
                for (int i = 0; i < role.buffList.Count; i++)
                {
                    if (role.buffList[i].buffId == targetBuff)
                    {
                        return true;
                    }
                }
                return false;
            }
            case 21:
            {
                GameEnum.RoleType targetRoleType = (GameEnum.RoleType)Enum.Parse(typeof(GameEnum.RoleType), targetNumber);
                for (int i = 0; i < PlayerData.My.MapRole.Count; i++)
                {
                    if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType.Equals(targetRoleType))
                    {
                        if (PlayerData.My.MapRole[i].startTradeList.Count > 0 ||
                            PlayerData.My.MapRole[i].endTradeList.Count > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            default:
                break;
        }

        if (condition == 0)
        {
            return target > int.Parse(targetNumber);
        }
        else if (condition == 1)
        {
            return target == int.Parse(targetNumber);
        }
        else if (condition == 2)
        {
            return target < int.Parse(targetNumber);
        }
        else if (condition == 3)
        {
            return target != int.Parse(targetNumber);
        }
        return false;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="requireId"></param>
    /// <param name="_role"></param>
    public Requirement(int requireId,BaseMapRole _role)
    {
        requirementData = GameDataMgr.My.GetRequirementDataById(requireId);
        role = _role;
        isOpen = false;
        CheckCondition();
    }
}
