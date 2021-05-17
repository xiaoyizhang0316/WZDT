using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class SkillCheckManager : MonoSingleton<SkillCheckManager>
{
    // 击杀数
    public int killNum;
    // 特殊弹种击杀数
    public int specialBulletKillNum;
    // 口味击杀数
    public int tasteKillNum;
    // 获取的Mega值
    public int getMega;
    // 得分
    public int playerScore;
    // 非消费者收入
    public int nonConsumerIncome;
    // 分成
    public int dividedProfit;
    // 角色检测相关的设置
    [SerializeField]
    public List<RoleSkillSelect> allCheckRoles=new List<RoleSkillSelect>();
    // 当前开启检测的角色
    private List<RoleSkillSelect> onCheckRoles=new List<RoleSkillSelect>();
    // 检测详情
    public GameObject checkPrefab;
    // 检测详情父物体
    public Transform checkContent;
    // 开启检测总开关
    private bool isCheckActive;
    // 是否分成
    public bool checkDivide;
    // 分成的比例
    public float proportion;//分成比例

    /// <summary>
    /// 激活角色功能，开启检测
    /// </summary>
    /// <param name="role">要激活的角色</param>
    /// <param name="select">选项</param>
    public void ActiveRoleCheck(BaseMapRole role, int select)
    {
        RoleSkillSelect rscd = GetCheckDetail(role.baseRoleData);
        if (rscd == null || select >= rscd.roleSkillSelect.Count)
        {
            return;
        }

        if (!isCheckActive)
        {
            isCheckActive = true;
        }

        rscd.checkedCount = 0;
        rscd.roleID = role.baseRoleData.ID;
        onCheckRoles.Add(rscd);
        for(int i = 0; i < rscd.roleSkillSelect[select].checkDetails.Count; i++)
        {
            GameObject go= Instantiate(checkPrefab, checkContent);
            AttachComponent(go, rscd.roleSkillSelect[select].checkDetails[i].checkBaseScript.ToString());
            go.GetComponent<SkillCheckBase>().ActiveCheck(role, rscd.checkTurn, rscd.roleSkillSelect[select].checkDetails[i], rscd.checkCount);
        }

        transform.DOScale(Vector3.one, 0.5f).Play();
    }

    /// <summary>
    /// 检测角色功能完成
    /// </summary>
    /// <param name="role">相关角色</param>
    //private static readonly object _lock = new object();
    public void NotifyEnd(BaseMapRole role)
    {
        for (int i = 0; i < onCheckRoles.Count; i++)
        {
            if (onCheckRoles[i].RoleType == role.baseRoleData.baseRoleData.roleType)
            {
                CheckRoleSkillEnd(onCheckRoles[i]);
            }
        }
    }

    /// <summary>
    /// 检测角色的每个检测是否成功
    /// </summary>
    /// <param name="rscd"></param>
    void CheckRoleSkillEnd(RoleSkillSelect rscd)
    {
        bool isSuccess = true;
        foreach (Transform child in checkContent)
        {
            if (child.GetComponent<SkillCheckBase>().roleID == rscd.roleID)
            {
                if (!child.GetComponent<SkillCheckBase>().isSuccess)
                {
                    isSuccess = false;
                    break;
                }
            }
        }

        if (isSuccess)
        {
            // TODO reach target
            Debug.Log("check success");
        }
        else
        {
            rscd.checkedCount += 1;
            if (rscd.checkedCount >= rscd.checkCount)
            {
                // TODO fail
                Debug.Log("check fail");
            }
            else
            {
                foreach (Transform child in checkContent)
                {
                    if (child.GetComponent<SkillCheckBase>().roleID == rscd.roleID)
                    {
                        Debug.Log("check reset");
                        child.GetComponent<SkillCheckBase>().ResetCheck();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 击杀数累加
    /// </summary>
    /// <param name="isSpecialBullet">是否特殊弹种击杀</param>
    /// <param name="isTaste">是否口味击杀</param>
    public void AddKillNum(bool isSpecialBullet, bool isTaste)
    {
        if (!isCheckActive)
        {
            return;
        }
        killNum++;
        if (isTaste)
        {
            tasteKillNum++;
        }

        if (isSpecialBullet)
        {
            specialBulletKillNum++;
        }
    }

    /// <summary>
    /// 非消费者收入累加
    /// </summary>
    /// <param name="income">收入</param>
    public void AddNonConsumerIncome(int income)
    {
        if (!isCheckActive)
        {
            return;
        }
        nonConsumerIncome += income;
    }

    /// <summary>
    /// 获得mega值累加
    /// </summary>
    /// <param name="addMega">获得的mega值数</param>
    public void AddMega(int addMega)
    {
        if (!isCheckActive)
        {
            return;
        }
        getMega += addMega;
    }

    /// <summary>
    /// 分数累加
    /// </summary>
    /// <param name="num">获得的分数</param>
    public void AddScore(int num)
    {
        if (!isCheckActive)
        {
            return;
        }
        playerScore += num;
    }

    /// <summary>
    /// 分成累加
    /// </summary>
    /// <param name="num">获得的分成</param>
    public void AddDividedProfit(int num)
    {
        if (!isCheckActive)
        {
            return;
        }

        dividedProfit += num;
    }

    /// <summary>
    /// 给gameobject添加脚本
    /// </summary>
    /// <param name="go">指定的game object</param>
    /// <param name="componentName">脚本名称</param>
    void AttachComponent(GameObject go, string componentName)
    {
        var com = Type.GetType(componentName);
        go.AddComponent(com);
    }

    /// <summary>
    /// 查找角色的选项和检测
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    RoleSkillSelect GetCheckDetail(Role role)
    {
        for (int i = 0; i < allCheckRoles.Count; i++)
        {
            if (allCheckRoles[i].roleID == role.ID && allCheckRoles[i].RoleType == role.baseRoleData.roleType)
            {
                return allCheckRoles[i];
            }
        }

        return null;
    }
}

[Serializable]
public class RoleSkillSelect
{
    public GameEnum.RoleType RoleType;
    [Tooltip("填上对应的角色的ID")]
    public double roleID;
    [Tooltip("要检测的回合数")]
    public int checkTurn;
    [Tooltip("最多检测次数")]
    public int checkCount;
    [Tooltip("不用填")]
    public int checkedCount;
    [Tooltip("选择选项对应的检测")]
    public List<CheckDetails> roleSkillSelect=new List<CheckDetails>();
}

[Serializable]
public class CheckDetails
{
    public List<CheckDetail> checkDetails=new List<CheckDetail>();
}

[Serializable]
public class CheckDetail
{
    [Tooltip("检测的内容")]
    public string checkContent;
    [Tooltip("检测的目标")]
    public string target;
    [Header("Optional")]
    [Tooltip("分成比例，依赖于分成相关的检测")]
    public float proportion;
    [Tooltip("是否用百分比展示当前的数据")]
    public bool isPercent;
    [Tooltip("用于通知Mananger是否完成检测，一个选项中有且只能√上一个")]
    public bool isMainTarget;// 用于通知技能结束
    [Tooltip("检测的类型")]
    public SkillCheckType checkBaseScript;
}

public enum SkillCheckType
{
    KillCheck,
    SpecialBulletKillCheck,
    TasteKillCheck,
    NonConsumerCheck,
    GetMegaCheck,
    GetScoreCheck,
    DividedProfitCheck
}