using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class RoleUpdateInfo : MonoSingleton<RoleUpdateInfo>
{ 
    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;
    public GameObject dealer;
    public Role currentRole;

    public Text name;
    public int nextLevel;
    public int currentLevel;
    public Button close;
    public Text level;

    public Button hammer;
    public Button update;
    public string roleName;
    // Start is called before the first frame update
    void Start()
    {
        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(Role role )
    {
     

        name.text = role.baseRoleData.roleName;
        roleName = role.baseRoleData.roleName;
        currentRole = role; 
        seed.SetActive(false);
        peasant.SetActive(false);
        merchant.SetActive(false);
        dealer.SetActive(false);
        nextLevel = role.baseRoleData.level+1;
        currentLevel = role.baseRoleData.level;
        ReInit(role);
        if (currentLevel >= 5)
        {
            update.interactable = false;
            hammer.interactable = false;
        }
        else
        {
            update.interactable = true;
            hammer.interactable = true;

        }
    }

    public void ReInit(Role role )
    {
        level.text = role.baseRoleData.level.ToString();
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
