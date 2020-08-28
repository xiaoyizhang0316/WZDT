using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReviewBuffSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        ReviewBuffManager.My.ShowBuffContent( GameDataMgr.My.GetBuffDataByID(int.Parse(GetComponent<Image>().sprite.name)).BuffDesc);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ReviewBuffManager.My.CloseBuffContent();
    }
}
