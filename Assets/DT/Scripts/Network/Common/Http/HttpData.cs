﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpData
{
    public int httpId;
    public string url;
    public Action<UnityWebRequest> action;
    public SortedDictionary<string, string> keyValues;
    public HttpType httpType;

    public bool needRetry;

    public HttpData(int httpId, string url, Action<UnityWebRequest> action, SortedDictionary<string, string> keyValues, HttpType httpType, bool needRetry)
    {
        this.httpId = httpId;
        this.url = url;
        this.action = action;
        this.keyValues = keyValues;
        this.httpType = httpType;
        this.needRetry = needRetry;
    }

    public void SetNeedRetry(bool retry)
    {
        needRetry = retry;
    }
}

public static class HttpId
{
    public const int loginID                    = 1000;
    public const int updatePlayerDatasID        = 1001;
    public const int uploadAnswerID             = 1002;
    public const int getAnswerID                = 1003;
    public const int updateLevelProID           = 1004;
    public const int getLevelProID              = 1005;
    public const int addReplayDataID            = 1006;
    public const int getReplayDataID            = 1007;
    public const int getReplayListID            = 1008;
    public const int getRankListID              = 1009;
    public const int getEquipID                 = 1010;
    public const int addEquipID                 = 1011;
    public const int addEquipsID                = 1012;
}
