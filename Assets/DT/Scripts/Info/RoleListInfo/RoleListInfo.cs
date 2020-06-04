using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class RoleListInfo : MonoSingleton<RoleListInfo>
{
    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;
    public GameObject dealer;

    public Button close;
    public GameObject roleInfo;
    public Button changeRole;
    public Button remove;
    public Button startTrade;
    public Button updateRole;
    public Role currentRole;
    public GameObject CreatRole;
    // Start is called before the first frame update
    void Start()
    {
        SetDependency();
        roleInfo.SetActive(false);
        close.onClick.AddListener(() =>
        {
            roleInfo.gameObject.SetActive(false);
            close.gameObject.SetActive(false);

        });
        changeRole.onClick.AddListener(() =>
        {
            CreatRole.gameObject.SetActive(true);
            CreatRoleManager.My.Open(currentRole);
            roleInfo.gameObject.SetActive(false);
            close.gameObject.SetActive(false);

        });
        remove.onClick.AddListener(() =>
        {
            roleInfo.gameObject.SetActive(false);
            close.gameObject.SetActive(false);
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要删除" + currentRole.baseRoleData.roleName + "吗？";
            DeleteUIManager.My.Init(str,()=> { PlayerData.My.DeleteRole(currentRole.ID); });
        });
        startTrade.onClick.AddListener(() =>
        {
            NewCanvasUI.My.CreateTrade(PlayerData.My.GetMapRoleById(currentRole.ID));
            roleInfo.gameObject.SetActive(false);
            close.gameObject.SetActive(false);
        });
        updateRole.onClick.AddListener(() =>
        {
            NewCanvasUI.My.Panel_Update.SetActive(true);
            RoleUpdateInfo.My.Init(currentRole);
            roleInfo.gameObject.SetActive(false);
            close.gameObject.SetActive(false);
        });
        close.gameObject.SetActive(false);
    }

    public void SetDependency()
    {
        startTrade = transform.Find("RoleInfo/RoleSetting/CreateTrade").GetComponent<Button>();
        updateRole = transform.Find("RoleInfo/RoleSetting/UpdateRole").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Role role)
    {
        currentRole = role;
        roleInfo.SetActive(true);
        seed.SetActive(false);
        peasant.SetActive(false);
        merchant.SetActive(false);
        dealer.SetActive(false);
        close.gameObject.SetActive(true);
        if (role.baseRoleData.roleType == GameEnum.RoleType.Seed)
        {
            seed.SetActive(true);
            seed.GetComponent<BaseRoleListInfo>().Init(role);
        }
        if (role.baseRoleData.roleType == GameEnum.RoleType.Peasant)
        {
            peasant.SetActive(true);
            peasant.GetComponent<BaseRoleListInfo>().Init(role);

        }
        if (role.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            merchant.SetActive(true);
            merchant.GetComponent<BaseRoleListInfo>().Init(role);

        }
        if (role.baseRoleData.roleType == GameEnum.RoleType.Dealer)
        {
            dealer.SetActive(true);
            dealer.GetComponent<BaseRoleListInfo>().Init(role);
        }
    }
}
