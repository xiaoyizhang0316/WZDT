using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EncourageSkillInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string showStr;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrEmpty(showStr))
        {
            return;
        }
        FloatWindow.My.Init(showStr);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
