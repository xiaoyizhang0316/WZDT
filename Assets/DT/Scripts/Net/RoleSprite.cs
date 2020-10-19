﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static GameEnum;

public class RoleSprite : MonoBehaviour
{
    public BaseMapRole mapRole;

    public SpriteRenderer sprite;

    public SpriteRenderer lockSprite;

    public void CheckSprite()
    {
        sprite = GetComponent<SpriteRenderer>();
        mapRole = GetComponentInParent<BaseMapRole>();
        if (!mapRole.isNpc)
        {
            sprite.sprite = Resources.Load<Sprite>("Sprite/hong/" + mapRole.baseRoleData.baseRoleData.roleType.ToString() + mapRole.baseRoleData.baseRoleData.level.ToString());
        }
        else
        {
            List<RoleType> typeList = new List<RoleType> {RoleType.Seed,RoleType.Peasant,RoleType.Merchant,RoleType.Dealer };
            if (!mapRole.npcScript.isCanSee)
            {
                if (typeList.Contains(mapRole.baseRoleData.baseRoleData.roleType))
                {
                    sprite.sprite = Resources.Load<Sprite>("Sprite/lan/unknown");
                }
                else
                {
                    sprite.sprite = Resources.Load<Sprite>("Sprite/npcF/unknown");
                }
            }
            else
            {
                if (typeList.Contains(mapRole.baseRoleData.baseRoleData.roleType))
                {
                    sprite.sprite = Resources.Load<Sprite>("Sprite/lan/" + mapRole.baseRoleData.baseRoleData.roleType.ToString() + mapRole.baseRoleData.baseRoleData.level.ToString());
                }
                else
                {
                    sprite.sprite = Resources.Load<Sprite>("Sprite/npcF/" + mapRole.baseRoleData.baseRoleData.roleType.ToString());
                }
            }
        }
    }

    public void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (!mapRole.isNpc ||(mapRole.isNpc && mapRole.npcScript.isCanSee))
            {
                string desc = GameDataMgr.My.GetTranslateName(mapRole.baseRoleData.baseRoleData.roleType.ToString());
                RoleFloatWindow.My.Init(transform, desc, mapRole.baseRoleData.baseRoleData.roleSkillType, mapRole.baseRoleData.baseRoleData.roleType);
            }
            else
            {
                RoleFloatWindow.My.Init(transform, "未知角色", RoleSkillType.Solution, RoleType.All);
            }
        }
    }

    private void OnMouseExit()
    {
        RoleFloatWindow.My.Hide();
    }

    public void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            NewCanvasUI.My.Panel_NPC.SetActive(false);
            NewCanvasUI.My.Panel_Update.SetActive(false);
            if (mapRole.isNpc)
            {
                NewCanvasUI.My.Panel_NPC.SetActive(true);
                //NewCanvasUI.My.Panel_RoleInfo.SetActive(true);
                //RoleListInfo.My.Init(currentRole);
                if (mapRole.npcScript.isCanSee)
                {
                    if (mapRole.npcScript.isLock)
                    {
                        NPCListInfo.My.ShowUnlckPop(mapRole.transform);
                    }
                    else
                    {
                        NPCListInfo.My.ShowNpcInfo(mapRole.transform);
                    }
                }
                else if (int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]) > 3)
                {
                    NPCListInfo.My.ShowHideTipPop("使用广角镜发现角色");
                }
            }
            else
            {
                NewCanvasUI.My.Panel_Update.SetActive(true);
                RoleUpdateInfo.My.Init(mapRole.baseRoleData);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        mapRole = GetComponentInParent<BaseMapRole>();
        lockSprite = GetComponentsInChildren<SpriteRenderer>()[1];
        mapRole.roleSprite = this;
        gameObject.SetActive(false);
        if (!mapRole.isNpc)
        {
            mapRole.CheckRoleDuty();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mapRole.isNpc && mapRole.npcScript.isCanSee && mapRole.npcScript.isLock)
        {
            lockSprite.gameObject.SetActive(true);
        }
        else
        {
            lockSprite.gameObject.SetActive(false);
        }
    }
}
