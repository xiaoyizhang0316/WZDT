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
    }
}
