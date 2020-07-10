using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMap : MonoBehaviour
{
    public Text title;
    public List<LevelSign> levelSigns;
    // Start is called before the first frame update
    void Start()
    {
        if (NetworkMgr.My.isUsingHttp)
        {
            InitMap();
        }
    }

    void InitMap()
    {
        //NetworkMgr.My.GetAnswers(()=>title.text = NetworkMgr.My.currentAnswer);
        GetAnswers();
        GetLevelProgress();
        GetEquips();
    }

    private void GetAnswers()
    {
        NetworkMgr.My.GetAnswers(() => title.text = NetworkMgr.My.currentAnswer);
    }

    private void GetLevelProgress()
    {
        if (NetworkMgr.My.levelProgresses == null)
        {
            NetworkMgr.My.GetLevelProgress(() => { InitLevel(); }, () => {
                if (NetworkMgr.My.playerDatas.fteProgress > 1)
                {
                    HttpManager.My.ShowClickTip("获取数据失败，点击重试！", InitMap);
                    //HttpManager.My.ShowTwoClickTip("获取数据失败，点击重试或取消");
                }
                else
                {
                    InitLevel();
                }
            });
        }
        else
        {
            InitLevel();
        }
    }

    private void GetEquips()
    {
        if (PlayerData.My.playerGears.Count == 0)
            NetworkMgr.My.GetPlayerEquips((data)=> {
                PlayerData.My.InitPlayerEquip(data);
            });
    }

    void InitLevel()
    {
        foreach(var ls in levelSigns)
        {
            int level = int.Parse( ls.loadScene.Split('_')[1]);
            if (level == 1)
            {
                ls.InitLevel(GetStar(level), "");
            }
            else
            {
                ls.InitLevel(GetStar(level), GetStar(level - 1));
            }
        }
    }

    string GetStar(int level)
    {
        foreach(var l in NetworkMgr.My.levelProgressList)
        {
            if(l.levelID == level)
            {
                return l.levelStar;
            }
        }
        return "";
    }

    LevelProgress GetProgressByLevel(int level)
    {
        foreach (var l in NetworkMgr.My.levelProgressList)
        {
            if (l.levelID == level)
            {
                return l;
            }
        }
        return null;
    }
}
