﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_1_13 : BaseGuideStep
{
    public GameObject trade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator StepStart()
    {
       
 
        yield break;
    }

    public override IEnumerator StepEnd()
    {
      
        trade.SetActive(false);
        yield break;
    }

  
}
