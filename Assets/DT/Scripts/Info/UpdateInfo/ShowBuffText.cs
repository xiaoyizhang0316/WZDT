using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBuffText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BuffData currentbuffData;

    public BaseMapRole role;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentbuffData != null)
            RoleUpdateInfo.My.ShowBuffText(currentbuffData.GenerateBuffDesc(role));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RoleUpdateInfo.My.closeBuffContent(); 

    }
}
