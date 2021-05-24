using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EncourageLevelInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public string showStr;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        FloatWindow.My.Init(showStr);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FloatWindow.My.Hide();
    }
}
