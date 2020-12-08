using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tipsWin : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public  GameObject text;
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
        text.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.gameObject.SetActive(false);
    }
    
}
