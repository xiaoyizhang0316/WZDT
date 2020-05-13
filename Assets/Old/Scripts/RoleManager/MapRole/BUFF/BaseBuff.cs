using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static JYFS;

[Serializable]
public class BaseBuff
{
    public BuffConfig buffConfig;

    public BuffData buffData;

    public int buffId;

    public int duration;

    public int interval;

    public bool canHeap;

    public string buffName;

    public int count;

    public BaseMapRole castRole;

    public BaseMapRole targetRole;

    public BaseMapRole buffRole;

    //Dictionary<string,List<int>>

    /// <summary>
    /// 当BUFF添加时
    /// </summary>
    public void BuffAdd()
    {
        foreach (string str in buffData.OnBuffAdd)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当BUFF移除时
    /// </summary>
    public void BuffRemove()
    {
        foreach (string str in buffData.OnBuffRemove)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当交易进行时
    /// </summary>
    public void OnTradeConduct()
    {
        foreach (string str in buffData.OnTradeConduct)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当消费者进店时
    /// </summary>
    public void OnConsumerInShop(ref ConsumeData consumer)
    {
        foreach (string str in buffData.OnConsumerInShop)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
            CheckConsumerNumber(str, ref consumer);
        }
    }

    /// <summary>
    /// 当消费者购物时
    /// </summary>
    public void OnConsumerBuyProduct()
    {
        foreach (string str in buffData.OnConsumerBuyProduct)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当消费者满意度结算时
    /// </summary>
    public void OnConsumerSatisfy(ref float satisfy)
    {
        foreach (string str in buffData.OnConsumerSatisfy)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
            CheckConsumerSatisfyNumber(str, ref satisfy);
        }
    }

    /// <summary>
    /// 当濒临破产时
    /// </summary>
    public void OnBeforeDead()
    {
        foreach (string str in buffData.OnBeforeDead)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
        }
    }

    /// <summary>
    /// 当生产活动完成时
    /// </summary>
    public void OnProductionComplete(ref ProductData product)
    {
        foreach (string str in buffData.OnProductionComplete)
        {
            CheckStaticNumber(str);
            CheckRoleNumber(str);
            CheckProductNumber(str, ref product);
        }
    }

    /// <summary>
    /// 周期性活动时
    /// </summary>
    public void OnTick()
    {
        count++;
        if (count == buffData.interval)
        {
            foreach (string str in buffData.OnTick)
            {
                CheckStaticNumber(str);
                CheckRoleNumber(str);
            }
            count = 0;
        }
    }

    /// <summary>
    /// 将buff付给目标
    /// </summary>
    public void AddBuffToTarget()
    {
        buffRole.AddBuff(this);
    }

    /// <summary>
    /// 设置buff的发起者，承受者，目标
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="target"></param>
    public void SetRoleBuff(BaseMapRole _castRole, BaseMapRole target, BaseMapRole _buffRole)
    {
        castRole = _castRole;
        targetRole = target;
        buffRole = _buffRole;
        AddBuffToTarget();
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
        //Debug.Log(buff.duration);
        duration = buff.duration;
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
                CalculateNumber(str,ref buffConfig.bossSatisfyChange);
                StageGoal.My.bossSatisfy += buffConfig.bossSatisfyChange;
                break;
            case 2:
                CalculateNumber(str, ref buffConfig.consumerSafisfyChange);
                StageGoal.My.customerSatisfy += buffConfig.consumerSafisfyChange;
                break;
            case 3:
                CalculateNumber(str, ref buffConfig.executionChange);
                ExecutionManager.My.AddExecution(buffConfig.executionChange);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 计算角色相关的buff
    /// </summary>
    /// <param name="str"></param>
    public void CheckRoleNumber(string str)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 11:
                CalculateNumber(str,ref buffConfig.roleCapacityChange);
                //todo
             //   targetRole.baseRoleData.capacity += buffConfig.roleCapacityChange;
                break;
            case 12:
                CalculateNumber(str,ref buffConfig.roleEfficiencyChange);
                targetRole.baseRoleData.efficiency += buffConfig.roleEfficiencyChange;
                break;
            case 13:
                CalculateNumber(str,ref buffConfig.roleQualityChange);
             //   targetRole.baseRoleData.quality += buffConfig.roleQualityChange;
                break;
            case 14:
                CalculateNumber(str,ref buffConfig.roleBrandChange);
             //   targetRole.baseRoleData.brand += buffConfig.roleBrandChange;
                break;
            case 15:
                CalculateNumber(str,ref buffConfig.roleSearchChange);
             //   targetRole.baseRoleData.search += buffConfig.roleSearchChange;
                break;
            case 16:
                CalculateNumber(str, ref buffConfig.roleBargainChange);
                //targetRole.baseRoleData.bargain += buffConfig.roleBargainChange;
                break;
            case 17:
                CalculateNumber(str,ref buffConfig.roleDeliverChange);
               // targetRole.baseRoleData.delivery += buffConfig.roleDeliverChange;
                break;
            case 18:
                CalculateNumber(str, ref buffConfig.roleRiskChange);
              //  targetRole.baseRoleData.risk += buffConfig.roleRiskChange;
                break;
            case 19:
                CalculateNumber(str, ref buffConfig.roleMonthCostChange);
               // targetRole.baseRoleData.costMonth += buffConfig.roleMonthCostChange;
                break;
            default:
                break;
        }
    }

