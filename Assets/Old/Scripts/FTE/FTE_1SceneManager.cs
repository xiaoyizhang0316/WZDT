using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1SceneManager: MonoBehaviour
{
    public Button seed;

    public Button peasant;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckRole",1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 检测当前创建角色数量是否超过两个
    /// </summary>
    public void CheckRole()
    {
        seed.interactable = true;
        peasant.interactable = true;
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed )
            {
               
                seed.interactable = false;
            }
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Peasant)
            {
                peasant.interactable = false;
            }
        }
        
    }
}
