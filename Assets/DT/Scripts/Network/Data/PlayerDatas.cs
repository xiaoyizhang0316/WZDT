using System.Collections;
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
    public string playerID;
    public int levelID;
    public string levelStar;
    public string rewardStatus;
    public int score;

    public LevelProgress(string playerID, int levelID, string levelStar, string rewardStatus, int score)
    {
        this.playerID = playerID;
        this.levelID = levelID;
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
    public int equipID;
    public int count;
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
    public int buildTpCost;
    public int mirrorTpCost;
    public int unlockTpCost;
    public int npcTpIncome;
    public int wordkerTpIncome;
    public int waveCount;
    public int remainCoin;
    public int score;
    public int timeCost;
    public int startTime;
    public int uploadTime;
}