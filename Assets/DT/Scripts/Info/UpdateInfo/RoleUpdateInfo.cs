﻿using System.Collections;
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
    public Button delete;
    public Text level;

    public Button hammer;
    public Button update;
    public string roleName;

    public Sprite AOE;
    public Sprite normallpp;
    public Sprite lightning;
    public Sprite tow;
    public Sprite seedSpeed;
    public Sprite melonSpeed;


    public List<Image> roleBuff;

    public Transform buffTF;

    public GameObject buffPrb;

    public Sprite buffNull;

    public GameObject buffcontent;
    public Text buffcontentText;
    
    // Start is called before the first frame update
    void Start()
    {
        SetDependency();
        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        delete.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            NewCanvasUI.My.Panel_Delete.SetActive(true);
            string str = "确定要删除" + currentRole.baseRoleData.roleName + "吗？";
            DeleteUIManager.My.Init(str, () => { PlayerData.My.DeleteRole(currentRole.ID); });
        });
        buffcontent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDependency()
    {
        delete = transform.Find("Delete").GetComponent<Button>();
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

        InitBuff();
    }

    public void InitBuff()
    {
        BaseMapRole baseMapRole =    PlayerData.My.GetBaseMapRoleByName(currentRole.baseRoleData.roleName);
        
        for (int i = 0; i <roleBuff.Count; i++)
        {
            roleBuff[i].sprite = buffNull;
            roleBuff[i].GetComponent<ShowBuffText>().currentbuffData = null;
        }
        for (int i = 0; i <baseMapRole.GetEquipBuffList().Count; i++)
        {
            roleBuff[i].sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.GetEquipBuffList()[i]);
            roleBuff[i].GetComponent<ShowBuffText>().currentbuffData =
                GameDataMgr.My.GetBuffDataByID(baseMapRole.GetEquipBuffList()[i]);
        }

        for (int i = 0; i <buffTF.childCount; i++)
        {
            Destroy(buffTF.GetChild(i));
        }
        for (int i = 0; i <baseMapRole.buffList.Count; i++)
        {
           GameObject game= Instantiate(buffPrb, buffTF);
           game.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Buff/" + baseMapRole.buffList[i]);
           game.GetComponent<ShowBuffText>().currentbuffData =
               baseMapRole.buffList[i].buffData;
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
            dealer.GetComponent<BaseRoleListInfo>().Init(role);
        }
    }

    public void ShowBuffText(string text)
    {
        buffcontent.SetActive(true);
        buffcontentText.text = text;
    }

    public void closeBuffContent()
    {
        buffcontent.SetActive(false);
        buffcontentText.text = null;
    }
}
