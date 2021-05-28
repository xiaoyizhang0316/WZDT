using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SkillCheckBase scb;
    string ToString()
    {
        if(scb==null)
            scb = GetComponent<SkillCheckBase>();
        string tip = "检测的角色：" + scb.dependRole.baseRoleData.baseRoleData.roleName + "\n" +
                     "检测的回合：" + scb.checkedTurn + "/" + scb.checkTurn + "\n" +
                     "失败的次数：" + scb.checkedCount + "/" + scb.checkCount;
        return tip;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       FloatWindow.My.Init(ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
