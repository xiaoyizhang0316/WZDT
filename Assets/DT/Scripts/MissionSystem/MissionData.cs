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

    public bool isFail;

    public bool CheckNumFinish()
    {
        if (currentNum >= maxNum)
        {
            return true;
        }
        return false;
    }
}

[Serializable]
public class MissionDatas
{
    public List<MissionData> data = new List<MissionData>();

    public bool CheckEnd()
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (!data[i].isFinish)
            {
                return false;
            }
        }

        return true;
    }
}