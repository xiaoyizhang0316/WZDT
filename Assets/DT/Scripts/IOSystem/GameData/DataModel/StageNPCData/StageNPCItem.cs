using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StageNPCItem
{
    public string roleType;

    public string level;

    public string npcName;

    public string effect;

    public string npcID;

    public string efficiency;

    public string risk;

    public string tradeCost;

    public string range;

    public string bulletCount;

    public string posX;

    public string posY;

    public string startEncourageLevel;

    public string isCanSee;

    public string isLock;

    public string isCanSeeEquip;

    public string lockNumber;

    public string skillDesc;

    public string initBuffList;

    public string hideBuffList;

    public string goodBaseBuffList;

    public string badBaseBuffList;

    public string npcTag;
}

[Serializable]
public class StageNPCsData
{
    public List<StageNPCItem> stageNPCItems = new List<StageNPCItem>();
}
