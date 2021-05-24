﻿using System;
using System.Collections.Generic;
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

    private Queue<TransitionCause> failedTransitionCauses;

    public bool IsTransition => isTransition;

    private void Start()
    {
        startRoleType = GetComponent<BaseMapRole>().baseRoleData.baseRoleData.roleType;
        currentRoleType = startRoleType;
        _role = GetComponent<BaseMapRole>();
        
        _objects = new TransitionCause[1];
        _objects[0] = null;
        isNpc = _role.isNpc;
        failedTransitionCauses = new Queue<TransitionCause>();
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
            failedTransitionCauses.Enqueue(cause);
            return;
        }

        active = true;
        isTransition = true;
        restartSkill = false;
        _objects[0] = cause;
        currentRoleType = roleType;
        _role.baseRoleData.baseRoleData.roleType = currentRoleType;
        Transition();
    }

    /// <summary>
    /// 还原到初始状态
    /// </summary>
    public void Restore()
    {
        CancelInvoke();
        Debug.Log("变形重置");
        _objects[0] = null;
        currentRoleType = startRoleType;
        _role.baseRoleData.baseRoleData.roleType = currentRoleType;
        Transition(true);
        isTransition = false;
        CheckNext();
    }

    public bool restartSkill = false;

    void CheckNext()
    {
        if (failedTransitionCauses.Count > 0)
        {
            var cause = failedTransitionCauses.Peek();
            if (cause.causeType == CauseType.Trade)
            {
                restartSkill = true;
                cause.role.GetComponent<BaseServiceSkill>().RestartTradeData(cause.t_data);
            }
            else
            {
                // TODO 装备变形
            }

            failedTransitionCauses.Dequeue();
        }
    }

    /// <summary>
    /// 检测使该角色变形的条件是否被移除
    /// </summary>
    private void CheckTransitionEnd()
    {
        if (_objects[0] == null)
        {
            CancelInvoke();
            return;
        }
        if (isNpc || _objects[0].causeType == CauseType.Trade)
        {
            Debug.Log("check end transition");
            if (!_role.ContainsTrade( _objects[0].id))
            {
                //CancelInvoke();
                Restore();
            }
        }
        else
        {
            Debug.Log("check equip");
            if (_objects[0].causeType == CauseType.Equip)
            {
                if (!_role.baseRoleData.HasEquip(_objects[0].id))
                {
                    //CancelInvoke();
                    Restore();
                }
            }
            else
            {
                if (!_role.baseRoleData.HasWorker(_objects[0].id))
                {
                    //CancelInvoke();
                    Restore();
                }
            }
        }
    }

    /// <summary>
    /// 变形
    /// </summary>
    private void Transition(bool isRestore=false)
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
        /*if (!isRestore)
            InvokeRepeating("CheckTransitionEnd", 1, 1 );*/
    }

    /// <summary>
    /// 变形失败，重置
    /// </summary>
    /// <param name="isAddFailed"></param>
    /// <param name="skillName"></param>
    private void TransitionFailed(bool isAddFailed, string skillName="") 
    {
        Debug.Log("变形失败");
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

        //isAddFailed = false;

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
            case RoleType.PickingGarden:
                skillName = "ProductPick";
                break;
            case RoleType.Peasant:
                skillName = "ProductMelon";
                break;
        }

        return skillName;
    }
}

public class TransitionCause
{
    public CauseType causeType;
    public int id;
    public BaseMapRole role;
    public TradeData t_data;
    public EquipData e_data;
    public WorkerData w_data;


    public TransitionCause(CauseType causeType, int id,BaseMapRole role=null, TradeData data=null, EquipData eData=null, WorkerData wData=null)
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
