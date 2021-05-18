using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    /// <summary>
    /// 角色
    /// </summary>
    public BaseMapRole role;

    /// <summary>
    /// 是否打开
    /// </summary>
    public bool IsOpen;

    /// <summary>
    /// 是否开始
    /// </summary>
    public bool isPlay;

    
    public bool isAvaliable = true;

    /// <summary>
    /// 是否播放动画
    /// </summary>
    public bool isAnimPlaying = false;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;
    /// <summary>
    /// 附带buff列表
    /// </summary>
    public List<int> buffList;

    /// <summary>
    /// 当角色激活时有动画效果的物体
    /// </summary>
    public List<GameObject> animationPart = new List<GameObject>();

    // Start is called before the first frame update
    public virtual void Start()
    {
        role = GetComponent<BaseMapRole>();
        if (IsOpen)
        {
            UnleashSkills();
        }
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public abstract void Skill();

    /// <summary>
    /// 释放技能
    /// </summary>
    public virtual void UnleashSkills()
    {
        isPlay = true;
        
        float d = Mathf.Min( 4f,1f / (role.baseRoleData.efficiency * 0.05f));
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            d /= 2f;
        }
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
        if (!IsOpen)
        {
            return;
        }

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
        if (role.isNpc)
        {
            if (role.npcScript.isCanSeeEquip)
            {
                foreach (int i in role.npcScript.NPCBuffList)
                {
                    targetRole.RemoveBuffById(i);
                }
            }
        }
    }

    /// <summary>
    /// 重启释放技能
    /// </summary>
    public void ReUnleashSkills()
    {
        IsOpen = true;
        //Debug.Log("重启技能" + role.baseRoleData.ID);
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
        IsOpen = false;
    }
}
