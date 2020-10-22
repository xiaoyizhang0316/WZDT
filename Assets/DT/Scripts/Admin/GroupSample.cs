using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupSample : MonoBehaviour
{
    public int groupID = 0;
    private Text groupName;
    private Text totalCount;
    private Text winCount;
    private Text totalTimes;
    private Text winRates;
    private Text time;
    private Button btn;


    public void Setup( PlayerGroup pg, GroupTotalPlayCount playCount)
    {
        Init();
        groupName.text = pg.groupName;
        groupID = pg.groupID;
        btn.onClick.RemoveAllListeners();
        if (playCount == null)
        {
            totalCount.text = "0";
            winCount.text = "0";
            totalTimes.text = "0";
            winRates.text = "0%";
            time.text = "0";
            return;
        }
        btn.onClick.AddListener(OnClick);
        totalCount.text = playCount.total.ToString();
        winCount.text = playCount.win.ToString();
        //totalTimes.text = (playCount.times / 3600.0).ToString("F2") + "h";
        totalTimes.text = AdminManager.My.GetTimeString(playCount.times );
        winRates.text = (((float)playCount.win / playCount.total) * 100).ToString("F2") + "%";
        //time.text = (playCount.times * 1.0 / playCount.total / 60).ToString("F2") + "m";
        time.text = AdminManager.My.GetTimeString(playCount.times / playCount.total);
    }

    private void Init()
    {
        groupName = transform.Find("Name").GetComponent<Text>();
        totalCount = transform.Find("Total").GetComponent<Text>();
        winCount = transform.Find("Win").GetComponent<Text>();
        totalTimes = transform.Find("Times").GetComponent<Text>();
        winRates = transform.Find("WinRate").GetComponent<Text>();
        time = transform.Find("Time").GetComponent<Text>();
        btn = transform.GetComponent<Button>();
    }

    private void OnClick()
    {
        AdminManager.My.ClickShowGroupMoreInfos(groupID);
    }
}
