﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnirxTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Test()
    {
        Debug.Log(TimeStamp.GetCurrentTimeStamp());
        yield return new WaitForSeconds(2);
        Debug.Log(TimeStamp.GetCurrentTimeStamp());
        yield return new WaitForSeconds(2);
        Debug.Log(TimeStamp.GetCurrentTimeStamp());
        yield return new WaitForSeconds(2);
        Debug.Log(TimeStamp.GetCurrentTimeStamp());
    }
}
