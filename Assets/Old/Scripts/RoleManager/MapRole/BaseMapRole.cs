﻿using System;
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

    public BaseNpc npcScript;

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
        InvokeRepeating("AddTechPoint", 0f, 10f);
        if (!PlayerData.My.MapRole.Contains(this))
        {
            PlayerData.My.MapRole.Add(this);
        }
        if (!PlayerData.My.RoleData.Contains(baseRoleData))
        {
            PlayerData.My.RoleData.Add(baseRoleData);
        }
    }

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
        ConsumeSign preStatus = shootTarget;
        //if (shootTarget != null)
        //{
        //    if (!shootTarget.isCanSelect)
        //        shootTarget = null;
        //}
        shootTarget = null;
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
        if (preStatus == null && shootTarget != null && !GetComponent<BaseSkill>().IsOpen)
        {
            GetComponent<BaseSkill>().ReUnleashSkills();
        }
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
        baseBuff.RoleBuffAdd();
        //print("buff生成");
    }

    /// <summary>
    /// buff删除时回调
    /// </summary>
    /// <param name="baseBuff"></param>
    public void RemoveBuff(BaseBuff baseBuff)
    {
        baseBuff.RoleBuffRemove();
        buffList.Remove(baseBuff);
        //print("buff消失");
    }

    /// <summary>
    /// 删除指定id的buff
    /// </summary>
    /// <param name="buffId"></param>
    public void RemoveBuffById(int buffId)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].buffId == buffId)
            {
                RemoveBuff(buffList[i]);
                break;
            }
        }
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
            buffList[i].OnRoleTick();
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
            int result = 0;
            result += baseRoleData.baseRoleData.cost;
            result += baseRoleData.equipCost;
            result += baseRoleData.workerCost;
            result += baseRoleData.landCost;
            result = 0 - result;
            //print("每月成本 " + result.ToString());
        }
    }

    public void AddTechPoint()
    {
        StageGoal.My.GetTechPoint(baseRoleData.techAdd);
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

    public List<int > GetEquipBuffList()
    {
        List<int> bufflist = new List<int>();
        if (isNpc)
        {
            if (GetComponent<BaseNpc>().isCanSeeEquip)
            {
                for (int i = 0; i <baseRoleData.EquipList.Keys.ToList().Count; i++)
                {
                    GearData data = GameDataMgr.My.GetGearData(baseRoleData.EquipList.Keys.ToList()[i]);
                    foreach (int item in data.buffList)
                    {
                        if (item != -1)
                        {
                            bufflist.Add(item);
                        }
                    }
                }
            }
           
        }
        else
        {
            for (int i = 0; i <baseRoleData.EquipList.Keys.ToList().Count; i++)
            {
                GearData data = GameDataMgr.My.GetGearData(baseRoleData.EquipList.Keys.ToList()[i]);
                foreach (int item in data.buffList)
                {
                    if (item != -1)
                    {
                        bufflist.Add(item);
                    }
                }
            }
        }
        return bufflist;
    }


    private void OnDestroy()
    {

    }
}