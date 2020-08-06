using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMap : MonoBehaviour
{
    public Text title;
    public List<LevelSign> levelSigns;
    public Transform threeWords;
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
        GetChaseLevel();
        GetAnswers();
        GetLevelProgress();
        GetEquips();
    }

    public void GetChaseLevel()
    {
        
    }

    public void InitChaseLevel(int chaseLevel)
    {
        foreach (var ls in levelSigns)
        {
            //int level = int.Parse( ls.loadScene.Split('_')[1]);
            switch(chaseLevel)
            {
                case 0:
                    break;
                case 1:
                    {
                        ls.starRequirement = ls.initStarRequirement - ls.initStarRequirement > 2 ? 2 : ls.initStarRequirement;
                        break;
                    }
                case 2:
                    {
                        ls.starRequirement = ls.initStarRequirement - ls.initStarRequirement > 5 ? 5 : ls.initStarRequirement;
                        break;
                    }
                case 3:
                    {
                        ls.starRequirement = ls.initStarRequirement - ls.initStarRequirement > 7 ? 7 : ls.initStarRequirement;
                        break;
                    }
                default:
                    break;

            }
        }
    }

    private void GetAnswers()
    {
        NetworkMgr.My.GetAnswers(() => title.text = NetworkMgr.My.currentAnswer);
    }

    private void GetLevelProgress()
    {
        //Debug.Log("get ");
        if (NetworkMgr.My.levelProgresses == null)
        {
            NetworkMgr.My.GetLevelProgress(() => {
                InitLevel();
                threeWords.gameObject.SetActive(true);
            }, () => {
                if (NetworkMgr.My.playerDatas.fteProgress > 1)
                {
                    HttpManager.My.ShowClickTip("获取数据失败，点击重试！", InitMap);
                    //HttpManager.My.ShowTwoClickTip("获取数据失败，点击重试或取消");
                }
                else
                {
                    InitLevel();
                    threeWords.gameObject.SetActive(true);
                }
            });
        }
        else
        {
            InitLevel();
            threeWords.gameObject.SetActive(true);
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
            //int level = int.Parse( ls.loadScene.Split('_')[1]);
            int level = ls.levelID;
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
