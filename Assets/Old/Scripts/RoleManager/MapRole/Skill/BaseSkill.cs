using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{

    public BaseMapRole role;

    public bool IsOpen;

    public bool isPlay;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;
    /// <summary>
    /// 附带buff列表
    /// </summary>
    public List<int> buffList;
    // Start is called before the first frame update
    void Start()
    {
        role = GetComponent<BaseMapRole>();
        if (IsOpen)
        {
            UnleashSkills();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public abstract void Skill();

    public virtual void UnleashSkills()
    {
        isPlay = true;
        float d = 1f / (role.baseRoleData.efficiency * 0.05f);
        transform.DORotate(transform.eulerAngles, d ).OnComplete(() =>
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
        UnleashSkills();
    }

    /// <summary>
    ///  取消技能
    /// </summary>
    public void CancelSkill()
    {
        IsOpen = false;
        isPlay = false;
    }
}
