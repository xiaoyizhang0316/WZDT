﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMaga : BaseFinancialCompanyThreshold
{
    public int mageAdd = 0 ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Threshold()
    {
        if (StageGoal.My.techCost > mageAdd)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string FailedTip()
    {
        return "累计消耗Maga值为达标";
    }
}