using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class FTEData  
{
   public List<FTEStepData> datas = new List<FTEStepData>();
}
[Serializable]
public class FTEStepData
{
    public int stepID;

    public string textContent_1;
    public string textContent_2;
    public string textContent_3;
    public string textContent_4;
    public string textContent_5;
    
    
}