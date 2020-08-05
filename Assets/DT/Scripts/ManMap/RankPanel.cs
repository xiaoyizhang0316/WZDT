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

    public string currentSceneName = "";
    public string lastSceneName = "";

    private int currentShowIndex = -1;

    //private List<ReplayList> replayLists = new List<ReplayList>();
    public List<RankList> groupList = new List<RankList>();
    public List<RankList> globalList = new List<RankList>();

    bool getRecord = false;
    bool getGroup = false;
    bool getGlobal = false;

    // Start is called before the first frame update
    void Start()
    {
        record_toggle.onValueChanged.AddListener(isOn=>Record(isOn));
        group_toggle.onValueChanged.AddListener(isOn=> Group(isOn));
        global_toggle.onValueChanged.AddListener(isOn=> Global(isOn));

        prePage.onClick.AddListener(PrePage);
        nextPage.onClick.AddListener(NextPage);
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
        title.text = "History";
        currentShowIndex = 0;
        SetButton(false);
        if (isOn)
        {
            Debug.Log("record");
            SetToggleStatus(record_toggle.transform);
            if (!getRecord)
            {
                NetworkMgr.My.GetReplayLists(currentSceneName, () => {
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
        title.text = "Ranking";
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
            }
            else
            {
                NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList)=> {
                    
                    foreach(var g in gList)
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
                NetworkMgr.My.GetGroupRank(currentSceneName, () => {
                    ShowMyRank(true);
                });
                getGroup = true;
            }
        }
        SetButton(true);
    }

    void Global(bool isOn)
    {
        if (currentShowIndex == 2)
        {
            return;
        }
        title.text = "Ranking";
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
            }
            else
            {
                NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) => {
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
                NetworkMgr.My.GetGlobalRank(currentSceneName, () => {
                    ShowMyRank(false);
                });
                getGlobal = true;
            }
        }
        SetButton(true);
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
            ShowRankList(true);
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
                NetworkMgr.My.GetGroupRankingList(currentSceneName, NetworkMgr.My.currentGroupPage, (gList) => {
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
                NetworkMgr.My.GetGlobalRankingList(currentSceneName, NetworkMgr.My.currentGlobalPage, (gList) => {
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
        record_toggle.transform.SetAsFirstSibling() ;
        group_toggle.transform.SetAsFirstSibling() ;
        global_toggle.transform.SetAsFirstSibling();

        toggle.SetAsLastSibling();
    }

    void SetButton(bool show)
    {
        playerRankObj.SetActive(show);
        pageButtons.SetActive(show);
    }

    void ShowRecordList()
    {
        int childCount = listContent.childCount;
        if (childCount <= NetworkMgr.My.replayLists.Count)
        {
            for(int i=0;i<NetworkMgr.My.replayLists.Count; i++)
            {
                if (i < childCount)
                {
                    listContent.GetChild(i).GetComponent<RankItem>().Setup(NetworkMgr.My.replayLists[i]);
                    listContent.GetChild(i).gameObject.SetActive(true);
                    continue;
                }
                GameObject rankObj = Instantiate(rankPrefab, listContent);
                rankObj.GetComponent<RankItem>().Setup(NetworkMgr.My.replayLists[i]);
            }
        }
        else
        {
            for(int i=0; i<childCount; i++)
            {
                listContent.GetChild(i).gameObject.SetActive(false);
                if (i < NetworkMgr.My.replayLists.Count)
                {
                    listContent.GetChild(i).GetComponent<RankItem>().Setup(NetworkMgr.My.replayLists[i]);
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
                        if(i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(groupList[i+NetworkMgr.My.currentGroupPage*CommonParams.rankPageMaxNum],isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);

                        }
                        continue;
                    }
                    if(i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
                    {
                        GameObject rankObj = Instantiate(rankPrefab, listContent);
                        rankObj.GetComponent<RankItem>().Setup(groupList[i+ NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum],isGroup);

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
                        if(i + NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum < groupList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(groupList[i+ NetworkMgr.My.currentGroupPage * CommonParams.rankPageMaxNum],isGroup);
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
                        if(i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(globalList[i+ NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum], isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);

                        }
                        continue;
                    }
                    if(i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
                    {
                        GameObject rankObj = Instantiate(rankPrefab, listContent);
                        rankObj.GetComponent<RankItem>().Setup(globalList[i+ NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum], isGroup);

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
                        if(i + NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum < globalList.Count)
                        {
                            listContent.GetChild(i).GetComponent<RankItem>().Setup(globalList[i+ NetworkMgr.My.currentGlobalPage * CommonParams.rankPageMaxNum], isGroup);
                            listContent.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }
            }
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

            else if(CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGroupPage + 1) == groupList.Count)
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

            else if(CommonParams.rankPageMaxNum * (NetworkMgr.My.currentGroupPage + 1)> groupList.Count)
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
}
