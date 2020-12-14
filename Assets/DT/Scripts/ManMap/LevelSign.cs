using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSign : MonoBehaviour
{
    public int levelID = 0;
    public string levelName;

    public string content;

    public string mission_1;
    public string mission_2;
    public string mission_3;

    //public int starRequirement;

    //public int actualStarRequirement;

    public Button LevelButton;

    //public Text starNeed;

    public string loadScene;
    string stars="";

    public Transform lastFte;


    //public Sprite lockImage;
    // Start is called before the first frame update
    void Start()
    {
        //starNeed = transform.Find("StarNeed").GetComponent<Text>();
        LevelButton.onClick.AddListener(() =>
        {
            Debug.Log("1231231231");
            Init();
        });
        if(!NetworkMgr.My.isUsingHttp)
            InitLevel();
    }

    public void Init()
    {
        if (NetworkMgr.My.isUsingHttp)
        {
            LevelInfoManager.My.Init(stars, levelName, content, mission_1, mission_2, mission_3, () =>
            {
                if (!PlayerData.My.isSOLO)
                {
                    string str = "LoadScene|" + loadScene;
                    if (PlayerData.My.isServer)
                    {
                        PlayerData.My.server.SendToClientMsg(str);
                    }
                    else
                    {
                        PlayerData.My.client.SendToServerMsg(str);
                    }
                }
                if (!PlayerData.My.isSOLO)
                {
                    if (PlayerData.My.isServer)
                    {
                        //SceneManager.LoadScene(loadScene);
                        NetworkMgr.My.GetPoorPlayerEquips((data) =>
                        {
                            Debug.Log("装备回调");
                            if (data.Count != 0)
                            {
                                PlayerData.My.InitPlayerEquip(data);
                            }
                            SceneManager.LoadScene(loadScene);
                            NetworkMgr.My.SetPlayerStatus(loadScene, NetworkMgr.My.currentBattleTeamAcount.teamID);
                        });
                    }
                }
                else
                {
                    SceneManager.LoadScene(loadScene);
                    NetworkMgr.My.SetPlayerStatus(loadScene, "");
                }
            }, loadScene);
            //NetworkMgr.My.GetReplayLists(loadScene,()=> {
            //    LevelInfoManager.My.listScript.Init(NetworkMgr.My.replayLists);
            //});
            RankPanel.My.ShowRankPanel(loadScene);
        }
        else
        {
            LevelInfoManager.My.Init(levelName, content, mission_1, mission_2, mission_3, () =>
            {
                SceneManager.LoadScene(loadScene);
            });
        }
    }

    public void OnClick(string recordID)
    {
        NetworkMgr.My.GetReplayDatas(recordID, (datas)=> {
            string str = "{ \"playerOperations\":" + datas.operations + "}";
            PlayerOperations operations = JsonUtility.FromJson<PlayerOperations>(str);
            string str1 = "{ \"dataStats\":" + datas.dataStats + "}";
            PlayerStatus status = JsonUtility.FromJson<PlayerStatus>(str1);
        });
    }

    void InitLevel()
    {
        string[] strArr = loadScene.Split('_');
        int level = int.Parse(strArr[1]);
        NetworkMgr.My.currentLevel = level;
        if(PlayerPrefs.GetInt(loadScene+"|1", 0) == 0)
        {
            if(level != 1)
            {
                if(PlayerPrefs.GetInt("FTE_"+(level-1)+"|1", 0) == 0)
                {
                    // lock
                    transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
                    transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
                }
            }
            // hide all stars
            HideAllStars();
        }
        else
        {
            // show stars
            ShowStars();
        }
    }

    void ShowStars()
    {
        if(PlayerPrefs.GetInt(loadScene+"|2", 0) == 0)
        {
            transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt(loadScene + "|3", 0) == 0)
        {
            transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
        }
    }

    void HideAllStars()
    {
        transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
        transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
        transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
    }

    // net
    public void InitLevel(string currentStar, string lastStar)
    {
        while (lastStar.Length < 3)
        {
            lastStar = "0"+ lastStar;
        }
        while (currentStar.Length < 3)
        {
            currentStar = "0" + currentStar;
        }
        stars = currentStar;

        if (NetworkMgr.My.levelProgressList.Count >= 4 && NetworkMgr.My.playerDatas.threeWordsProgress == 1)
        {
            ThreeWordsPanel.My.OpenAnswerInputField();
            return;
        }
        else if (NetworkMgr.My.levelProgressList.Count >= 8 && NetworkMgr.My.playerDatas.threeWordsProgress == 2)
        {
            ThreeWordsPanel.My.OpenAnswerInputField();
            return;
        }
        if(levelID!=1)
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
        if (lastStar == "000" && loadScene!="FTE_1" ||  !CheckUserLevel() ||(!PlayerData.My.isSOLO && !PlayerData.My.isServer))
        {
            HideAllStars();
            transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
        }
        else
        {
            //Debug.Log(levelID);
            //Debug.Log(currentStar);
            if (currentStar.Equals("000"))
            {
                if (NetworkMgr.My.playerDatas.unlockStatus.Split('_')[levelID - 1].Equals("0"))
                {
                    // 上传关卡解锁状态
                    string[] arr = NetworkMgr.My.playerDatas.unlockStatus.Split('_');
                    arr[levelID - 1] = "1";
                    string newStatus = "1";
                    for(int i=1; i< arr.Length; i++)
                    {
                        newStatus += "_"+arr[i];
                    }
                    Debug.Log(newStatus);
                    // 解锁动画
                    // fade
                    //transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.75f).OnComplete(()=> {
                    //    transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                    //    transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.75f).OnComplete(()=> {
                    //        Debug.Log("donghua");
                    //        //transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                    //        if (currentStar[0] == '0')
                    //        {
                    //            transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                    //        }
                    //        if (currentStar[1] == '0')
                    //        {
                    //            transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                    //        }
                    //        if (currentStar[2] == '0')
                    //        {
                    //            transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                    //        }
                    //        if (levelID == 2)
                    //        {
                    //            // 开启引导
                    //            MapGuideManager.My.currentGuideIndex = 0;
                    //            MapGuideManager.My.PlayCurrentIndexGuide();
                    //        }
                    //    });
                    //});

                    // scale
                    transform.GetChild(0).DOScale(0, 0.75f).Play().OnComplete(() =>
                    {
                        //Debug.Log("do scale");
                        transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                        transform.GetChild(0).DOScale(1, 0.5f).Play().OnComplete(() =>
                        {
                            //Debug.Log("donghua");
                            //transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                            
                            if (levelID == 2)
                            {
                                // 开启引导
                                MapGuideManager.My.currentGuideIndex = 0;
                                MapGuideManager.My.PlayCurrentIndexGuide();
                            }
                        });
                    });

                    // shake
                    //transform.GetChild(0).DOShakePosition(1, new Vector3(10, 10, 10), 10, 180).OnComplete(()=> {
                    //    Debug.Log("donghua");
                    //    transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                    //    if (currentStar[0] == '0')
                    //    {
                    //        transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                    //    }
                    //    if (currentStar[1] == '0')
                    //    {
                    //        transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                    //    }
                    //    if (currentStar[2] == '0')
                    //    {
                    //        transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                    //    }
                    //    if (levelID == 2)
                    //    {
                    //        // 开启引导
                    //        MapGuideManager.My.currentGuideIndex = 0;
                    //        MapGuideManager.My.PlayCurrentIndexGuide();
                    //    }
                    //});
                    if (currentStar[0] == '0')
                    {
                        transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[1] == '0')
                    {
                        transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[2] == '0')
                    {
                        transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                    }
                    if (levelID != 1&& levelID!=2&& levelID != 4)
                    {
                        //Debug.Log("update status");
                        NetworkMgr.My.UpdateUnlockStatus(newStatus);
                    }
                    
                }
                else
                {
                    transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                    if (currentStar[0] == '0')
                    {
                        transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[1] == '0')
                    {
                        transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[2] == '0')
                    {
                        transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                if (currentStar[0] == '0')
                {
                    transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                }
                if (currentStar[1] == '0')
                {
                    transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                }
                if (currentStar[2] == '0')
                {
                    transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                }
                if (levelID == 4)
                {
                    // 判断开启天赋教学
                    if (NetworkMgr.My.playerDatas.unlockStatus.Split('_')[levelID - 1].Equals("0"))
                    {
                        // 开启教学
                        //GuideManager.My.currentGuideIndex = 2;
                        MapGuideManager.My.GetComponent<MapObject>().openCG.SetActive(true);
                        // 上传关卡解锁状态
                        //string[] arr = NetworkMgr.My.playerDatas.unlockStatus.Split('_');
                        //arr[levelID - 1] = "1";
                        //string newStatus = "1";
                        //for (int i = 1; i < arr.Length; i++)
                        //{
                        //    newStatus += "_"+arr[i];
                        //}
                        //NetworkMgr.My.UpdateUnlockStatus(newStatus, () => {

                        //}, () => {
                        //    HttpManager.My.ShowTip("解锁关卡出错");
                        //});
                    }
                }
            }

            
        }
        LevelButton.onClick.RemoveAllListeners();
        LevelButton.onClick.AddListener(Init);
        //InitStarNeedText();
    }

    public void InitNewLevel(string currentStar, string lastStar)
    {
        while (lastStar.Length < 3)
        {
            lastStar = "0"+ lastStar;
        }
        while (currentStar.Length < 3)
        {
            currentStar = "0" + currentStar;
        }
        stars = currentStar;
        
        if (NetworkMgr.My.playerDatas.fte.Equals("0"))
        {
            if (NetworkMgr.My.playerDatas.fteProgress > 0)
            {
                if (NetworkMgr.My.playerDatas.fteProgress == 1)
                {
                    NetworkMgr.My.playerDatas.fte = "1";
                }
                else if (NetworkMgr.My.playerDatas.fteProgress == 2)
                {
                    NetworkMgr.My.playerDatas.fte = "2";
                }
                else
                {
                    NetworkMgr.My.playerDatas.fte = "4";
                }
                NetworkMgr.My.UpdatePlayerFTE(NetworkMgr.My.playerDatas.fte);
            }
        }

        if (NetworkMgr.My.levelProgressList.Count >= 4 && NetworkMgr.My.playerDatas.threeWordsProgress == 1)
        {
            ThreeWordsPanel.My.OpenAnswerInputField();
            return;
        }
        else if (NetworkMgr.My.levelProgressList.Count >= 8 && NetworkMgr.My.playerDatas.threeWordsProgress == 2)
        {
            ThreeWordsPanel.My.OpenAnswerInputField();
            return;
        }
        if(levelID!=1)
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
        if (lastStar == "000" && loadScene!="FTE_1" ||  !CheckUserLevel() ||(!PlayerData.My.isSOLO && !PlayerData.My.isServer))
        {
            HideAllStars();
            transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
        }
        else
        {
            //Debug.Log(levelID);
            //Debug.Log(currentStar);
            if (currentStar.Equals("000"))
            {
                if (levelID == 1 && NetworkMgr.My.playerDatas.fte.Equals("0") ||
                    levelID == 2 && NetworkMgr.My.playerDatas.fte.Equals("1") ||
                    levelID == 3 && NetworkMgr.My.playerDatas.fte.Equals("2"))
                {
                    lastFte.GetChild(0).GetComponent<Image>().raycastTarget = true;
                    lastFte.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                }
                else
                {
                    lastFte.GetChild(0).GetComponent<Image>().raycastTarget = false;
                    lastFte.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
                    if (NetworkMgr.My.playerDatas.unlockStatus.Split('_')[levelID - 1].Equals("0"))
                    {
                    // 上传关卡解锁状态
                    string[] arr = NetworkMgr.My.playerDatas.unlockStatus.Split('_');
                    arr[levelID - 1] = "1";
                    string newStatus = "1";
                    for(int i=1; i< arr.Length; i++)
                    {
                        newStatus += "_"+arr[i];
                    }
                    Debug.Log(newStatus);
                    // 解锁动画
                    // scale
                    transform.GetChild(0).DOScale(0, 0.75f).Play().OnComplete(() =>
                    {
                        //Debug.Log("do scale");
                        transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                        transform.GetChild(0).DOScale(1, 0.5f).Play().OnComplete(() =>
                        {
                            //Debug.Log("donghua");
                            //transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                            
                            if (levelID == 2)
                            {
                                // 开启引导
                                MapGuideManager.My.currentGuideIndex = 0;
                                MapGuideManager.My.PlayCurrentIndexGuide();
                            }
                        });
                    });

                    if (currentStar[0] == '0')
                    {
                        transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[1] == '0')
                    {
                        transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[2] == '0')
                    {
                        transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                    }
                    if (levelID != 1&& levelID!=2&& levelID != 4)
                    {
                        //Debug.Log("update status");
                        NetworkMgr.My.UpdateUnlockStatus(newStatus);
                    }
                    
                }
                else
                {
                    transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                    if (currentStar[0] == '0')
                    {
                        transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[1] == '0')
                    {
                        transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                    }
                    if (currentStar[2] == '0')
                    {
                        transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                    }
                }
                }
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
                if (currentStar[0] == '0')
                {
                    transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
                }
                if (currentStar[1] == '0')
                {
                    transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
                }
                if (currentStar[2] == '0')
                {
                    transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
                }
                if (levelID == 4)
                {
                    // 判断开启天赋教学
                    if (NetworkMgr.My.playerDatas.unlockStatus.Split('_')[levelID - 1].Equals("0"))
                    {
                        // 开启教学
                        //GuideManager.My.currentGuideIndex = 2;
                        MapGuideManager.My.GetComponent<MapObject>().openCG.SetActive(true);
                        // 上传关卡解锁状态
                        //string[] arr = NetworkMgr.My.playerDatas.unlockStatus.Split('_');
                        //arr[levelID - 1] = "1";
                        //string newStatus = "1";
                        //for (int i = 1; i < arr.Length; i++)
                        //{
                        //    newStatus += "_"+arr[i];
                        //}
                        //NetworkMgr.My.UpdateUnlockStatus(newStatus, () => {

                        //}, () => {
                        //    HttpManager.My.ShowTip("解锁关卡出错");
                        //});
                    }
                }
            }

            
        }
        LevelButton.onClick.RemoveAllListeners();
        LevelButton.onClick.AddListener(Init);
    }
    
    /// <summary>
    /// for level 9
    /// </summary>
    /// <param name="isOpen"></param>
    /// <param name="stars"></param>
    public void InitLevel(bool isOpen, string currentStar)
    {
        if (isOpen)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelUnlockImage;
            if (currentStar[0] == '0')
            {
                transform.Find("Star_0").GetChild(0).gameObject.SetActive(false);
            }
            if (currentStar[1] == '0')
            {
                transform.Find("Star_1").GetChild(0).gameObject.SetActive(false);
            }
            if (currentStar[2] == '0')
            {
                transform.Find("Star_2").GetChild(0).gameObject.SetActive(false);
            }
            stars = currentStar;
            LevelButton.onClick.RemoveAllListeners();
            LevelButton.onClick.AddListener(Init);
        }
        else
        {
            HideAllStars();
            transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
        }
    }

    ///// <summary>
    ///// 初始化关卡星数需求文字颜色
    ///// </summary>
    //public void InitStarNeedText()
    //{
    //    starNeed.text = actualStarRequirement.ToString();
    //    starNeed.color = CheckPrevStar() ? Color.green : Color.red;
    //}

    /// <summary>
    /// 检测之前所有关卡的星数有没有达到要求
    /// </summary>
    /// <returns></returns>
    //public bool CheckPrevStar()
    //{
    //    int index = int.Parse(loadScene.Split('_')[1]);
    //    int result = 0;
    //    foreach (LevelProgress l in NetworkMgr.My.levelProgressList)
    //    {
    //        if (l.levelID < index)
    //        {
    //            result += l.starNum;
    //        }
    //    }
    //    return result >= actualStarRequirement;
    //}

    public bool CheckUserLevel()
    {
        return NetworkMgr.My.playerLimit == 0 ? true : NetworkMgr.My.playerLimit >= levelID ;
    }
}
