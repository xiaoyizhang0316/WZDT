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

    public GameObject chaseText;

    public Text userLevelText;

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
        GetEquips();
        GetUserLevel();
        NetworkMgr.My.GetPlayerGroupInfo(()=> { });
        PlayerData.My.isAllReady = false;
        PlayerData.My.isLocalReady = false;
    }

    public void GetUserLevel()
    {
        switch(NetworkMgr.My.playerLimit)
        {
            case 0:
                userLevelText.text = "高级用户";
                userLevelText.color = Color.yellow;
                break;
            case 1:
                userLevelText.text = "体验用户";
                userLevelText.color = Color.gray;
                break;
            case 4:
                userLevelText.text = "试玩用户";
                userLevelText.color = Color.blue;
                break;
            case 9:
                userLevelText.text = "正式用户";
                userLevelText.color = Color.green;
                break;
            default:
                userLevelText.text = "普通用户";
                userLevelText.color = Color.green;
                break;
        }
    }

    public void GetChaseLevel()
    {
        //InitChaseLevel(0);
        NetworkMgr.My.GetCatchLevel((catchLevel) =>
        {
            Debug.Log("catchLevel:" + catchLevel);
            InitChaseLevel(catchLevel);
            GetLevelProgress();
        });
    }

    public void InitChaseLevel(int chaseLevel)
    {
        foreach (var ls in levelSigns)
        {
            if (ls.levelID == 1)
                continue;
            //int level = int.Parse( ls.loadScene.Split('_')[1]);
            switch(chaseLevel)
            {
                case 0:
                    {
                        ls.actualStarRequirement = ls.starRequirement;
                        chaseText.SetActive(false);
                        break;
                    }
                case 1:
                    {
                        ls.actualStarRequirement = ls.starRequirement - (ls.starRequirement > 2 ? 2 : ls.starRequirement);
                        chaseText.SetActive(true);
                        chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().text = "1";
                        chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().color = Color.green;
                        chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().text = "所有关卡需求星数-2";
                        chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().color = Color.green;
                        break;
                    }
                case 2:
                    {
                        ls.actualStarRequirement = ls.starRequirement - (ls.starRequirement > 4 ? 4 : ls.starRequirement);
                        chaseText.SetActive(true);
                        chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().text = "2";
                        chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().color = Color.yellow;
                        chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().text = "所有关卡需求星数-4";
                        chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().color = Color.yellow;
                        break;
                    }
                case 3:
                    {
                        ls.actualStarRequirement = ls.starRequirement - (ls.starRequirement > 6 ? 6 : ls.starRequirement);
                        chaseText.SetActive(true);
                        chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().text = "3";
                        chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().color = Color.red;
                        chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().text = "所有关卡需求星数-6";
                        chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().color = Color.red;
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
