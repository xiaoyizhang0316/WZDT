using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;

public class MainAttribute : MonoSingleton<MainAttribute>
{
    /// <summary>
    /// 所有角色的主要属性对应的脚本
    /// </summary>
    RoleInfoItem[] tempItems;

    /// <summary>
    /// 为所有脚本更新角色属性信息
    /// </summary>
    public void UpdateMainAttriInfo()
    {
        tempItems[0].SetSlide(CreatRoleManager.My.finalEffect);
        tempItems[1].SetSlide(CreatRoleManager.My.finalEfficiency);
        tempItems[2].SetSlide(CreatRoleManager.My.finalRange);
        //tempItems[0].SetSlide(CreatRoleManager.My.gearBrand, CreatRoleManager.My.CurrentRole.baseRoleData.needBrand);
        //tempItems[1].SetSlide(CreatRoleManager.My.workerBrand);
        //tempItems[2].SetSlide(CreatRoleManager.My.finalBrand);
        //tempItems[3].SetPosition(CreatRoleManager.My.CurrentRole.baseRoleData.needBrand);
        //tempItems[4].SetSlide(CreatRoleManager.My.gearQuality, CreatRoleManager.My.CurrentRole.baseRoleData.needQuality);
        //tempItems[5].SetSlide(CreatRoleManager.My.workerQuality);
        //tempItems[6].SetSlide(CreatRoleManager.My.finalQuality);
        //tempItems[7].SetPosition(CreatRoleManager.My.CurrentRole.baseRoleData.needQuality);
        //tempItems[8].SetSlide(CreatRoleManager.My.gearCapacity, CreatRoleManager.My.CurrentRole.baseRoleData.needCapacity);
        //tempItems[9].SetSlide(CreatRoleManager.My.workerCapacity);
        //tempItems[10].SetSlide(CreatRoleManager.My.finalCapacity);
        //tempItems[11].SetPosition(CreatRoleManager.My.CurrentRole.baseRoleData.needCapacity);
        //tempItems[12].SetSlide(CreatRoleManager.My.gearEfficiency, CreatRoleManager.My.CurrentRole.baseRoleData.needEfficiency);
        //tempItems[13].SetSlide(CreatRoleManager.My.workerEfficiency);
        //tempItems[14].SetSlide(CreatRoleManager.My.finalEfficiency);
        //tempItems[15].SetPosition(CreatRoleManager.My.CurrentRole.baseRoleData.needEfficiency);
        //TODO
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
