using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StageEnemyItem
{
    public string waveNumber;

    public string point1;

    public string point2;

    public string point3;

    public string point4;

    public string point5;

    public string point6;
}

public class StageEnemysData
{
    public List<StageEnemyItem> stageEnemyItems = new List<StageEnemyItem>();
}
