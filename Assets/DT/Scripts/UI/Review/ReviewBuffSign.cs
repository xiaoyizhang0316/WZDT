using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReviewBuffSign : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
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
        ReviewBuffManager.My.ShowBuffContent( GameDataMgr.My.GetBuffDataByID(int.Parse(GetComponent<Image>().sprite.name)).BuffDesc);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ReviewBuffManager.My.CloseBuffContent();
    }
}
