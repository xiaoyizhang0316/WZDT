using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
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

    public GameObject panel;

    public Button play;
    public Button close;

    public Toggle isUseGuide;

    public Action loadScene;

    public Sprite levelLockImage;
    public Sprite box1OpenedImage;
    public Sprite box2OpenedImage;
    public Sprite box3OpenedImage;
    public Sprite box1CloseImage;
    public Sprite box2CloseImage;
    public Sprite box3CloseImage;

    public RecordList listScript;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        listScript.gameObject.SetActive(false);
        close.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            listScript.gameObject.SetActive(false);

        });
        play.onClick.AddListener(() => { loadScene(); });
        isUseGuide.onValueChanged.AddListener((bool b) =>
        {
            PlayerPrefs.SetInt("isUseGuide",b ? 1 : 0);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(string name,string contet,string mission_1,string mission_2,string mission_3,Action loadScene)
    {
        levelName.text = name;
        content.text = contet;
        this.mission_1.text = mission_1;
        this.mission_2.text = mission_2;
        this.mission_3.text = mission_3;
        isUseGuide.isOn = PlayerPrefs.GetInt("isUseGuide") == 1;
        panel.SetActive(true);
        listScript.gameObject.SetActive(true);
        this.loadScene = loadScene;
        InitBox(name);

    }

    void InitBox(string level)
    {
        SetBoxStatus(1, level, mission_1.transform, box1OpenedImage, box1CloseImage);
        SetBoxStatus(2, level, mission_2.transform, box2OpenedImage, box2CloseImage);
        SetBoxStatus(3, level, mission_3.transform, box3OpenedImage, box3CloseImage);
    }

    void SetBoxStatus(int boxID, string level, Transform child, Sprite opened, Sprite close)
    {
        if(PlayerPrefs.GetInt("FTE_" + level + "|"+boxID, 0) == 1)
        {
            child.parent.GetComponent<Image>().sprite = opened;
        }
        else
        {
            child.parent.GetComponent<Image>().sprite = close;
        }
    }
    // net
    public void Init(string star, string name, string contet, string mission_1, string mission_2, string mission_3, Action loadScene)
    {
        levelName.text = name;
        content.text = contet;
        this.mission_1.text = mission_1;
        this.mission_2.text = mission_2;
        this.mission_3.text = mission_3;
        panel.SetActive(true);
        this.loadScene = loadScene;
        InitBoxs(star);
        listScript.gameObject.SetActive(true);
    }

    void InitBoxs(string star)
    {
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
}
