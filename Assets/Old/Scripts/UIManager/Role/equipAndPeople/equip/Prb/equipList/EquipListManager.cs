using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;

public class EquipListManager : MonoSingleton<EquipListManager>
{
    public  List<EquipSign> _signs= new List<EquipSign>();



    /// <summary>
    /// 初始化，从角色物品栏中赋值
    /// </summary>
    public void Init()
    {
        foreach (PlayerGear p in PlayerData.My.playerGears)
        {
            //print(PlayerData.My.playerGears.Count);
            //print(p.GearId);
            //CreatRoleManager.My.equipPrb
            GameObject go = Instantiate(CreatRoleManager.My.equipPrb, transform.Find("Viewport/Content").position, transform.Find("Viewport/Content").rotation, transform.Find("Viewport/Content"));
            //go.transform.SetParent(transform.Find("Viewport/Content"));
            go.transform.GetComponent<EquipSign>().Init(p.GearId, p.isEquiped);
        }
        foreach (EquipSign e in _signs)
        {
            if (e.isOccupation)
                e.CheckOccupyStatus();
        }
    }

    /// <summary>
    /// 将所有装备的占用情况更新到角色物品栏中
    /// </summary>
    public void QuitAndSave()
    {
        foreach (EquipSign e in _signs)
        {
            PlayerData.My.SetGearStatus(e.ID, e.isOccupation);
        }
    }

    /// <summary>
    /// 将角色身上的装备脱下
    /// </summary>
    /// <param name="id"></param>
    public void UninstallEquip(int id)
    {
        foreach (EquipSign e in _signs)
        {
            if (e.ID == id)
                e.SetOccupyStatus(false);
        }
    }

    public Transform equipPos;
    /// <summary>
    /// 
    /// </summary>
    public Transform equipPrb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
