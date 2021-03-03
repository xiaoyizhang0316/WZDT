using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine.SceneManagement;
using static GameEnum;

public class NetworkMgr : MonoSingletonDontDestroy<NetworkMgr>
{
    #region Player datas
    public bool isUsingHttp = false;
    public bool isShowFPS = false;
    public bool useLocalJson = false;

    public string loginRecordID;
    private string token;
    private int groupID = 0;
    private string deviceID;
    public string playerID;
    public int playerLimit = 1;
    public PlayerDatas playerDatas;
    public int currentLevel = 0;
    public int startTime = 0;

    public Answers answers;
    public string currentAnswer = "";

    private LevelProgress levelProgress;
    public LevelProgresses levelProgresses;
    public List<LevelProgress> levelProgressList;

    public List<ReplayList> replayLists;

    public GlobalRankList globalList;
    public GroupRankList groupList;
    public GroupRankList groupRank;
    public GlobalRankList globalRank;
    public int currentGroupPage = 0;
    public int currentGlobalPage = 0;
    

    private PlayerEquip playerEquip;
    public PlayerEquips playerEquips;
    public List<PlayerEquip> playerEquipsList;
    public Dictionary<RoleType, int> roleFoundDic = new Dictionary<RoleType, int>();
    #endregion

    private void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        levelProgressList = new List<LevelProgress>();
        replayLists = new List<ReplayList>();
        
