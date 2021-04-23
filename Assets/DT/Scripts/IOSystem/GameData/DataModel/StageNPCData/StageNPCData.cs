using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageNPCData
{
    public string roleType;

    public int level;

    public string npcName;

    public int effect;

    public double npcID;

    public int efficiency;

    public int risk;

    public int tradeCost;

    public int range;

    public int bulletCount;

    public int posX;

    public int posY;

    public int startEncourageLevel;

    public bool isCanSee;

    public bool isLock;

    public bool isCanSeeEquip;

    public int lockNumber;

    public string skillDesc;

    public List<int> initBuffList;

    public List<int> hideBuffList;

    public StageNPCData(StageNPCItem item)
    {
        roleType = item.roleType;
        level = int.Parse(item.level);
        npcName = item.npcName;
        effect = int.Parse(item.effect);
        npcID = double.Parse(item.npcID);
        efficiency = int.Parse(item.efficiency);
        risk = int.Parse(item.risk);
        tradeCost = int.Parse(item.tradeCost);
        range = int.Parse(item.range);
        bulletCount = int.Parse(item.bulletCount);
        posX = int.Parse(item.posX);
        posY = int.Parse(item.posY);
        startEncourageLevel = int.Parse(item.startEncourageLevel);
        //startEncourageLevel = 0;
        isCanSee = bool.Parse(item.isCanSee);
        isCanSeeEquip = bool.Parse(item.isCanSeeEquip);
        isLock = bool.Parse(item.isLock);
        lockNumber = int.Parse(item.lockNumber);
        skillDesc = item.skillDesc;
        initBuffList = new List<int>();
        hideBuffList = new List<int>();
        foreach (var str in item.initBuffList.Split(','))
        {
            if (int.Parse(str) != -1)
                initBuffList.Add(int.Parse(str));
        }
        foreach (var str in item.hideBuffList.Split(','))
        {
            if (int.Parse(str) != -1)
                hideBuffList.Add(int.Parse(str));
        }
    }

    public StageNPCData()
    {

    }
}
