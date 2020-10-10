using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;

public class RoleInfoManager : MonoSingleton<RoleInfoManager>
{
    /// <summary>
    /// 主要属性对应的GO
    /// </summary>
    public GameObject MainAttriObj;

    /// <summary>
    /// 次要属性对应的GO
    /// </summary>
    public GameObject SecondAttriObj;

    #region 三个指示器   
    public GameObject workerOnEquip;

    public GameObject atLeastOne;

    #endregion

    /// <summary>
    /// 更新所有属性和三个指示图标
    /// </summary>
    public void UpdateRoleInfo()
    {
      //  MainAttribute.My.UpdateMainAttriInfo();
      //  SecondAttribute.My.UpdateSecondAttriInfo();
      //  workerOnEquip.GetComponent<StatusIcon>().status = CreatRoleManager.My.isWorkerOnEquip;
      //  atLeastOne.GetComponent<StatusIcon>().status = CreatRoleManager.My.isAtLeastOneWorkerEquip;
    }
}
