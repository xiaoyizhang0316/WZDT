using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DT.Fight.Bullet;
using UnityEngine;

[Serializable]
public class BaseMapRole : MonoBehaviour
{
    public Role baseRoleData;

    /// <summary>
    /// 仓库
    /// </summary>
    public List<ProductData> warehouse;
    
    /// <summary>
    /// 是否是NPC
    /// </summary>
    public bool isNpc = false;

    /// <summary>
    /// 交易列表
    /// </summary>
    public List<TradeSign> tradeList = new List<TradeSign>();
 
    /// <summary>
    /// 自身buff列表
    /// </summary>
    public List<BaseBuff> buffList;

    /// <summary>
    /// 周围的building列表
    /// </summary>
    public Dictionary<int, Building> buildingList = new Dictionary<int, Building>();

    /// <summary>
    /// 召唤消费者间隔
    /// </summary>
    public int spawnConsumerInterval;

    /// <summary>
    /// 进入射程的消费者列表
    /// </summary>
    public List<ConsumeSign> shootTargetList = new List<ConsumeSign>();

    /// <summary>
    /// 当前射击目标
    /// </summary>
    public ConsumeSign shootTarget;

    public int CarCount;

    public bool AI;

    /// <summary>
    /// 角色贡献度
    /// </summary>
    public int contributionNum;

    public Dictionary<int, PayRelationShipData> payRelationShips = new Dictionary<int, PayRelationShipData>();

    #region UI显示信息

    public float totalProfit;

    public float monthlyProfit;

    public float totalSatisfy;

    public float monthlySatisfy;

    public float totalCost;

    public float rentCost;

    public float operationCost;

    public float activityCost;

    public float tradeCost;

    #endregion
    /// <summary>
    /// 商店   最多不能多于  baseroleData.counter
    /// </summary>
    public List<ProductData> shop;

    public void InitBaseRoleData()
    {
        baseRoleData = PlayerData.My.GetRoleById(double.Parse(name));
    }

    // Start is called before the first frame update
    void Start()
    {
        buffList = new List<BaseBuff>();
        contributionNum = 0;
        if (!isNpc)
            InitBaseRoleData();
    
        InvokeRepeating("MonthlyCost", 1f, 60f);
        if (!PlayerData.My.MapRole.Contains(this))
        {
            PlayerData.My.MapRole.Add(this);
        }

        if (!PlayerData.My.RoleData.Contains(baseRoleData))
        {
            PlayerData.My.RoleData.Add(baseRoleData);
        }

        DayBegin();
    }

    #region 交易记录
    /// <summary>
    /// 记录每个独特ID的交易，若该交易重复则更新
    /// </summary>
    /// <param name="data"></param>
    /// <param name="tradeCost"></param>
    public void GenerateTradeHistory(TradeData data, float tradeCost)
    {
        
        
    }

    #endregion

    #region 战斗

    /// <summary>
    /// 将目标消费者加入射击范围列表（防止重复）
    /// </summary>
    /// <param name="sign"></param>
    public void AddConsumerIntoShootList(ConsumeSign sign)
    {
        if (shootTargetList.Contains(sign))
            return;
        else
            shootTargetList.Add(sign);
    }

    /// <summary>
    /// 将目标消费者移出射击范围列表
    /// </summary>
    /// <param name="sign"></param>
    public void RemoveConsumerFromShootList(ConsumeSign sign)
    {
        if (!shootTargetList.Contains(sign))
            return;
        else
            shootTargetList.Remove(sign);
    }

    /// <summary>
    /// 选择范围内最近的消费者作为射击目标
    /// </summary>
    public void SetShootTarget()
    {
        if (shootTargetList.Count == 0)
            return;
        int max = 0;
        float maxDis = Vector3.Distance(transform.position, shootTargetList[0].transform.position);
        for (int i = 0; i < shootTargetList.Count; i++)
        {
            if (Vector3.Distance(transform.position, shootTargetList[i].transform.position) < maxDis)
            {
                maxDis = Vector3.Distance(transform.position, shootTargetList[i].transform.position);
                max = i;
            }
        }
        shootTarget = shootTargetList[max];
    }

