using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
 [Serializable]

public class Role 
{
    
    /// <summary>
    /// 角色基础数据
    /// </summary>
    public  BaseRoleData baseRoleData;

    /// <summary>
    /// 效果值
    /// </summary>
    public int effect;

    /// <summary>
    /// 效率值
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 范围
    /// </summary>
    public int range;

    /// <summary>
    /// 交易成本
    /// </summary>
    public int tradeCost;

    /// <summary>
    /// 固定成本
    /// </summary>
    public int cost;

    /// <summary>
    /// 风险抗力
    /// </summary>
    public int riskResistance;

    /// <summary>
    /// 子弹容量
    /// </summary>
    public int bulletCapacity;

    /// <summary>
    /// ID
    /// </summary>
    public double ID;

    /// <summary>
    /// 装备总成本
    /// </summary>
    public int equipCost;

    /// <summary>
    /// 工人总成本
    /// </summary>
    public int workerCost;

    /// <summary>
    /// 地块总成本
    /// </summary>
    public int landCost;

    /// <summary>
    /// 是否在地图中
    /// </summary>
    public bool inMap;


    public bool isNpc;
    public Dictionary<int, Vector3> EquipList =new Dictionary<int, Vector3>();
    public Dictionary<int, Vector3> peoPleList =new Dictionary<int, Vector3>();
    // Start is called before the first frame update
    
    public Role(BaseRoleData baseRoleData, Dictionary<int, Vector3> _EquipList, Dictionary<int, Vector3> _peoPleList,double ID)
    {
        this.baseRoleData = baseRoleData;
        this.EquipList = new Dictionary<int, Vector3>();
        this.peoPleList = new Dictionary<int, Vector3>();
        foreach (var v in _EquipList)
        {
            this.EquipList.Add(v.Key, v.Value);
        }
        foreach (var v in _peoPleList)
        {
            this.peoPleList.Add(v.Key, v.Value);
        }
        this.ID = ID;
    }

    public Role()
    {

    }


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 初始化角色数值，放置多重叠加
    /// </summary>
    public void InitRoleValue()
    {
        effect =  baseRoleData.effect;
        efficiency = baseRoleData.efficiency;
        range =  baseRoleData.range;
        tradeCost = baseRoleData.tradeCost;
        cost = baseRoleData.cost;
        bulletCapacity =  baseRoleData.bulletCapacity;
        riskResistance =  baseRoleData.riskResistance;
        equipCost = 0;
        workerCost = 0;
        //CurrentRole.cost = CurrentRole.baseRoleData.cost;
    }

    /// <summary>
    /// 计算角色属性数值
    /// </summary>
    public void CalculateAllAttribute()
    {
        InitRoleValue();
        foreach (var i in EquipList)
        {
            GearData tempData = GameDataMgr.My.GetGearData(i.Key);
            effect += tempData.effect;
            efficiency += tempData.efficiency;
            range += tempData.range;
            tradeCost += tempData.tradeCost;
            cost += tempData.cost;
            bulletCapacity += tempData.riskResistance;
            riskResistance += tempData.bulletCapacity;
             equipCost += tempData.cost;
      
        }
     
        foreach (var i in peoPleList)
        {
            WorkerData tempData = GameDataMgr.My.GetWorkerData(i.Key);
            effect += tempData.effect;
            efficiency += tempData.efficiency;
            range += tempData.range;
            tradeCost += tempData.tradeCost;
            cost += tempData.cost;
            riskResistance += tempData.riskResistance;
            bulletCapacity += tempData.bulletCapacity;
            workerCost += tempData.cost;
        }
       
    }
  
    /// <summary>
    /// 设置装备
    /// </summary>
    /// <param name="ID">装备ID</param>
    /// <param name="pos">装备位置</param>
    public void SetEquip(int ID, Vector3 pos)
    {
        if (EquipList.ContainsKey(ID))
        {
            EquipList.Add(ID,pos);
        }

    }

    /// <summary>
    /// 设置人物
    /// </summary>
    /// <param name="ID">人物ID</param>
    /// <param name="pos">人物位置信息</param>
    public void SetPeople(int ID, Vector3 pos)
    {
        if (peoPleList.ContainsKey(ID))
        {
            peoPleList.Add(ID, pos);
        }
    }
}
