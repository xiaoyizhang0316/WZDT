using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IndicationSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    public string describe;
    public void OnPointerEnter(PointerEventData eventData)
    {
       IndicatingManager.My.Init(transform,describe);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IndicatingManager.My.Hide();
    }
}
