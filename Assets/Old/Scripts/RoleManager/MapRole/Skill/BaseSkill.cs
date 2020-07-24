using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public BaseMapRole role;

    public bool IsOpen;

    public bool isPlay;

    public bool isAvaliable = true;

    /// <summary>
    /// 需要多棱镜发现的buff
    /// </summary>
    public List<int> goodSpecialBuffList;
    /// <summary>
    /// 需要多棱镜发现的buff实体
    /// </summary>
    public List<BaseBuff> goodBaseBuffs = new List<BaseBuff>();

    /// <summary>
    /// 不需要多棱镜发现的buff
    /// </summary>
    public List<int> badSpecialBuffList;
    /// <summary>
    /// 不需要多棱镜发现的buff实体
    /// </summary>
    public List<BaseBuff> badBaseBuffs = new List<BaseBuff>();

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;
    /// <summary>
    /// 附带buff列表
    /// </summary>
    public List<int> buffList;

    public List<GameObject> animationPart = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        role = GetComponent<BaseMapRole>();
        if (IsOpen)
        {
            UnleashSkills();
        }
        for (int i = 0; i < goodSpecialBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(goodSpecialBuffList[i]);
            buff.Init(data);
            buff.targetRole = role;
            buff.castRole = role;
            buff.buffRole = role;
            goodBaseBuffs.Add(buff);
        }
        for (int i = 0; i < badSpecialBuffList.Count; i++)
        {
            BaseBuff buff = new BaseBuff();
            BuffData data = GameDataMgr.My.GetBuffDataByID(badSpecialBuffList[i]);
            buff.Init(data);
            buff.targetRole = role;
            buff.castRole = role;
            buff.buffRole = role;
            badBaseBuffs.Add(buff);
        }
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public abstract void Skill();

    public virtual void UnleashSkills()
    {
        isPlay = true;
        float d = 1f / (role.baseRoleData.efficiency * 0.05f);
        transform.DORotate(transform.eulerAngles, d).OnComplete(() =>
        {
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });
    }

    /// <summary>
    /// 添加增益Buff
    /// </summary>
    public void AddRoleBuff(TradeData tradeData)
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            var buff = GameDataMgr.My.GetBuffDataByID(buffList[i]);
            BaseBuff baseb = new BaseBuff();
            baseb.Init(buff);
            baseb.SetRoleBuff(PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
        }
        if (role.isNpc)
        {
            if (role.npcScript.isCanSeeEquip)
            {
                for (int i = 0; i < role.npcScript.NPCBuffList.Count; i++)
                {
                    var buff = GameDataMgr.My.GetBuffDataByID(role.npcScript.NPCBuffList[i]);
                    BaseBuff baseb = new BaseBuff();
                    baseb.Init(buff);
                    baseb.SetRoleBuff(PlayerData.My.GetMapRoleById(double.Parse(tradeData.castRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)), PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)));
                }
            }
        }
    }

    /// <summary>
    /// 移除增益buff
    /// </summary>
    /// <param name="tradeData"></param>
    public void DeteleRoleBuff(TradeData tradeData)
    {
        BaseMapRole targetRole = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        foreach (int i in buffList)
        {
            targetRole.RemoveBuffById(i);
        }
    }

    /// <summary>
    /// 重启释放技能
    /// </summary>
    public void ReUnleashSkills()
    {
        IsOpen = true;
        Debug.Log("重启技能" + role.baseRoleData.ID);
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            if (!isPlay)
                UnleashSkills();
        }
        else
            UnleashSkills();
    }

    /// <summary>
    ///  取消技能
    /// </summary>
    public void CancelSkill()
    {
        Debug.Log("取消技能" + role.baseRoleData.ID);
        IsOpen = false;
    }
}
