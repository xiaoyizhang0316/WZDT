using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_3SceneManager: MonoBehaviour
{
    public Button seed;

    public Button dealer;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckRole",0,1);
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
        dealer.interactable = true;
        seed.interactable = true;
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Dealer &&!PlayerData.My.RoleData[i].isNpc)
            {
           
                dealer.interactable = false;
            }
            if (PlayerData.My.RoleData[i].baseRoleData.roleType == GameEnum.RoleType.Seed&&!PlayerData.My.RoleData[i].isNpc)
            {
                seed.interactable = false;
            }
        }
        
    }
}
