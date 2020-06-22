using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBuffText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BuffData currentbuffData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
