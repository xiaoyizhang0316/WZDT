using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static GameEnum;

/// <summary>
/// Buff实体类，角色和消费者的buff都是basebuff
/// 使用规则：
/// 1. Basebuff buff = new Basebuff();
/// 2. Buffdata data = 根据buffId获取对应的buffData实体
/// 3. buff.Init(data)
/// 4. 如果目标是角色，就调用setRoleBuff,如果目标是消费者，就调用SetConsumerBuff
/// </summary>
[Serializable]
public class BaseBuff
{
    //buff实际生效的数值记录（用于添加和buff消除时扣除属性）
    public BuffConfig buffConfig;
    //buffData实体
    public BuffData buffData;
    //ID
    public int buffId;
    //buff持续时间（-1 = 无限持续时间）
    public int duration;
    //buff回合持续时间（=1 = 无限持续时间）
    public int turnDuration;
    //buff生效周期性间隔（-1 为没有周期性效果）
    public int interval;
    //buff名称
    public string buffName;
    //周期时间计数器
    public int count;
    //buff描述
    public string buffDesc = "";
    //buff施法方
    public BaseMapRole castRole;
    //buff目标方
    public BaseMapRole targetRole;
    //buff实际生效方
    public BaseMapRole buffRole;
    //buff目标方（目标是消费者）
    public ConsumeSign targetConsume;

    /// <summary>
    /// 复制当前的buff实体
    /// </summary>
    /// <returns></returns>
    public BaseBuff CopyNew()
    {
        BaseBuff buff = new BaseBuff();
        buff.Init(buffData.CopyNew());
        return buff;
    }
    
