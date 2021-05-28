using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGoldInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string ToString()
    {
        return "玩家金钱：<color=green>" + StageGoal.My.playerGold + "</color>\n投资支持：<color=green>" + StageGoal.My.financialGold+"</color>";
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
