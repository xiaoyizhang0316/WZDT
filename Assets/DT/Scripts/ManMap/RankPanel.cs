using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine.UI;

public class RankPanel : MonoSingleton<RankPanel>
{
    public Text title;
    public List<Sprite> rankSprites;
    public Toggle record_toggle;
    public Toggle group_toggle;
    public Toggle global_toggle;
    public Transform listContent;
    public GameObject rankPrefab;

    public GameObject playerRankObj;
    public GameObject pageButtons;
    public Button prePage;
    public Button nextPage;
    public Button refresh_btn;

    public string currentSceneName = "";
    public string lastSceneName = "";

    private int currentShowIndex = -1;
    private static int timeInterval = 10;

    //private List<ReplayList> replayLists = new List<ReplayList>();
    public List<RankList> groupList = new List<RankList>();
    public List<RankList> globalList = new List<RankList>();

    bool getRecord = false;
    bool getGroup = false;
    bool getGlobal = false;

    // Start is called before the first frame update
    void Start()
    {
        record_toggle.onValueChanged.AddListener(isOn => Record(isOn));
        group_toggle.onValueChanged.AddListener(isOn => Group(isOn));
        global_toggle.onValueChanged.AddListener(isOn => Global(isOn));

        prePage.onClick.AddListener(PrePage);
        nextPage.onClick.AddListener(NextPage);
        refresh_btn.onClick.AddListener(Refresh);
        //NetworkMgr.My.token = "fpx1gHApQ8QooMuoGNV";
        //NetworkMgr.My.playerID = "999999";
        //NetworkMgr.My.groupID = 1;
        //ShowRankPanel("FTE_1");
    }

    public void ShowRankPanel(string sceneName)
    {
        currentSceneName = sceneName;
        if (currentSceneName != lastSceneName)
        {
            groupList.Clear();
            globalList.Clear();
            currentShowIndex = -1;
            getRecord = false;
            getGroup = false;
            getGlobal = false;
            NetworkMgr.My.currentGroupPage = 0;
            NetworkMgr.My.currentGlobalPage = 0;
        }
        lastSceneName = currentSceneName;
        if (record_toggle.isOn)
        {
            Record(true);
        }
        else
        {
            record_toggle.isOn = true;
        }
    }

    void Record(bool isOn)
    {
        if (currentShowIndex == 0)
        {
            return;
        }
        HideAllList();
        title.text = "历史记录";
        currentShowIndex = 0;
        SetButton(false);
        if (isOn)
        {
            Debug.Log("record");
            SetToggleStatus(record_toggle.transform);
            if (!getRecord)
            {
                NetworkMgr.My.GetReplayLists(currentSceneName, () =>
                {
                    ShowRecordList();
                });
                getRecord = true;
            }
            else
            {
                ShowRecordList();
            }
        }
    }

