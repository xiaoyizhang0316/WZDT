using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HideRect : MonoBehaviour, IPointerEnterHandler
{
    public bool needMouseOn = false;
    [SerializeField]
    private bool mouseOn = false;
    private void Start()
    {
        //Invoke("Hide", 2);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //Debug.Log("enable exe");
        if (needMouseOn)
        {
            mouseOn = true;
            GetComponent<Image>().raycastTarget = true;
        }
        
        InvokeRepeating("Check", 0, 0.5f);
    }

    private int i = 0;
    void Check()
    {
        //Debug.Log("check"+(i++));
        if (!mouseOn)
        {
            //Debug.Log("hide"+(i++));
            Invoke("Hide", 2);
            CancelInvoke("Check");
        }
    }
    
    

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (needMouseOn)
        {
            mouseOn = false;
            GetComponent<Image>().raycastTarget = false;
        }
    }
}
