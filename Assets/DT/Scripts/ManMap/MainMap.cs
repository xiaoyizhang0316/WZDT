﻿using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
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

    public List<Button> normalLevels;
    

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkMgr.My.isUsingHttp)
        {
            InitMap();
        }

        if (SaveMenu.instance)
        {
            SaveMenu.instance.HideInMainMap();
        }
    }

    void InitMap()
    {
        //NetworkMgr.My.GetAnswers(()=>title.text = NetworkMgr.My.currentAnswer);
        //GetChaseLevel();
        if (NetworkMgr.My.needReLogin)
        {
            NetworkMgr.My.needReLogin = false;
            SceneManager.LoadScene("Login");
            return;
        }
        GetAnswers();
        GetUserLevel();
        GetLevelProgress();
        GetRoleTemplateData();
        GetEquips();
        PlayerData.My.Reset();
        PlayerData.My.isAllReady = false;
        PlayerData.My.isLocalReady = false;
    }

    void GetRoleTemplateData()
    {
        OriginalData.My.ReadRoleTemplateJson();
    }

    private void GetGroupInfos(Action doEnd=null)
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
            doEnd?.Invoke();
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
                //InitLevel();
                InitNewLevel();
            }, () => {
                if (NetworkMgr.My.playerDatas.fteProgress > 1)
                {
                    HttpManager.My.ShowClickTip("获取数据失败，点击重试！", InitMap);
                    //HttpManager.My.ShowTwoClickTip("获取数据失败，点击重试或取消");
                }
                else
                {
                    threeWords.gameObject.SetActive(true);
                    //InitLevel();
                    InitNewLevel();
                }
            });
        }
        else
        {
            threeWords.gameObject.SetActive(true);
            //InitLevel();
            InitNewLevel();
        }
    }

    private void GetEquips()
    {
        //if (PlayerData.My.playerGears.Count == 0)
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
                if (ls.GetComponent<AnswerBeforeGame>())
                {
                    ls.GetComponent<Button>().onClick.AddListener(()=>ls.GetComponent<AnswerBeforeGame>().OpenAnswerPanel());
                }
            }
            else
            {
                ls.InitLevel(GetStar(level), GetStar(level - 1));
                if (ls.GetComponent<AnswerBeforeGame>())
                {
                    ls.GetComponent<Button>().onClick.AddListener(()=>ls.GetComponent<AnswerBeforeGame>().OpenAnswerPanel());
                }
            }
        }
        GetGroupInfos();
        //TalentPanel.My.Init();
    }

    void InitNewLevel()
    {
        
        GetGroupInfos(() =>
        {
            string fte = GetFTEProgress();
            foreach(var ls in levelSigns)
            {
                //int level = int.Parse( ls.loadScene.Split('_')[1]);
                int level = ls.levelID;
                if (level == 1)
                {
                    ls.InitNewLevel(GetStar(level), "", fte);
                }
                else
                {
                    ls.InitNewLevel(GetStar(level), GetStar(level - 1),fte);
                }
            }
        });
    }

    string GetFTEProgress()
    {
        string fte = CompareFTE();
        /*switch (NetworkMgr.My.levelProgressList.Count)
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
        }*/

        /*if (NetworkMgr.My.playerGroupInfo.isOpenLimitLevel)
        {
            fte = NetworkMgr.My.levelProgressList.Count.ToString();
            if (float.Parse(NetworkMgr.My.playerDatas.fte) < 4.5f)
            {
                fte = NetworkMgr.My.playerDatas.fte;
            }
            else
            {
                if (float.Parse(fte) < float.Parse(NetworkMgr.My.playerDatas.fte))
                {
                    fte = NetworkMgr.My.playerDatas.fte;
                }
            }
        }
        else
        {
            fte = NetworkMgr.My.levelProgressList.Count.ToString();
            //Debug.Log("fte "+fte);
            //Debug.Log("fte "+NetworkMgr.My.playerDatas.fte);

            if (float.Parse(NetworkMgr.My.playerDatas.fte.Equals("")?"0":NetworkMgr.My.playerDatas.fte) < float.Parse(fte))
            {
                //Debug.Log("fte "+NetworkMgr.My.playerDatas.fte);
                NetworkMgr.My.UpdatePlayerFTE(fte);
            }
            else
            {
                fte = NetworkMgr.My.playerDatas.fte;
                //Debug.Log("fte "+fte);
            }
        }*/

        

        //InitFTELevel(fte);
        InitNewFTELevel(fte);

        return fte;
    }

    string CompareFTE()
    {
        string fte = NetworkMgr.My.playerDatas.fte;
        LevelProgress lp;
        switch (fte)
        {
            case "0.7":
                lp = NetworkMgr.My.GetLevelProgressByIndex(1);
                if (lp != null && lp.starNum > 0)
                {
                    fte = "1";
                }
                break;
            case "1.6":
                lp = NetworkMgr.My.GetLevelProgressByIndex(2);
                if (lp != null && lp.starNum > 0)
                {
                    fte = "2";
                }
                break;
            case "2.5":
                lp = NetworkMgr.My.GetLevelProgressByIndex(3);
                if (lp != null && lp.starNum > 0)
                {
                    fte = "3";
                }
                break;
            case "3.5":
                lp = NetworkMgr.My.GetLevelProgressByIndex(4);
                if (lp != null && lp.starNum > 0)
                {
                    fte = "4";
                }
                break;
            default:
                break;
        }

        return fte;
    }

    void InitFTELevel(string fte)
    {
        SetTeachLevelStatus(teachLevels[0], false);
        switch (fte)
        {
           case "0":
               
           case "0.5":
               SetTeachLevelStatus(teachLevels[1], true);
               SetTeachLevelStatus(teachLevels[2], true);
               break;
           case "1":
           case "1.5":

               /*if (NetworkMgr.My.levelProgressList.Count == 1)
               {
                   SetTeachLevelStatus(teachLevels[1], false);
               }
               else
               {
                   SetTeachLevelStatus(teachLevels[1], true);
               }*/
               SetTeachLevelStatus(teachLevels[1], false);
               SetTeachLevelStatus(teachLevels[2], true);
               break;
           case "2":
           case "2.5":

               SetTeachLevelStatus(teachLevels[1], false);
               /*if (NetworkMgr.My.levelProgressList.Count == 2)
               {
                   SetTeachLevelStatus(teachLevels[2], false);
               }
               else
               {
                   SetTeachLevelStatus(teachLevels[2], true);
               }*/
               SetTeachLevelStatus(teachLevels[2], false);
               break;
           default:
               SetTeachLevelStatus(teachLevels[1], false);
               SetTeachLevelStatus(teachLevels[2], false);
               break;
        }
    }

    public List<GameObject> newTeachLevel = new List<GameObject>();
    void InitNewFTELevel(string fte)
    {
        //Debug.Log(fte);
        for (int i = 0; i < newTeachLevel.Count; i++)
        {
            SetTeachLevelStatus(newTeachLevel[i], true);
        }
        switch (fte)
        {
            case "0":
                SetTeachLevelStatus(newTeachLevel[0], false);
                break;
            case "0.5":
                Debug.Log("case "+fte);
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                break;
            case "0.6":
            case "0.7":    
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                SetTeachLevelStatus(newTeachLevel[2], false);
                break;
            case "1":
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                SetTeachLevelStatus(newTeachLevel[2], false);
                SetTeachLevelStatus(newTeachLevel[3], false);
                break;
            case "1.5": 
            case "1.6":
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                SetTeachLevelStatus(newTeachLevel[2], false);
                SetTeachLevelStatus(newTeachLevel[3], false);
                SetTeachLevelStatus(newTeachLevel[4], false);
                break;
            case "2":
            case "2.5":
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                SetTeachLevelStatus(newTeachLevel[2], false);
                SetTeachLevelStatus(newTeachLevel[3], false);
                SetTeachLevelStatus(newTeachLevel[4], false);
                SetTeachLevelStatus(newTeachLevel[5], false);
                break;
            case "3":
            case "3.5":
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                SetTeachLevelStatus(newTeachLevel[2], false);
                SetTeachLevelStatus(newTeachLevel[3], false);
                SetTeachLevelStatus(newTeachLevel[4], false);
                SetTeachLevelStatus(newTeachLevel[5], false);
                SetTeachLevelStatus(newTeachLevel[6], false);
                break;
            case "4":
            case "4.5":
                SetTeachLevelStatus(newTeachLevel[0], false);
                SetTeachLevelStatus(newTeachLevel[1], false);
                SetTeachLevelStatus(newTeachLevel[2], false);
                SetTeachLevelStatus(newTeachLevel[3], false);
                SetTeachLevelStatus(newTeachLevel[4], false);
                SetTeachLevelStatus(newTeachLevel[5], false);
                SetTeachLevelStatus(newTeachLevel[6], false);
                SetTeachLevelStatus(newTeachLevel[7], false);
                break;
            default:
                for (int i = 0; i < newTeachLevel.Count; i++)
                {
                    SetTeachLevelStatus(newTeachLevel[i], false);
                }
                break;
        }
    }

    void SetTeachLevelStatus(GameObject level, bool isLock)
    {
        if (isLock)
        {
            level.GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
            if(level.GetComponent<ToScene>())
                level.GetComponent<ToScene>().enabled = false;
            level.GetComponent<Button>().enabled = false;
        }
        else
        {
            level.GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
            if(level.GetComponent<ToScene>())
                level.GetComponent<ToScene>().enabled = true;
            level.GetComponent<Button>().enabled = true;
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
