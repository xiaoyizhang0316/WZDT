using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class AllRoleManager : MonoSingleton<AllRoleManager>
{
    public  List<BaseRoleManager> RoleManagers;

    /// <summary>
    /// 根据ID获取角色信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BaseRoleManager  GetRoleManager(int  id)
    {
        for (int i = 0; i < RoleManagers.Count; i++)
        {
            if (RoleManagers[i].roleId == id)
            {
                return RoleManagers[i];
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
