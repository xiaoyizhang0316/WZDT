using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupInfos : MonoBehaviour
{
    public Transform itemContent;
    public GameObject groupItem;

    public Transform detailInfoPanel;
    public GameObject detailInfoItem;
    public Transform detailContent;
    #region data
    List<GroupItemInfo> groupItemInfos = new List<GroupItemInfo>();

    GroupTotalPlayCount groupTotal;

    private int index = -1;
    #endregion
    #region UI
    public Text playCount;
    public Text winCount;
    public Text totalTimes;
    public Text winRates;
    public Text perTime;
    public Text totalPeople;

    public Text groupName;
    public Toggle groupTotal_tg;
    public Toggle playerPlayCount_tg;
    public Toggle playerWinCount_tg;
    public Toggle playerTimeCount_tg;

    public List<Button> levelButtons;

    private Dictionary<string, GroupPlayer> groupPlayers = new Dictionary<string, GroupPlayer>();

    private Dictionary<string, GroupPlayer> tryGroupPlayer = new Dictionary<string, GroupPlayer>();

    public Button close;
    public Button detailClose;

    public Button timeSort_btn;
    public Button scoreSort_btn;

    private bool isSortByScore = false;
    #endregion

    private void Start()
    {
        groupTotal_tg.onValueChanged.AddListener((isOn) => { ShowTotalToggle(isOn); });
        playerPlayCount_tg.onValueChanged.AddListener((isOn) => { ShowCountToggle(isOn); });
        playerWinCount_tg.onValueChanged.AddListener((isOn) => { ShowWinToggle(isOn); });
        playerTimeCount_tg.onValueChanged.AddListener((isOn) => { ShowTimeToggle(isOn); });
        if (AdminManager.My.playerDatas.levelID == 12)
        {
            close.gameObject.SetActive(false);
        }
        else
        {
            close.gameObject.SetActive(true);
        }
        close.onClick.AddListener(Close);
        detailClose.onClick.AddListener(DetailClose);
        timeSort_btn.onClick.AddListener(() => Sort(false));
        scoreSort_btn.onClick.AddListener(() => Sort(true));
        InitLevelButtons();
    }

    /// <summary>
    /// 显示小组的信息
    /// </summary>
    /// <param name="gtpc"></param>
    public void ShowInfos(GroupTotalPlayCount gtpc)
    {
        detailInfoPanel.gameObject.SetActive(false);
        groupTotal = gtpc;
        groupTotal_tg.isOn = true;
        index = 1;
        ClearContent();
        ShowTotal();
        groupItemInfos.Clear();
        groupItemInfos.Add(new GroupItemInfo("游戏场次", false, false));
        groupItemInfos.Add(new GroupItemInfo("胜利场次",false,false));
        groupItemInfos.Add(new GroupItemInfo("游戏时间",true,false));
        groupItemInfos.Add(new GroupItemInfo("游戏胜率",true,true));
        groupItemInfos.Add(new GroupItemInfo("场均时间",true,false));
        groupItemInfos.Add(new GroupItemInfo("通过人数",false,false));
        groupItemInfos.Add(new GroupItemInfo("通过率",true,true));
        for (int i=0; i<AdminManager.My.playerLevelPlayCounts.Count; i++)
        {
            groupItemInfos[0].levels.Add(AdminManager.My.playerLevelPlayCounts[i].playCount);
            groupItemInfos[1].levels.Add(AdminManager.My.playerLevelPlayCounts[i].winCount);
            groupItemInfos[2].levels.Add(AdminManager.My.playerLevelPlayCounts[i].times);
            groupItemInfos[3].levels.Add((float)AdminManager.My.playerLevelPlayCounts[i].winCount/ AdminManager.My.playerLevelPlayCounts[i].playCount);
            groupItemInfos[4].levels.Add((float)AdminManager.My.playerLevelPlayCounts[i].times/ AdminManager.My.playerLevelPlayCounts[i].playCount);
        }

        for(int i=0; i< AdminManager.My.levelPasses.Count; i++)
        {
            groupItemInfos[5].levels.Add(AdminManager.My.levelPasses[i].passNum);
            groupItemInfos[6].levels.Add((float)AdminManager.My.levelPasses[i].passNum/AdminManager.My.playerPlayCount.Count);
        }
        ShowGroupTotal();
    }

    /// <summary>
    /// 显示小组的总体数据
    /// </summary>
    public void ShowTotal()
    {
        int validPeople = GetValidPeople();
        playCount.text = groupTotal.total.ToString();
        winCount.text = groupTotal.win.ToString();
        //totalTimes.text = ((groupTotal.times / 3600.0)/(validPeople==0?1:validPeople)).ToString("F2") + "h";
        totalTimes.text = AdminManager.My.GetTimeString(groupTotal.times/(validPeople==0?1:validPeople));
        winRates.text = (((float)groupTotal.win / groupTotal.total) * 100).ToString("F2") + "%";
        //perTime.text = (groupTotal.times * 1.0 / groupTotal.total / 60).ToString("F2") + "m";
        perTime.text = AdminManager.My.GetTimeString (groupTotal.times / groupTotal.total);
        totalPeople.text = AdminManager.My.playerPlayCount.Count.ToString();
        PlayerGroup group = AdminManager.My.GetPlayerGroupByGroupID(groupTotal.groupID);
        groupName.text = group == null ? "" : group.groupName;
    }

    /// <summary>
    /// 初始化关卡详细通过信息按钮
    /// </summary>
    private void InitLevelButtons()
    {
        //for(int i=0; i<levelButtons.Count; i++)
        //{
        //    levelButtons[i].onClick.RemoveAllListeners();
        //}

        levelButtons[0].onClick.AddListener(() => GetLevelPassDetail(1));
        levelButtons[1].onClick.AddListener(() => GetLevelPassDetail(2));
        levelButtons[2].onClick.AddListener(() => GetLevelPassDetail(3));
        levelButtons[3].onClick.AddListener(() => GetLevelPassDetail(4));
        levelButtons[4].onClick.AddListener(() => GetLevelPassDetail(5));
        levelButtons[5].onClick.AddListener(() => GetLevelPassDetail(6));
        levelButtons[6].onClick.AddListener(() => GetLevelPassDetail(7));
        levelButtons[7].onClick.AddListener(() => GetLevelPassDetail(8));
        levelButtons[8].onClick.AddListener(() => GetLevelPassDetail(9));
    }

    /// <summary>
    /// 获取某关详细的通过情况
    /// </summary>
    /// <param name="levelID"></param>
    private void GetLevelPassDetail(int levelID)
    {
        ClearDetailContent();
        if (levelID == AdminManager.My.passDetailLevelID && AdminManager.My.passDetailGroupID == AdminManager.My.lastGroupID)
        {
            // Show detail infos
            DealWithPassDetailData();
        }
        else
        {

            AdminManager.My.GetGroupLevelPassDetail(levelID, () => {
                // Show detail infos
                AdminManager.My.GetGroupTryLevelPassDetail("FTE_" + levelID, () => {
                    GetNotPass(DealWithPassDetailData);

                });

            });
        }
    }

    /// <summary>
    /// 处理并展示详细通过情况
    /// </summary>
    private void DealWithPassDetailData()
    {
        ClearDetailContent();
        detailInfoPanel.gameObject.SetActive(true);
        int num = 1;
        for (int i = 0; i < AdminManager.My.levelPassDetails.Count; i++)
        {
            GameObject go = Instantiate(detailInfoItem, detailContent);
            LevelDetailInfo ldi = go.GetComponent<LevelDetailInfo>();
            ldi.Setup(AdminManager.My.levelPassDetails[i], num);
            num++;
        }

        foreach (string key in tryGroupPlayer.Keys)
        {
            GameObject go = Instantiate(detailInfoItem, detailContent);
            LevelDetailInfo ldi = go.GetComponent<LevelDetailInfo>();
            ldi.Setup(tryGroupPlayer[key], num, true);
            num++;
        }

        foreach (string key in groupPlayers.Keys)
        {
            GameObject go = Instantiate(detailInfoItem, detailContent);
            LevelDetailInfo ldi = go.GetComponent<LevelDetailInfo>();
            ldi.Setup(groupPlayers[key], num);
            num++;
        }
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="byScore"></param>
    private void Sort(bool byScore)
    {
        if (isSortByScore == byScore)
        {
            return;
        }
        if (byScore)
        {
            AdminManager.My.levelPassDetails.Sort((x, y) => x.score.CompareTo(y.score));
        }
        else
        {
            AdminManager.My.levelPassDetails.Sort((x, y) => x.id.CompareTo(y.id));
        }
        isSortByScore = byScore;
        DealWithPassDetailData();
    }

    /// <summary>
    /// 获取未通过的人数
    /// </summary>
    /// <param name="doEnd"></param>
    private void GetNotPass(Action doEnd )
    {
        groupPlayers.Clear();
        tryGroupPlayer.Clear();
        if (AdminManager.My.tryGroupPlayers.Count == 0)
        {
            return;
        }
        if (AdminManager.My.groupPlayers.Count > AdminManager.My.levelPassDetails.Count)
        {
            foreach (string key in AdminManager.My.groupPlayers.Keys)
            {
                groupPlayers.Add(key, AdminManager.My.groupPlayers[key]);
            }
            for (int i = 0; i < AdminManager.My.levelPassDetails.Count; i++)
            {
                if (groupPlayers.ContainsKey(AdminManager.My.levelPassDetails[i].playerID))
                {
                    groupPlayers.Remove(AdminManager.My.levelPassDetails[i].playerID);
                }
            }
        }

        if (AdminManager.My.tryGroupPlayers.Count > AdminManager.My.levelPassDetails.Count)
        {
            foreach (string key in AdminManager.My.tryGroupPlayers.Keys)
            {
                if (groupPlayers.ContainsKey(key))
                {
                    groupPlayers.Remove(key);
                    tryGroupPlayer.Add(key, AdminManager.My.tryGroupPlayers[key]);
                }
            }
        }
        doEnd?.Invoke();
    }

    /// <summary>
    /// 获取小组实际游玩的人数
    /// </summary>
    /// <returns></returns>
    int  GetValidPeople()
    {
        int validPeople = 0;
        for(int i = 0; i< AdminManager.My.playerPlayCount.Count; i++)
        {
            if (AdminManager.My.playerPlayCount[i].level1 > 0)
            {
                validPeople += 1;
            }
        }
        Debug.Log(groupTotal.times / 3600.0);
        Debug.Log(validPeople);
        return validPeople;
    }

    /// <summary>
    /// 显示小组总体信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
    private void ShowTotalToggle(bool isOn)
    {
        if (isOn)
        {
            if (index == 1)
            {
                return;
            }
            else
            {
                ClearContent();
                index = 1;
                ShowGroupTotal();
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 显示组员游玩次数信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
    private void ShowCountToggle(bool isOn)
    {
        if (isOn)
        {
            if (index == 2)
            {
                return;
            }
            else
            {
                ClearContent();
                index = 2;
                ShowPlayerInfos(AdminManager.My.playerPlayCount);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 显示小组组员胜利次数信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
    private void ShowWinToggle(bool isOn)
    {
        if (isOn)
        {
            if (index == 3)
            {
                return;
            }
            else
            {
                ClearContent();
                index = 3;
                ShowPlayerInfos(AdminManager.My.playerWinCount);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 显示小组组员每关游玩时间信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
    private void ShowTimeToggle(bool isOn)
    {
        if (isOn)
        {
            if (index == 4)
            {
                return;
            }
            else
            {
                ClearContent();
                index = 4;
                ShowPlayerInfos(AdminManager.My.playerTimeCount,true);
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 显示小组总体信息
    /// </summary>
    private void ShowGroupTotal()
    {
        for(int i = 0; i < groupItemInfos.Count; i++)
        {
            GameObject go = Instantiate(groupItem, itemContent);
            GroupItemSample gis = go.GetComponent<GroupItemSample>();
            gis.Setup(groupItemInfos[i]);
        }
    }

    /// <summary>
    /// 显示组员信息
    /// </summary>
    /// <param name="playCounts"></param>
    /// <param name="isTime"></param>
    private void ShowPlayerInfos(List<GroupPlayerLevelPlayCount> playCounts, bool isTime=false)
    {
        for (int i = 0; i < playCounts.Count; i++)
        {
            GameObject go = Instantiate(groupItem, itemContent);
            GroupItemSample gis = go.GetComponent<GroupItemSample>();
            gis.Setup(playCounts[i], isTime);
        }
    }

    private void ClearContent()
    {
        //for(int i=0; i< itemContent.childCount; i++)
        //{
        //    Destroy(itemContent.GetChild(i).gameObject);
        //}

        foreach (Transform child in itemContent)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearDetailContent()
    {
        //for (int i = 0; i < detailContent.childCount; i++)
        //{
        //    DestroyImmediate(detailContent.GetChild(i).gameObject);
        //}

        foreach(Transform child in detailContent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 关闭
    /// </summary>
    private void Close()
    {
        gameObject.SetActive(false);
        Sort(false);
        detailInfoPanel.gameObject.SetActive(false);
        ClearContent();
    }

    /// <summary>
    /// 关闭通过详情
    /// </summary>
    private void DetailClose()
    {
        ClearDetailContent();
        Sort(false);
        detailInfoPanel.gameObject.SetActive(false);
    }
}


public class GroupItemInfo
{
    public string itemName;
    public List<float> levels;

    public bool isFloat=false;
    public bool isPercent = false;

    public GroupItemInfo(string name, bool isFloat=false, bool isPercent=false)
    {
        itemName = name;
        this.isFloat = isFloat;
        this.isPercent = isPercent;
        levels = new List<float>();
    }
}