    /// <summary>
    /// 当buff添加到角色时调用
    /// </summary>
    public void RoleBuffAdd()
    {
        foreach (string str in buffData.OnBuffAdd)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当buff从角色上移除时调用
    /// </summary>
    public void RoleBuffRemove()
    {
        foreach (string str in buffData.OnBuffRemove)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当buff添加到消费者身上时调用
    /// </summary>
    public void ConsumerBuffAdd()
    {
        foreach (string str in buffData.OnBuffAdd)
        {
            CheckStaticNumber(str);
            CheckConsumerNumber(str);
        }
    }

    /// <summary>
    /// 当buff从消费者身上移除时调用
    /// </summary>
    public void ConsumerBuffRemove()
    {
        foreach (string str in buffData.OnBuffRemove)
        {
            CheckStaticNumber(str);
            CheckConsumerNumber(str);
        }
    }

    /// <summary>
    /// 消费者濒临死亡时调用
    /// </summary>
    public void OnConsumerBeforeDead()
    {
        foreach (string str in buffData.OnBeforeDead)
        {
            CheckStaticNumber(str);
            CheckConsumerNumber(str);
        }
    }

    /// <summary>
    /// 当玩家濒临失败时调用
    /// </summary>
    public void OnPlayerBeforeDead()
    {
        foreach (string str in buffData.OnBeforeDead)
        {
            CheckStaticNumber(str);
        }
    }

    /// <summary>
    /// 角色计算周期性效果时调用
    /// </summary>
    public void OnRoleTick()
    {
        count++;
        if (count == buffData.interval)
        {
            foreach (string str in buffData.OnTick)
            {
                if (StageGoal.My.currentType != StageType.Normal)
                {
                    CheckStaticNumber(str);
                }
                CheckRoleNumber(str);
            }
            count = 0;
        }
    }

    /// <summary>
    /// 回合结束时角色身上buff调用
    /// </summary>
    public void OnRoleTurn()
    {
        foreach (string str in buffData.OnEndTurn)
        {
            CheckStaticNumber(str);
        }

    }

    /// <summary>
    /// 消费者周期性效果结算时调用
    /// </summary>
    public void OnConsumerTick()
    {
        count++;
        if (count == buffData.interval)
        {
            foreach (string str in buffData.OnTick)
            {
                CheckStaticNumber(str);
                CheckConsumerNumber(str);
                targetConsume.AddEffect(buffId);
            }
            count = 0;
        }
    }

    /// <summary>
    /// 计算关于弹药属性变更时调用
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnProduct(ref ProductData data)
    {
        foreach (string str in buffData.OnProduct)
        {
            CheckStaticNumber(str);
            CheckProductNumber(str, ref data);
        }
    }

    /// <summary>
    /// 将buff付给目标(角色)
    /// </summary>
    public void AddBuffToTargetRole()
    {
        buffRole.AddBuff(this);
    }

    /// <summary>
    /// 将buff付给目标（消费者）
    /// </summary>
    public void AddBuffToTargetConsumer()
    {
        targetConsume.AddBuff(this);
    }

    /// <summary>
    /// 设置buff的发起者，承受者，目标（当buff对角色生效时调用此方法）
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="target"></param>
    public void SetRoleBuff(BaseMapRole _castRole = null, BaseMapRole target = null, BaseMapRole _buffRole = null)
    {
        castRole = _castRole;
        targetRole = target;
        buffRole = _buffRole;
        AddBuffToTargetRole();
    }

    /// <summary>
    /// 当buff对消费者生效时调用此方法
    /// </summary>
    /// <param name="consume"></param>
    public void SetConsumerBuff(ConsumeSign consume)
    {
        targetConsume = consume;
        AddBuffToTargetConsumer();
    }

    /// <summary>
    /// buff数值初始化
    /// </summary>
    /// <param name="buff"></param>
    public void Init(BuffData buff)
    {
        buffConfig = new BuffConfig();
        buffData = buff;
        buffId = buff.BuffID;
        duration = buff.duration;
        turnDuration = buff.turnDuration;
        buffName = buff.BuffName;
    }

    /// <summary>
    /// 计算全局相关的buff
    /// </summary>
    /// <param name="str"></param>
    public void CheckStaticNumber(string str)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 1:
                {
                    CalculateNumber(str, ref buffConfig.playerGoldChange, StageGoal.My.playerGold);
                    int goldChange = buffConfig.playerGoldChange;
                    StageGoal.My.GetPlayerGold(goldChange);
                    if (targetConsume != null)
                        StageGoal.My.Income(goldChange, IncomeType.Other, null, buffName);
                    else if (castRole.isNpc)
                        StageGoal.My.Income(goldChange, IncomeType.Npc, castRole);
                    //StageGoal.My.Income(buffConfig.playerGoldChange, IncomeType.Npc, castRole);
                    break;
                }
            case 2:
                CalculateNumber(str, ref buffConfig.playerSatisfyChange, StageGoal.My.playerSatisfy);
                StageGoal.My.GetSatisfy(buffConfig.playerSatisfyChange);
                StageGoal.My.GetHealth(buffConfig.playerSatisfyChange);
                break;
            case 3:
                CalculateNumber(str, ref buffConfig.playerHealthChange, StageGoal.My.playerHealth);
                StageGoal.My.GetHealth(buffConfig.playerHealthChange);
                break;
            case 4:
                CalculateNumber(str, ref buffConfig.playerTechPointChange, StageGoal.My.playerTechPoint);
                StageGoal.My.GetTechPoint(buffConfig.playerTechPointChange);;
                StageGoal.My.IncomeTp(buffConfig.playerTechPointChange, IncomeTpType.Buff);
                break;
            case 5:
                CalculateNumber(str, ref buffConfig.stageRiskLevelChange, BaseLevelController.My.riskControlLevel);
                BaseLevelController.My.riskControlLevel += buffConfig.stageRiskLevelChange;
                break;
            case 6:
                CalculateNumber(str, ref buffConfig.stageTradeCostLevelChange, BaseLevelController.My.tradeCostLevel);
                BaseLevelController.My.tradeCostLevel += buffConfig.stageTradeCostLevelChange;
                break;
            case 7:
                CalculateNumber(str, ref buffConfig.stageDistanceLevelChange, BaseLevelController.My.distanceLevel);
                BaseLevelController.My.distanceLevel += buffConfig.stageDistanceLevelChange;
                break;
            case 8:
                CalculateNumber(str, ref buffConfig.stageMonthCostChange, BaseLevelController.My.monthCostLevel);
                BaseLevelController.My.monthCostLevel += buffConfig.stageMonthCostChange;
                break;
            case 9:
                CalculateNumber(str, ref buffConfig.stageEncourageLevelChange, BaseLevelController.My.encourageLevel);
                BaseLevelController.My.encourageLevel += buffConfig.stageEncourageLevelChange;
                break;
            case 101:
                CalculateNumber(str, ref buffConfig.stageEnvironmentLevel1Change, BaseLevelController.My.environmentLevel1);
                BaseLevelController.My.environmentLevel1 += buffConfig.stageEnvironmentLevel1Change;
                break;
            case 102:
                CalculateNumber(str, ref buffConfig.stageEnvironmentLevel2Change, BaseLevelController.My.environmentLevel2);
                BaseLevelController.My.environmentLevel2 += buffConfig.stageEnvironmentLevel2Change;
                break;
            case 103:
                CalculateNumber(str, ref buffConfig.stageEnvironmentLevel3Change, BaseLevelController.My.environmentLevel3);
                BaseLevelController.My.environmentLevel3 += buffConfig.stageEnvironmentLevel3Change;
                break;
            case 104:
                CalculateNumber(str, ref buffConfig.stageSatisfyRateChange, (int)BaseLevelController.My.satisfyRate);
                BaseLevelController.My.satisfyRate += buffConfig.stageSatisfyRateChange / 100f;
                break;
            case 105:
                CalculateNumber(str, ref buffConfig.stageTasteDamageRateChange, (int)BaseLevelController.My.tasteDamageRate);
                BaseLevelController.My.tasteDamageRate += buffConfig.stageTasteDamageRateChange / 100f;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 计算角色相关的buff
    /// </summary>
    /// <param name="str"></param>
    public void CheckRoleNumber(string str,bool isReAdd = false)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 11:
                {
                    CalculateNumber(str, ref buffConfig.roleEffectChange, targetRole.baseRoleData.effect);
                    targetRole.baseRoleData.effect += buffConfig.roleEffectChange;
                    break;
                }
            case 12:
                {
                    CalculateNumber(str, ref buffConfig.roleEfficiencyChange, targetRole.baseRoleData.efficiency);
                    targetRole.baseRoleData.efficiency += buffConfig.roleEfficiencyChange;
                    break;
                }
            case 13:
                {
                    CalculateNumber(str, ref buffConfig.roleRangeChange, targetRole.baseRoleData.range);
                    targetRole.baseRoleData.range += buffConfig.roleRangeChange;
                    break;
                }
            case 14:
                {
                    CalculateNumber(str, ref buffConfig.roleTradeCostChange, targetRole.baseRoleData.tradeCost);
                    targetRole.baseRoleData.tradeCost += buffConfig.roleTradeCostChange;
                    break;
                }
            case 15:
                {
                    CalculateNumber(str, ref buffConfig.roleRiskResistanceChange, targetRole.baseRoleData.riskResistance);
                    targetRole.baseRoleData.riskResistance += buffConfig.roleRiskResistanceChange;
                    break;
                }
            case 16:
                {
                    CalculateNumber(str, ref buffConfig.roleCostChange, targetRole.baseRoleData.cost);
                    targetRole.baseRoleData.cost += buffConfig.roleCostChange;
                    break;
                }
            case 17:
                {
                    CalculateNumber(str, ref buffConfig.roleBulletCapacityChange, targetRole.baseRoleData.bulletCapacity);
                    targetRole.baseRoleData.bulletCapacity += buffConfig.roleBulletCapacityChange;
                    break;
                }
            case 18:
                {
                    if (isReAdd)
                    {
                        break;
                    }
                    if (attri.Length == 2)
                    {
                        buffConfig.roleEncourageLevelChange = int.Parse(attri[1]);
                    }
                    else
                    {
                        buffConfig.roleEncourageLevelChange = 0 - buffConfig.roleEncourageLevelChange;
                    }
                    targetRole.startEncourageLevel += buffConfig.roleEncourageLevelChange;
                    targetRole.RecalculateEncourageLevel(true);
                    break;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 计算角色相关BUFF(用于UI显示)
    /// </summary>
    /// <param name="str"></param>
    public void CheckRoleNumberNoChange(string str)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 11:
                CalculateNumber(str, ref buffConfig.roleEffectChange, targetRole.baseRoleData.effect);
                break;
            case 12:
                CalculateNumber(str, ref buffConfig.roleEfficiencyChange, targetRole.baseRoleData.efficiency);
                break;
            case 13:
                CalculateNumber(str, ref buffConfig.roleRangeChange, targetRole.baseRoleData.range);
                break;
            case 14:
                CalculateNumber(str, ref buffConfig.roleTradeCostChange, targetRole.baseRoleData.tradeCost);
                break;
            case 15:
                CalculateNumber(str, ref buffConfig.roleRiskResistanceChange, targetRole.baseRoleData.riskResistance);
                break;
            case 16:
                CalculateNumber(str, ref buffConfig.roleCostChange, targetRole.baseRoleData.cost);
                break;
            case 17:
                CalculateNumber(str, ref buffConfig.roleBulletCapacityChange, targetRole.baseRoleData.bulletCapacity);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 计算消费者相关的buff结算
    /// </summary>
    /// <param name="str"></param>
    /// <param name="consumeData"></param>
    public void CheckConsumerNumber(string str)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 30:
                CalculateNumber(str, ref buffConfig.consumerHealthChange, targetConsume.consumeData.maxHealth);
                targetConsume.ChangeHealth(buffConfig.consumerHealthChange);
                break;
            case 31:
                CalculateNumber(str, ref buffConfig.consumerSpeedChange, (int)(targetConsume.tweener.timeScale * 100));
                targetConsume.ChangeSpeed(buffConfig.consumerSpeedChange);
                break;
            case 32:
                CalculateNumber(str, ref buffConfig.consumerKillMoneyChange, targetConsume.consumeData.killMoney);
                targetConsume.consumeData.killMoney += buffConfig.consumerKillMoneyChange;
                break;
            case 33:
                CalculateNumber(str, ref buffConfig.consumerKillSatisfyChange, targetConsume.consumeData.killSatisfy);
                targetConsume.consumeData.killSatisfy += buffConfig.consumerKillSatisfyChange;
                break;
            case 34:
                CalculateNumber(str, ref buffConfig.consumerLiveSatisfyChange, targetConsume.consumeData.liveSatisfy);
                targetConsume.consumeData.liveSatisfy += buffConfig.consumerLiveSatisfyChange;
                break;
            case 35:
                CalculateNumber(str, ref buffConfig.consumerNormalChange, targetConsume.elementResistance[ProductElementType.Normal]);
                targetConsume.elementResistance[ProductElementType.Normal] += buffConfig.consumerNormalChange;
                break;
            case 36:
                CalculateNumber(str, ref buffConfig.consumerDiscountChange, targetConsume.elementResistance[ProductElementType.Discount]);
                targetConsume.elementResistance[ProductElementType.Discount] += buffConfig.consumerDiscountChange;
                break;
            case 37:
                CalculateNumber(str, ref buffConfig.consumerGoodPackChange, targetConsume.elementResistance[ProductElementType.GoodPack]);
                targetConsume.elementResistance[ProductElementType.GoodPack] += buffConfig.consumerGoodPackChange;
                break;
            case 38:
                CalculateNumber(str, ref buffConfig.consumerSoftChange, targetConsume.elementResistance[ProductElementType.Soft]);
                targetConsume.elementResistance[ProductElementType.Soft] += buffConfig.consumerSoftChange;
                break;
            case 39:
                CalculateNumber(str, ref buffConfig.consumerCrispChange, targetConsume.elementResistance[ProductElementType.Crisp]);
                targetConsume.elementResistance[ProductElementType.Crisp] += buffConfig.consumerCrispChange;
                break;
            case 40:
                CalculateNumber(str, ref buffConfig.consumerSweetChange, targetConsume.elementResistance[ProductElementType.Sweet]);
                targetConsume.elementResistance[ProductElementType.Sweet] += buffConfig.consumerSweetChange;
                break;
            case 41:
                CalculateNumber(str, ref buffConfig.consumerIgnoreResist);
                targetConsume.isIgnoreResistance = buffConfig.consumerIgnoreResist;
                break;
            case 42:
                CalculateNumber(str, ref buffConfig.consumerMaxHealthChange, targetConsume.consumeData.maxHealth);
                targetConsume.consumeData.maxHealth += buffConfig.consumerMaxHealthChange;
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 计算产品相关的buff
    /// </summary>
    /// <param name="product"></param>
    public void CheckProductNumber(string str, ref ProductData product)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 50:
                CalculateNumber(str, ref buffConfig.bulletDamageChange, (int)product.damage);
                product.damage += buffConfig.bulletDamageChange;
                break;
            case 51:
                CalculateNumber(str, ref buffConfig.bulletLoadingChange, (int)(product.loadingSpeed * 100));
                product.loadingSpeed += buffConfig.bulletLoadingChange / 100f;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 执行特殊操作
    /// </summary>
    /// <param name="str"></param>
    public void DoSpecialFunction(string str)
    {
        if (!IsNumberic(str))
        {
            string funcName = str.Split('_')[0];
            string[] temp = str.Split('_');
            List<string> paramList = new List<string>();
            for (int i = 1; i < temp.Length; i++)
            {
                paramList.Add(temp[i]);
            }
        }
    }

    /// <summary>
    /// 判断字符串是不是数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsNumberic(string str)
    {
        try
        {
            int temp = int.Parse(str);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// 重新计算角色身上的buff效果（当buff的数值发生变化时）
    /// </summary>
    public void ResetRoleBuff()
    {
        foreach (string str in buffData.OnBuffRemove)
        {
            CheckRoleNumber(str);
        }
        foreach (string str in buffData.OnBuffAdd)
        {
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 重新添加角色身上的buff效果
    /// </summary>
    public void ReaddRoleBuff()
    {
        foreach (string str in buffData.OnBuffAdd)
        {
            CheckRoleNumber(str,true);
        }
    }

    #region 具体计算（三个重载）

    /// <summary>
    /// 计算每个BUFF生效的具体数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public void CalculateNumber(string str, ref int num, int sourceNum)
    {
        string[] attri = str.Split('_');
        if (attri.Length == 1)
        {
            num = 0 - num;
            return;
        }
        else if (Mathf.Abs(float.Parse(attri[1])) > 1f)
        {
            num = int.Parse(attri[1]);
        }
        else
        {
            num = (int)(sourceNum * float.Parse(attri[1]));
        }
    }

    /// <summary>
    /// 计算每个BUFF生效的具体数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool CalculateNumber(string str, ref bool _bool)
    {
        string[] attri = str.Split('_');
        if (attri.Length == 1)
        {
            _bool = !_bool;
            return !_bool;
        }
        _bool = bool.Parse(attri[1]);
        return bool.Parse(attri[1]);
    }
    #endregion

    //public TradeRoleAttributeChange GetRoleAttributeChange()
    //{
    //    TradeRoleAttributeChange tradeRoleAttribute = new TradeRoleAttributeChange();
    //    foreach (string str in buffData.OnBuffAdd)
    //    {
    //        CheckRoleNumberNoChange(str);
    //    }
    //    return tradeRoleAttribute;
    //}
}
