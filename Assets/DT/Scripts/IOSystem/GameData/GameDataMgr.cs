using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using static GameEnum;
using System.Linq;
using System;
using UnityEngine.SceneManagement;


public class GameDataMgr : MonoSingletonDontDestroy<GameDataMgr>
{
    /// <summary>
    /// 角色模板库
    /// </summary>
    public List<RoleTemplateModelData> roleTemplateModelDatas;

    /// <summary>
    /// 装备库
    /// </summary>
    public List<GearData> gearDatas;

    /// <summary>
    /// 工人库
    /// </summary>
    public List<WorkerData> workerDatas; 

    /// <summary>
    /// Buff数据库
    /// </summary>
    public List<BuffData> buffDatas;

    /// <summary>
    /// Buff数据库
    /// </summary>
    public List<ConsumableData> consumableDatas;

    /// <summary>
    /// 关卡配置数据库
    /// </summary>
    public List<StageData> stageDatas;

    public List<ConsumerTypeData> consumerTypeDatas;

    /// <summary>
    /// 根据职业类型和等级获取角色模板
    /// </summary>
    /// <param name="roleType"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public BaseRoleData GetModelData(RoleType roleType, int level)
    {
        foreach (RoleTemplateModelData r in roleTemplateModelDatas)
        {
            if (r.level == level && roleType.ToString().Equals(r.roleType.ToString()))
            {
                BaseRoleData data = new BaseRoleData();
                data.bulletCapacity = r.bulletCapacity;
                data.effect = r.effect;
                data.efficiency = r.efficiency;
                data.cost = r.cost;
                data.level = r.level;
                data.range = r.range;
                data.roleSkillType = r.roleSkillType;
                data.riskResistance = r.riskResistance;
                data.roleType = r.roleType;
                data.tradeCost = r.tradeCost;
                data.PrePath = r.PrePath;
                data.RoleSpacePath = r.RoleSpacePath;
                data.SpritePath = r.SpritePath;
                data.upgradeCost = r.upgradeCost;
                data.roleName = "";
                data.costTech = r.costTech;
                return data;
            }
        }
        print("------------查不到此角色模板!-----------");
        return null;
    }

    /// <summary>
    /// 根据ID获取装备
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GearData GetGearData(int id)
    {
        foreach (GearData g in gearDatas)
        {
            if (g.ID == id)
                return g;
        }
        print("------------查不到此装备!-----------");
        return null;
    }

    /// <summary>
    /// 根据ID获取工人
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public WorkerData GetWorkerData(int id)
    {
        foreach (WorkerData w in workerDatas)
        {
            if (w.ID == id)
                return w;
        }
        print("------------查不到此工人!-----------");
        return null;
    }

    /// <summary>
    /// 根据buff ID查找BUFF数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BuffData GetBuffDataByID(int id)
    {
        foreach (BuffData b in buffDatas)
        {
            if (b.BuffID == id)
            {
                BuffData temp = new BuffData();
                temp.BuffID = b.BuffID;
                temp.bulletBuffType = b.bulletBuffType;
                temp.BuffName = b.BuffName;
                temp.BuffDesc = b.BuffDesc;
                temp.elementType = b.elementType;
                temp.attackEffect = b.attackEffect;
                temp.OnBuffAdd.AddRange(b.OnBuffAdd);
                temp.OnBuffRemove.AddRange(b.OnBuffRemove);
                temp.OnBeforeDead.AddRange(b.OnBeforeDead);
                temp.OnTick.AddRange(b.OnTick);
                temp.OnProduct.AddRange(b.OnProduct);
                temp.duration = b.duration;
                temp.interval = b.interval;
                return temp;
            }
        }
        print("------------查不到此BUFF!-----------" + id);
        return null;
    }

    /// <summary>
    /// 根据消耗品ID查找消耗品数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ConsumableData GetConsumableDataByID(int id)
    {
        foreach (ConsumableData c in consumableDatas)
        {
            if (c.consumableId == id)
                return c;
        }
        print("----------查不到此消耗品-------------");
        return null;
    }

    /// <summary>
    /// 根据名称查找关卡配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public StageData GetStageDataByName(string name)
    {
        foreach (StageData s in stageDatas)
        {
            if (s.sceneName.Equals(name))
            {
                return s;
            }
        }
        print("---------------查不到当前场景配置表！！--------------");
        return null;
    }

    /// <summary>
    /// 根据消费者类别查找消费者数据
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ConsumerTypeData GetConsumerTypeDataByType(ConsumerType type)
    {
        foreach (ConsumerTypeData c in consumerTypeDatas)
        {
            if (c.consumerType == type)
            {
                return c;
            }
        }
        print("----------------查不到此消费者类别----------------");
        return null;
    }

