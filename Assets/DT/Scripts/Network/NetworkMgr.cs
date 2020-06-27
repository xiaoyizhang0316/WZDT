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

    private PlayerEquip playerEquip;
    public PlayerEquips playerEquips;
    public List<PlayerEquip> playerEquipsList;
    #endregion

    private void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        levelProgressList = new List<LevelProgress>();
        playerEquipsList = new List<PlayerEquip>();
    }

    #region login
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
                    Debug.Log("token-----" + token);
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

    public void CheckDevice()
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("deviceID", deviceID);
        keyValues.Add("token", token);

        StartCoroutine(HttpManager.My.HttpSend(Url.checkDeviceID, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowClickTip(response.errMsg, ()=> {
                    SceneManager.LoadScene("Login");
                });
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }

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
        }, keyValues, HttpType.Post));
    }
    #endregion

    #region three words
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
        }, keyValues, HttpType.Post));
    }

    public void GetAnswers(Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("token", token);
        StartCoroutine(HttpManager.My.HttpSend(Url.getAnswers, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            Debug.Log(response.errMsg);
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
        }, keyValues, HttpType.Post));
    }
    #endregion

    #region Level
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
                Debug.Log(response.data);
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
        }, keyValues, HttpType.Post));
    }

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

    #region equip
    /// <summary>
    /// 获取所有装备
    /// </summary>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void GetPlayerEquips(Action doSuccess=null, Action doFail=null)
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
                doSuccess?.Invoke();
            }
            else
            {
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post));
    }

    /// <summary>
    /// 获得装备
    /// </summary>
    /// <param name="equipID">装备id</param>
    /// <param name="equipType">装备类型</param>
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
                for(int i = 0; i < playerEquipsList.Count; i++)
                {
                    if(playerEquipsList[i].equipID == playerEquip.equipID && playerEquipsList[i].equipType == playerEquip.equipType)
                    {
                        playerEquipsList.RemoveAt(i);
                        break;
                    }
                }
                playerEquipsList.Add(playerEquip);
                doSuccess?.Invoke();
            }
            else
            {
                doFail?.Invoke();
            }
            SetMask();
        }, keyValues, HttpType.Post));
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
