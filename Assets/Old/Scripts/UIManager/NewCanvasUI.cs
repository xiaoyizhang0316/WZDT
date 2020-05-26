using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class NewCanvasUI : MonoSingleton<NewCanvasUI>
{
    /// <summary>
    /// 角色列表物体创建预制体
    /// </summary>
    public GameObject roleListSignOBJ;

    /// <summary>
    /// 角色列表创建位置
    /// </summary>
    public Transform roleListCreatPos;
    public GameObject Panel_ChoseRole;

    public GameObject Panel_AssemblyRole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 创建玩家角色列表  刷新玩家角色列表
    /// </summary>
    public void UpdateRoleList()
    {

        for (int i = 0; i < PlayerData.My.RoleManager.Count; i++)
        {
            Destroy(PlayerData.My.RoleManager[i]);
        }
        PlayerData.My.RoleManager.Clear();
        for (int i = 0; i < PlayerData.My.RoleData.Count; i++)
        {
            if (PlayerData.My.RoleData[i].isNpc)
            {
                continue;
            }
            GameObject roleListSign = Instantiate(roleListSignOBJ, roleListCreatPos);
            roleListSign.GetComponent<CreatRole_Button>().RolePrb =
                Resources.Load<GameObject>(PlayerData.My.RoleData[i].baseRoleData.PrePath);
            roleListSign.GetComponent<Image>().sprite =
                Resources.Load<Sprite>(PlayerData.My.RoleData[i].baseRoleData.SpritePath);
            roleListSign.name = PlayerData.My.RoleData[i].baseRoleData.roleType.ToString() + "_" + PlayerData.My.RoleData[i].ID;
            PlayerData.My.RoleManager.Add(roleListSign);
            if (PlayerData.My.RoleData[i].inMap)
            {
                roleListSign.GetComponent<Button>().interactable = false;
                roleListSign.GetComponent<Image>().raycastTarget = false;
            }
        }
    }
}
