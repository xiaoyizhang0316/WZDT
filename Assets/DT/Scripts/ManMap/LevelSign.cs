using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSign : MonoBehaviour
{

    public string levelName;

    public string content;

    public string mission_1;
    public string mission_2;
    public string mission_3;

    public Button LevelButton;

    public string loadScene;
    string stars="";


    //public Sprite lockImage;
    // Start is called before the first frame update
    void Start()
    {
        LevelButton.onClick.AddListener(() =>
        {
            Debug.Log("1231231231");
            Init();
        });
        if(!NetworkMgr.My.isUsingHttp)
            InitLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        if (NetworkMgr.My.isUsingHttp)
        {
            LevelInfoManager.My.Init(stars, levelName, content, mission_1, mission_2, mission_3, () =>
            {
             
                SceneManager.LoadScene(loadScene);
            });
        }
        else
        {
            LevelInfoManager.My.Init(levelName, content, mission_1, mission_2, mission_3, () =>
            {
                SceneManager.LoadScene(loadScene);
            });
        }
        
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
        if(lastStar == "000" && loadScene!="FTE_1")
        {
            HideAllStars();
            transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
            transform.GetChild(0).GetComponent<Image>().sprite = LevelInfoManager.My.levelLockImage;
        }
        else
        {
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
        LevelButton.onClick.RemoveAllListeners();
        LevelButton.onClick.AddListener(Init);
    }
}
