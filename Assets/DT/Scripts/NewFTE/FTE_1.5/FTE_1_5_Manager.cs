using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_5_Manager : MonoSingleton<FTE_1_5_Manager>
{
    public bool isClearGoods = false;
    public int goal1FinalCost = 0;
    public bool needSkip = false;
    private void Start()
    {
        isClearGoods = false;
    }
}
