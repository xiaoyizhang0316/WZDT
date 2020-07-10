using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Url
{
    private static string ip = "http://39.106.226.52:8080/";

    #region login
    public static string loginUrl = ip + "login";
    public static string reConnUrl = ip + "reconnect";
    public static string registerUrl = ip + "register";
    public static string logout = ip + "logout";
    public static string createPlayerDatas = ip + "createPlayerDatas";
    public static string creatKeyUrl = ip + "createKeyCodes";
    public static string getKeyUrl = ip + "getKeyCodes";
    public static string checkDeviceID = ip + "checkDeviceID";
    #endregion

    #region player infos
    public static string updatePlayerDatas = ip + "updatePlayerDatas";
    #endregion

    #region three words 
    public static string uploadWords = ip + "setAnswer";
    public static string getAnswers = ip + "getAnswers";
    #endregion

    #region level
    public static string updateLevelProgress = ip + "updateLevelProgress";
    public static string addLevelRecord = ip + "addLevelRecord";
    public static string getLevelProgress = ip + "getLevelProgress";


    public static string addReplayData = ip + "addReplayData";
    public static string getReplayDatas = ip + "getReplayDatas";
    public static string getReplayLists = ip + "getReplayLists";
    public static string getRankingLists = ip + "getRankingList";
    #endregion

    #region equip
    public static string addEquip = ip + "addEquip";
    public static string getEquips = ip + "getPlayerEquips";
    public static string deleteEquip = ip + "deleteEquip";
    public static string addEquips = ip + "addEquips";
    #endregion

    #region test
    public static string testPost = ip + "testPost";
    public static string testGet = ip + "testGet";
    #endregion
}
