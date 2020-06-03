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

    public Action loadScene;

    public Sprite levelLockImage;
    public Sprite box1OpenedImage;
    public Sprite box2OpenedImage;
    public Sprite box3OpenedImage;
    public Sprite box1CloseImage;
    public Sprite box2CloseImage;
    public Sprite box3CloseImage;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        close.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            
        });
        play.onClick.AddListener(() => { loadScene(); });
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
        panel.SetActive(true);
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
}
