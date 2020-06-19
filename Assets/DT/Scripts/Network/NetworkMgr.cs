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
    public string deviceID;
    public string playerID;
    public PlayerDatas playerDatas;

    public Answers answers;
    public string currentAnswer = "";

    public LevelProgress levelProgress;
    public LevelProgresses levelProgresses;
    public List<LevelProgress> levelProgressList;
    #endregion

    private void Start()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        levelProgressList = new List<LevelProgress>();
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
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }

    public void CheckDevice()
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("deviceID", deviceID);

        StartCoroutine(HttpManager.My.HttpSend(Url.checkDeviceID, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowClickTip(response.errMsg, ()=> {
                    SceneManager.LoadScene("Login");
                });
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }

    public void Logout(Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        keyValues.Add("recordID", loginRecordID);

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
        StartCoroutine(HttpManager.My.HttpSend(Url.createPlayerDatas, (www) => {
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
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }

    public void UpdatePlayerDatas(int fteProgress, int threeWordsProgress,Action doSuccess = null, Action doFail = null)
    {
        Debug.Log("更新fte"+fteProgress);
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("fteProgress", fteProgress.ToString());
        keyValues.Add("threeWordsProgress", threeWordsProgress.ToString());
        keyValues.Add("playerID", playerID);
        StartCoroutine(HttpManager.My.HttpSend(Url.updatePlayerDatas, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
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
            HttpManager.My.mask.SetActive(false);
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

        StartCoroutine(HttpManager.My.HttpSend(Url.uploadWords, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
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
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }

    public void GetAnswers(Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        StartCoroutine(HttpManager.My.HttpSend(Url.getAnswers, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            Debug.Log(response.errMsg);
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
                    answers = JsonUtility.FromJson<Answers>(response.data);
                    Debug.LogError(answers.answer1);
                    currentAnswer = answers.GetCurrentAnswer(playerDatas.threeWordsProgress);
                    Debug.LogError(currentAnswer);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
                doSuccess();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }
    #endregion

    #region Level
    public void UpdateLevelProgress(int levelID, string levelStar, string rewardStatus, int score, Action doSuccess=null, Action doFail=null)
    {
        LevelProgress levelProgress = new LevelProgress(playerID, levelID, levelStar, rewardStatus, score);

        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("levelProgress", JsonUtility.ToJson(levelProgress));
        StartCoroutine(HttpManager.My.HttpSend(Url.updateLevelProgress, (www) => {
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
                    levelProgress = JsonUtility.FromJson<LevelProgress>(response.data);
                    foreach (var lp in levelProgressList)
                    {
                        if (levelProgress.levelID == lp.levelID)
                        {
                            levelProgressList.Remove(lp);
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
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }

    public void GetLevelProgress(Action doSuccess=null, Action doFail=null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("playerID", playerID);
        StartCoroutine(HttpManager.My.HttpSend(Url.getLevelProgress, (www) =>
        {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                //HttpManager.My.ShowTip(response.errMsg);
                doFail?.Invoke();
            }
            else
            {
                //Debug.Log(response.data);
                try
                {
                    levelProgresses = JsonUtility.FromJson<LevelProgresses>(response.data);
                    levelProgressList.Clear();
                    foreach (var lp in levelProgresses.levelProgresses)
                    {
                        levelProgressList.Add(lp);
                    }
                    doSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Post));
    }
    #endregion

    private void OnApplicationQuit()
    {
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
