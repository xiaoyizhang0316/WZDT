﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerDatas
{
    public int status;
    public string msg;
    public string playerID;
    public string playerName;
    public string playerIcon;

    public int fteProgress;
    public int threeWordsProgress;
    public string loginRecordID;
    public string token;
}

[Serializable]
public class Answers
{
    public string answer1;
    public string answer2;
    public string answer3;

    public string GetCurrentAnswer(int qID)
    {
        switch (qID)
        {
            case 1:
                return answer1;
            case 2:
                return answer2;
            case 3:
                return answer3;
            default:
                break;
        }
        return null;
    }
}

[Serializable]
public class LevelProgress
{
    public int id;
    public string playerID;
    public int levelID;
    public int starNum;
    public string levelStar;
    public string rewardStatus;
    public int score;

    public LevelProgress(string playerID, int levelID, int stars, string levelStar, string rewardStatus, int score)
    {
        this.playerID = playerID;
        this.levelID = levelID;
        this.starNum = stars;
        this.levelStar = levelStar;
        this.rewardStatus = rewardStatus;
        this.score = score;
    }
}

[Serializable]
public class LevelProgresses
{
    public List<LevelProgress> levelProgresses;
}

[Serializable]
public class PlayerEquip
{
    public string playerID;
    public int equipType;
    public int equipID;
    public int count;

    public PlayerEquip(string playerID, int equipType, int equipID, int count)
    {
        this.playerID = playerID;
        this.equipType = equipType;
        this.equipID = equipID;
        this.count = count;
    }
}

[Serializable]
public class PlayerEquips
{
    public List<PlayerEquip> playerEquips;
}

[Serializable]
public class LevelRecord
{
    public string playerID;
    public int levelID;
    public int levelStar;
    public int tradeCost;
    public int currencyCost;
    public int extraCost;
    public int consumerIncome;
    public int npcIncome;
    public int otherIncome;
    public int buildTpCost; // 科技点相关
    public int mirrorTpCost;
    public int unlockTpCost;
    public int npcTpIncome;
    public int wordkerTpIncome;
    public int buffTpIncome;
    public int remainTp;
    public int waveCount;
    public int remainCoin;
    public int score;
    public int timeCost;
    public int startTime;
    public int uploadTime;

    public LevelRecord(string playerID, int levelID, int levelStar, int tradeCost, int currencyCost, int extraCost, int consumerIncome, int npcIncome, int otherIncome, int buildTpCost, int mirrorTpCost, int unlockTpCost, int npcTpIncome, int wordkerTpIncome, int buffTpIncome, int remainTp, int waveCount, int remainCoin, int score, int timeCost, int startTime, int uploadTime)
    {
        this.playerID = playerID;
        this.levelID = levelID;
        this.levelStar = levelStar;
        this.tradeCost = tradeCost;
        this.currencyCost = currencyCost;
        this.extraCost = extraCost;
        this.consumerIncome = consumerIncome;
        this.npcIncome = npcIncome;
        this.otherIncome = otherIncome;
        this.buildTpCost = buildTpCost;
        this.mirrorTpCost = mirrorTpCost;
        this.unlockTpCost = unlockTpCost;
        this.npcTpIncome = npcTpIncome;
        this.wordkerTpIncome = wordkerTpIncome;
        this.buffTpIncome = buffTpIncome;
        this.remainTp = remainTp;
        this.waveCount = waveCount;
        this.remainCoin = remainCoin;
        this.score = score;
        this.timeCost = timeCost;
        this.startTime = startTime;
        this.uploadTime = uploadTime;
    }
}