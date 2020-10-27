<<<<<<< HEAD
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupInfos : MonoBehaviour
{
    public Transform itemContent;
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupInfos : MonoBehaviour
{
    public Transform itemContent;
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public GameObject groupItem;

    public Transform detailInfoPanel;
    public GameObject detailInfoItem;
    public Transform detailContent;
<<<<<<< HEAD
    #region data
=======
    #region data
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    List<GroupItemInfo> groupItemInfos = new List<GroupItemInfo>();

    GroupTotalPlayCount groupTotal;

    private int index = -1;
    #endregion
<<<<<<< HEAD
    #region UI
=======
    #region UI
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public Text playCount;
    public Text winCount;
    public Text totalTimes;
    public Text winRates;
<<<<<<< HEAD
    public Text perTime;
    public Text totalPeople;

    public Text groupName;
    public Toggle groupTotal_tg;
    public Toggle playerPlayCount_tg;
    public Toggle playerWinCount_tg;
=======
    public Text perTime;
    public Text totalPeople;

    public Text groupName;
    public Toggle groupTotal_tg;
    public Toggle playerPlayCount_tg;
    public Toggle playerWinCount_tg;
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    public Toggle playerTimeCount_tg;
    public Toggle playerTotalScore_tg;

    public List<Button> levelButtons;

    private Dictionary<string, GroupPlayer> groupPlayers = new Dictionary<string, GroupPlayer>();

    private Dictionary<string, GroupPlayer> tryGroupPlayer = new Dictionary<string, GroupPlayer>();

    public Button close;
    public Button detailClose;

    public Button timeSort_btn;
    public Button scoreSort_btn;

    private bool isSortByScore = false;
<<<<<<< HEAD
    #endregion

=======
    #endregion

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void Start()
    {
        groupTotal_tg.onValueChanged.AddListener((isOn) => { ShowTotalToggle(isOn); });
        playerPlayCount_tg.onValueChanged.AddListener((isOn) => { ShowCountToggle(isOn); });
        playerWinCount_tg.onValueChanged.AddListener((isOn) => { ShowWinToggle(isOn); });
        playerTimeCount_tg.onValueChanged.AddListener((isOn) => { ShowTimeToggle(isOn); });
        playerTotalScore_tg.onValueChanged.AddListener((isOn) => { ShowTotalScoreToggle(isOn); });
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示小组的信息
    /// </summary>
    /// <param name="gtpc"></param>
=======
    }

    /// <summary>
    /// 显示小组的信息
    /// </summary>
    /// <param name="gtpc"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示小组的总体数据
    /// </summary>
=======
    }

    /// <summary>
    /// 显示小组的总体数据
    /// </summary>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 初始化关卡详细通过信息按钮
    /// </summary>
=======
    }

    /// <summary>
    /// 初始化关卡详细通过信息按钮
    /// </summary>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 获取某关详细的通过情况
    /// </summary>
    /// <param name="levelID"></param>
=======
    }

    /// <summary>
    /// 获取某关详细的通过情况
    /// </summary>
    /// <param name="levelID"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 处理并展示详细通过情况
    /// </summary>
=======
    }

    /// <summary>
    /// 处理并展示详细通过情况
    /// </summary>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
        }

=======
        }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
        foreach (string key in tryGroupPlayer.Keys)
        {
            GameObject go = Instantiate(detailInfoItem, detailContent);
            LevelDetailInfo ldi = go.GetComponent<LevelDetailInfo>();
            ldi.Setup(tryGroupPlayer[key], num, true);
            num++;
<<<<<<< HEAD
        }

=======
        }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
        foreach (string key in groupPlayers.Keys)
        {
            GameObject go = Instantiate(detailInfoItem, detailContent);
            LevelDetailInfo ldi = go.GetComponent<LevelDetailInfo>();
            ldi.Setup(groupPlayers[key], num);
            num++;
<<<<<<< HEAD
        }
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="byScore"></param>
=======
        }
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="byScore"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 获取未通过的人数
    /// </summary>
    /// <param name="doEnd"></param>
=======
    }

    /// <summary>
    /// 获取未通过的人数
    /// </summary>
    /// <param name="doEnd"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 获取小组实际游玩的人数
    /// </summary>
    /// <returns></returns>
=======
    }

    /// <summary>
    /// 获取小组实际游玩的人数
    /// </summary>
    /// <returns></returns>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示小组总体信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
=======
    }

    /// <summary>
    /// 显示小组总体信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示组员游玩次数信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
=======
    }

    /// <summary>
    /// 显示组员游玩次数信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示小组组员胜利次数信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
=======
    }

    /// <summary>
    /// 显示小组组员胜利次数信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示小组组员每关游玩时间信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
=======
    }

    /// <summary>
    /// 显示小组组员每关游玩时间信息的toggle事件
    /// </summary>
    /// <param name="isOn"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void ShowTotalScoreToggle(bool isOn)
    {
        if (isOn)
        {
            if (index == 5)
            {
                return;
            }
            else
            {
                ClearContent();
                index = 5;
                ShowPlayerTotalScoreInfos(AdminManager.My.playerTotalScores);
            }
        }
        else
        {
            return;
        }
<<<<<<< HEAD
    }

    /// <summary>
    /// 显示小组总体信息
    /// </summary>
=======
    }

    /// <summary>
    /// 显示小组总体信息
    /// </summary>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void ShowGroupTotal()
    {
        for(int i = 0; i < groupItemInfos.Count; i++)
        {
            GameObject go = Instantiate(groupItem, itemContent);
            GroupItemSample gis = go.GetComponent<GroupItemSample>();
            gis.Setup(groupItemInfos[i]);
        }
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    /// <summary>
    /// 显示组员信息
    /// </summary>
    /// <param name="playCounts"></param>
<<<<<<< HEAD
    /// <param name="isTime"></param>
=======
    /// <param name="isTime"></param>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void ShowPlayerInfos(List<GroupPlayerLevelPlayCount> playCounts, bool isTime=false)
    {
        for (int i = 0; i < playCounts.Count; i++)
        {
            GameObject go = Instantiate(groupItem, itemContent);
            GroupItemSample gis = go.GetComponent<GroupItemSample>();
            gis.Setup(playCounts[i], isTime);
        }
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void ShowPlayerTotalScoreInfos(List<PlayerTotalScore> totalScores, bool isTime = false)
    {
        for (int i = 0; i < totalScores.Count; i++)
        {
            GameObject go = Instantiate(groupItem, itemContent);
            GroupItemSample gis = go.GetComponent<GroupItemSample>();
            gis.Setup(totalScores[i]);
        }
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

=======
    }

>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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
<<<<<<< HEAD
    }

    /// <summary>
    /// 关闭
    /// </summary>
=======
    }

    /// <summary>
    /// 关闭
    /// </summary>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void Close()
    {
        gameObject.SetActive(false);
        Sort(false);
        detailInfoPanel.gameObject.SetActive(false);
        ClearContent();
<<<<<<< HEAD
    }

    /// <summary>
    /// 关闭通过详情
    /// </summary>
=======
    }

    /// <summary>
    /// 关闭通过详情
    /// </summary>
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
    private void DetailClose()
    {
        ClearDetailContent();
        Sort(false);
        detailInfoPanel.gameObject.SetActive(false);
<<<<<<< HEAD
    }
}


public class GroupItemInfo
{
=======
    }
}


public class GroupItemInfo
{
>>>>>>> 15ac25df35d06066a4c7fbf2ef2f15d90fa4aa47
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