using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Debug.Log("ffasdsadsad");
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
