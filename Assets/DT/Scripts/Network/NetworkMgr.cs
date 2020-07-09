using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine.SceneManagement;

public class NetworkMgr : MonoSingletonDontDestroy<NetworkMgr>
{
    #region Player datas
    public bool isUsingHttp = false;

    public string loginRecordID;
    private string token;
    public string deviceID;
    public string playerID;
    public PlayerDatas playerDatas;
    public int currentLevel = 0;
    public int startTime = 0;

    public Answers answers;
    public string currentAnswer = "";

    private LevelProgress levelProgress;
    public LevelProgresses levelProgresses;
    public List<LevelProgress> levelProgressList;

    public List<ReplayList> replayLists;
    public List<ReplayList> rankList;

    private PlayerEquip playerEquip;
    public PlayerEquips playerEquips;
    public List<PlayerEquip> playerEquipsList;
    #endregion

    private void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        levelProgressList = new List<LevelProgress>();
        replayLists = new List<ReplayList>();
        rankList = new List<ReplayList>();
        playerEquipsList = new List<PlayerEquip>();
    }

    #region login
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void Login(string userName, string password, Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);

        StartCoroutine(HttpManager.My.HttpSend(Url.loginUrl, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                //Debug.Log(response.data);
                try
                {
                    playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    playerID = playerDatas.playerID;
                    loginRecordID = playerDatas.loginRecordID;
                    token = playerDatas.token;
                    //Debug.Log(playerID + " " + token);
                    //Debug.Log("token-----" + token);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.loginID));
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void Logout(Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("recordID", loginRecordID);
        keyValues.Add("token", token);

        StartCoroutine(HttpManager.My.HttpSend(Url.logout, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            
        }, keyValues, HttpType.Post));
    }
    #endregion

    #region playerDatas
    /// <summary>
    /// 创建角色信息
    /// </summary>
    /// <param name="playerName">昵称</param>
    /// <param name="playerIcon">头像</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void CreatPlayerDatas(string playerName, string playerIcon, Action doSuccess=null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerIcon", playerIcon);
        keyValues.Add("playerName", playerName);
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.createPlayerDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if(response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                //Debug.Log(response.data);
                try
                {
                    playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }

    /// <summary>
    /// 更新信息
    /// </summary>
    /// <param name="fteProgress">教学进度 无更新请填 0</param>
    /// <param name="threeWordsProgress">三句话进度 无更新请填 0</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void UpdatePlayerDatas(int fteProgress, int threeWordsProgress,Action doSuccess = null, Action doFail = null)
    {
        Debug.Log("更新fte"+fteProgress);
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("fteProgress", fteProgress.ToString());
        keyValues.Add("threeWordsProgress", threeWordsProgress.ToString());
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.updatePlayerDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                Debug.Log(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                Debug.Log(response.data);
                try
                {
                    playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.updatePlayerDatasID));
    }
    #endregion

    #region three words
    /// <summary>
    /// 回答问题
    /// </summary>
    /// <param name="words">答案</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void UploadThreeWords(string words, Action doSuccess=null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("answer", words);
        keyValues.Add("qID", (playerDatas.threeWordsProgress+1).ToString());
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);

        StartCoroutine(HttpManager.My.HttpSend(Url.uploadWords, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                try
                {
                    playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post,HttpId.uploadAnswerID));
    }

    /// <summary>
    /// 获取答案
    /// </summary>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetAnswers(Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.getAnswers, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            //Debug.Log(response.errMsg);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                //Debug.Log(response.data);
                try
                {
                    answers = JsonUtility.FromJson<Answers>(response.data);
                    //Debug.LogError(answers.answer1);
                    currentAnswer = answers.GetCurrentAnswer(playerDatas.threeWordsProgress);
                    //Debug.LogError(currentAnswer);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
                doSuccess();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getAnswerID));
    }
    #endregion

    #region Level
    /// <summary>
    /// 更新过关信息
    /// </summary>
    /// <param name="levelID">关卡id</param>
    /// <param name="stars">星数</param>
    /// <param name="levelStar">星数排列 如 “101“</param>
    /// <param name="rewardStatus">领取奖励状态 同levelStar</param>
    /// <param name="score">分数</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void UpdateLevelProgress(int levelID, int stars, string levelStar, string rewardStatus, int score, Action doSuccess=null, Action doFail=null)
    {
        LevelProgress lp = new LevelProgress(playerID, levelID, stars, levelStar, rewardStatus, score);

        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("levelProgress", JsonUtility.ToJson(lp));
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        StartCoroutine(HttpManager.My.HttpSend(Url.updateLevelProgress, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                //Debug.Log(response.data);
                try
                {
                    levelProgress = JsonUtility.FromJson<LevelProgress>(response.data);
                    for (int i=0; i<levelProgressList.Count; i++)
                    {
                        if (levelProgress.levelID == levelProgressList[i].levelID)
                        {
                            levelProgressList.RemoveAt(i);
                            break;
                        }
                    }
                    levelProgressList.Add(levelProgress);
                    Debug.Log("上传关卡"+ levelID.ToString() + "进度完成" );
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.updateLevelProID));
    }

    /// <summary>
    /// 获取所有的过关信息
    /// </summary>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetLevelProgress(Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.getLevelProgress, (www) =>
        {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 0)
            {
                //HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                Debug.Log(response.data);
                try
                {
                    levelProgresses = JsonUtility.FromJson<LevelProgresses>(response.data);
                    //Debug.LogWarning(levelProgresses.levelProgresses.Count+"-------------------");
                    levelProgressList.Clear();
                    foreach (var lp in levelProgresses.levelProgresses)
                    {
                        levelProgressList.Add(lp);
                    }
                    //Debug.LogWarning(levelProgressList.Count + "-------------------list");
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.TargetSite);
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getLevelProID));
    }

    #region 弃用
    [Obsolete("接口需求暂时取消, 请使用上传复盘数据方法（AddReplayData）",true)]
    public void AddLevelRecord(LevelRecord levelRecord, Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("levelRecord", JsonUtility.ToJson(levelRecord));
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);

        StartCoroutine(HttpManager.My.HttpSend(Url.addLevelRecord, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if(response.status == 0)
            {
                Debug.LogWarning(response.errMsg);
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }
    #endregion

    /// <summary>
    /// 上传复盘数据
    /// </summary>
    /// <param name="playerReplay">复盘的数据</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void AddReplayData(PlayerReplay playerReplay, Action doSuccess=null, Action doFail=null)
    {
        try
        {
            SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
            keyValues.Add("token", token);
            keyValues.Add("playerID", playerID);
            keyValues.Add("data", CompressUtils.Compress(JsonUtility.ToJson(playerReplay)));

            StartCoroutine(HttpManager.My.HttpSend(Url.addReplayData, (www) => {
                HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
                if (response.status == -1)
                {
                    GoToLogin(response.errMsg);
                    return;
                }
                if (response.status == 1)
                {
                    Debug.Log("上传完成");
                    doSuccess?.Invoke();
                }
                else
                {
                    Debug.Log("上传失败");
                    doFail?.Invoke();
                }
                SetMask();
            }, keyValues, HttpType.Post, HttpId.addReplayDataID));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            Debug.Log(ex.StackTrace);
        }
        
    }

    /// <summary>
    /// 获取某关的复盘数据
    /// </summary>
    /// <param name="recordID">复盘id（从复盘list里获取）</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetReplayDatas(string recordID, Action<ReplayDatas> doSuccess = null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("recordID", recordID);

        StartCoroutine(HttpManager.My.HttpSend( Url.getReplayDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 1)
            {
                string json = CompressUtils.Uncompress(response.data);
                ReplayDatas datas = JsonUtility.FromJson<ReplayDatas>(json);
                Debug.Log(datas.recordID);
                Debug.Log(datas.operations);
                Debug.Log(datas.dataStats);
                doSuccess?.Invoke(datas);
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getReplayDataID));

    }

    /// <summary>
    /// 获取某关的复盘列表（replayLists），可能为空，仅展示列表用，如下载复盘的数据请从中获取recordID，然后调用 GetReplayDatas 方法
    /// </summary>
    /// <param name="sceneName">场景名 如：FTE_1</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetReplayLists(string sceneName, Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("sceneName", sceneName);

        StartCoroutine(HttpManager.My.HttpSend(Url.getReplayLists, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if(response.status == 1)
            {
                ReplayLists replayList = JsonUtility.FromJson<ReplayLists>(response.data);
                replayLists.Clear();
                foreach(var rl in replayList.replayLists)
                {
                    replayLists.Add(rl);
                }
                //Debug.Log(replayLists.Count);
                //Debug.Log(replayLists[0].recordTime);
                //Debug.Log(replayLists[0].win);
                //Debug.Log(replayLists[0].recordID);
                doSuccess?.Invoke();
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getReplayListID));
    }

    /// <summary>
    /// 获取排行榜，存储在该类的 rankList 中， 可能为空，后续相关操作，请自行判断
    /// </summary>
    /// <param name="sceneName">场景名称 如：FTE_1</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetRankingList(string sceneName, Action doSuccess=null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        //keyValues.Add("token", token);
        keyValues.Add("playerID", "11111");
        keyValues.Add("sceneName", sceneName);

        StartCoroutine(HttpManager.My.HttpSend(Url.getRankingLists, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 1)
            {
                ReplayLists replayList = JsonUtility.FromJson<ReplayLists>(response.data);
                rankList.Clear();
                foreach (var rl in replayList.replayLists)
                {
                    rankList.Add(rl);
                }
                //Debug.Log(rankList.Count);
                //Debug.Log(rankList[0].recordTime);
                //Debug.Log(rankList[0].win);
                //Debug.Log(rankList[0].recordID);
                //Debug.Log(response.data);
                doSuccess?.Invoke();
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getRankListID));
    }

    /// <summary>
    /// 根据关卡ID获取关卡通关数据
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public LevelProgress GetLevelProgressByIndex(int index)
    {
        for (int i = 0; i < levelProgressList.Count; i++)
        {
            if (levelProgressList[i].levelID == index)
                return levelProgressList[i];
        }
        return null;
    }
    #endregion

    #region equip
    /// <summary>
    /// 获取所有装备
    /// </summary>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetPlayerEquips(Action<List<PlayerEquip>> doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);

        StartCoroutine(HttpManager.My.HttpSend(Url.getEquips, (www) => {
            HttpResponse httpResponse = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (httpResponse.status == -1)
            {
                GoToLogin(httpResponse.errMsg);
                return;
            }
            if (httpResponse.status == 1)
            {
                playerEquips = JsonUtility.FromJson<PlayerEquips>(httpResponse.data);
                playerEquipsList.Clear();
                foreach(var pe in playerEquips.playerEquips)
                {
                    playerEquipsList.Add(pe);
                }
                Debug.LogWarning(playerEquipsList.Count + "=========playerequip");
                doSuccess?.Invoke(playerEquipsList);
            }
            else
            {
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getEquipID));
    }

    /// <summary>
    /// 获得装备
    /// </summary>
    /// <param name="equipID">装备id</param>
    /// <param name="equipType">装备类型0 装备 1 工人</param>
    /// <param name="count">数量</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void AddEquip(int equipID, int equipType, int count, Action doSuccess=null, Action doFail=null)
    {
        PlayerEquip pe = new PlayerEquip(playerID, equipType, equipID, count);
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("playerEquip", JsonUtility.ToJson(pe));
        //return;

        StartCoroutine(HttpManager.My.HttpSend(Url.addEquip, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 1)
            {
                playerEquip = JsonUtility.FromJson<PlayerEquip>(response.data);
                //for(int i = 0; i < playerEquipsList.Count; i++)
                //{
                //    if(playerEquipsList[i].equipID == playerEquip.equipID && playerEquipsList[i].equipType == playerEquip.equipType)
                //    {
                //        playerEquipsList.RemoveAt(i);
                //        break;
                //    }
                //}
                //playerEquipsList.Add(playerEquip);
                playerEquipsList.Clear();
                playerEquipsList.Add(playerEquip);
                PlayerData.My.InitPlayerEquip(playerEquipsList);
                Debug.Log("获得装备 " + pe.equipID);
                doSuccess?.Invoke();
            }
            else
            {
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.addEquipID));
    }

    public void AddEquipList(List<PlayerEquip> playerEquips, Action doSuccess=null, Action doFail = null)
    {
        PlayerEquips equips = new PlayerEquips();
        equips.playerEquips = playerEquips;
        string json = JsonUtility.ToJson(equips);
        Debug.Log(json);
        //{"playerEquips":[{"playerID":"999999","equipType":0,"equipID":22202,"count":1},{"playerID":"999999","equipType":1,"equipID":10001,"count":1},{"playerID":"999999","equipType":0,"equipID":22202,"count":1},{"playerID":"999999","equipType":1,"equipID":10001,"count":1}]}

        Debug.Log(JsonUtility.FromJson<ParseEquips>(json).playerEquips);
        return;
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("playerEquips", JsonUtility.ToJson(playerEquips));
        Debug.Log(JsonUtility.ToJson(playerEquips));

        StartCoroutine(HttpManager.My.HttpSend(Url.addEquips, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin(response.errMsg);
                return;
            }
            if (response.status == 1)
            {
                PlayerEquips pes = JsonUtility.FromJson<PlayerEquips>(response.data);

                PlayerData.My.InitPlayerEquip(pes.playerEquips);
                foreach(var pe in playerEquips)
                {
                    Debug.Log("获得装备" + pe.equipID);
                }
                doSuccess?.Invoke();
            }
            else
            {
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.addEquipsID));
    }
    #endregion

    #region other
    public void LevelStartTime()
    {
        startTime = TimeStamp.GetCurrentTimeStamp();
    }

    private void GoToLogin(string tip)
    {
        HttpManager.My.ShowClickTip(tip, () => { SceneManager.LoadScene("Login"); });
    }

    private void SetMask()
    {
        HttpManager.My.mask.SetActive(false);
    }

    public void TestPost(string json)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("json", json);
        keyValues.Add("playerID", "11111");

        StartCoroutine(HttpManager.My.HttpSend(Url.testPost, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            Debug.Log(response.data);
        }, keyValues, HttpType.Post));
    }
    #endregion

    private void OnApplicationQuit()
    {
        if(isUsingHttp&&playerID!=null)
            Logout();
    }
}

[Serializable]
public class HttpResponse
{
    public int status;
    public string data;
    public string errMsg;
}