    /// <summary>
    /// 将Buff源数据转成可用数据
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseBuffData(BuffsData rawData)
    {
        buffDatas = new List<BuffData>();
        foreach (BuffItem b in rawData.buffSigns)
        {
            BuffData temp = new BuffData();
            temp.BuffID = int.Parse(b.BuffID);
            temp.bulletBuffType = (BulletBuffType)Enum.Parse(typeof(BulletBuffType), b.BulletBuffType);
            temp.BuffName = b.BuffName;
            temp.BuffDesc = b.BuffDesc;
            temp.elementType = (ProductElementType)Enum.Parse(typeof(ProductElementType), b.ElementType);
            temp.attackEffect = int.Parse(b.AttackEffect);
            temp.OnBuffAdd = b.OnBuffAdd.Split(',').ToList();
            temp.OnBuffRemove = b.OnBuffRemove.Split(',').ToList();
            temp.OnBeforeDead = b.OnBeforeDead.Split(',').ToList();
            temp.OnProduct = b.OnProduct.Split(',').ToList();
            temp.OnTick = b.OnTick.Split(',').ToList();
            temp.duration = int.Parse(b.Duration);
            temp.interval = int.Parse(b.Interval);
            buffDatas.Add(temp);
        }
        //print(buffDatas.Count);
    }

    /// <summary>
    /// 将道具源数据转成可用数据
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseConsumableData(ConsumablesData rawData)
    {
        consumableDatas = new List<ConsumableData>();
        foreach (ConsumableItem b in rawData.consumableSigns)
        {
            ConsumableData temp = new ConsumableData();
            temp.consumableId = int.Parse(b.consumableId);
            temp.consumableName = b.consumableName;
            temp.consumableDesc = b.consumableDesc;
            string[] strList = b.targetBuffList.Split(',');
            temp.targetBuffList = new List<int>();
            foreach (string str in strList)
            {
                temp.targetBuffList.Add(int.Parse(str));
            }
            consumableDatas.Add(temp);
        }
        foreach (ConsumableData c in consumableDatas)
        {
            PlayerData.My.playerConsumables.Add(new PlayerConsumable(c.consumableId, 3));
        }
        //ConsumableListManager.My.Init();
    }

    /// <summary>
    /// parse关卡数据源
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseStageData(StagesData rawData)
    {
        stageDatas = new List<StageData>();
        foreach (StageItem s in rawData.stageSigns)
        {
            StageData temp = new StageData();
            temp.sceneName = s.sceneName;
            temp.maxWaveNumber = int.Parse(s.maxWaveNumber);
            temp.startPlayerHealth = int.Parse(s.startPlayerHealth);
            temp.startPlayerGold = int.Parse(s.startPlayerGold);
            temp.startTech = int.Parse(s.startTech);
            temp.stageType = (StageType)Enum.Parse(typeof(StageType), s.stageType);
            temp.waveWaitTime = new List<int>();
            string[] waitTimeList = s.waveWaitTime.Split(',');
            foreach (string str in waitTimeList)
            {
                temp.waveWaitTime.Add(int.Parse(str));
            }
            temp.starOneEquip = new List<int>();
            temp.starOneWorker = new List<int>();
            temp.starTwoEquip = new List<int>();
            temp.starTwoWorker = new List<int>();
            temp.starThreeEquip = new List<int>();
            temp.starThreeWorker = new List<int>();
            string[] strList = s.starOneEquip.Split(',');
            foreach (string str in strList)
            {
                if (int.Parse(str) != -1)
                    temp.starOneEquip.Add(int.Parse(str));
            }
            strList = s.starOneWorker.Split(',');
            foreach (string str in strList)
            {
                if (int.Parse(str) != -1)
                    temp.starOneWorker.Add(int.Parse(str));
            }
            strList = s.starTwoEquip.Split(',');
            foreach (string str in strList)
            {
                if (int.Parse(str) != -1)
                    temp.starTwoEquip.Add(int.Parse(str));
            }
            strList = s.starTwoWorker.Split(',');
            foreach (string str in strList)
            {
                if (int.Parse(str) != -1)
                    temp.starTwoWorker.Add(int.Parse(str));
            }
            strList = s.starThreeEquip.Split(',');
            foreach (string str in strList)
            {
                if (int.Parse(str) != -1)
                    temp.starThreeEquip.Add(int.Parse(str));
            }
            strList = s.starThreeWorker.Split(',');
            foreach (string str in strList)
            {
                if (int.Parse(str) != -1)
                    temp.starThreeWorker.Add(int.Parse(str));
            }
            stageDatas.Add(temp);
        }
    }