        playerEquipsList = new List<PlayerEquip>();
    }


    public void PingTest(string ip , Action doSuccess, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("pingData", "valid");
        //string originIp = Url.ipAddr;
        //Url.ipAddr = ip;
        Url.SetIp(ip);
        StartCoroutine(HttpManager.My.HttpSend(Url.PingIp, (www) => {
            try
            {

                HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
                if (response.status == 0)
                {
                    HttpManager.My.ShowTip(response.errMsg);
                    doFail?.Invoke();
                }
                else
                {
                    if (response.data.Equals("valid"))
                    {

                        doSuccess?.Invoke();
                    }
                    else
                    {
                        //Url.ipAddr = originIp;
                        Url.SetIp(null);
                        doFail?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                Url.SetIp(null);
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.pingTestID));
    }
    #region login
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="password">密码</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void Login(string userName, string password, Action doSuccess = null, Action<bool,string> doFail= null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);
        keyValues.Add("DeviceId", deviceID);

        StartCoroutine(HttpManager.My.HttpSend(Url.NewLoginUrl, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                //HttpManager.My.ShowTip(response.errMsg);
                if (response.data.Contains("密码"))
                {
                    doFail?.Invoke(true,response.data);
                }
                else
                {

                doFail?.Invoke(false,response.data);
                }
            }
            else
            {
                Debug.Log(response.data);
                try
                {
                    playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    if (playerDatas.isOutDate)
                    {
                        GoToLogin("账号已失效！");
                        return;
                    }
                    playerID = playerDatas.playerID;
                    loginRecordID = playerDatas.loginRecordID;
                    token = playerDatas.token;
                    groupID = playerDatas.groupID;
                    playerLimit = playerDatas.limit;
                    InitRoleFoundDic(playerDatas.roleFound);
                    PlayerData.My.ParsePlayerTalent(playerDatas.talent);
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
    /// 5_22_23_24_21_26
    /// </summary>
    /// <param name="roleFound"></param>
    private void InitRoleFoundDic(string roleFound)
    {
        roleFoundDic.Clear();
        string[] roleArr= roleFound.Split('_');
        string[] rfa;
        if (roleFound.Contains("-"))
        {
            for(int i =0; i < roleArr.Length; i++)
            {
                rfa = roleArr[i].Split('-');
                roleFoundDic.Add((RoleType)int.Parse(rfa[0]), int.Parse(rfa[1]));
            }
        }
        else
        {
            for (int i = 0; i < roleArr.Length; i++)
            {
                roleFoundDic.Add((RoleType)int.Parse(roleArr[i]), 0);
            }
        }
    }

    public void SetPlayerStatus(string sceneName, string teamID, Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        keyValues.Add("sceneName", sceneName);
        keyValues.Add("teamID", teamID);

        StartCoroutine(HttpManager.My.HttpSend(Url.SetPlayerStatusScene, (www) => {
            //HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            //if (response.status == -1)
            //{
            //    ShowReconn();
            //    return;
            //}
            //if (response.status == 0)
            //{
            //    HttpManager.My.ShowTip(response.errMsg);
            //    Debug.Log(response.errMsg);
            //    doFail?.Invoke();
            //}
            //else
            //{
            //    Debug.Log(response.data);
                
            //    doSuccess?.Invoke();
            //}
            SetMask();
        }, keyValues, HttpType.Post));
    }

    /// <summary>
    /// 更新看过的角色
    /// </summary>
    public void UpdateRoleFound()
    {
        string roleFound = "";
        //if (roleFoundDic.Count == 0)
        //{
        //    roleFound = "0";
        //}
        //else
        //{
            foreach(var key in roleFoundDic.Keys)
            {
                roleFound += (int)key + "-" + roleFoundDic[key] + "_";
            }
            roleFound = roleFound.Substring(0, roleFound.Length - 1);
        //}
        UpdatePlayerDatas(0, 0, roleFound);
    }

    public void UpdateTalent( Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("talent", PlayerData.My.GeneratePlayerTalent());
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.UpdatePlayerTalent, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    InitRoleFoundDic(playerDatas.roleFound);
                    PlayerData.My.ParsePlayerTalent(playerDatas.talent);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.UpdatePlayerTalent));
    }

    public void UpdateUnlockStatus(string unlockStatus, Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("unlockStatus", unlockStatus);
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.UpdatePlayerUnlockStatus, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    InitRoleFoundDic(playerDatas.roleFound);
                    PlayerData.My.ParsePlayerTalent(playerDatas.talent);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.UpdatePlayerUnlockStatus));
    }

    public bool needReLogin = false;
    public void ReConnect(Action doSuccess=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);

        StartCoroutine(HttpManager.My.HttpSend(Url.NewReConnUrl, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin("重连失败！");
                return;
            }
            else
            {
                playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                if (playerDatas.isOutDate)
                {
                    GoToLogin("账号失效！");
                    return;
                }
                token = playerDatas.token;
                playerLimit = playerDatas.limit;
                InitRoleFoundDic(playerDatas.roleFound);
                PlayerData.My.ParsePlayerTalent(playerDatas.talent);
                if (SceneManager.GetActiveScene().name.StartsWith("FTE")&& SceneManager.GetActiveScene().name!="FTE_0")
                {
                    StageGoal.My.CheckAfterReconnect();
                    needReLogin = true;
                }
                if(SceneManager.GetActiveScene().name == "Map")
                {
                    SceneManager.LoadScene("Map");
                }
                doSuccess?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }

    public void ConnectToMap(Action doSuccess = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);

        StartCoroutine(HttpManager.My.HttpSend(Url.NewReConnUrl, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                GoToLogin("连接失败！");
                return;
            }
            else
            {
                playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                if (playerDatas.isOutDate)
                {
                    GoToLogin("账号失效！");
                    return;
                }
                token = playerDatas.token;
                playerLimit = playerDatas.limit;
                InitRoleFoundDic(playerDatas.roleFound);
                PlayerData.My.ParsePlayerTalent(playerDatas.talent);
                SceneManager.LoadScene("Map");
                
                doSuccess?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }

    public void GetJsonDatas(Action<string> doSuccess, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        keyValues.Add("version", "new");

        StartCoroutine(HttpManager.My.HttpSend(Url.GetJsonDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    string data = CompressUtils.Uncompress(response.data);
                    doSuccess?.Invoke(data);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Source);
                    Debug.Log(ex.Message);
                    Debug.Log(ex.StackTrace);
                }
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.getJsonDatasID));
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
        if (!PlayerData.My.isSOLO)
        {
            keyValues.Add("teamID", currentBattleTeamAcount.teamID);
        }

        StartCoroutine(HttpManager.My.HttpSend(Url.Logout, (www) => {
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
        StartCoroutine(HttpManager.My.HttpSend(Url.CreatePlayerDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if(response.status == -1)
            {
                ShowReconn();
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
    /// 设置昵称和头像
    /// </summary>
    /// <param name="playerName">昵称</param>
    /// <param name="playerIcon">头像</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void SetPlayerDatas(string playerName, string playerIcon, Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerName", playerName);
        keyValues.Add("playerIcon", playerIcon);
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);

        StartCoroutine(HttpManager.My.HttpSend(Url.SetPlayerDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
        }, keyValues, HttpType.Post, HttpId.setPlayerDatasID));
    }

    /// <summary>
    /// 更新信息
    /// </summary>
    /// <param name="fteProgress">教学进度 无更新请填 0</param>
    /// <param name="threeWordsProgress">三句话进度 无更新请填 0</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void UpdatePlayerDatas(int fteProgress, int threeWordsProgress, string roleFound,Action doSuccess = null, Action doFail = null)
    {
        Debug.Log("更新fte"+fteProgress);
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("fteProgress", fteProgress.ToString());
        keyValues.Add("threeWordsProgress", threeWordsProgress.ToString());
        keyValues.Add("roleFound", roleFound);
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.UpdatePlayerDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    InitRoleFoundDic(playerDatas.roleFound);
                    PlayerData.My.ParsePlayerTalent(playerDatas.talent);
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

    public void UpdatePlayerFTE(string fte, Action doSuccess = null, Action doFail = null)
    {
        Debug.Log("更新fte" + fte);
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("fte", fte);
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.UpdatePlayerFTE, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    InitRoleFoundDic(playerDatas.roleFound);
                    PlayerData.My.ParsePlayerTalent(playerDatas.talent);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.UpdatePlayerFTE));
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

        StartCoroutine(HttpManager.My.HttpSend(Url.UploadWords, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    //Debug.Log(response.data);
                    playerDatas = JsonUtility.FromJson<PlayerDatas>(response.data);
                    InitRoleFoundDic(playerDatas.roleFound);
                    PlayerData.My.ParsePlayerTalent(playerDatas.talent);
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
        StartCoroutine(HttpManager.My.HttpSend(Url.GetAnswers, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            //Debug.Log(response.errMsg);
            if (response.status == -1)
            {
                ShowReconn();
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

    public void GetCatchLevel(Action<int> doSuccess, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        keyValues.Add("groupID", groupID.ToString());

        StartCoroutine(HttpManager.My.HttpSend(Url.GetCatchLevel, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            
            if (response.status == -1)
            {
                ShowReconn();
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                doSuccess( int.Parse(response.data));
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.getCatchLevelID));
    }
    public PlayerGroupInfo playerGroupInfo;
    public void GetPlayerGroupInfo(Action doSuccess, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        keyValues.Add("groupID", groupID.ToString());

        StartCoroutine(HttpManager.My.HttpSend(Url.GetPlayerGroupInfo, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);

            if (response.status == -1)
            {
                ShowReconn();
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                Debug.LogWarning(response.data);
                playerGroupInfo = JsonUtility.FromJson<PlayerGroupInfo>(response.data);
                doSuccess();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.GetPlayerGroupInfo));
    }
    #endregion

    #region team
    public List<TeamAcount> teamAcounts = new List<TeamAcount>();
    public TeamAcount currentBattleTeamAcount;
    public TeamAcount onTeam;
    public bool isOnTeamBattle = false;

    public void GetPlayerTeamList(Action doSuccess, Action doFail)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);

        StartCoroutine(HttpManager.My.HttpSend(Url.GetTeamAcounts, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);

            if (response.status == -1)
            {
                ShowReconn();
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                TeamAcounts tas = JsonUtility.FromJson<TeamAcounts>(response.data);
                teamAcounts.Clear();
                for(int i = 0; i< tas.teamAcounts.Count; i++)
                {
                    //if(tas.teamAcounts[i].isDisbanded == false)
                    //{
                    //    onTeam = tas.teamAcounts[i];
                    //}
                    teamAcounts.Add(tas.teamAcounts[i]);
                }
                doSuccess();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.GetTeamAcounts));
    }

    public void BuildTeam(string teamName, List<PlayerDatas> playerDatas,Action doSuccess=null, Action doFail=null)
    {
        TeamPlayers teamPlayers = new TeamPlayers(playerDatas);
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        keyValues.Add("groupID", groupID.ToString());
        keyValues.Add("teamName", teamName);
        keyValues.Add("playerIDs", teamPlayers.playerIDs);
        keyValues.Add("playerNames", teamPlayers.playerNames);

        StartCoroutine(HttpManager.My.HttpSend(Url.CreateTeamAcount, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);

            if (response.status == -1)
            {
                ShowReconn();
                return;
            }
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                currentBattleTeamAcount = JsonUtility.FromJson<TeamAcount>(response.data);
                ChangeTeamAcountList(currentBattleTeamAcount);
                isOnTeamBattle = true;
                doSuccess();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.CreateTeamAcount));
    }

    private void ChangeTeamAcountList(TeamAcount ta)
    {
        for(int i=0; i<teamAcounts.Count; i++)
        {
            if (teamAcounts[i].teamID == ta.teamID)
            {
                teamAcounts.RemoveAt(i);
                break;
            }
        }
        teamAcounts.Add(ta);
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
        StartCoroutine(HttpManager.My.HttpSend(Url.UpdateLevelProgress, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
        StartCoroutine(HttpManager.My.HttpSend(Url.GetLevelProgress, (www) =>
        {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                    Debug.LogWarning(levelProgresses.levelProgresses.Count+"-------------------");
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
                    Debug.Log(ex.StackTrace);
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

        StartCoroutine(HttpManager.My.HttpSend(Url.AddLevelRecord, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if(response.status == 0)
            {
                Debug.LogWarning(response.errMsg);
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }
    #endregion
    public TeamConfiguration teamConfiguration;
    public bool isServer = false;
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
            keyValues.Add("talent", PlayerData.My.GeneratePlayerTalentReview());
            if (!PlayerData.My.isSOLO)
            {
                if(PlayerData.My.isServer)// is server
                {
                    keyValues.Add("teamID", currentBattleTeamAcount.teamID);
                    keyValues.Add("teamName", currentBattleTeamAcount.teamName);
                    keyValues.Add("teamConfiguration", "");
                }
                else
                {
                    return;
                }
            }

            StartCoroutine(HttpManager.My.HttpSend(Url.AddReplayData, (www) => {
                HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
                if (response.status == -1)
                {
                    ShowReconn();
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

        StartCoroutine(HttpManager.My.HttpSend( Url.GetReplayDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
    /// 获取某关的行为数据
    /// </summary>
    /// <param name="recordID">记录id（从复盘list里获取）</param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetBehaviorDatas(string recordID, Action<BehaviourData> doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("recordID", recordID);

        StartCoroutine(HttpManager.My.HttpSend(Url.GetBehaviorDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
                return;
            }
            if (response.status == 1)
            {
                string json = CompressUtils.Uncompress(response.data);
                BehaviourData datas = JsonUtility.FromJson<BehaviourData>(json);
                //Debug.Log(datas.recordID);
                //Debug.Log(datas.behaviors);
                doSuccess?.Invoke(datas);
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.getBehaviorDatasID));

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

        StartCoroutine(HttpManager.My.HttpSend(Url.GetReplayLists, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
    /// 获取小组排行榜
    /// </summary>
    public void GetGroupRankingList(string sceneName, int page, Action<List<RankList>> doSuccess)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("sceneName", sceneName);
        keyValues.Add("page", page.ToString());
        keyValues.Add("groupID", groupID.ToString());
        currentGroupPage = page;

        StartCoroutine(HttpManager.My.HttpSend(Url.GetGroupRankingList, (www) => {

            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
                return;
            }

            if (response.status == 1)
            {
                try
                {
                    //Debug.Log(response.data);
                    groupList = JsonUtility.FromJson<GroupRankList>(response.data);
                    doSuccess?.Invoke(groupList.rankLists);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.TargetSite);
                    Debug.Log(ex.Message);
                }
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                //doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.getGroupRankingID));
    }

    /// <summary>
    /// 获取全球排行榜
    /// </summary>
    public void GetGlobalRankingList(string sceneName,int page, Action<List<RankList>> doSuccess)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("sceneName", sceneName);
        keyValues.Add("page", page.ToString());
        currentGlobalPage = page;

        StartCoroutine(HttpManager.My.HttpSend(Url.GetGlobalRankingList, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
                return;
            }

            if (response.status == 1)
            {
                try
                {
                    //Debug.Log(response.data);
                    globalList = JsonUtility.FromJson<GlobalRankList>(response.data);
                    doSuccess?.Invoke(globalList.rankLists);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.TargetSite);
                    Debug.Log(ex.Message);
                }
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                //doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.getGlobalRankingID));
    }

    public void GetGlobalRank(string sceneName, Action doSuccess)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("sceneName", sceneName);

        StartCoroutine(HttpManager.My.HttpSend(Url.GetPlayerGlobalRanking, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
                return;
            }

            if (response.status == 1)
            {
                try
                {
                    //Debug.Log(response.data);
                    globalRank = JsonUtility.FromJson<GlobalRankList>(response.data);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.TargetSite);
                    Debug.Log(ex.Message);
                }
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                //doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.getplayerGlobalRankingID));
    }

    public void GetGroupRank(string sceneName, Action doSuccess)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("sceneName", sceneName);
        keyValues.Add("groupID", groupID.ToString());

        StartCoroutine(HttpManager.My.HttpSend(Url.GetPlayerGroupRanking, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
                return;
            }

            if (response.status == 1)
            {
                try
                {
                    //Debug.Log(response.data);
                    groupRank = JsonUtility.FromJson<GroupRankList>(response.data);
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.TargetSite);
                    Debug.Log(ex.Message);
                }
            }
            else
            {
                HttpManager.My.ShowTip(response.errMsg);
                //doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.getplayerGroupRankingID));
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

    #region match
    public void AddScore(int score, int bossLevel, bool isEnd, Action doSuccess=null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        //keyValues.Add("groupID", groupID.ToString());
        PlayerRTScore prt = new PlayerRTScore();
        prt.playerID = playerID;
        prt.playerName = playerDatas.playerName;
        prt.groupID = groupID;
        prt.score = score;
        prt.bossLevel = bossLevel;
        prt.isGameEnd = isEnd;
        keyValues.Add("playerScore", JsonUtility.ToJson(prt).ToString());

        StartCoroutine(HttpManager.My.HttpSend(Url.AddPlayerScore, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);

            if (response.status == -1)
            {
                NewCanvasUI.My.GamePause();
                ShowReconn();
                return;
            }
            
            if (response.status == 0)
            {
                Debug.LogWarning(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                if (response.status == 2)
                {
                    stopMatch = true;
                    Debug.LogWarning("停止计分！");
                }
                else
                {
                    Debug.LogWarning(response.data);
                }
                doSuccess?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.AddPlayerScore));
    }
    public List<PlayerRTScore> playerRTScores = new List<PlayerRTScore>();
    public bool stopMatch = false;
    public void GetGroupRTScore( Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        
        keyValues.Add("groupID", groupID.ToString());
        Debug.LogWarning("groupID "+groupID);
        StartCoroutine(HttpManager.My.HttpSend(Url.GetGroupPlayerScore, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);

            if (response.status == -1)
            {
                ShowReconn();
                return;
            }

            if (response.status == 0)
            {
                Debug.LogWarning(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                if (response.status == 2)
                {
                    stopMatch = true;
                    Debug.LogWarning("停止计分！");
                    Debug.LogWarning(response.data);
                    PlayerRTScoreList prts = JsonUtility.FromJson<PlayerRTScoreList>(response.data);
                    playerRTScores.Clear();
                    for (int i = 0; i < prts.prts.Count; i++)
                    {
                        playerRTScores.Add(prts.prts[i]);
                    }
                    playerRTScores.Sort((x, y) => {
                        if (x.bossLevel == y.bossLevel)
                        {
                            return -x.score.CompareTo(y.score);
                        }
                        else
                        {
                            return -x.bossLevel.CompareTo(y.bossLevel);
                        }
                    });
                }
                else
                {
                    Debug.LogWarning(response.data);
                    PlayerRTScoreList prts = JsonUtility.FromJson<PlayerRTScoreList>(response.data);
                    playerRTScores.Clear();
                    for(int i=0; i< prts.prts.Count; i++)
                    {
                        playerRTScores.Add(prts.prts[i]);
                    }
                    playerRTScores.Sort((x, y) => {
                        if (x.bossLevel == y.bossLevel)
                        {
                            return -x.score.CompareTo(y.score);
                        }
                        else
                        {
                            return -x.bossLevel.CompareTo(y.bossLevel);
                        }
                    });
                }
                    doSuccess?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Get, HttpId.GetGroupPlayerScore));
    }

    public void GetGroupScoreStatus( Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        
        keyValues.Add("groupID", groupID.ToString());

        StartCoroutine(HttpManager.My.HttpSend(Url.GetGroupScoreStatus, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);

            if (response.status == 0)
            {
                Debug.LogWarning(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                if (response.status == 1)
                {
                    Debug.LogWarning("计分中...");
                    stopMatch = false;
                }
                else
                {
                    Debug.LogWarning("计分停止...");
                    stopMatch = true;
                }
                doSuccess?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post, HttpId.AddPlayerScore));
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

        StartCoroutine(HttpManager.My.HttpSend(Url.GetEquips, (www) => {
            HttpResponse httpResponse = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (httpResponse.status == -1)
            {
                ShowReconn();
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
    public string poorPlayerID = "";
    public void GetPoorPlayerEquips(Action<List<PlayerEquip>> doSuccess = null, Action doFail = null)
    {
        if (poorPlayerID.Equals(""))
        {
            doSuccess(new List<PlayerEquip>());
            return;
        }
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("token", token);
        keyValues.Add("playerID", playerID);
        keyValues.Add("poorPlayerID", poorPlayerID);

        StartCoroutine(HttpManager.My.HttpSend(Url.GetEquips, (www) => {
            HttpResponse httpResponse = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (httpResponse.status == -1)
            {
                ShowReconn();
                return;
            }
            if (httpResponse.status == 1)
            {
                playerEquips = JsonUtility.FromJson<PlayerEquips>(httpResponse.data);
                playerEquipsList.Clear();
                foreach (var pe in playerEquips.playerEquips)
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

        StartCoroutine(HttpManager.My.HttpSend(Url.AddEquip, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
                Debug.LogWarning("获得装备 " + pe.equipID);
                doSuccess?.Invoke();
            }
            else
            {
                Debug.LogError("get equip fail");
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

        StartCoroutine(HttpManager.My.HttpSend(Url.AddEquips, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == -1)
            {
                ShowReconn();
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
       
        HttpManager.My.ShowClickTip(tip, () => {
            if(SceneManager.GetActiveScene().name!="Login")
                Logout();
            SceneManager.LoadScene("Login"); });
    }

    private void ShowReconn(string str=null)
    {
        HttpManager.My.ShowReConn(str);
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

        StartCoroutine(HttpManager.My.HttpSend(Url.TestPost, (www)=> {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            Debug.Log(response.data);
        }, keyValues, HttpType.Post));
    }

    public void TestGet(Action<string> action)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        

        StartCoroutine(HttpManager.My.HttpSend(Url.TestGet, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            Debug.Log(response.data);
            action(response.data);
        }, keyValues, HttpType.Get));
    }
    #endregion

    private void OnApplicationQuit()
    {
        if(isUsingHttp&&playerID!=null)
            Logout();
    }
}

