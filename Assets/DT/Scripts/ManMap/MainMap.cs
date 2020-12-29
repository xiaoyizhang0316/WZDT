using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMap : MonoBehaviour
{
    public Text title;
    public List<LevelSign> levelSigns;

    public LevelSign lastLevel;
    public Transform threeWords;

    public Text userLevelText;

    public List<GameObject> teachLevels;
    

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
        //GetChaseLevel();
        GetAnswers();
        GetEquips();
        GetUserLevel();
        GetLevelProgress();
        GetRoleTemplateData();
        
        PlayerData.My.isAllReady = false;
        PlayerData.My.isLocalReady = false;
    }

    void GetRoleTemplateData()
    {
        OriginalData.My.ReadRoleTemplateJson();
    }

    private void GetGroupInfos()
    {
        NetworkMgr.My.GetPlayerGroupInfo(() => {
            if (NetworkMgr.My.playerGroupInfo.outdateTime < TimeStamp.GetCurrentTimeStamp() && NetworkMgr.My.playerGroupInfo.outdateTime != 0)
            {
                // 账号过期
                HttpManager.My.ShowClickTip("账号已失效！", () => SceneManager.LoadScene("Login"));
                return;
                //SceneManager.LoadScene("Login");
            }
            if (NetworkMgr.My.playerGroupInfo.isLoginLimit)
            {
                HttpManager.My.ShowClickTip("限制登陆！", () => SceneManager.LoadScene("Login"));
                return;
            }

            // 是否解锁第9关
            if (NetworkMgr.My.playerGroupInfo.isOpenLastLevel)
            {
                // 解锁第9关
                string stars = GetStar(9);
                if (stars.Equals(""))
                {
                    lastLevel.InitLevel(true, "000");
                }
                else
                {
                    lastLevel.InitLevel(true, stars);
                }
            }
            else
            {
                lastLevel.InitLevel(false, "");
            }
        });
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

    //public void GetChaseLevel()
    //{
    //    //InitChaseLevel(0);
    //    NetworkMgr.My.GetCatchLevel((catchLevel) =>
    //    {
    //        Debug.Log("catchLevel:" + catchLevel);
    //        //InitChaseLevel(catchLevel);

    //    });
    //}

    //public void InitChaseLevel(int chaseLevel)
    //{
    //    foreach (var ls in levelSigns)
    //    {
    //        if (ls.levelID == 1)
    //            continue;
    //        //int level = int.Parse( ls.loadScene.Split('_')[1]);
    //        switch(chaseLevel)
    //        {
    //            case 0:
    //                {
    //                    ls.actualStarRequirement = ls.starRequirement;
    //                    chaseText.SetActive(false);
    //                    break;
    //                }
    //            case 1:
    //                {
    //                    ls.actualStarRequirement = ls.starRequirement - (ls.starRequirement > 2 ? 2 : ls.starRequirement);
    //                    chaseText.SetActive(true);
    //                    chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().text = "1";
    //                    chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().color = Color.green;
    //                    chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().text = "所有关卡需求星数-2";
    //                    chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().color = Color.green;
    //                    break;
    //                }
    //            case 2:
    //                {
    //                    ls.actualStarRequirement = ls.starRequirement - (ls.starRequirement > 4 ? 4 : ls.starRequirement);
    //                    chaseText.SetActive(true);
    //                    chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().text = "2";
    //                    chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().color = Color.yellow;
    //                    chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().text = "所有关卡需求星数-4";
    //                    chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().color = Color.yellow;
    //                    break;
    //                }
    //            case 3:
    //                {
    //                    ls.actualStarRequirement = ls.starRequirement - (ls.starRequirement > 6 ? 6 : ls.starRequirement);
    //                    chaseText.SetActive(true);
    //                    chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().text = "3";
    //                    chaseText.transform.Find("ChaseLevel1").GetComponent<Text>().color = Color.red;
    //                    chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().text = "所有关卡需求星数-6";
    //                    chaseText.transform.Find("ChaseLevel2").GetComponent<Text>().color = Color.red;
    //                    break;
    //                }
    //            default:
    //                break;

    //        }
    //    }
    //}

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
                threeWords.gameObject.SetActive(true);
                InitLevel();
            }, () => {
                if (NetworkMgr.My.playerDatas.fteProgress > 1)
                {
                    HttpManager.My.ShowClickTip("获取数据失败，点击重试！", InitMap);
                    //HttpManager.My.ShowTwoClickTip("获取数据失败，点击重试或取消");
                }
                else
                {
                    threeWords.gameObject.SetActive(true);
                    InitLevel();
                }
            });
        }
        else
        {
            threeWords.gameObject.SetActive(true);
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
        GetGroupInfos();
        //TalentPanel.My.Init();
    }

    string GetFTEProgress()
    {
        string fte = "";
        switch (NetworkMgr.My.levelProgressList.Count)
        {
            case 0:
                fte= "0";
                break;
            case 1:
                fte= "0.5";
                break;
            case 2:
                fte = "1.5";
                break;
            case 3:
                fte = "2.5";
                break;
            default:
                fte = "2.5";
                break;
        }

        if (float.Parse(NetworkMgr.My.playerDatas.fte) < float.Parse(fte))
        {
            NetworkMgr.My.UpdatePlayerFTE(fte);
        }
        else
        {
            fte = NetworkMgr.My.playerDatas.fte;
        }

        InitFTELevel(fte);

        return fte;
    }

    void InitFTELevel(string fte)
    {
        switch (fte)
        {
           case "0":
               teachLevels[0].SetActive(true);
               teachLevels[1].SetActive(true);
               teachLevels[2].SetActive(true);
               break;
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
