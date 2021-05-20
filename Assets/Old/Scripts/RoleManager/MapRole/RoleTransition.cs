using System;
using UnityEngine;
using static GameEnum;
using Object = System.Object;

public class RoleTransition : MonoBehaviour
{
    public RoleType startRoleType;
    public RoleType currentRoleType;
    private BaseMapRole _role;
    private TransitionCause[] _objects;
    private bool isNpc = false;
    private bool isTransition = false;

    public bool IsTransition => isTransition;

    private void Start()
    {
        startRoleType = GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType;
        currentRoleType = startRoleType;
        _role = GetComponent<BaseMapRole>();
        
        _objects = new TransitionCause[1];
        _objects[0] = null;
        isNpc = _role.isNpc;
    }

    /// <summary>
    /// 激活角色转型
    /// </summary>
    /// <param name="roleType">要转换的类型</param>
    /// <param name="cause">转换的原因（EquipSign,WorkerSign,TradeSign）</param>
    /// <param name="active">激活成功(可用来判断能否达成交易)</param>
    public void ActiveTransition(RoleType roleType, TransitionCause cause, out bool active)
    {
        if (_objects[0] != null)
        {
            active = false;
            return;
        }

        active = true;
        isTransition = true;
        _objects[0] = cause;
        currentRoleType = roleType;
        _role.baseRoleData.baseRoleData.roleType = currentRoleType;
        Transition();
    }

    /// <summary>
    /// 还原到初始状态
    /// </summary>
    private void Restore()
    {
        _objects[0] = null;
        currentRoleType = startRoleType;
        Transition();
        isTransition = false;
    }

    /// <summary>
    /// 检测使该角色变形的条件是否被移除
    /// </summary>
    private void CheckTransitionEnd()
    {
        if (isNpc || _objects[0].causeType == CauseType.Trade)
        {
            if (!_role.ContainsTrade( _objects[0].id))
            {
                CancelInvoke();
                Restore();
            }
        }
        else
        {
            if (_objects[0].causeType == CauseType.Equip)
            {
                if (!_role.baseRoleData.HasEquip(_objects[0].id))
                {
                    CancelInvoke();
                    Restore();
                }
            }
            else
            {
                if (!_role.baseRoleData.HasWorker(_objects[0].id))
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
        if (skillName.Equals("")|| !transform.GetComponent(skillName))
        {
            Debug.Log("未找到 skill 脚本");
            return;
        }

        try
        {
            RemoveCurrentSkill(skillName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CancelInvoke();
            TransitionFailed(false);
            return;
        }
        
        var newSkillName = GetNewSkillNameByRoleType(currentRoleType);
        
        try
        {
            CommonFunc.AddComponent(gameObject,newSkillName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CancelInvoke();
            TransitionFailed(true, skillName);
            return;
        }
        // 重置role sprite
        transform.Find("RoleSprite").GetComponent<RoleSprite>().RestoreOrTransition();
        // TODO transform model 
        // TODO init role data 
        // TODO check trade 
        // 检查还原初始状态
        InvokeRepeating("CheckTransitionEnd", 1, 1 );
    }

    /// <summary>
    /// 变形失败，重置
    /// </summary>
    /// <param name="isAddFailed"></param>
    /// <param name="skillName"></param>
    private void TransitionFailed(bool isAddFailed, string skillName="") 
    {
        currentRoleType = startRoleType;
        _role.baseRoleData.baseRoleData.roleType = currentRoleType;
        if (_objects[0].causeType == CauseType.Trade)
        {
            // 删除相关交易
            TradeManager.My.DeleteTrade(_objects[0].id);
        }
        else
        {
            // 删除相关工人或设备
            if (_objects[0].causeType == CauseType.Equip)
            {
                _role.baseRoleData.RemoveEquip(_objects[0].id, true);
                PlayerData.My.SetGearStatus(_objects[0].id, false);
            }
            else
            {
                _role.baseRoleData.RemoveEquip(_objects[0].id, false);
                PlayerData.My.SetGearStatus(_objects[0].id, false);
            }
        }

        _objects[0] = null;
        if (isAddFailed)
        {
            CommonFunc.AddComponent(gameObject,skillName);
        }

        isTransition = false;
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
            throw new Exception(e.Message);
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
            case RoleType.Merchant:
                skillName = "ProductMerchant";
                break;
            case RoleType.JuiceFactory:
                skillName = "ProductMelon_Boom";
                break;
            case RoleType.PackageFactory:
                break;
            case RoleType.DrinksCompany:
                skillName = "ProductMelon_Juice";
                break;
            case RoleType.InstrumentFactory:
                skillName = "ServiceInstrument";
                break;
        }

        return skillName;
    }
}

public class TransitionCause
{
    public CauseType causeType;
    public int id;

    public TransitionCause(CauseType causeType, int id)
    {
        this.causeType = causeType;
        this.id = id;
    }
}

public enum CauseType
{
    Trade,
    Equip,
    Worker
}
