using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Fungus;
using Debug = UnityEngine.Debug;

public class Requirement
{
    public RequirementData requirementData;

    public BaseMapRole role;

    public bool isOpen;

    public void OnMeetRequire()
    {
        //TODO 生成buff并附加在角色上
        isOpen = true;
    }

    public void CancelRequire()
    {
        //TODO 取消对应ID的角色buff
        isOpen = false;
    }
    

    public void CheckCondition()
    {
        for (int i = 0; i < requirementData.requireList.Count; i++)
        {
            if (!CheckSingleCondition(requirementData.requireList[i]))
            {
                if (isOpen)
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
        int targetNumber = int.Parse(strList[2]);
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
                target = role.baseRoleData.peoPleList.Count;
                break;
            case 7:
                target = role.baseRoleData.baseRoleData.level;
                break;
            case 8:
                //TODO 高地条件检测
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            
            default:
                break;
        }
        return true;
    }

    public Requirement(int requireId,BaseMapRole _role)
    {
        requirementData = GameDataMgr.My.GetRequirementDataById(requireId);
        role = _role;
        isOpen = false;
        CheckCondition();
    }
}
