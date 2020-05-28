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
    private CreatRole_Button button;
    public Role currentRole;
    public GameObject CreatRole;
    // Start is called before the first frame update
    void Start()
    {
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
           Destroy(button.gameObject); 
           roleInfo.gameObject.SetActive(false);
           close.gameObject.SetActive(false);
           int index = PlayerData.My.RoleData.IndexOf(currentRole);
           PlayerData.My.RoleData.RemoveAt(index);
        });
        close.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Role role,CreatRole_Button button)
    {
        this.button = button;
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
            merchant.GetComponent<BaseRoleListInfo>().Init(role);

        }
    }
}