    #endregion

    #region BUFF结算

    /// <summary>
    /// buff增加时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void AddBuff(BaseBuff baseBuff)
    {
        //print(baseBuff.buffName);
        buffList.Add(baseBuff);
        baseBuff.BuffAdd();
        //print("buff生成");
    }

    /// <summary>
    /// buff删除时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void RemoveBuff(BaseBuff baseBuff)
    {
        baseBuff.BuffRemove();
        buffList.Remove(baseBuff);
        //print("buff消失");
    }

    /// <summary>
    /// 检测所有buff的持续时间
    /// </summary>
    public void CheckBuffDuration()
    {
        if (buffList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].OnTick();
            if (buffList[i].duration != -1)
            {
                buffList[i].duration--;
                //print("持续时间:" + buffList[i].duration);
                if (buffList[i].duration == 0)
                {
                    RemoveBuff(buffList[i]);
                }
            }
        }
    }

    /// <summary>
    /// 当消费者进商店时
    /// </summary>
    /// <param name="consumer"></param>
    public void OnConsumerInShop(ref ConsumeData consumer)
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnConsumerInShop(ref consumer);
        }
    }

    /// <summary>
    /// 当生产活动完成时
    /// </summary>
    public void OnProductionComplete(ref ProductData product)
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnProductionComplete(ref product);
        }
    }

    /// <summary>
    /// 当交易进行时
    /// </summary>
    public void OnTradeConduct()
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnTradeConduct();
        }
    }

    /// <summary>
    /// 当消费者满意度结算时
    /// </summary>
    /// <param name="satisfy"></param>
    public void OnConsumerSatisfy(ref float satisfy)
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnConsumerSatisfy(ref satisfy);
        }
    }

    /// <summary>
    /// 当濒临破产时
    /// </summary>
    public void OnBeforeDead()
    {
        foreach (BaseBuff b in buffList)
        {
            b.OnBeforeDead();
        }
    }

    #endregion

    #region 钱，满意度相关
    /// <summary>
    /// 每月消耗成本
    /// </summary>
    public void MonthlyCost()
    {
        if (!baseRoleData.isNpc)
        {
            CheckLandPrice();
            int result = 0;
            result += baseRoleData.baseRoleData.cost;
            result += baseRoleData.equipCost;
            result += baseRoleData.workerCost;
            result += baseRoleData.landCost;
            result = 0 - result;
            GetMoney(result);
            //print("每月成本 " + result.ToString());
        }
    }

    /// <summary>
    /// 计算角色的地块成本
    /// </summary>
    public void CheckLandPrice()
    {
        baseRoleData.landCost = 0;
        operationCost = baseRoleData.workerCost + baseRoleData.equipCost + baseRoleData.baseRoleData.cost;
        foreach (MapSign m in GetComponent<RolePosSign>().MapSigns)
        {
            baseRoleData.landCost += MapManager.My.GetLandRoleCost(baseRoleData.baseRoleData.roleType, m.mapType);
        }
    }

    /// <summary>
    /// 累计消费者满意度
    /// </summary>
    /// <param name="num"></param>
    public void GetConsumerSatisfy(float num)
    {
        totalSatisfy += num;
        monthlySatisfy += num;
        num = num * StageGoal.My.playerContributionPerf;
        BubbleManager.My.CreateConsumerBubble(num, transform.localPosition, baseRoleData.ID);
    }

    /// <summary>
    /// 收入
    /// </summary>
    /// <param name="num"></param>
    public void GetMoney(int num)
    {
        //print(num);
        if (num >= 0)
        {
            totalProfit += num;
            monthlyProfit += num;
            if (!baseRoleData.isNpc)
            {
                StageGoal.My.MakeProfit(num);
            }
            ProcessDividePay(num);
        }
        else
        {
            num = Mathf.Abs(num);
            totalCost += num;
            if (!baseRoleData.isNpc)
            {
                StageGoal.My.CostMoney(num);
            }
        }
    }

    /// <summary>
    /// 零售收入
    /// </summary>
    /// <param name="num"></param>
    public void GetConsumerMoney(int num)
    {
        if (!baseRoleData.isNpc)
            BubbleManager.My.CreateMoneyBubble(num, transform.localPosition, baseRoleData.ID);
        //print("收钱：" + num);
    }

    /// <summary>
    /// 付钱给另一个角色
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="num"></param>
    public void PayToRole(string roleId, int num)
    {
        //print("付钱： " + num);
        BaseMapRole targetRole = PlayerData.My.GetMapRoleById(double.Parse(roleId));
        targetRole.GetConsumerMoney(num);
        int result = 0 - num;
        GetMoney(result);
        activityCost += num;

    }

    /// <summary>
    /// 处理分成选项
    /// </summary>
    public int ProcessDividePay(int money)
    {
        List<int> keys = new List<int>(payRelationShips.Keys);
        if (keys.Count == 0)
        {
            return money;
        }
        int rest = money;
        for (int i = 0; i < payRelationShips.Count; i++)
        {
            int temp = (int)(money * payRelationShips[keys[i]].payPercent);
            PayToRole(payRelationShips[keys[i]].targetRole, temp);
            rest -= temp;
        }
        return rest;
    }

    /// <summary>
    /// 获得角色当前分成总计比例
    /// </summary>
    /// <returns></returns>
    public float GetTotalDividePer(int tradeId)
    {
        float result = 0f;
        List<int> keys = new List<int>(payRelationShips.Keys);
        if (keys.Count == 0)
        {
            return result;
        }
        for (int i = 0; i < payRelationShips.Count; i++)
        {
            if (keys[i] != tradeId)
            {
                result += payRelationShips[keys[i]].payPercent;
            }
        }
        return result;
    }

    /// <summary>
    /// 获得新的或者更新已有的付钱关系
    /// </summary>
    /// <param name="tradeData"></param>
    public void GetPayRelationShip(TradeData tradeData)
    {

    }

    /// <summary>
    /// 删除指定的付钱关系
    /// </summary>
    /// <param name="tradeId"></param>
    public void RemovePayRelationShip(int tradeId)
    {
        if (!payRelationShips.ContainsKey(tradeId))
            return;
        else
        {
            payRelationShips.Remove(tradeId);
        }
    }


    /// <summary>
    /// 每月重新统计
    /// </summary>
    public void RecheckInfo()
    {
        rentCost = baseRoleData.landCost;
        operationCost = baseRoleData.workerCost + baseRoleData.equipCost + baseRoleData.baseRoleData.cost;
        monthlyProfit = 0;
        monthlySatisfy = 0;
        activityCost = 0;
        tradeCost = 0;
    }

    #endregion

    #region  移动物品

    /// <summary>
    /// 移动物品到仓库
    /// </summary>
    /// <param name="productData"></param>
    public void MoveGoodsToWareHouse(ProductData productData)
    {
  
        warehouse.Add(productData);
        //Input.Remove(productData);
    }
 
    /// <summary>
    /// 将仓库产品输入到商店
    /// </summary>
    /// <param name="productData"></param>
    public void ShiftProductWarehouseToShop(ProductData productData)
    {
        shop.Add(productData);
//        Debug.Log(warehouse.Count);
       // Debug.Log(warehouse.Contains(productData));
        if (baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer || baseRoleData.baseRoleData.roleType == GameEnum.RoleType.BigDealer)
        {
            //todo
          //  productData.Quality += (int)(baseRoleData.quality * 0.2f);
         //   productData.Brand += (int)(baseRoleData.brand * 0.2f);
        }
        warehouse.Remove(productData);
        //Debug.Log(warehouse.Count);
    }
 
    #endregion

    #region 获取物品

    /// <summary>
    /// 获取输入口产品
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    //  public ProductData GetInputProductData(GameEnum.ProductType type)
    //  {
    //      //print(type);
    //      if (type == GameEnum.ProductType.None)
    //      {
    //          ProductData newProduct = new ProductData();
    //          newProduct.birthday = (int)TimeManager.My.cumulativeTime;
    //          StageGoal.My.productList.Add(newProduct);
    //          return newProduct;
    //      }

    //      for (int i = 0; i < Input.Count; i++)
    //      {
    //          if (Input[i].productType == type)
    //          {
    //              return Input[i];
    //          }
    //      }

    //      return null;
    //  }
    public void AddPruductToWareHouse(ProductData data)
    {
        if (warehouse.Count >baseRoleData.bulletCapacity)
        {
            return;
        }
        else
        {
            warehouse.Add(data);
        }
    }

    /// <summary>
 /// 查找仓库产品
 /// </summary>
 /// <param name="type"></param>
 /// <returns></returns>
 public ProductData SearchWarehouseProductData(BulletType type)
 {
     ProductData pro = null;
     for (int i = 0; i < warehouse.Count; i++)
     {
         if (warehouse[i].bulletType == type)
         {
             pro = warehouse[i];
            

             break;
         }
     }

     return pro;
 }

    /// <summary>
    /// 提取仓库产品
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ProductData GetWarehouseProductData(BulletType type)
    {
        ProductData pro = null;
        for (int i = 0; i < warehouse.Count; i++)
        {
            if (warehouse[i].bulletType == type)
            {
                pro = warehouse[i];
                warehouse.Remove(warehouse[i]);

                break;
            }
        }

        return pro;
    }
 
    #endregion

    #region  技能

    
 

    #endregion

    #region 商店/零售
 
    /// <summary>
    /// 将周围的居民楼加入list
    /// </summary>
    /// <param name="buildingId"></param>
    /// <param name="building"></param>
    public void AddBuilding(int buildingId,Building building)
    {
        if (buildingList.ContainsKey(buildingId))
        {
            return;
        }
        else
        {
            buildingList.Add(buildingId, building);
        }
    }

    /// <summary>
    /// 周期性召唤消费者
    /// </summary>
    public void SpawnConsumer()
    {
        int number = 5;
        List<GameObject> availableConsumer = new List<GameObject>();
        List<int> keys = new List<int>(buildingList.Keys);
        for (int i = 0; i < buildingList.Count; i++)
        {
            for (int j = 0; j < buildingList[keys[i]].consumerGoList.Count; j++)
            {
                if (!buildingList[keys[i]].consumerGoList[j].activeInHierarchy)
                {
                    availableConsumer.Add(buildingList[keys[i]].consumerGoList[j]);
                }
            }
        }
        List<GameObject> resultList = GetRandom(availableConsumer, number);
        for (int i = 0; i < resultList.Count; i++)
        {
            resultList[i].SetActive(true);
            resultList[i].GetComponent<ConsumeSign>().InitAndMove(this);
        }
    }

    /// <summary>
    /// 白天开始时开始召唤消费者
    /// </summary>
    public void DayBegin()
    {
        if (baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            InvokeRepeating("SpawnConsumer", 0f, 10f);
            InvokeRepeating("SetShootTarget", 0f, 0.2f);
        }
    }

    /// <summary>
    /// 白天结束时停止召唤消费者
    /// </summary>
    public void DayEnd()
    {
        if (baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            CancelInvoke("SpawnConsumer");
            CancelInvoke("SetShootTarget");
        }
    }

    /// <summary>
    /// 从列表中随机N个Gameobject
    /// </summary>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public List<GameObject> GetRandom(List<GameObject> list,int num)
    {
        List<GameObject> result = new List<GameObject>();
        if (list.Count <= num)
        {
            return list;
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                int selectId = UnityEngine.Random.Range(0,list.Count);
                result.Add(list[selectId]);
                list.RemoveAt(selectId);
            }
        }
        return result;
    }

    #endregion

    /// <summary>
    /// 当角色获得贡献度时
    /// </summary>
    /// <param name="contributionNumber"></param>
    public void GetContribution(int contributionNumber)
    {
        contributionNum += contributionNumber;
    }


    private void OnDestroy()
    {

    }

    [Serializable]
    public class TradeRecordData
    {
        public string startRole;

        public string endRole;

        public string selectSkill;

        public int skillCost;

        public float transactionCost;

        public float income;
    }

    [Serializable]
    public class PayRelationShipData
    {
        public string targetRole;

        public float payPercent;
    }
}