using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MissionData
{

    public string content;

    public int  currentNum = 0;

    public int maxNum ;

    public bool isMainmission;

    public bool isFinish;
}

[Serializable]
public class MissionDatas
{
    public List<MissionData> data = new List<MissionData>();
}