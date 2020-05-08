using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;

public class SecondAttribute : MonoSingleton<SecondAttribute>
{
    /// <summary>
    /// 所有角色的次要属性对应的脚本
    /// </summary>
    RoleInfoItem[] tempItems;

    /// <summary>
    /// 为所有脚本更新角色属性信息
    /// </summary>
    public void UpdateSecondAttriInfo()
    {
        tempItems[0].SetText(CreatRoleManager.My.CurrentRole.risk);
        tempItems[1].SetText(CreatRoleManager.My.CurrentRole.search);
        tempItems[2].SetText(CreatRoleManager.My.CurrentRole.bargain);
        tempItems[3].SetText(CreatRoleManager.My.CurrentRole.delivery);
       // tempItems[4].SetText(CreatRoleManager.My.CurrentRole.fixedCost);
        tempItems[4].SetText(CreatRoleManager.My.CurrentRole.costMonth);
        tempItems[5].SetText(CreatRoleManager.My.CurrentRole.workerCost);
        tempItems[6].SetText(CreatRoleManager.My.CurrentRole.equipCost);
    }

    // Start is called before the first frame update
    void Start()
    {
        tempItems = GetComponentsInChildren<RoleInfoItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
