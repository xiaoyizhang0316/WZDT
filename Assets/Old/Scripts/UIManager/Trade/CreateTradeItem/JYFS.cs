using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;
using System;

public class JYFS : MonoSingleton<JYFS>
{

    public Transform skillListTF;

    public GameObject skillItemPrb;

    public TradeRoleAttributeChange castRoleChange;

    public TradeRoleAttributeChange targetRoleChange;

    public Text skillCost;

    public Text skillDesc;

    public Text skillNeed;

    /// <summary>
    /// 当交易方式（技能）改变时
    /// </summary>
    public void OnValueChange(string skill)
    {
        CreateTradeManager.My.selectJYFS = skill;
        SetSelectStatus();
        SetSkillTarget();
        CheckSkillCondition();
        InitOtherSetting();
        SetSkillInfo();
        CreateTradeManager.My.CalculateTCOfTwo(CreateTradeManager.My.currentTrade.tradeData.startRole, CreateTradeManager.My.currentTrade.tradeData.endRole);
        if (CreateTradeManager.My.currentTrade.isFirstSelect)
        {
            PredictRoleAttribute();
            SetCastAndTargetRoleAttribute();
            SetCastAndTargetRoleInfo();
        }
    }

    /// <summary>
    /// 检测选择的交易是否满足条件
    /// </summary>
    public void CheckSkillCondition()
    {
        BaseMapRole castRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.castRole));
        BaseMapRole targetRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.targetRole));
        CreateTradeManager.My.isSkillCanRelease = castRole.GetSkillByName(CreateTradeManager.My.selectJYFS).DetectionCanRelease(targetRole);
    }

    /// <summary>
    /// 当交易方式改变时，初始化其他选项
    /// </summary>
    public void InitOtherSetting()
    {
        transform.parent.GetComponentInChildren<CashFlow>().Init();
        transform.parent.GetComponentInChildren<PayRelationship>().Init();
        transform.parent.GetComponentInChildren<SZFS>().Init();
        //transform.parent.GetComponentInChildren<TradeDestination>().Init();
        transform.parent.GetComponentInChildren<SelectProduct>().Init();
        transform.parent.GetComponentInChildren<ThirdParty>().Init();
    }

    public void ClearList()
    {
        int number = skillListTF.childCount;
        for (int i = 0; i < number; i++)
        {
            DestroyImmediate(skillListTF.GetChild(0).gameObject);
        }
    }

    public void SetSelectStatus()
    {
        for (int i = 0; i < skillListTF.childCount; i++)
        {
            if (skillListTF.GetChild(i).GetComponent<JYFSSkillItem>().skillName.Equals(CreateTradeManager.My.selectJYFS))
            {
                skillListTF.GetChild(i).GetComponent<JYFSSkillItem>().statusImg.sprite = skillListTF.GetChild(i).GetComponent<JYFSSkillItem>().isSelect;
            }
            else
            {
                skillListTF.GetChild(i).GetComponent<JYFSSkillItem>().statusImg.sprite = skillListTF.GetChild(i).GetComponent<JYFSSkillItem>().normal;
            }
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //Dropdown temp = GetComponent<Dropdown>();
        //temp.ClearOptions();
        ClearList();
        if (CreateTradeManager.My.currentTrade.isFirstSelect)
        {
            RoleType startRoleType = PlayerData.My.GetRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.startRole)).baseRoleData.roleType;
            RoleType endRoleType = PlayerData.My.GetRoleById(double.Parse(CreateTradeManager.My.currentTrade.tradeData.endRole)).baseRoleData.roleType;
            print(startRoleType);
            print(endRoleType);
            for (int i = 0; i < GameDataMgr.My.tradeSkillDatas.Count; i++)
            {
                if (GameDataMgr.My.tradeSkillDatas[i].startRole == startRoleType && GameDataMgr.My.tradeSkillDatas[i].endRole == endRoleType)
                {
                    if (!PlayerData.My.tradeSkillLock[GameDataMgr.My.tradeSkillDatas[i].ID])
                    {
                        CreateTradeManager.My.availableTradeSkill.Add(GameDataMgr.My.tradeSkillDatas[i]);
                        SkillData data = GameDataMgr.My.GetSkillDataByID(GameDataMgr.My.tradeSkillDatas[i].skillId);

                        GameObject go = Instantiate(skillItemPrb, skillListTF);
                        go.GetComponent<JYFSSkillItem>().Init(data.skillName);
                        //temp.options.Add(new Dropdown.OptionData(data.skillName, null));
                    }
                }
            }
            if (CreateTradeManager.My.availableTradeSkill.Count > 0)
            {
                string name = GameDataMgr.My.GetSkillDataByID(CreateTradeManager.My.availableTradeSkill[0].skillId).skillName;
                OnValueChange(name);
                SetSkillTarget();
            }
        }
        else
        {
            CreateTradeManager.My.selectJYFS = CreateTradeManager.My.currentTrade.tradeData.selectJYFS;
            GameObject go = Instantiate(skillItemPrb, skillListTF);
            go.GetComponent<JYFSSkillItem>().Init(CreateTradeManager.My.selectJYFS);
        }
        if (CreateTradeManager.My.availableTradeSkill.Count > 0)
        {
            SetSkillInfo();
            CheckSkillCondition();
            InitOtherSetting();
            SetCastAndTargetRoleAttribute();
            SetCastAndTargetRoleInfo();
            CreateTradeManager.My.CalculateTCOfTwo(CreateTradeManager.My.currentTrade.tradeData.startRole, CreateTradeManager.My.currentTrade.tradeData.endRole);
        }  
    }

    /// <summary>
    /// 设置技能目标
    /// </summary>
    public void SetSkillTarget()
    {
        int number = GetCurrentIndex();
        print(number);
        if (CreateTradeManager.My.availableTradeSkill[number].conductRole == CreateTradeManager.My.availableTradeSkill[number].startRole)
        {
            CreateTradeManager.My.castRole = CreateTradeManager.My.currentTrade.tradeData.startRole;
            CreateTradeManager.My.targetRole = CreateTradeManager.My.currentTrade.tradeData.endRole;
        }
        else
        {
            CreateTradeManager.My.castRole = CreateTradeManager.My.currentTrade.tradeData.endRole;
            CreateTradeManager.My.targetRole = CreateTradeManager.My.currentTrade.tradeData.startRole;
            SetCastAndTargetRoleAttribute();
        }
        CreateTradeManager.My.payRole = CreateTradeManager.My.targetRole;
        CreateTradeManager.My.receiveRole = CreateTradeManager.My.castRole;
    }

    public int GetCurrentIndex()
    {
        for (int i = 0; i < skillListTF.childCount; i++)
        {
            if (skillListTF.GetChild(i).GetComponent<JYFSSkillItem>().skillName.Equals(CreateTradeManager.My.selectJYFS))
                return i;
        }
        return 0;
    }

    public void SetCastAndTargetRoleInfo()
    {
        if (CreateTradeManager.My.castRole == CreateTradeManager.My.currentTrade.tradeData.startRole)
        {
            CreateTradeManager.My.startRoleInfo.GetComponent<RoleDataUI>().Init(CreateTradeManager.My.castRoleAttribute, castRoleChange);
            CreateTradeManager.My.endRoleInfo.GetComponent<RoleDataUI>().Init(CreateTradeManager.My.targetRoleAttribute, targetRoleChange);
        }
        else
        {
            CreateTradeManager.My.startRoleInfo.GetComponent<RoleDataUI>().Init(CreateTradeManager.My.targetRoleAttribute, targetRoleChange);
            CreateTradeManager.My.endRoleInfo.GetComponent<RoleDataUI>().Init(CreateTradeManager.My.castRoleAttribute, castRoleChange);
        }
    }

    public void SetCastAndTargetRoleAttribute()
    {
        BaseMapRole cast = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.castRole));
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.targetRole));
        CreateTradeManager.My.castRoleAttribute.brand = cast.baseRoleData.brand;
        CreateTradeManager.My.castRoleAttribute.quality = cast.baseRoleData.quality;
        CreateTradeManager.My.castRoleAttribute.capacity = cast.baseRoleData.capacity;
        CreateTradeManager.My.castRoleAttribute.effeciency = cast.baseRoleData.efficiency;
        CreateTradeManager.My.castRoleAttribute.search = cast.baseRoleData.search;
        CreateTradeManager.My.castRoleAttribute.bargain = cast.baseRoleData.bargain;
        CreateTradeManager.My.castRoleAttribute.delivery = cast.baseRoleData.delivery;
        CreateTradeManager.My.castRoleAttribute.risk = cast.baseRoleData.risk;
        CreateTradeManager.My.targetRoleAttribute.brand = target.baseRoleData.brand;
        CreateTradeManager.My.targetRoleAttribute.quality = target.baseRoleData.quality;
        CreateTradeManager.My.targetRoleAttribute.capacity = target.baseRoleData.capacity;
        CreateTradeManager.My.targetRoleAttribute.effeciency = target.baseRoleData.efficiency;
        CreateTradeManager.My.targetRoleAttribute.search = target.baseRoleData.search;
        CreateTradeManager.My.targetRoleAttribute.bargain = target.baseRoleData.bargain;
        CreateTradeManager.My.targetRoleAttribute.delivery = target.baseRoleData.delivery;
        CreateTradeManager.My.targetRoleAttribute.risk = target.baseRoleData.risk;
    }

    public void PredictRoleAttribute()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
        List<int> selfBuff = data.selfBuffList;
        List<int> selfUnlock = data.selfSkillUnlock;
        List<int> targetBuff = data.targetBuffList;
        List<int> targetUnlock = data.targetSkillUnlock;
        List<int> self = new List<int>();
        List<int> target = new List<int>();
        castRoleChange = new TradeRoleAttributeChange();
        targetRoleChange = new TradeRoleAttributeChange();
        BaseMapRole castRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.castRole));
        BaseMapRole targetRole = PlayerData.My.GetMapRoleById(double.Parse(CreateTradeManager.My.targetRole));
        foreach (int i in selfBuff)
        {
            if (i != -1)
                self.Add(i);
        }
        foreach (int i in selfUnlock)
        {
            if (i != -1)
            {
                SkillData skill = GameDataMgr.My.GetSkillDataByID(i);
                foreach (int j in skill.selfBuffList)
                {
                    if (j != i-1)
                    {
                        self.Add(j);
                    }
                }

            }
        }
        foreach (int i in targetBuff)
        {
            if (i != -1)
                target.Add(i);
        }
        foreach (int i in targetUnlock)
        {
            if (i != -1)
            {
                SkillData skill = GameDataMgr.My.GetSkillDataByID(i);
                foreach (int j in skill.selfBuffList)
                {
                    if (j != i - 1)
                    {
                        target.Add(j);
                    }
                }
            }
        }
        foreach (int i in self)
        {
            BuffData buff = GameDataMgr.My.GetBuffDataByID(i);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(buff);
            baseBuff.castRole = castRole;
            baseBuff.targetRole = castRole;
            TradeRoleAttributeChange tempChange = baseBuff.GetRoleAttributeChange();
            CalculateRoleChange(ref castRoleChange, tempChange);
        }
        foreach (int i in target)
        {
            BuffData buff = GameDataMgr.My.GetBuffDataByID(i);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(buff);
            baseBuff.castRole = targetRole;
            baseBuff.targetRole = targetRole;
            TradeRoleAttributeChange tempChange = baseBuff.GetRoleAttributeChange();
            CalculateRoleChange(ref targetRoleChange, tempChange);
        }
    }

    public void CalculateRoleChange(ref TradeRoleAttributeChange baseRolechange, TradeRoleAttributeChange roleChange)
    {
        baseRolechange.brandAdd += roleChange.brandAdd;
        baseRolechange.qualityAdd += roleChange.qualityAdd;
        baseRolechange.capacityAdd += roleChange.capacityAdd;
        baseRolechange.effeciencyAdd += roleChange.effeciencyAdd;
        baseRolechange.searchAdd += roleChange.searchAdd;
        baseRolechange.bargainAdd += roleChange.bargainAdd;
        baseRolechange.deliveryAdd += roleChange.deliveryAdd;
        baseRolechange.riskAdd += roleChange.riskAdd;
    }

    public void SetSkillInfo()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(CreateTradeManager.My.selectJYFS);
        if (data != null)
        {
            skillCost.text = data.cost.ToString();
            skillDesc.text = data.skillDesc;
            skillNeed.text = data.skillNeed;
        }
    }

    [Serializable]
    public class TradeRoleAttributeChange
    {
        public int brandAdd;

        public int qualityAdd;

        public int capacityAdd;

        public int effeciencyAdd;

        public int searchAdd;

        public int bargainAdd;

        public int deliveryAdd;

        public int riskAdd;
    }
}
