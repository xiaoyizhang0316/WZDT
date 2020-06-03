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

    //public Sprite lockImage;
    // Start is called before the first frame update
    void Start()
    {
        LevelButton.onClick.AddListener(() =>
        {
            Debug.Log("1231231231");
            Init();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        LevelInfoManager.My.Init(levelName,content,mission_1,mission_2,mission_3, () =>
            {
                SceneManager.LoadScene(loadScene);
            });
    }

    void InitLevel()
    {
        string[] strArr = loadScene.Split('_');
        int level = int.Parse(strArr[1]);
        
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
}