    /// <summary>
    /// parse工人数据源
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseWorkerData(WorkersData rawData)
    {
        workerDatas = new List<WorkerData>();
        foreach (WorkerItem w in rawData.workerItems)
        {
            WorkerData temp = new WorkerData();
            temp.ID = int.Parse(w.workerId);
            temp.workerName = w.workerName;
            temp.ProductOrder = int.Parse(w.level);
            temp.effect = int.Parse(w.effect);
            temp.efficiency = int.Parse(w.efficiency);
            temp.range = int.Parse(w.range);
            temp.riskResistance = int.Parse(w.riskResistance);
            temp.tradeCost = int.Parse(w.tradeCost);
            temp.cost = int.Parse(w.cost);
            temp.bulletCapacity = int.Parse(w.bulletCapacity);
            temp.techAdd = int.Parse(w.techAdd);
            //temp.PDP = (PDPType)Enum.Parse(typeof(PDPType), w.PDP);
            temp.Init();
            workerDatas.Add(temp);
        }
    }

    /// <summary>
    /// parse装备数据源
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseEquipData(GearsData rawData)
    {
        gearDatas = new List<GearData>();
        foreach (GearItem e in rawData.gearItems)
        {
            GearData temp = new GearData();
            temp.ID = int.Parse(e.equipId);
            temp.equipName = e.equipName;
            temp.ProductOrder = int.Parse(e.level);
            temp.effect = int.Parse(e.effect);
            temp.efficiency = int.Parse(e.efficiency);
            temp.range = int.Parse(e.range);
            temp.riskResistance = int.Parse(e.riskResistance);
            temp.tradeCost = int.Parse(e.tradeCost);
            temp.cost = int.Parse(e.cost);
            temp.bulletCapacity = int.Parse(e.bulletCapacity);
            temp.buffList = new List<int>();
            string[] strList = e.buffList.Split(',');
            foreach (string s in strList)
            {
                temp.buffList.Add(int.Parse(s));
            }
            temp.Init();
            gearDatas.Add(temp);
        }
    }

    /// <summary>
    /// parse角色模板数据源
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseRoleTemplateData(RoleTemplateModelsData rawData)
    {
        roleTemplateModelDatas = new List<RoleTemplateModelData>();
        foreach (RoleTemplateModelItem r in rawData.roleTemplateModelItems)
        {
            RoleTemplateModelData temp = new RoleTemplateModelData();
            temp.roleType = (RoleType)Enum.Parse(typeof(RoleType), r.roleType);
            temp.level = int.Parse(r.level);
            temp.effect = int.Parse(r.effect);
            temp.efficiency = int.Parse(r.efficiency);
            temp.range = int.Parse(r.range);
            temp.riskResistance = int.Parse(r.riskResistance);
            temp.tradeCost = int.Parse(r.tradeCost);
            temp.cost = int.Parse(r.cost);
            temp.bulletCapacity = int.Parse(r.bulletCapacity);
            temp.costTech = int.Parse(r.costTech);
            temp.upgradeCost = int.Parse(r.upgradeCost);
            temp.Init();
            roleTemplateModelDatas.Add(temp);
        }
    }

    /// <summary>
    /// parse消费者类型数据
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseConsumerTypeData(ConsumerTypesData rawData)
    {
        consumerTypeDatas = new List<ConsumerTypeData>();
        foreach (ConsumerTypeItem c in rawData.consumerTypeSigns)
        {
            ConsumerTypeData temp = new ConsumerTypeData();
            temp.consumerType = (ConsumerType)Enum.Parse(typeof(ConsumerType), c.consumerType);
            temp.typeDesc = c.typeDesc;
            temp.maxHealth = int.Parse(c.maxHealth);
            temp.moveSpeed = float.Parse(c.moveSpeed);
            temp.killMoney = int.Parse(c.killMoney);
            temp.killSatisfy = int.Parse(c.killSatisfy);
            temp.liveSatisfy = int.Parse(c.liveSatisfy);
            List<string> strList = c.bornBuff.Split(',').ToList();
            temp.bornBuff = new List<int>();
            foreach(string str in strList)
            {
                if (int.Parse(str) != -1)
                {
                    temp.bornBuff.Add(int.Parse(str));
                }
            }
            consumerTypeDatas.Add(temp);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        foreach (RoleTemplateModelData r in roleTemplateModelDatas)
        {
            r.Init();
        }
        foreach (GearData g in gearDatas)
        {
            g.Init();
        }
        foreach (WorkerData w in workerDatas)
        {
            w.Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}