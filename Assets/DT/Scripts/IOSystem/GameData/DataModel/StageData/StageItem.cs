using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class StageItem
{
    public string sceneName;

    public string startWorker;

    public string startEquip;

    public string maxWaveNumber;

    public string waveWaitTime;

    public string startPlayerHealth;

    public string startPlayerGold;
}

public class StagesData
{
    public List<StageItem> stageSigns = new List<StageItem>();
}