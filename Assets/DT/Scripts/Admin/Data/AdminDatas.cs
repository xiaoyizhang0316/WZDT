using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdminDatas 
{
    
}

[Serializable]
public class PlayerGroup
{
    public int groupID;
    public string groupName;
}

[Serializable]
public class PlayerGroups
{
    public List<PlayerGroup> playerGroups;
}

[Serializable]
public class TotalPlayCount
{
    public int total;
    public int win;
    public int times;
}

[Serializable]
public class LevelPlayCount
{
    public string levelName;
    public int playCount;
    public int winCount;
    public int times;
}

[Serializable]
public class LevelPlayCounts
{
    public List<LevelPlayCount> levelPlayCounts;
}

[Serializable]
public class GroupTotalPlayCount
{
    public int groupID;
    public int total;
    public int win;
    public int times;
}

[Serializable]
public class GroupTotalPlayCounts
{
    public List<GroupTotalPlayCount> groupTotalPlayCounts;
}

[Serializable]
public class GroupLevelPlayCount
{
    public int groupID;
    public string levelName;
    public int playCount;
    public int winCount;
    public int times;
}

[Serializable]
public class GroupLevelPlayCounts
{
    public List<GroupLevelPlayCount> groupTotalPlayCount;
}

[Serializable]
public class GroupPlayerLevelPlayCount
{
    public string playerID;
    public string playerName;

    public int level1;
    public int level2;
    public int level3;
    public int level4;
    public int level5;
    public int level6;
    public int level7;
    public int level8;
    public int level9;
}

[Serializable]
public class GroupPlayerLevelPlayCounts
{
    public List<GroupPlayerLevelPlayCount> groupPlayerLevelPlayCounts;
}

[Serializable]
public class LevelPass
{
    public int levelID;
    public int passNum;
}

[Serializable]
public class LevelPasses
{
    public List<LevelPass> levelPasses;
}

[Serializable]
public class GroupLevelPassDetail
{
    public int id;
    public string playerName;
    public string playerID;
    public int time;
    public int score;
}

[Serializable]
public class GroupPlayer
{
    public string playerID;
    public string playerName;

    public GroupPlayer(string playerID, string playerName)
    {
        this.playerID = playerID;
        this.playerName = playerName;
    }
}

[Serializable]
public class GroupPlayers
{
    public List<GroupPlayer> groupPlayer;
}

[Serializable]
public class GroupLevelPassDetails
{
    public List<GroupLevelPassDetail> groupLevelPassDetails;
}

[Serializable]
public class PlayerThreeWord
{
    public string playerID;
    public string playerName;
    public string word1;
    public string word2;
    public string word3;
    public int groupID;
}

[Serializable]
public class PlayerThreeWordList
{
    public List<PlayerThreeWord> playerThreeWords;
}

public class AdminUrls
{
    private static string ip = "http://127.0.0.1:8080/";
    //private static string ip = "http://39.106.226.52:8080/";

    public static string getPlayerGroups = ip + "getAllGroupList";
    public static string getTotalPlayCount = ip + "getTotalPlayCount";
    public static string getLevelPlayCount = ip + "getLevelPlayCount";
    public static string getGroupTotalPlayCount = ip + "getGroupTotalPlayCount";
    public static string getGroupLevelData = ip + "getGroupLevelData";
    public static string getGroupPlayerLevelPlayCount = ip + "getGroupPlayerLevelPlayCount";
    public static string getGroupPlayerLevelWinCount = ip + "getGroupPlayerLevelWinCount";
    public static string getGroupPlayerLevelTimeCount = ip + "getGroupPlayerLevelTimeCount";
    public static string getGroupLevelPass = ip + "getGroupLevelPass";
    public static string adminLogin = ip + "adminLogin";
    public static string getGroupLevelPassDetail = ip + "getGroupLevelPassDetail";
    public static string getGroupTryLevelPassDetail = ip + "getGroupTryLevelPassDetail";
    public static string getGroupPlayerThreeWords = ip + "getGroupPlayerThreeWords";
    public static string getGroupPlayerStatus = ip + "getGroupPlayerStatus";
}