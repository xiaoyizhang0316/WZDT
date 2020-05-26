using System;
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
    public Role CurrentClickRole;
    public BaseMapRole currentMapRole;
    public GameObject Panel_AssemblyRole; 
    public Transform RoleTF;
    /// <summary>
    /// 需要遮挡的UI
    /// </summary>
    public List<GameObject> needReycastTargetPanel;
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
    /// <summary>
    /// 点击角色显示UI
    /// </summary>
    /// <param name="target"></param>
    public void UpdateUIPosition(Transform target)
    {
        CurrentClickRole = PlayerData.My.GetRoleById(Double.Parse(target.name));
        currentMapRole = PlayerData.My.GetMapRoleById(Double.Parse(target.name));
        Vector2 mouseDown = Camera.main.WorldToScreenPoint(target.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform,
            mouseDown, Camera.main, out mouseUGUIPos);
        if (isRect)
        {
       //     UIManager.My.Panel_MapRoleUI.gameObject.SetActive(true);

          //  MapRoleUI.GetComponent<RectTransform>().anchoredPosition = mouseUGUIPos;
        }
    }

    /// <summary>
    /// 检测当前界面是否可以穿透panel
    /// </summary>
    public bool NeedRayCastPanel()
    {

        for (int i = 0; i <needReycastTargetPanel.Count; i++)
        {
            if (needReycastTargetPanel[i].activeSelf)
            {
               
                return true;
            }
        }

        return false;
    }

}
