using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBuffText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BuffData currentbuffData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentbuffData != null)
            RoleUpdateInfo.My.ShowBuffText(currentbuffData.BuffDesc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RoleUpdateInfo.My.closeBuffContent(); 

    }
}
