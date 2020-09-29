using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnum;

public class EncourageLevel : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BaseMapRole currentRole;

    public Text levelText;

    public string showStr;

    public Image indexImg;

    public void Init(BaseMapRole role)
    {
        currentRole = role;
        showStr = "激励等级0，无任何影响";
        levelText.text = role.encourageLevel.ToString();
        indexImg.transform.localPosition = new Vector2(-73.3f + role.encourageLevel * 36.7f, indexImg.transform.localPosition.y);
        levelText.color = Color.white;
        if (role.baseRoleData.baseRoleData.roleSkillType == RoleSkillType.Service)
        {
            if (role.encourageLevel > 0)
            {
                showStr = "激励等级" + role.encourageLevel.ToString() + " 角色的增益效果提高" + (role.encourageLevel * 10).ToString("##.##") +"%";
                levelText.color = Color.green;
            }
            else if (role.encourageLevel < 0)
            {
                showStr = "激励等级" + role.encourageLevel.ToString() + " 角色的增益效果降低" + (role.encourageLevel * -10).ToString("##.##") + "%";
                levelText.color = Color.red;
            }
        }
        else
        {
            if (role.encourageLevel > 0)
            {
                showStr = "激励等级" + role.encourageLevel.ToString() + " 角色的生产速率提高" + (role.encourageLevel * 5).ToString("##.##") + "%";
                levelText.color = Color.green;
            }
            else if (role.encourageLevel < 0)
            {
                showStr = "激励等级" + role.encourageLevel.ToString() + " 角色的生产速率降低" + (role.encourageLevel * -10).ToString("##.##") + "%";
                levelText.color = Color.red;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FloatWindow.My.Init(showStr);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
