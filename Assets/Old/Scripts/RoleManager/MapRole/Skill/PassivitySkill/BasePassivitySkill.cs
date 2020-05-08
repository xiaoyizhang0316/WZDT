using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePassivitySkill : MonoBehaviour
{
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName;

    public int skillTime;
    /// <summary>
    /// 是否打开
    /// </summary>
    public bool isOpen;



    public bool isLock;

    public Button skillButton;
    /// <summary>
    /// 技能图片
    /// </summary>
    public Image skillImage;

    /// <summary>
    /// 对施放者提供的buff列表
    /// </summary>
    public List<BuffData> selfBuffList;

    /// <summary>
    /// 初始化buff
    /// </summary>
    public void InitBuff()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(SkillName);
        selfBuffList = new List<BuffData>();
        foreach (int i in data.selfBuffList)
        {
            if (i != -1)
            {
                selfBuffList.Add(GameDataMgr.My.GetBuffDataByID(i));
            }
        }
    }
    /// <summary>
    /// 主动技能施法者
    /// </summary>
    public BaseMapRole targetMapRole;

    /// <summary>
    /// 让buff生效
    /// </summary>
    public void CastBuff(BaseMapRole baseMapRole)
    {
        for (int i = 0; i < selfBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            buff.Init(selfBuffList[i]);
            buff.SetRoleBuff(baseMapRole, baseMapRole, baseMapRole);
        }
    }

    /// <summary>
    /// 技能花费成本
    /// </summary>
    public void SkillCost(BaseMapRole mapRole)
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(SkillName);
        if (data != null)
        {
            int result = 0 - data.cost;
            mapRole.GetMoney(result);
            mapRole.activityCost += data.cost;
        }
    }

    /// <summary>
    /// 被动技能增长角色贡献
    /// </summary>
    public virtual void SkillContribution()
    {
        SkillData data = GameDataMgr.My.GetSkillDataByName(SkillName);
        BaseMapRole role = GetComponent<BaseMapRole>();
        if (role.contributionNum > 0 || role.monthlySatisfy != 0)
        {
            role.GetContribution(data.skillContribution);
        }
    }

    public abstract void SwitchButton(BaseMapRole baseMapRole,Button skillButton);
    public float startTime;
    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="baseMapRole"></param>
    /// <param name="tradeData"></param>
    /// <param name="onComplete"></param>
    /// <returns></returns>
    public abstract void  ReleaseSkills(BaseMapRole baseMapRole , Action onComplete = null);

    public abstract void  ShowImage(BaseMapRole baseMapRole);

    public virtual void OnLock()
    {
        //Debug.Log("当锁住的时候");
    }

    public virtual void OnUnLock()
    {
        //Debug.Log("当解锁的时候");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool islocked;
    // Update is called once per frame
    void Update()
    {
        if (isLock&&!islocked)
        {
            islocked = true;
            OnLock();
        }

        if (!isLock && islocked)
        {
            islocked = false;
            OnUnLock();
        }
    }
}
