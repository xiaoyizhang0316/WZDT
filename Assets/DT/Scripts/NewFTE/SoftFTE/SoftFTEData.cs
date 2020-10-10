using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class SoftFTEData : MonoSingleton<SoftFTEData>
{
    public List<SoftFTEItem> softFTEList;

    public SoftFTEItem GetSoftItemByType(RoleType type)
    {
        for (int i = 0; i < softFTEList.Count; i++)
        {
            if (softFTEList[i].roleType == type)
            {
                return softFTEList[i];
            }
        }
        throw new Exception("找不到该角色的信息");
    }
}

[Serializable]
public class SoftFTEItem
{
    public RoleType roleType;

    public string roleDesc;

    public string roleVideoDesc;

}