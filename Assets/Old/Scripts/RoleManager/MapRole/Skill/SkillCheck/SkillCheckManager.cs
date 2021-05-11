using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class SkillCheckManager : MonoSingleton<SkillCheckManager>
{
    public int killNum;
    public int specialBulletKillNum;
    public int tasteKillNum;
    public int getMega;
    public int playerScore;
    public int nonConsumerIncome;
    public int dividedProfit;
    [SerializeField]
    public List<RoleSkillSelect> allCheckRoles=new List<RoleSkillSelect>();
    private List<RoleSkillSelect> onCheckRoles=new List<RoleSkillSelect>();

    public GameObject checkPrefab;
    public Transform checkContent;

    private bool isCheckActive;
    public bool checkDivide;
    public float proportion;//分成比例

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
            //go.AddComponent<rscd.checkDetailsList[select].checkDetails[i].checkBase>().ActiveCheck(role, rscd.checkDetailsList[select].checkDetails[i]);
        }

        transform.DOScale(Vector3.one, 0.5f).Play();
    }

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
                    if (child.GetComponent<SkillCheckBase>().dependRole.baseRoleData.baseRoleData.roleType == rscd.RoleType)
                    {
                        Debug.Log("check reset");
                        child.GetComponent<SkillCheckBase>().ResetCheck();
                    }
                }
            }
        }
    }

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

    public void AddNonConsumerIncome(int income)
    {
        if (!isCheckActive)
        {
            return;
        }
        nonConsumerIncome += income;
    }

    public void AddMega(int addMega)
    {
        if (!isCheckActive)
        {
            return;
        }
        getMega += addMega;
    }

    public void AddScore(int num)
    {
        if (!isCheckActive)
        {
            return;
        }
        playerScore += num;
    }

    public void AddDividedProfit(int num)
    {
        if (!isCheckActive)
        {
            return;
        }

        dividedProfit += num;
    }

    void AttachComponent(GameObject go, string componentName)
    {
        var com = Type.GetType(componentName);
        go.AddComponent(com);
    }

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