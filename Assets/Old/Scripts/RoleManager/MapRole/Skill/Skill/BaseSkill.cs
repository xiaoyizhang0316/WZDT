using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;


public abstract class BaseSkill : MonoBehaviour
{
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName;

    /// <summary>
    /// 是否打开
    /// </summary>
    public bool isOpen;


    /// <summary>
    /// 活动时长
    /// </summary>
    public int skillTime;

    /// <summary>
    /// 技能图片
    /// </summary>
    public Image skillImage;


    /// <summary>
    /// 对施放者提供的buff列表
    /// </summary>
    public List<BuffData> selfBuffList;

    /// <summary>
    /// 对目标提供的buff列表
    /// </summary>
    public List<BuffData> targetBuffList;

    /// <summary>
    /// 初始化buff
    /// </summary>
    public void InitBuff()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(SkillName);
        selfBuffList = new List<BuffData>();
        targetBuffList = new List<BuffData>();
        foreach (int i in data.selfBuffList)
        {
            if (i != -1)
            {
                selfBuffList.Add(GameDataMgr.My.GetBuffDataByID(i));
            }
        }
        foreach (int i in data.targetBuffList)
        {
            if (i != -1)
            {
                targetBuffList.Add(GameDataMgr.My.GetBuffDataByID(i));
            }
        }
    }

    /// <summary>
    /// 让buff生效
    /// </summary>
    public void CastBuff(BaseMapRole baseMapRole, TradeData tradeData)
    {
        for (int i = 0; i < selfBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            buff.Init(selfBuffList[i]);
            buff.SetRoleBuff(baseMapRole, PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), baseMapRole);
        }
        for (int i = 0; i < targetBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            buff.Init(targetBuffList[i]);
            buff.SetRoleBuff(baseMapRole, PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
        }
    }

    /// <summary>
    /// 技能花费成本
    /// </summary>
    public void SkillCost(BaseMapRole mapRole)
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(SkillName);
        int result = 0 - data.cost;
        mapRole.GetMoney(result);
        mapRole.activityCost += data.cost;
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="baseMapRole"></param>
    /// <param name="tradeData"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public abstract bool ReleaseSkills(BaseMapRole baseMapRole,TradeData tradeData, Action onComplete = null);

    public virtual bool DetectionCanRelease(BaseMapRole target)
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
