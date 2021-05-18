using System;
using UnityEngine;
using static GameEnum;

public class RoleTransition : MonoBehaviour
{
    public RoleType startRoleType;
    public RoleType currentRoleType;
    private BaseMapRole _role;
    private Transform[] _objects;

    private void Start()
    {
        startRoleType = GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType;
        currentRoleType = startRoleType;
        _role = GetComponent<BaseMapRole>();
        
        _objects = new Transform[1];
        _objects[0] = null;
    }

    /// <summary>
    /// 激活角色转型
    /// </summary>
    /// <param name="roleType">要转换的类型</param>
    /// <param name="cause">转换的原因（EquipSign,WorkerSign,TradeSign）</param>
    /// <param name="active">激活成功(可用来判断能否达成交易)</param>
    public void ActiveTransition(RoleType roleType, Transform cause, out bool active)
    {
        if (_objects[0] != null)
        {
            active = false;
            return;
        }

        active = true;
        _objects[0] = cause;
        currentRoleType = roleType;
        _role.baseRoleData.baseRoleData.roleType = currentRoleType;
        Transition();
        InvokeRepeating("CheckTransitionEnd", 1, 1 );
    }

    /// <summary>
    /// 还原到初始状态
    /// </summary>
    private void Restore()
    {
        _objects[0] = null;
        currentRoleType = startRoleType;
        Transition();
    }

    /// <summary>
    /// 检测使该角色变形的条件是否被移除
    /// </summary>
    private void CheckTransitionEnd()
    {
        if (_objects[0].GetComponent<TradeSign>())
        {
            if (!_role.tradeList.Contains(_objects[0].GetComponent<TradeSign>()))
            {
                CancelInvoke();
                Restore();
            }
        }
        else
        {
            if (_objects[0].GetComponent<EquipSign>())
            {
                if (!_role.baseRoleData.HasEquip(_objects[0].GetComponent<EquipSign>().ID))
                {
                    CancelInvoke();
                    Restore();
                }
            }
            else
            {
                if (!_role.baseRoleData.HasWorker(_objects[0].GetComponent<WorkerSign>().ID))
                {
                    CancelInvoke();
                    Restore();
                }
            }
        }
    }

    /// <summary>
    /// 变形
    /// </summary>
    private void Transition()
    {
        var skillName =
            CommonFunc.GetContentInBracketsWithoutBrackets(transform.GetComponent<BaseSkill>().ToString());
        RemoveCurrentSkill(skillName);
        var newSkillName = GetNewSkillNameByRoleType(currentRoleType);
        
        try
        {
            CommonFunc.AddComponent(gameObject,newSkillName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CancelInvoke();
            TransitionFailed();
            return;
        }
        // 重置role sprite
        transform.Find("RoleSprite").GetComponent<RoleSprite>().RestoreOrTransition();
        // TODO transform model 
        // TODO init role data 
        // TODO check trade 
    }

    private void TransitionFailed() 
    {
        // TODO 还原到初始状态
        // TODO 移除装备或者交易
    }

    /// <summary>
    /// 移除相关的skill脚本
    /// </summary>
    /// <param name="skillName">脚本名称</param>
    private void RemoveCurrentSkill(string skillName)
    {
        try
        {
            Destroy(transform.GetComponent(skillName));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CancelInvoke();
            Restore();
            throw;
        }
    }

    /// <summary>
    /// 根据角色类型获取Skill脚本 // TODO 
    /// </summary>
    /// <param name="type">角色类型</param>
    /// <returns></returns>
    private string GetNewSkillNameByRoleType(RoleType type)
    {
        string skillName = "";
        switch (type)
        {
            case RoleType.Seed:
                skillName = "";
                break;
            case RoleType.Peasant:
                break;
            case RoleType.Merchant:
                break;
            case RoleType.Dealer:
                break;
            case RoleType.School:
                break;
            case RoleType.Bank:
                break;
            case RoleType.Investor:
                break;
            case RoleType.CutFactory:
                break;
            case RoleType.JuiceFactory:
                break;
            case RoleType.CanFactory:
                break;
            case RoleType.WholesaleFactory:
                break;
            case RoleType.PackageFactory:
                break;
            case RoleType.SoftFactory:
                break;
            case RoleType.CrispFactory:
                break;
            case RoleType.SweetFactory:
                break;
            case RoleType.Insurance:
                break;
            case RoleType.Consulting:
                break;
            case RoleType.PublicRelation:
                break;
            case RoleType.GasStation:
                break;
            case RoleType.Advertisment:
                break;
            case RoleType.Fertilizer:
                break;
            case RoleType.ResearchInstitute:
                break;
            case RoleType.Youtuber:
                break;
            case RoleType.OrderCompany:
                break;
            case RoleType.Marketing:
                break;
            case RoleType.RubishProcess:
                break;
            case RoleType.DataCenter:
                break;
            case RoleType.ConsumerItemFactory:
                break;
            case RoleType.RoleItemFactory:
                break;
            case RoleType.All:
                break;
            case RoleType.financialCompany:
                break;
        }

        return skillName;
    }
}
