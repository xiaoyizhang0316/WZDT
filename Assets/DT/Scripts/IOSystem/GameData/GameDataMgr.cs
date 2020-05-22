﻿using System.Collections;
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
    /// 活动库
    /// </summary>
    public List<ActivityData> activityDatas;

    /// <summary>
    /// 技能数据库
    /// </summary>
    public List<SkillData> skillDatas;

    /// <summary>
    /// 交易技能解锁库
    /// </summary>
    public List<TradeSkillData> tradeSkillDatas;

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
                return r.tempRoleData;
        }
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
        return null;
    }

    /// <summary>
    /// 根据ID获取活动数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ActivityData GetActivityDataById(int id)
    {
        foreach (ActivityData a in activityDatas)
        {
            if (a.ID == id)
                return a;
        }
        return null;
    }

    /// <summary>
    /// 根据技能ID查找技能
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SkillData GetSkillDataByID(int id)
    {
        foreach (SkillData s in skillDatas)
        {
            if (s.skillID == id)
                return s;
        }
        return null;
    }

    /// <summary>
    /// 根据技能名称查找技能
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public SkillData GetSkillDataByName(string name)
    {
        foreach (SkillData s in skillDatas)
        {
            if (s.skillName.Equals(name))
                return s;
        }
        return null;
    }

    /// <summary>
    /// 根据发起方，承受方和技能名称查找技能数据
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="skillName"></param>
    /// <returns></returns>
    public TradeSkillData GetSkillDataByStartEndConductSkill(RoleType start, RoleType end, RoleType cast, string skillName)
    {

        SkillData data = GetSkillDataByName(skillName);
        foreach (TradeSkillData s in tradeSkillDatas)
        {
            if (s.startRole == start && s.endRole == end && s.conductRole == cast && data.skillID == s.skillId)
                return s;
        }
        print("找不到技能数据--------------");
        return null;
    }

    /// <summary>
    /// 根据技能ID查找技能数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TradeSkillData GetTradeSkillDataByID(int id)
    {
        foreach (TradeSkillData s in tradeSkillDatas)
        {
            if (s.ID == id)
                return s;
        }
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
                return b;
        }
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
        print("查不到此消耗品");
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
        print("查不到当前场景配置表！！");
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
        print("查不到此消费者类别----------------");
        return null;
    }

    /// <summary>
    /// 将技能源数据转成可用数据
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseSkillData(SkillsData rawData)
    {
        skillDatas = new List<SkillData>();
        foreach (SkillItem s in rawData.skillSigns)
        {
            SkillData skillData = new SkillData();
            skillData.skillID = int.Parse(s.skillID);
            skillData.skillName = s.skillName;
            skillData.skillDesc = s.skillDesc;
            skillData.skillNeed = s.skillNeed;
            skillData.skillType = (RoleSkillType)Enum.Parse(typeof(RoleSkillType), s.skillType);
            skillData.skillLastType = (SkillLastingType)Enum.Parse(typeof(SkillLastingType), s.skillLastType);
            skillData.cost = int.Parse(s.cost);
            string[] selfBuff = s.selfBuffList.Split(',');
            string[] targetBuff = s.targetBuffList.Split(',');
            string[] SZFS = s.supportSZFS.Split(',');
            string[] CashFlow = s.supportCashFlow.Split(',');
            string[] selfSkill = s.selfSkillUnlock.Split(',');
            string[] targetSkill = s.targetSkillUnlock.Split(',');
            skillData.selfBuffList = new List<int>();
            skillData.targetBuffList = new List<int>();
            skillData.supportSZFS = new List<SZFSType>();
            skillData.supportCashFlow = new List<CashFlowType>();
            skillData.selfSkillUnlock = new List<int>();
            skillData.targetSkillUnlock = new List<int>();
            foreach (string str in selfBuff)
            {
                skillData.selfBuffList.Add(int.Parse(str));
            }
            foreach (string str in targetBuff)
            {
                skillData.targetBuffList.Add(int.Parse(str));
            }
            foreach (string str in SZFS)
            {
                skillData.supportSZFS.Add((SZFSType)Enum.Parse(typeof(SZFSType), str));
            }
            foreach (string str in CashFlow)
            {
                skillData.supportCashFlow.Add((CashFlowType)Enum.Parse(typeof(CashFlowType), str));
            }
            foreach (string str in selfSkill)
            {
                skillData.selfSkillUnlock.Add(int.Parse(str));
            }
            foreach (string str in targetSkill)
            {
                skillData.targetSkillUnlock.Add(int.Parse(str));
            }
            skillData.supportFree = bool.Parse(s.supportFree);
            skillData.supportThird = bool.Parse(s.supportThird);
            skillData.skillContribution = int.Parse(s.skillContribution);
            skillData.baseDivide = float.Parse(s.baseDivide);
            skillDatas.Add(skillData);
        }
    }

    /// <summary>
    /// 将交易技能源数据转成可用数据
    /// </summary>
    /// <param name="rawData"></param>
    public void ParseTradeSkillData(TradeSkillsData rawData)
    {
        tradeSkillDatas = new List<TradeSkillData>();
        foreach (TradeSkillItem s in rawData.tradeSkillSigns)
        {
            TradeSkillData tradeSkillData = new TradeSkillData();
            tradeSkillData.ID = int.Parse(s.ID);
            tradeSkillData.startRole = (RoleType)Enum.Parse(typeof(RoleType), s.startRole);
            tradeSkillData.endRole = (RoleType)Enum.Parse(typeof(RoleType), s.endRole);
            tradeSkillData.conductRole = (RoleType)Enum.Parse(typeof(RoleType), s.conductRole);
            tradeSkillData.anotherRole = (RoleType)Enum.Parse(typeof(RoleType), s.anotherRole);
            tradeSkillData.skillId = int.Parse(s.skillId);
            tradeSkillData.isLock = bool.Parse(s.isLock);
            tradeSkillData.searchInPerA = float.Parse(s.searchIn.Split(',')[0]);
            tradeSkillData.searchInPerB = float.Parse(s.searchIn.Split(',')[1]);
            tradeSkillData.searchInAdd = float.Parse(s.searchIn.Split(',')[2]);
            tradeSkillData.bargainInPerA = float.Parse(s.bargainIn.Split(',')[0]);
            tradeSkillData.bargainInPerB = float.Parse(s.bargainIn.Split(',')[1]);
            tradeSkillData.bargainInAdd = float.Parse(s.bargainIn.Split(',')[2]);
            tradeSkillData.deliverInPerA = float.Parse(s.deliverIn.Split(',')[0]);
            tradeSkillData.deliverInPerB = float.Parse(s.deliverIn.Split(',')[1]);
            tradeSkillData.deliverInAdd = float.Parse(s.deliverIn.Split(',')[2]);
            tradeSkillData.riskInPerA = float.Parse(s.riskIn.Split(',')[0]);
            tradeSkillData.riskInPerB = float.Parse(s.riskIn.Split(',')[1]);
            tradeSkillData.searchOutPerA = float.Parse(s.searchOut.Split(',')[0]);
            tradeSkillData.searchOutPerB = float.Parse(s.searchOut.Split(',')[1]);
            tradeSkillData.bargainOutPerA = float.Parse(s.bargainOut.Split(',')[0]);
            tradeSkillData.bargainOutPerB = float.Parse(s.bargainOut.Split(',')[1]);
            tradeSkillData.deliverOutPerA = float.Parse(s.deliverOut.Split(',')[0]);
            tradeSkillData.deliverOutPerB = float.Parse(s.deliverOut.Split(',')[1]);
            tradeSkillData.riskOutPerA = float.Parse(s.riskOut.Split(',')[0]);
            tradeSkillData.riskOutPerB = float.Parse(s.riskOut.Split(',')[1]);
            PlayerData.My.tradeSkillLock.Add(tradeSkillData.ID, tradeSkillData.isLock);
            tradeSkillDatas.Add(tradeSkillData);
        }
        //print(tradeSkillDatas.Count);
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
            string[] waitTimeList = s.waveWaitTime.Split(',');
            temp.waveWaitTime = new List<int>();
            foreach (string str in waitTimeList)
            {
                temp.waveWaitTime.Add(int.Parse(str));
            }
            string[] strList = s.startWorker.Split(',');
            temp.startWorker = new List<int>();
            temp.startEquip = new List<int>();
            foreach (string str in strList)
            {
                if (!str.Equals("-1"))
                    temp.startWorker.Add(int.Parse(str));
            }
            strList = s.startEquip.Split(',');
            foreach (string str in strList)
            {
                if (!str.Equals("-1"))
                    temp.startEquip.Add(int.Parse(str));
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
            temp.maxHealth = int.Parse(c.maxHealth);
            temp.moveSpeed = float.Parse(c.moveSpeed);
            temp.killMoney = int.Parse(c.killMoney);
            temp.killSatisfy = int.Parse(c.killSatisfy);
            temp.liveSatisfy = int.Parse(c.liveSatisfy);
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
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}