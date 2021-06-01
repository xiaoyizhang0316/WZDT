using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoManager : MonoSingleton<LevelInfoManager>
{
    public Text levelName;

    public Text content;

    public Text mission_1;
    public Text mission_2;
    public Text mission_3;

    public Text starNeed;

    public GameObject panel;

    public Button play;
    public Button close;

    public Toggle isUseGuide;

    public Toggle cheat1;
    public Toggle cheat2;
    public Toggle cheat3;

    public GameObject cheatPanel;

    public GameObject cheatDesc;

    public Action loadScene;

    public Sprite levelLockImage;
    public Sprite levelUnlockImage;
    public Sprite box1OpenedImage;
    public Sprite box2OpenedImage;
    public Sprite box3OpenedImage;
    public Sprite box1CloseImage;
    public Sprite box2CloseImage;
    public Sprite box3CloseImage;

    public RecordList listScript;

    public GameObject rankPanel;

    public Toggle conduty;

    public GameObject missionConfirm;
    public string currentSceneName = "";

    public Transform rtPanel;
    //public bool stepOver = false;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        //stepOver = false;
        listScript.gameObject.SetActive(false);
        rankPanel.SetActive(false);
        close.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            listScript.gameObject.SetActive(false);
            rankPanel.SetActive(false);
            rtPanel.GetComponent<RTPanel>().rank_btn.gameObject.SetActive(false);
            //rtPanel.GetComponent<RTPanel>().Close();
        });
        play.onClick.AddListener(() =>
        {
            if (PlayerData.My.server != null)
            {
                string str1 = "ConfirmDuty|";
             //  if (conduty.isOn)
             //  {
                    str1 += "1,0,0,1,1,1,1,1"; 
                      NetManager.My.ConfirmDuty("1,0,0,1,1,1,1,1");
           //    }
           //    else
           //    {
           //        str1 += "0,1,1,0,0,0,0,0"; 
           //        NetManager.My.ConfirmDuty( "0,1,1,0,0,0,0,0");

           //    } 
                 
                 PlayerData.My.server.SendToClientMsg(str1);
                transform.DORotate(Vector3.zero,0.5f ).Play().OnComplete(()=>{
                    PlayerData.My.server.SendToClientMsg("OpenDutyConfirmUI|1");
                    missionConfirm.SetActive(true);
                });
         
            }
            else
            {
                if (PlayerData.My.isSOLO)
                {
                    
                loadScene(); 
                }
                //if (currentSceneName.Equals("FTE_2"))
                //{
                //    stepOver = true;
                //}
            }
            
            
        });
        isUseGuide.onValueChanged.AddListener((bool b) =>
        {
            //print(b);
            PlayerPrefs.SetInt("isUseGuide", b ? 1 : 0);
        });
        cheat1.onValueChanged.AddListener((bool b) =>
        {
            PlayerData.My.cheatIndex1 = b;
            CheckCheat();
        });
        cheat2.onValueChanged.AddListener((bool b) =>
        {
            PlayerData.My.cheatIndex2 = b;
            CheckCheat();
        });
        cheat3.onValueChanged.AddListener((bool b) =>
        {
            PlayerData.My.cheatIndex3 = b;
            CheckCheat();
        });
    }

    public void Init(string name, string contet, string mission_1, string mission_2, string mission_3, Action loadScene)
    {
        levelName.text = name;
        content.text = contet;
        this.mission_1.text = mission_1;
        this.mission_2.text = mission_2;
        this.mission_3.text = mission_3;
        isUseGuide.isOn = PlayerPrefs.GetInt("isUseGuide") == 1;
        panel.SetActive(true);
        //listScript.gameObject.SetActive(true);
        rankPanel.SetActive(true);
        this.loadScene = loadScene;
        InitBox(name);

    }

    public void Close()
    {
        panel.SetActive(false);
        listScript.gameObject.SetActive(false);
        rankPanel.SetActive(false);
        rtPanel.GetComponent<RTPanel>().rank_btn.gameObject.SetActive(false);
    }

    void InitBox(string level)
    {
        SetBoxStatus(1, level, mission_1.transform, box1OpenedImage, box1CloseImage);
        SetBoxStatus(2, level, mission_2.transform, box2OpenedImage, box2CloseImage);
        SetBoxStatus(3, level, mission_3.transform, box3OpenedImage, box3CloseImage);
    }

    void SetBoxStatus(int boxID, string level, Transform child, Sprite opened, Sprite close)
    {
        if (PlayerPrefs.GetInt("FTE_" + level + "|" + boxID, 0) == 1)
        {
            child.parent.GetComponent<Image>().sprite = opened;
        }
        else
        {
            child.parent.GetComponent<Image>().sprite = close;
        }
    }
    // net
    public void Init(string star, string name, string contet, string mission_1, string mission_2, string mission_3, Action loadScene, string sceneName)
    {
        //print(sceneName);
        //if (int.Parse(sceneName.Split('_')[1]) >= 5 && int.Parse(sceneName.Split('_')[1]) <= 8)
        //    isUseGuide.gameObject.SetActive(false);
        isUseGuide.gameObject.SetActive(true);
        currentSceneName = sceneName;
        /*if (star[0] == '1')
        {
            PlayerPrefs.SetInt("isUseGuide", 0);
            isUseGuide.interactable = true;
        }
        else
        {
            if(NetworkMgr.My.playerDatas.fteProgress>= int.Parse(sceneName.Split('_')[1]))
            {
                PlayerPrefs.SetInt("isUseGuide", 0);
                isUseGuide.interactable = true;
            }
            else
            {
                PlayerPrefs.SetInt("isUseGuide", 1);
                isUseGuide.interactable = true;
            }
        }*/

        /*if (float.Parse(sceneName.Split('_')[1]) <= float.Parse(NetworkMgr.My.playerDatas.fte))
        {
            PlayerPrefs.SetInt("isUseGuide", 0);
            isUseGuide.interactable = true;
        }
        else
        {
            PlayerPrefs.SetInt("isUseGuide", 1);
            isUseGuide.interactable = false;
        }
        isUseGuide.isOn = PlayerPrefs.GetInt("isUseGuide") == 1;*/
        if (int.Parse(sceneName.Split('_')[1]) == 1 || int.Parse(sceneName.Split('_')[1]) == 9)
        {
            cheatPanel.SetActive(false);
        }
        else
        {
            cheatPanel.SetActive(true);
            cheat1.isOn = PlayerData.My.cheatIndex1;
            cheat2.isOn = PlayerData.My.cheatIndex2;
            cheat3.isOn = PlayerData.My.cheatIndex3;
            CheckCheat();
        }

        levelName.text = name;
        content.text = contet;
        this.mission_1.text = mission_1;
        this.mission_2.text = mission_2;
        this.mission_3.text = mission_3;
        panel.SetActive(true);
        this.loadScene = loadScene;
        InitBoxs(star);
        //listScript.gameObject.SetActive(true);
        rankPanel.SetActive(true);
        rtPanel.GetComponent<RTPanel>().InitRTPanel();
    }

    void InitBoxs(string star)
    {
        if (string.IsNullOrEmpty(star))
        {
            return;
        }
        SetBoxsStatus(star[0], mission_1.transform, box1OpenedImage, box1CloseImage);
        SetBoxsStatus(star[1], mission_2.transform, box2OpenedImage, box2CloseImage);
        SetBoxsStatus(star[2], mission_3.transform, box3OpenedImage, box3CloseImage);
    }

    void SetBoxsStatus(char pos, Transform child, Sprite opened, Sprite close)
    {
        if (pos == '1')
        {
            child.parent.GetComponent<Image>().sprite = opened;
        }
        else
        {
            child.parent.GetComponent<Image>().sprite = close;
        }
    }

    public void CheckCheat()
    {
        if (cheat1.isOn || cheat2.isOn || cheat3.isOn)
        {
            cheatDesc.SetActive(true);
        }
        else
        {
            cheatDesc.SetActive(false);
        }
    }
}