    public void CheckRoleNumberNoChange(string str)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 11:
                CalculateNumber(str, ref buffConfig.roleCapacityChange);
                break;
            case 12:
                CalculateNumber(str, ref buffConfig.roleEfficiencyChange);
                break;
            case 13:
                CalculateNumber(str, ref buffConfig.roleQualityChange);
                break;
            case 14:
                CalculateNumber(str, ref buffConfig.roleBrandChange);
                break;
            case 15:
                CalculateNumber(str, ref buffConfig.roleSearchChange);
                break;
            case 16:
                CalculateNumber(str, ref buffConfig.roleBargainChange);
                break;
            case 17:
                CalculateNumber(str, ref buffConfig.roleDeliverChange);
                break;
            case 18:
                CalculateNumber(str, ref buffConfig.roleRiskChange);
                break;
            case 19:
                CalculateNumber(str, ref buffConfig.roleMonthCostChange);
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
    public void CheckConsumerNumber(string str, ref ConsumeData consumeData)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 30:
                buffConfig.consumerSweetChange = CalculateNumber(str);
                consumeData.needSweetness += buffConfig.consumerSweetChange;
                break;
            case 31:
                buffConfig.consumerCrispChange = CalculateNumber(str);
                consumeData.needCrisp += buffConfig.consumerCrispChange;
                break;
            case 32:
                buffConfig.consumerMentalPriceChange = CalculateNumber(str);
                consumeData.mentalPrice += buffConfig.consumerMentalPriceChange;
                break;
            case 33:
                buffConfig.consumerSearchDistanceChange = CalculateNumber(str);
                consumeData.searchDistance += buffConfig.consumerSearchDistanceChange;
                break;
            case 34:
                buffConfig.consumerBuyPowerChange = CalculateNumber(str);
                consumeData.buyPower += buffConfig.consumerBuyPowerChange;
                break;
            case 35:
                buffConfig.consumerBuyRangeChange = CalculateNumber(str);
                consumeData.buyRange += buffConfig.consumerBuyRangeChange;
                break;
            case 36:
                buffConfig.consumerSearchChange = CalculateNumber(str);
                consumeData.search += buffConfig.consumerSearchChange;
                break;
            case 37:
                buffConfig.consumerBargainChange = CalculateNumber(str);
                consumeData.bargain += buffConfig.consumerBargainChange;
                break;
            case 38:
                buffConfig.consumerDeliverChange = CalculateNumber(str);
                consumeData.delivery += buffConfig.consumerDeliverChange;
                break;
            case 39:
                buffConfig.consumerRiskChange = CalculateNumber(str);
                consumeData.risk += buffConfig.consumerRiskChange;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 计算消费者满意度相关的buff
    /// </summary>
    /// <param name="str"></param>
    /// <param name="satisfyNum"></param>
    public void CheckConsumerSatisfyNumber(string str, ref float satisfyNum)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 40:
                buffConfig.consumerSingleSatisfyChange = CalculateNumber(str);
                satisfyNum += buffConfig.consumerSingleSatisfyChange;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 计算产品相关的buff
    /// </summary>
    /// <param name="product"></param>
    public void CheckProductNumber(string str,ref ProductData product)
    {
        string[] attri = str.Split('_');
        switch (int.Parse(attri[0]))
        {
            case -1:
                break;
            case 50:
                buffConfig.productSweetChange = CalculateNumber(str);
                product.Sweetness += buffConfig.productSweetChange;
                break;
            case 51:
                buffConfig.productCrispChange = CalculateNumber(str);
                product.Crisp += buffConfig.productCrispChange;
                break;
            case 52:
                buffConfig.productQualityChange = CalculateNumber(str);
                product.Quality += buffConfig.productQualityChange;
                break;
            case 53:
                buffConfig.productBrandChange = CalculateNumber(str);
                product.Brand += buffConfig.productBrandChange;
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

    #region 具体计算（三个重载）
    /// <summary>
    /// 计算每个BUFF生效的具体数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public void CalculateNumber(string str,ref float num)
    {
        string[] attri = str.Split('_');
        if (attri.Length == 1)
        {
            num = 0 - num;
            return;
        }
        if (attri[1].Equals("TAR"))
        {
            switch(attri[2])
            {
                //todo
                case "CAPACITY":
              //      num = (targetRole.baseRoleData.capacity * float.Parse(attri[3]));
                    break;
                case "Efficiency":
                  //  num = (int)(targetRole.baseRoleData.efficiency * float.Parse(attri[3]));
                    break;
                case "QUALITY":
                //    num = (int)(targetRole.baseRoleData.quality * float.Parse(attri[3]));
                    break;
                case "BRAND":
                 //   num = (int)(targetRole.baseRoleData.brand * float.Parse(attri[3]));
                    break;
                case "SEARCH":
                 //   num = (int)(targetRole.baseRoleData.search * float.Parse(attri[3]));
                    break;
                case "BARGAIN":
                //    num = (int)(targetRole.baseRoleData.bargain * float.Parse(attri[3]));
                    break;
                case "DELIVERY":
               //     num = (int)(targetRole.baseRoleData.delivery * float.Parse(attri[3]));
                    break;
                case "RISK":
                  //  num = (int)(targetRole.baseRoleData.risk * float.Parse(attri[3]));
                    break;
                default:
                    break;
            }
        }
        else if (attri[1].Equals("CAST"))
        {
            switch (attri[2])
            {
                //todo
                case "CAPACITY":
              //      num = (int)(castRole.baseRoleData.capacity * float.Parse(attri[3]));
                    break;
                case "Efficiency":
                    num = (int)(castRole.baseRoleData.efficiency * float.Parse(attri[3]));
                    break;
                case "QUALITY":
               //     num = (int)(castRole.baseRoleData.quality * float.Parse(attri[3]));
                    break;
                case "BRAND":
                 //   num = (int)(castRole.baseRoleData.brand * float.Parse(attri[3]));
                    break;
                case "SEARCH":
                  //  num = (int)(castRole.baseRoleData.search * float.Parse(attri[3]));
                  break;
                case "BARGAIN":
                  //  num = (int)(castRole.baseRoleData.bargain * float.Parse(attri[3]));
                    break;
                case "DELIVERY":
                   // num = (int)(castRole.baseRoleData.delivery * float.Parse(attri[3]));
                    break;
                case "RISK":
                  //  num = (int)(castRole.baseRoleData.risk * float.Parse(attri[3]));
                    break;
                default:
                    break;
            }
        }
        else if (IsNumberic(attri[1]))
        {
            num = float.Parse(attri[1]);
        }
    }

    /// <summary>
    /// 计算每个BUFF生效的具体数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public void CalculateNumber(string str, ref int num)
    {
        string[] attri = str.Split('_');
        if (attri.Length == 1)
        {
            num = 0 - num;
            return;
        }
        if (attri[1].Equals("TAR"))
        {
            switch (attri[2])
            {
                //todo
                case "CAPACITY":
                 //   num = (int)(targetRole.baseRoleData.capacity * float.Parse(attri[3]));
                    break;
                case "Efficiency":
                  //  num = (int)(targetRole.baseRoleData.efficiency * float.Parse(attri[3]));
                    break;
                case "QUALITY":
                 //   num = (int)(targetRole.baseRoleData.quality * float.Parse(attri[3]));
                    break;
                case "BRAND":
                 //   num = (int)(targetRole.baseRoleData.brand * float.Parse(attri[3]));
                    break;
                case "SEARCH":
                  //  num = (int)(targetRole.baseRoleData.search * float.Parse(attri[3]));
                    break;
                case "BARGAIN":
                 //   num = (int)(targetRole.baseRoleData.bargain * float.Parse(attri[3]));
                    break;
                case "DELIVERY":
                 //   num = (int)(targetRole.baseRoleData.delivery * float.Parse(attri[3]));
                    break;
                case "RISK":
                 //   num = (int)(targetRole.baseRoleData.risk * float.Parse(attri[3]));
                    break;
                default:
                    break;
            }
        }
        else if (attri[1].Equals("CAST"))
        {
            switch (attri[2])
            {
                //todo
                case "CAPACITY":
               //     num = (int)(castRole.baseRoleData.capacity * float.Parse(attri[3]));
                    break;
                case "Efficiency":
               //     num = (int)(castRole.baseRoleData.efficiency * float.Parse(attri[3]));
                    break;
                case "QUALITY":
                //    num = (int)(castRole.baseRoleData.quality * float.Parse(attri[3]));
                    break;
                case "BRAND":
                 //   num = (int)(castRole.baseRoleData.brand * float.Parse(attri[3]));
                    break;
                case "SEARCH":
                 //   num = (int)(castRole.baseRoleData.search * float.Parse(attri[3]));
                    break;
                case "BARGAIN":
                 //   num = (int)(castRole.baseRoleData.bargain * float.Parse(attri[3]));
                    break;
                case "DELIVERY":
                 //   num = (int)(castRole.baseRoleData.delivery * float.Parse(attri[3]));
                    break;
                case "RISK":
                 //   num = (int)(castRole.baseRoleData.risk * float.Parse(attri[3]));
                    break;
                default:
                    break;
            }
        }
        else if (IsNumberic(attri[1]))
        {
            num = int.Parse(attri[1]);
        }
    }

    /// <summary>
    /// 计算每个BUFF生效的具体数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public int CalculateNumber(string str)
    {
        string[] attri = str.Split('_');
        if (attri[1].Equals("TAR"))
        {
            //todo
            switch (attri[2])
            {
             //   case "CAPACITY":
             //       return (int)(targetRole.baseRoleData.capacity * float.Parse(attri[3]));
             //   case "Efficiency":
             //       return (int)(targetRole.baseRoleData.efficiency * float.Parse(attri[3]));
             //   case "QUALITY":
             //       return (int)(targetRole.baseRoleData.quality * float.Parse(attri[3]));
             //   case "BRAND":
             //       return (int)(targetRole.baseRoleData.brand * float.Parse(attri[3]));
             //   case "SEARCH":
             //       return (int)(targetRole.baseRoleData.search * float.Parse(attri[3]));
             //   case "BARGAIN":
             //       return (int)(targetRole.baseRoleData.bargain * float.Parse(attri[3]));
             //   case "DELIVERY":
             //       return (int)(targetRole.baseRoleData.delivery * float.Parse(attri[3]));
             //   case "RISK":
             //       return (int)(targetRole.baseRoleData.risk * float.Parse(attri[3]));
             //   default:
             //       return 0;
            }
        }
        else if (attri[1].Equals("CAST"))
        {
            switch (attri[2])
            {
              // case "CAPACITY":
              //     return (int)(castRole.baseRoleData.capacity * float.Parse(attri[3]));
              // case "Efficiency":
              //     return (int)(castRole.baseRoleData.efficiency * float.Parse(attri[3]));
              // case "QUALITY":
              //     return (int)(castRole.baseRoleData.quality * float.Parse(attri[3]));
              // case "BRAND":
              //     return (int)(castRole.baseRoleData.brand * float.Parse(attri[3]));
              // case "SEARCH":
              //     return (int)(castRole.baseRoleData.search * float.Parse(attri[3]));
              // case "BARGAIN":
              //     return (int)(castRole.baseRoleData.bargain * float.Parse(attri[3]));
              // case "DELIVERY":
              //     return (int)(castRole.baseRoleData.delivery * float.Parse(attri[3]));
              // case "RISK":
              //     return (int)(castRole.baseRoleData.risk * float.Parse(attri[3]));
              // default:
              //     return 0;
            }
        }
        else if (IsNumberic(attri[1]))
        {
            return int.Parse(attri[1]);
        }
        return 0;
    }
    #endregion

    public TradeRoleAttributeChange GetRoleAttributeChange()
    {
        TradeRoleAttributeChange tradeRoleAttribute = new TradeRoleAttributeChange();
        foreach (string str in buffData.OnBuffAdd)
        {
            CheckRoleNumberNoChange(str);
        }
        tradeRoleAttribute.brandAdd = buffConfig.roleBrandChange;
        tradeRoleAttribute.qualityAdd = buffConfig.roleQualityChange;
        tradeRoleAttribute.capacityAdd = buffConfig.roleCapacityChange;
        tradeRoleAttribute.efficiencyAdd = buffConfig.roleEfficiencyChange;
        tradeRoleAttribute.searchAdd = buffConfig.roleSearchChange;
        tradeRoleAttribute.bargainAdd = buffConfig.roleBargainChange;
        tradeRoleAttribute.deliveryAdd = buffConfig.roleDeliverChange;
        tradeRoleAttribute.riskAdd = buffConfig.roleRiskChange;
        return tradeRoleAttribute;
    }
}
