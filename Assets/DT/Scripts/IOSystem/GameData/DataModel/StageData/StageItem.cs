using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StageItem
{
    public string sceneName;

    public string maxWaveNumber;

    public string waveWaitTime;

    public string startPlayerHealth;

    public string startPlayerGold;

    public string startTech;

    public string stageType;

    //public string maxMinusGold;

    /// <summary>
    /// 1星奖励装备
    /// </summary>
    public string starOneEquip;

    /// <summary>
    /// 1星奖励工人
    /// </summary>
    public string starOneWorker;

    /// <summary>
    /// 2星奖励装备
    /// </summary>
    public string starTwoEquip;

    /// <summary>
    /// 2星奖励工人
    /// </summary>
    public string starTwoWorker;

    /// <summary>
    /// 3星奖励装备
    /// </summary>
    public string starThreeEquip;

    /// <summary>
    /// 3星奖励工人
    /// </summary>
    public string starThreeWorker;
}

public class StagesData
{
    public List<StageItem> stageSigns = new List<StageItem>();
}