using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleUpdateInfo : MonoBehaviour
{ 
    public GameObject seed;
    public GameObject peasant;
    public GameObject merchant;
    public GameObject dealer;
    public Role currentRole;

    public Text level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(Role role )
    {
    
        currentRole = role; 
        seed.SetActive(false);
        peasant.SetActive(false);
        merchant.SetActive(false);
        dealer.SetActive(false);
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
