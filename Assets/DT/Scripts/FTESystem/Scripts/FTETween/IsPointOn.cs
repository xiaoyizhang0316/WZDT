using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsPointOn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public bool IsOn;
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
        IsOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsOn = false;
         
    }
}
