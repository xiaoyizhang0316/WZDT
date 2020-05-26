using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static StaticRoleName;

public class ChoseRoleManager : MonoBehaviour
{
    public Button dealer ;
    public Button merchant;
    public Button peasant;
    public Button seed;

    public GameObject CreatRolePanel;
     
    // Start is called before the first frame update
    void Start()
    {
        CreatRolePanel.SetActive(false);
        dealer.onClick.AddListener(() =>
        {
            Role role = new Role();
            role.baseRoleData = GameDataMgr.My.GetModelData(GameEnum.RoleType.Dealer, 1);
            
            role.baseRoleData.roleName = StaticRoleName.DealerName[Random.Range(0, StaticRoleName.DealerName.Length)];
            role.ID = CommonData.My.GetTimestamp(DateTime.Now);
             
            CreatRolePanel.SetActive(true);

            CreatRoleManager.My.Open(role);
        });
        merchant.onClick.AddListener(() =>
        {
            Role role = new Role();
            role.ID = CommonData.My.GetTimestamp(DateTime.Now);
            role.baseRoleData = GameDataMgr.My.GetModelData(GameEnum.RoleType.Merchant, 1);
            
            role.baseRoleData.roleName = StaticRoleName.MerchantName[Random.Range(0, StaticRoleName.MerchantName.Length)];
             
            CreatRolePanel.SetActive(true);
            CreatRoleManager.My.Open(role);

        });
        peasant.onClick.AddListener(() =>
        {
            Role role = new Role();
            role.ID = CommonData.My.GetTimestamp(DateTime.Now);
            role.baseRoleData = GameDataMgr.My.GetModelData(GameEnum.RoleType.Peasant, 1);
            role.baseRoleData.roleName = StaticRoleName.PeasantName[Random.Range(0, StaticRoleName.PeasantName.Length)];
             
            CreatRolePanel.SetActive(true);
            CreatRoleManager.My.Open(role);
        });
        seed.onClick.AddListener(() =>
        {
            Role role = new Role();
            print(StaticRoleName.SeedName[0]);
            //
           
            role.ID = CommonData.My.GetTimestamp(DateTime.Now) ;
///            Debug.Log( CommonData.My.GetTimestamp(DateTime.Now) +"IDS");
            role.baseRoleData = GameDataMgr.My.GetModelData(GameEnum.RoleType.Seed, 1);
            role.baseRoleData.roleName = StaticRoleName.SeedName[Random.Range(0, StaticRoleName.SeedName.Length)];
    
            CreatRolePanel.SetActive(true);
            CreatRoleManager.My.Open(role); 

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
