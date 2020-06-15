﻿using System.Collections;
using System.Collections.Generic;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class FloatWindow : MonoSingleton<FloatWindow>
{
    public Text showText;

    public float x = 2;

    public float y = -2;

    public void Init(Transform _transform, string str)
    {
        showText.text = str;
        transform.position = _transform.position + new Vector3(3,-2,0f);
    }

    public void Hide()
    {
        transform.position = new Vector3(10000f,0f,0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}