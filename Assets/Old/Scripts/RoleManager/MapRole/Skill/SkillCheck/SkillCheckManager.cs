using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class SkillCheckManager : MonoSingleton<SkillCheckManager>
{
    public int killNum;
    public int specialBulletKillNum;
    public int tasteKillNum;
    public int getMega;
    public int nonConsumerIncome;
    [SerializeField]
    public List<RoleSkillSelect> allCheckRoles=new List<RoleSkillSelect>();
    private List<RoleSkillSelect> onCheckRoles=new List<RoleSkillSelect>();

    public GameObject checkPrefab;
    public Transform checkContent;

    public void ActiveRoleCheck(BaseMapRole role, int select)
    {
        RoleSkillSelect rscd = GetCheckDetail(role.baseRoleData.baseRoleData.roleType);
        if (rscd == null || select >= rscd.checkDetailsList.Count)
        {
            return;
        }

        rscd.checkedCount = 0;
        onCheckRoles.Add(rscd);
        for(int i = 0; i < rscd.checkDetailsList[select].checkDetails.Count; i++)
        {
            GameObject go= Instantiate(checkPrefab, checkContent);
            AttachComponent(go, rscd.checkDetailsList[select].checkDetails[i].checkBaseScript.ToString());
            go.GetComponent<SkillCheckBase>().ActiveCheck(role, rscd.checkTurn, rscd.checkDetailsList[select].checkDetails[i]);
            //go.AddComponent<rscd.checkDetailsList[select].checkDetails[i].checkBase>().ActiveCheck(role, rscd.checkDetailsList[select].checkDetails[i]);
        }
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
            if (child.GetComponent<SkillCheckBase>().dependRole.baseRoleData.baseRoleData.roleType == rscd.RoleType)
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
        }
        else
        {
            rscd.checkedCount += 1;
            if (rscd.checkedCount >= rscd.checkCount)
            {
                // TODO fail
            }
            else
            {
                foreach (Transform child in checkContent)
                {
                    if (child.GetComponent<SkillCheckBase>().dependRole.baseRoleData.baseRoleData.roleType == rscd.RoleType)
                    {
                        child.GetComponent<SkillCheckBase>().ResetCheck();
                    }
                }
            }
        }
    }

    public void AddKillNum(bool isSpecialBullet, bool isTaste)
    {
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
        nonConsumerIncome += income;
    }

    void AttachComponent(GameObject go, string componentName)
    {
        var com = Type.GetType(componentName);
        go.AddComponent(com);
    }

    RoleSkillSelect GetCheckDetail(GameEnum.RoleType roleType)
    {
        for (int i = 0; i < allCheckRoles.Count; i++)
        {
            if (allCheckRoles[i].RoleType == roleType)
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
    public int checkTurn;
    public int checkCount;
    public int checkedCount;
    public List<CheckDetails> checkDetailsList=new List<CheckDetails>();
}

[Serializable]
public class CheckDetails
{
    public List<CheckDetail> checkDetails=new List<CheckDetail>();
}

[Serializable]
public class CheckDetail
{
    public string checkContent;
    public string target;
    public bool isPersent;
    public bool isMainTarget;// 用于通知技能结束
    public SkillCheckType checkBaseScript;
}

public enum SkillCheckType
{
    KillCheck
}