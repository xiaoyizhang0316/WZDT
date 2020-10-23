using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminManager : MonoSingleton<AdminManager>
{
    public int lastGroupID = -1;
    public int currentGroupID = -1;

    public int passDetailGroupID = -1;
    public int passDetailLevelID = -1;

    public GameObject groupData;
    public Transform groupContent;

    public Transform groupMoreInfos;
    public Transform totalPanel;

    #region UI
    public Text playCount;
    public Text winCount;
    public Text totalTimes;
    public Text winRates;
    public Text perTime;

    public Button refresh;
    public Text lastRefreshTimeText;

    public Button exit_btn;
    #endregion

    #region datas
    private int lastRefreshTime = 0;
    static int refreshInterval = 10;

    int timeCount = 0;
    public List<PlayerGroup> playerGroups = new List<PlayerGroup>();

    public TotalPlayCount totalPlayCount;

    public PlayerDatas playerDatas;

    public List<LevelPlayCount> levelPlayCounts = new List<LevelPlayCount>();

    public List<GroupTotalPlayCount> groupTotalPlayCounts = new List<GroupTotalPlayCount>();

    public List<GroupLevelPlayCount> playerLevelPlayCounts = new List<GroupLevelPlayCount>();

    public List<GroupPlayerLevelPlayCount> playerPlayCount = new List<GroupPlayerLevelPlayCount>();

    public List<GroupPlayerLevelPlayCount> playerWinCount = new List<GroupPlayerLevelPlayCount>();

    public List<GroupPlayerLevelPlayCount> playerTimeCount = new List<GroupPlayerLevelPlayCount>();

    public List<GroupLevelPassDetail> levelPassDetails = new List<GroupLevelPassDetail>();

    public List<LevelPass> levelPasses = new List<LevelPass>();

    public List<PlayerThreeWord> groupThreeWords = new List<PlayerThreeWord>();

    public List<PlayerOnlineStatus> playerOnlineStatuses = new List<PlayerOnlineStatus>();

    public Dictionary<string, GroupPlayer> tryGroupPlayers = new Dictionary<string, GroupPlayer>();

    public Dictionary<string,GroupPlayer> groupPlayers = new Dictionary<string, GroupPlayer>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //ShowGroupsAndTotalData();

        InitRefreshButton();
        exit_btn.onClick.AddListener(Exit);
    }

    /// <summary>
    /// 显示管理员可以获得的信息
    /// </summary>
    public void ShowAdmin()
    {
        totalPanel.gameObject.SetActive(true);
        refresh.gameObject.SetActive(true);
        exit_btn.gameObject.SetActive(true);
        transform.GetComponent<Image>().raycastTarget = true;
        ShowGroupsAndTotalData();
    }

    /// <summary>
    /// 初始化刷新按钮
    /// </summary>
    private void InitRefreshButton()
    {
        //refresh.gameObject.SetActive(true);
        refresh.onClick.AddListener(Refresh);
        lastRefreshTime = PlayerPrefs.GetInt("lastRefreshTime", 0);
        lastRefreshTimeText.text = lastRefreshTime == 0 ? "" : TimeStamp.TimeStampToDateString(lastRefreshTime);
        if (lastRefreshTime == 0)
        {
            lastRefreshTimeText.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            lastRefreshTimeText.transform.GetChild(0).gameObject.SetActive(true);
        }
        int interval = TimeStamp.GetCurrentTimeStamp() - lastRefreshTime;
        if (lastRefreshTime == 0)
        {
            refresh.interactable = true;
        }
        else
        {
            if (interval >= refreshInterval)
            {
                refresh.interactable = true;
            }
            else
            {
                refresh.interactable = false;
                TimeCountDown(interval);
            }
        }
    }

    /// <summary>
    /// 倒计时
    /// </summary>
    /// <param name="interval"></param>
    void TimeCountDown(int interval)
    {
        timeCount = interval+1;
        InvokeRepeating("CountDown",0,1);
    }

    /// <summary>
    /// 倒计时
    /// </summary>
    void CountDown()
    {
        timeCount -= 1;
        if (timeCount <= 0)
        {
            refresh.transform.GetChild(0).GetComponent<Text>().text = "刷新";
            refresh.interactable = true;
            CancelInvoke("CountDown");
        }
        else
        {
            refresh.transform.GetChild(0).GetComponent<Text>().text = timeCount+"s";
        }
    }

    #region 显示UI
    /// <summary>
    /// 显示次一级权限的信息
    /// </summary>
    public void ShowGroupInTeacherPrivilage()
    {
        refresh.gameObject.SetActive(true);
        exit_btn.gameObject.SetActive(true);
        transform.GetComponent<Image>().raycastTarget = true;
        GetPlayerGroups(() => GetGroupTotalPlayCount(() => {
            ClickShowGroupMoreInfos(playerDatas.groupID);
        }));
    }

    /// <summary>
    /// 显示总体数据
    /// </summary>
    private void ShowGroupsAndTotalData()
    {
        GetTotalPlayCount(()=> {
            ShowTotalPlayData();
        });
        GetPlayerGroups(()=> GetGroupTotalPlayCount(()=> {
            LoadAllGroupData();
            
        }));
    }

    /// <summary>
    /// 点击加载小组详细信息
    /// </summary>
    /// <param name="groupID"></param>
    public void ClickShowGroupMoreInfos(int groupID)
    {
        if (groupID!=lastGroupID)
        {
            lastGroupID = groupID;
            GetGroupLevelData(groupID, () => {
                //ShowGroupTotalInfos(GetGroupTotalPlayCountByGroupID(groupID));
                GetGroupPlayerLevelPlayCount(groupID,() => { GetGroupLevelPass(groupID,()=> ShowGroupTotalInfos(GetGroupTotalPlayCountByGroupID(groupID))); });
                GetGroupPlayerLevelWinCount(groupID, () => { });
                GetGroupPlayerLevelTimeCount(groupID, () => { });
            });
        }
        else
        {
            ShowGroupTotalInfos(GetGroupTotalPlayCountByGroupID(groupID));
        }
    }

    /// <summary>
    /// 显示某小组的详细信息
    /// </summary>
    /// <param name="gtpc"></param>
    private void ShowGroupTotalInfos(GroupTotalPlayCount gtpc)
    {
        groupMoreInfos.gameObject.SetActive(true);
        groupMoreInfos.GetComponent<GroupInfos>().ShowInfos(gtpc);
    }


    /// <summary>
    /// 显示总体数据
    /// </summary>
    private void ShowTotalPlayData()
    {
        playCount.text = totalPlayCount.total.ToString();
        winCount.text = totalPlayCount.win.ToString();
        //totalTimes.text = (totalPlayCount.times / 3600.0).ToString("F2") + "h";
        totalTimes.text = GetTimeString(totalPlayCount.times);
        winRates.text = (((float)totalPlayCount.win / totalPlayCount.total) * 100).ToString("F2") + "%";
        //perTime.text = (totalPlayCount.times * 1.0 / totalPlayCount.total / 60).ToString("F2") + "m";
        perTime.text = GetTimeString(totalPlayCount.times / totalPlayCount.total);
    }

    /// <summary>
    /// 加载所有组的信息
    /// </summary>
    private void LoadAllGroupData()
    {
        for(int i=0; i<playerGroups.Count; i++)
        {
            GameObject go = Instantiate(groupData, groupContent);
            GroupSample gs = go.GetComponent<GroupSample>();
            gs.Setup(playerGroups[i], GetGroupTotalPlayCountByGroupID(playerGroups[i].groupID));
        }
    }

    /// <summary>
    /// 清除
    /// </summary>
    private void ClearContent()
    {
        //for(int i=0; i<groupContent.childCount; i++)
        //{
        //    Destroy(groupContent.GetChild(i).gameObject);
        //}

        foreach (Transform child in groupContent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 刷新
    /// </summary>
    private void Refresh()
    {
        refresh.interactable = false;
        passDetailLevelID = -1;
        lastRefreshTime = TimeStamp.GetCurrentTimeStamp();
        lastRefreshTimeText.text = TimeStamp.TimeStampToDateString(lastRefreshTime);
        PlayerPrefs.SetInt("lastRefreshTime", lastRefreshTime);
        TimeCountDown(refreshInterval);
        ClearContent();
        ShowGroupsAndTotalData();

        if (groupMoreInfos.gameObject.activeInHierarchy)
        {
            GetGroupLevelData(lastGroupID, () => {
                //ShowGroupTotalInfos(GetGroupTotalPlayCountByGroupID(groupID));
                GetGroupPlayerLevelPlayCount(lastGroupID, () => { GetGroupLevelPass(lastGroupID, () => ShowGroupTotalInfos(GetGroupTotalPlayCountByGroupID(lastGroupID))); });
                GetGroupPlayerLevelWinCount(lastGroupID, () => { });
                GetGroupPlayerLevelTimeCount(lastGroupID, () => { });
            });
        }
        else
        {
            if (lastGroupID != -1)
            {
                GetGroupLevelData(lastGroupID, () => {
                    //ShowGroupTotalInfos(GetGroupTotalPlayCountByGroupID(lastGroupID));
                    GetGroupPlayerLevelPlayCount(lastGroupID, () => { });
                    GetGroupPlayerLevelWinCount(lastGroupID, () => { });
                    GetGroupPlayerLevelTimeCount(lastGroupID, () => { });
                });
            }
        }
    }

    /// <summary>
    /// 退出
    /// </summary>
    private void Exit()
    {
        //Application.Quit();
        totalPanel.gameObject.SetActive(false);
        groupMoreInfos.gameObject.SetActive(false);
        transform.GetComponent<Image>().raycastTarget = false;
        refresh.gameObject.SetActive(false);
        exit_btn.gameObject.SetActive(false);
    }
    #endregion

    #region 处理数据
    /// <summary>
    /// 获取小组总体数据
    /// </summary>
    /// <param name="groupID"></param>
    /// <returns></returns>
    public GroupTotalPlayCount GetGroupTotalPlayCountByGroupID(int groupID)
    {
        for(int i=0;i<groupTotalPlayCounts.Count; i++)
        {
            if (groupTotalPlayCounts[i].groupID == groupID)
            {
                return groupTotalPlayCounts[i];
            }
        }
        return null;
    }

    /// <summary>
    ///  获取组员信息
    /// </summary>
    /// <param name="groupID"></param>
    /// <returns></returns>
    public PlayerGroup GetPlayerGroupByGroupID(int groupID)
    {
        for(int i = 0; i< playerGroups.Count; i++)
        {
            if (playerGroups[i].groupID == groupID)
            {
                return playerGroups[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 时间（s）转时分秒显示
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public string GetTimeString(int time)
    {
        int hour = 0;
        int minute = 0;
        int second = 0;

        hour = time / 3600;
        if (hour > 0)
        {
            minute = (time - 3600 * hour) / 60;
            second = (time - 3600 * hour) % 60;
            return (hour < 10 ? "0" + hour : hour.ToString()) + ":" + (minute < 10 ? "0" + minute : minute.ToString()) + ":" + (second < 10 ? "0" + second : second.ToString());
        }
        else
        {
            minute = time / 60;
            second = time % 60;
            return  (minute < 10 ? "0" + minute : minute.ToString()) + ":" + (second < 10 ? "0" + second : second.ToString());
        }

    }
    #endregion

    #region 获取后台数据
    /// <summary>
    /// 管理员登录
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="doSuccess"></param>
    /// <param name="doFail"></param>
    public void Login(string userName, string password, Action doSuccess = null, Action doFail = null)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("username", userName);
        keyValues.Add("password", password);

        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.adminLogin, (www) => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.data);
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
        }, keyValues, HttpType.Post, 10010));
    }
    /// <summary>
    /// 获取分组列表
    /// </summary>
    private void GetPlayerGroups(Action doEnd)
    {
        if (playerGroups.Count > 0)
        {
            doEnd?.Invoke();
            return;
        }
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();

        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getPlayerGroups, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                PlayerGroups groups = JsonUtility.FromJson<PlayerGroups>(response.data);
                for(int i=0; i<groups.playerGroups.Count; i++)
                {
                    playerGroups.Add(groups.playerGroups[i]);
                }
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10001));
    }

    /// <summary>
    /// 获取整体数据
    /// </summary>
    private void GetTotalPlayCount(Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();

        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getTotalPlayCount, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                totalPlayCount = JsonUtility.FromJson<TotalPlayCount>(response.data);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10002));
    }

    /// <summary>
    /// 获取每关的数据
    /// </summary>
    private void GetLevelPlayCount(Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();

        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getLevelPlayCount, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                LevelPlayCounts groups = JsonUtility.FromJson<LevelPlayCounts>(response.data);
                levelPlayCounts.Clear();
                for (int i = 0; i < groups.levelPlayCounts.Count; i++)
                {
                    levelPlayCounts.Add(groups.levelPlayCounts[i]);
                }
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10003));
    }

    /// <summary>
    /// 获取各小组的整体数据
    /// </summary>
    private void GetGroupTotalPlayCount(Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();

        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupTotalPlayCount, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupTotalPlayCounts groups = JsonUtility.FromJson<GroupTotalPlayCounts>(response.data);
                groupTotalPlayCounts.Clear();
                for (int i = 0; i < groups.groupTotalPlayCounts.Count; i++)
                {
                    groupTotalPlayCounts.Add(groups.groupTotalPlayCounts[i]);
                }
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10004));
    }

    /// <summary>
    /// 获取小组的每关的整体数据
    /// </summary>
    /// <param name="groupID"></param>
    private void GetGroupLevelData(int groupID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", groupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupLevelData, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupLevelPlayCounts groups = JsonUtility.FromJson<GroupLevelPlayCounts>(response.data);
                playerLevelPlayCounts.Clear();
                for (int i = 0; i < groups.groupTotalPlayCount.Count; i++)
                {
                    playerLevelPlayCounts.Add(groups.groupTotalPlayCount[i]);
                }
                Debug.Log(playerLevelPlayCounts.Count);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10005));
    }

    /// <summary>
    /// 获取小组的每关的整体数据
    /// </summary>
    /// <param name="groupID"></param>
    private void GetGroupLevelPass(int groupID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", groupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupLevelPass, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                LevelPasses lp = JsonUtility.FromJson<LevelPasses>(response.data);
                levelPasses.Clear();
                for (int i = 0; i < lp.levelPasses.Count; i++)
                {
                    levelPasses.Add(lp.levelPasses[i]);
                }
                Debug.Log(levelPasses.Count);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10009));
    }

    /// <summary>
    /// 获取小组用户的每关的游玩次数
    /// </summary>
    /// <param name="groupID"></param>
    private void GetGroupPlayerLevelPlayCount(int groupID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", groupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupPlayerLevelPlayCount, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupPlayerLevelPlayCounts groups = JsonUtility.FromJson<GroupPlayerLevelPlayCounts>(response.data);
                playerPlayCount.Clear();
                groupPlayers.Clear();
                for (int i = 0; i < groups.groupPlayerLevelPlayCounts.Count; i++)
                {
                    playerPlayCount.Add(groups.groupPlayerLevelPlayCounts[i]);
                }
                
                doEnd?.Invoke();
                for(int i = 0; i< playerPlayCount.Count; i++)
                {
                    groupPlayers.Add(playerPlayCount[i].playerID,new GroupPlayer(playerPlayCount[i].playerID, playerPlayCount[i].playerName));
                }
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10006));
    }

    /// <summary>
    /// 获取小组用户的每关的胜利的次数
    /// </summary>
    /// <param name="groupID"></param>
    private void GetGroupPlayerLevelWinCount(int groupID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", groupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupPlayerLevelWinCount, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupPlayerLevelPlayCounts groups = JsonUtility.FromJson<GroupPlayerLevelPlayCounts>(response.data);
                playerWinCount.Clear();
                for (int i = 0; i < groups.groupPlayerLevelPlayCounts.Count; i++)
                {
                    playerWinCount.Add(groups.groupPlayerLevelPlayCounts[i]);
                }
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10007));
    }

    /// <summary>
    /// 获取小组用户的每关的游玩时间
    /// </summary>
    /// <param name="groupID"></param>
    private void GetGroupPlayerLevelTimeCount(int groupID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", groupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupPlayerLevelTimeCount, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupPlayerLevelPlayCounts groups = JsonUtility.FromJson<GroupPlayerLevelPlayCounts>(response.data);
                playerTimeCount.Clear();
                for (int i = 0; i < groups.groupPlayerLevelPlayCounts.Count; i++)
                {
                    playerTimeCount.Add(groups.groupPlayerLevelPlayCounts[i]);
                }
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10008));
    }

    /// <summary>
    /// 获取小组某关通过的详细信息
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="doEnd"></param>
    public void GetGroupLevelPassDetail( int levelID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", lastGroupID.ToString());
        keyValues.Add("levelID", levelID.ToString());
        passDetailGroupID = lastGroupID;
        passDetailLevelID = levelID;
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupLevelPassDetail, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupLevelPassDetails lp = JsonUtility.FromJson<GroupLevelPassDetails>(response.data);
                levelPassDetails.Clear();
                for (int i = 0; i < lp.groupLevelPassDetails.Count; i++)
                {
                    levelPassDetails.Add(lp.groupLevelPassDetails[i]);
                }
                Debug.Log(levelPassDetails.Count);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10012));
    }

    /// <summary>
    /// 获取小组玩过某关卡的组员
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="doEnd"></param>
    public void GetGroupTryLevelPassDetail(string levelID, Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", lastGroupID.ToString());
        keyValues.Add("levelID", levelID);
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupTryLevelPassDetail, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                GroupPlayers lp = JsonUtility.FromJson<GroupPlayers>(response.data);
                tryGroupPlayers.Clear();
                for (int i = 0; i < lp.groupPlayer.Count; i++)
                {
                    tryGroupPlayers.Add(lp.groupPlayer[i].playerID, lp.groupPlayer[i]);
                }
                Debug.Log(tryGroupPlayers.Count);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10013));
    }

    public void GetGroupPlayerThreeWord( Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("groupID", lastGroupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupPlayerThreeWords, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                PlayerThreeWordList ptw = JsonUtility.FromJson<PlayerThreeWordList>(response.data);
                groupThreeWords.Clear();
                for (int i = 0; i < ptw.playerThreeWords.Count; i++)
                {
                    groupThreeWords.Add(ptw.playerThreeWords[i]);
                }
                Debug.Log(groupThreeWords.Count);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10014));
    }

    public void GetGroupPlayerStatus( Action doEnd)
    {
        SortedDictionary<string, string> keyValues = new SortedDictionary<string, string>();
        keyValues.Add("levelID", playerDatas.levelID.ToString());
        keyValues.Add("groupID", lastGroupID.ToString());
        StartCoroutine(HttpManager.My.HttpSend(AdminUrls.getGroupPlayerStatus, www => {
            HttpResponse response = JsonUtility.FromJson<HttpResponse>(www.downloadHandler.text);
            if (response.status == 0)
            {
                HttpManager.My.ShowTip(response.errMsg);
            }
            else
            {
                PlayerOnlineStatuses pos = JsonUtility.FromJson<PlayerOnlineStatuses>(response.data);
                playerOnlineStatuses.Clear();
                for (int i = 0; i < pos.playerStatuses.Count; i++)
                {
                    playerOnlineStatuses.Add(pos.playerStatuses[i]);
                }
                Debug.Log(playerOnlineStatuses.Count);
                doEnd?.Invoke();
            }
            HttpManager.My.mask.SetActive(false);
        }, keyValues, HttpType.Get, 10015));
    }
    #endregion
}
