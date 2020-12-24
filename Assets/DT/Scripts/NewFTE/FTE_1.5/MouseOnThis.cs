using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class MouseOnThis : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public bool isOn = false;
    private float time = 0;
    private bool isIn = false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        isIn = true;
    }

    private void Update()
    {
        if (isIn)
        {
            time += Time.deltaTime;
        }

        if (time >= 1)
        {
            isOn = true;
        }
    }

    private void OnEnable()
    {
        if (isOn = true)
        {
            isOn = false;
            time = 0;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isIn = false;
        time = 0;
    }
}
