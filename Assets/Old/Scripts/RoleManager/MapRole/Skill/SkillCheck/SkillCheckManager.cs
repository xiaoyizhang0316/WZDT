using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillCheckManager : MonoSingleton<SkillCheckManager>
{
    public int killNum;
    public int specialBulletKillNum;
    public int tasteKillNum;
    public int getMega;
    public int nonConsumerIncome;
    public List<RoleSkillCheckDetail> AllCheckDetail=new List<RoleSkillCheckDetail>();
    private List<RoleSkillCheckDetail> onCheckRoles=new List<RoleSkillCheckDetail>();

    public GameObject checkPrefab;
    public Transform checkContent;

    public void TradeOnAndCheck(BaseMapRole role, int select)
    {
        RoleSkillCheckDetail rscd = GetCheckDetail(role.baseRoleData.baseRoleData.roleType);
        if (rscd == null || select >= rscd.checkDetailsList.Count)
        {
            return;
        }
        onCheckRoles.Add(rscd);
        for(int i = 0; i < rscd.checkDetailsList[select].checkDetails.Count; i++)
        {
            GameObject go= Instantiate(checkPrefab, checkContent);
            AttachComponent(go, rscd.checkDetailsList[select].checkDetails[i].checkBase.name);
            go.GetComponent<SkillCheckBase>().ActiveCheck(role, rscd.turnRange, rscd.checkDetailsList[select].checkDetails[i]);
            //go.AddComponent<rscd.checkDetailsList[select].checkDetails[i].checkBase>().ActiveCheck(role, rscd.checkDetailsList[select].checkDetails[i]);
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

    RoleSkillCheckDetail GetCheckDetail(GameEnum.RoleType roleType)
    {
        for (int i = 0; i < AllCheckDetail.Count; i++)
        {
            if (AllCheckDetail[i].RoleType == roleType)
            {
                return AllCheckDetail[i];
            }
        }

        return null;
    }
}

public class RoleSkillCheckDetail
{
    public GameEnum.RoleType RoleType;
    public int turnRange;
    public int checkCount;
    public List<CheckDetails> checkDetailsList;
}

public class CheckDetails
{
    public int select;
    public List<CheckDetail> checkDetails;
}

public class CheckDetail
{
    public string checkContent;
    public string target;
    public bool isPersent;
    public SkillCheckBase checkBase;
}