    void Group(bool isOn)
    {
        if (currentShowIndex == 1)
        {
            return;
        }
        HideAllList();
        title.text = "小组排名";
        currentShowIndex = 1;
        if (isOn)
        {
            Debug.Log("group");
            SetToggleStatus(group_toggle.transform);
            if (getGroup)
            {
                ShowRankList(true);
                ShowMyRank(true);
                RefreshPageButtons(true);
                SetButton(true);
            }
            else
            {
                NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList) =>
                {

                    foreach (var g in gList)
                    {
                        groupList.Add(g);
                    }
                    if (gList.Count > 10)
                    {
                        groupList.RemoveAt(groupList.Count - 1);
                    }
                    ShowRankList(true);
                    RefreshPageButtons(true);
                    SetButton(true);
                });
                NetworkMgr.My.GetGroupRank(currentSceneName, () =>
                {
                    ShowMyRank(true);
                });
                getGroup = true;
            }
        }
        //SetButton(true);
    }

    void Global(bool isOn)
    {
        if (currentShowIndex == 2)
        {
            return;
        }
        HideAllList();
        title.text = "全球排名";
        currentShowIndex = 2;
        if (isOn)
        {
            Debug.Log("global");
            SetToggleStatus(global_toggle.transform);
            if (getGlobal)
            {
                ShowRankList(false);
                ShowMyRank(false);
                RefreshPageButtons(false);
                SetButton(true);
            }
            else
            {
                NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) =>
                {
                    foreach (var g in gList)
                    {
                        globalList.Add(g);
                    }
                    if (gList.Count > 10)
                    {
                        globalList.RemoveAt(globalList.Count - 1);
                    }
                    ShowRankList(false);
                    RefreshPageButtons(false);
                    SetButton(true);
                });
                NetworkMgr.My.GetGlobalRank(currentSceneName, () =>
                {
                    ShowMyRank(false);
                });
                getGlobal = true;
            }
        }

    }

    public void Refresh()
    {
        groupList.Clear();
        globalList.Clear();
        PlayerPrefs.SetInt("rankFreshTime", TimeStamp.GetCurrentTimeStamp());
        TimeCountDown(timeInterval - 1);
        if (currentShowIndex == 0)
        {
            HideAllList();
            title.text = "历史记录";
            currentShowIndex = 0;
            SetButton(false);
            NetworkMgr.My.currentGlobalPage = 0;
            NetworkMgr.My.currentGroupPage = 0;

            SetToggleStatus(record_toggle.transform);

            NetworkMgr.My.GetReplayLists(currentSceneName, () =>
            {
                ShowRecordList();
            });
            getRecord = true;

            NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) =>
            {
                foreach (var g in gList)
                {
                    groupList.Add(g);
                }
                if (gList.Count > 10)
                {
                    groupList.RemoveAt(groupList.Count - 1);
                }
            });
            NetworkMgr.My.GetGlobalRank(currentSceneName, () =>
            {

            });
            NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList) =>
            {
                foreach (var g in gList)
                {
                    groupList.Add(g);
                }
                if (gList.Count > 10)
                {
                    groupList.RemoveAt(groupList.Count - 1);
                }
            });
            NetworkMgr.My.GetGroupRank(currentSceneName, () =>
            {

            });
        }
        else
        if (currentShowIndex == 1)
        {
            HideAllList();
            title.text = "小组排名";
            currentShowIndex = 1;
            NetworkMgr.My.currentGlobalPage = 0;
            NetworkMgr.My.currentGroupPage = 0;
            NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList) =>
            {

                foreach (var g in gList)
                {
                    groupList.Add(g);
                }
                if (gList.Count > 10)
                {
                    groupList.RemoveAt(groupList.Count - 1);
                }
                ShowRankList(true);
                RefreshPageButtons(true);
                SetButton(true);
            });
            NetworkMgr.My.GetGroupRank(currentSceneName, () =>
            {
                ShowMyRank(true);
            });
            getGroup = true;
            NetworkMgr.My.GetReplayLists(currentSceneName, () =>
            {

            });
            NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) =>
            {
                foreach (var g in gList)
                {
                    groupList.Add(g);
                }
                if (gList.Count > 10)
                {
                    groupList.RemoveAt(groupList.Count - 1);
                }
            });
            NetworkMgr.My.GetGlobalRank(currentSceneName, () =>
            {

            });
        }
        else
        {
            HideAllList();
            title.text = "全球排名";
            currentShowIndex = 2;

            Debug.Log("global");
            SetToggleStatus(global_toggle.transform);
            NetworkMgr.My.currentGlobalPage = 0;
            NetworkMgr.My.currentGroupPage = 0;
            NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) =>
            {
                foreach (var g in gList)
                {
                    globalList.Add(g);
                }
                if (gList.Count > 10)
                {
                    globalList.RemoveAt(globalList.Count - 1);
                }
                ShowRankList(false);
                RefreshPageButtons(false);
                SetButton(true);
            });
            NetworkMgr.My.GetGlobalRank(currentSceneName, () =>
            {
                ShowMyRank(false);
            });
            getGlobal = true;

            NetworkMgr.My.GetReplayLists(currentSceneName, () =>
            {

            });

            NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList) =>
            {
                foreach (var g in gList)
                {
                    groupList.Add(g);
                }
                if (gList.Count > 10)
                {
                    groupList.RemoveAt(groupList.Count - 1);
                }
            });
            NetworkMgr.My.GetGroupRank(currentSceneName, () =>
            {

            });
        }
    }

    void PrePage()
    {
        if (currentShowIndex == 1)
        {
            NetworkMgr.My.currentGroupPage -= 1;
            ShowRankList(true);
            RefreshPageButtons(true);
        }
        else
        {
            NetworkMgr.My.currentGlobalPage -= 1;
            ShowRankList(false);
            RefreshPageButtons(false);
        }
    }

    void NextPage()
    {
        Debug.Log("next");
        if (currentShowIndex == 1)
        {
            NetworkMgr.My.currentGroupPage += 1;
            if (NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
            {
                ShowRankList(true);
                RefreshPageButtons(true);
            }
            else
            {
                NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList) =>
                {
                    Debug.Log("next from net count:" + gList.Count);
                    foreach (var g in gList)
                    {
                        groupList.Add(g);
                    }
                    if (gList.Count > 10)
                    {
                        groupList.RemoveAt(groupList.Count - 1);
                    }
                    ShowRankList(true);
                    RefreshPageButtons(true);
                });
            }
        }
        else
        {
            NetworkMgr.My.currentGlobalPage += 1;
            if (NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
            {
                ShowRankList(false);
                RefreshPageButtons(false);
            }
            else
            {
                NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) =>
                {
                    foreach (var g in gList)
                    {
                        globalList.Add(g);
                    }
                    if (gList.Count > 10)
                    {
                        globalList.RemoveAt(globalList.Count - 1);
                    }
                    ShowRankList(false);
                    RefreshPageButtons(false);
                });
            }
        }
    }

    void ShowMyRank(bool isGroup)
    {
        if (isGroup)
        {
            if (NetworkMgr.My.groupRank.rankLists.Count == 0)
            {
                playerRankObj.GetComponent<RankItem>().SetMyRank();
            }
            else
            {
                playerRankObj.GetComponent<RankItem>().SetMyRank(NetworkMgr.My.groupRank.rankLists[0], isGroup);
            }
        }
        else
        {
            if (NetworkMgr.My.globalRank.rankLists.Count == 0)
            {
                playerRankObj.GetComponent<RankItem>().SetMyRank();
            }
            else
            {
                playerRankObj.GetComponent<RankItem>().SetMyRank(NetworkMgr.My.globalRank.rankLists[0], isGroup);
            }
        }
    }

    void SetToggleStatus(Transform toggle)
    {
        record_toggle.transform.SetAsFirstSibling();
        group_toggle.transform.SetAsFirstSibling();
        global_toggle.transform.SetAsFirstSibling();

        toggle.SetAsLastSibling();
    }

    void SetButton(bool show)
    {
        playerRankObj.SetActive(show);
        pageButtons.SetActive(show);
        //if (NetworkMgr.My.playerDatas.levelID >= 12) {

            refresh_btn.gameObject.SetActive(show);
        if (show)
        {
            RefreshTimeCount();
        }
        //}
        //else
        //{
        //    refresh_btn.gameObject.SetActive(false);
        //}
    }

    void ShowRecordList()
    {
        int childCount = listContent.childCount;
        if (childCount <= NetworkMgr.My.replayLists.Count)
        {
            for (int i = NetworkMgr.My.replayLists.Count - 1; i >= 0; i--)
            {
                if (NetworkMgr.My.replayLists.Count - 1 - i < childCount)
                {
                    listContent.GetChild(NetworkMgr.My.replayLists.Count - 1 - i).GetComponent<RankItem>().Setup(NetworkMgr.My.replayLists[i]);
                    listContent.GetChild(NetworkMgr.My.replayLists.Count - 1 - i).gameObject.SetActive(true);
                    continue;
                }
                GameObject rankObj = Instantiate(rankPrefab, listContent);
                rankObj.GetComponent<RankItem>().Setup(NetworkMgr.My.replayLists[i]);
            }
        }
        else
        {
            for (int i = 0; i < childCount; i++)
            {
                //listContent.GetChild(i).gameObject.SetActive(false);
                if (i < NetworkMgr.My.replayLists.Count)
                {
                    listContent.GetChild(i).GetComponent<RankItem>().Setup(NetworkMgr.My.replayLists[NetworkMgr.My.replayLists.Count - 1 - i]);
                    listContent.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    void ShowRankList(bool isGroup)
    {
        int childCount = listContent.childCount;
        if (isGroup)
        {
            if (childCount <= /*groupList.Count*/ CommonParams.rankPageMaxNum)
            {
                for (int i = 0; i < /*groupList.Count*/ CommonParams.rankPageMaxNum; i++)
                {
                    if (i < childCount)
                    {
                        listContent.GetChild(i).gameObject.SetActive(false);
                        if (i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(groupList[i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum], isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);

                        }
                        continue;
                    }
                    if (i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
                    {
                        GameObject rankObj = Instantiate(rankPrefab, listContent);
                        rankObj.GetComponent<RankItem>().Setup(groupList[i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum], isGroup);

                    }
                }
            }
            else
            {
                for (int i = 0; i < childCount; i++)
                {
                    listContent.GetChild(i).gameObject.SetActive(false);
                    if (i < /*groupList.Count*/ CommonParams.rankPageMaxNum)
                    {
                        if (i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(groupList[i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum], isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);

                        }
                    }
                }
            }
        }
        else
        {
            if (childCount <= /*globalList.Count*/ CommonParams.rankPageMaxNum)
            {
                for (int i = 0; i < /*globalList.Count*/ CommonParams.rankPageMaxNum; i++)
                {
                    if (i < childCount)
                    {
                        listContent.GetChild(i).gameObject.SetActive(false);
                        if (i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(globalList[i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum], isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);

                        }
                        continue;
                    }
                    if (i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
                    {
                        GameObject rankObj = Instantiate(rankPrefab, listContent);
                        rankObj.GetComponent<RankItem>().Setup(globalList[i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum], isGroup);

                    }
                }
            }
            else
            {
                for (int i = 0; i < childCount; i++)
                {
                    listContent.GetChild(i).gameObject.SetActive(false);
                    if (i < /*globalList.Count*/ CommonParams.rankPageMaxNum)
                    {
                        if (i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(globalList[i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum], isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    void HideAllList()
    {
        for (int i = 0; i < listContent.childCount; i++)
        {
            listContent.GetChild(i).gameObject.SetActive(false);
        }
    }

    void RefreshPageButtons(bool isGroup)
    {
        if (isGroup)
        {
            if (NetworkMgr.My.currentGroupPage == 0)
            {
                prePage.interactable = false;
            }
            else
            {
                prePage.interactable = true;
            }

            if (CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGroupPage + 1) < groupList.Count)
            {
                nextPage.interactable = true;
            }

            else if (CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGroupPage + 1) == groupList.Count)
            {
                if (NetworkMgr.My.groupList.rankLists.Count > 10)
                {
                    nextPage.interactable = true;
                }
                else
                {
                    nextPage.interactable = false;
                }
            }

            else if (CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGroupPage + 1) > groupList.Count)
            {
                nextPage.interactable = false;
            }
        }
        else
        {
            if (NetworkMgr.My.currentGlobalPage == 0)
            {
                prePage.interactable = false;
            }
            else
            {
                prePage.interactable = true;
            }

            if (CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGlobalPage + 1) < globalList.Count)
            {
                nextPage.interactable = true;
            }

            else if (CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGlobalPage + 1) == globalList.Count)
            {
                if (NetworkMgr.My.globalList.rankLists.Count > 10)
                {
                    nextPage.interactable = true;
                }
                else
                {
                    nextPage.interactable = false;
                }
            }

            else if (CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGlobalPage + 1) > globalList.Count)
            {
                nextPage.interactable = false;
            }
        }
    }

    void RefreshTimeCount()
    {
        int currentTime = TimeStamp.GetCurrentTimeStamp();
        int lastRefreshTime = PlayerPrefs.GetInt("rankFreshTime", 0);
        int gap = currentTime - lastRefreshTime;
        if (lastRefreshTime == 0|| gap>=timeInterval)
        {
            refresh_btn.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            
            TimeCountDown(timeInterval- gap);
        }
    }
    int timeCountDown = 0;
    void TimeCountDown(int interval)
    {
        CancelInvoke("RankCountDown");
        refresh_btn.interactable = false;
        timeCountDown = interval + 1;
        InvokeRepeating("RankCountDown", 0, 1);
    }

    void RankCountDown()
    {
        timeCountDown -= 1;
        if (timeCountDown <= 0)
        {
            refresh_btn.transform.GetChild(0).gameObject.SetActive(false);
            refresh_btn.interactable = true;
            CancelInvoke("RankCountDown");
        }
        else
        {
            refresh_btn.transform.GetChild(0).gameObject.SetActive(true);
            refresh_btn.transform.GetChild(0).GetComponent<Text>().text = timeCountDown + "s";
        }
    }
}
