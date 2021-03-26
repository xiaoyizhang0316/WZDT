using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_Tips : MonoBehaviour
{
    public string tips;

    public float waitTime = 30;
    // Start is called before the first frame update
    void Start()
    {
        FTE_TipManager.My.HideTips();
        Invoke("ShowTip", waitTime);
    }

    void ShowTip()
    {
        FTE_TipManager.My.ShowTip(tips);
    }

    private void OnDisable()
    {
        FTE_TipManager.My.HideTips();
        CancelInvoke();
    }
}
