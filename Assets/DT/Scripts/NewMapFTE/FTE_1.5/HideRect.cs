using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRect : MonoBehaviour
{
    private void Start()
    {
        Invoke("Hide", 2);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
