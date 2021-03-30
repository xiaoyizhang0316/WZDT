using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRect : MonoBehaviour
{
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
        Invoke("Hide", 2);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
