using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideManager : IOIntensiveFramework.MonoSingleton.MonoSingleton<GuideManager>
{
    /// <summary>
    /// 高亮
    /// </summary>
    public DarkEffect darkEffect;

    /// <summary>
    /// 所有步骤合集
    /// </summary>
    public List<BaseGuideStep> baseGuideSteps = new List<BaseGuideStep>();

    /// <summary>
    /// 当前引导步骤
    /// </summary>
    public int currentGuideIndex;

    public GameObject ftegob;

    
    
    public void PlayCurrentIndexGuide()
    {

        for (int i = 0; i < baseGuideSteps.Count; i++)
        {
            baseGuideSteps[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < darkEffect._items.Count; i++)
        {
            darkEffect._items[i].radius = 0;
        }
        if (currentGuideIndex == -1)
        {
            //当前没有新手引导
            CloseFTE();
            return;
        }

        if (currentGuideIndex >= baseGuideSteps.Count)
        {
            CloseFTE();
            return;
        }


        baseGuideSteps[currentGuideIndex].gameObject.SetActive(true);

        StartCoroutine(baseGuideSteps[currentGuideIndex].Play());
    }

    public void Init()
    {
        if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2")
        {
            currentGuideIndex = 0;
        }
        else if (currentGuideIndex >= 0 && PlayerPrefs.GetInt("isUseGuide") == 1)
        {
            currentGuideIndex = 0;
            NewCanvasUI.My.GamePause(false);
        }
        else
        {
            currentGuideIndex = -1;
            CloseFTE();
        }
        foreach (var item in NewCanvasUI.My.highLight)
        {
            item.SetActive(false);
        }
        PlayCurrentIndexGuide();
    }

    // Start is called before the first frame update
    void Start()
    {
        Prologue temp = FindObjectOfType<Prologue>();
        if (temp == null)
            Init();
    }


    public void CloseFTE()
    {
        ftegob.SetActive(false);
        foreach (var item in NewCanvasUI.My.highLight)
        {
            item.SetActive(true);
        }

        foreach (var VARIABLE in  darkEffect._items)
        {
            VARIABLE.radius = 0;
        }
        darkEffect._darkColor =new Color(1,1,1,0);
        foreach (var VARIABLE in MapManager.My._mapSigns)
        {
            if (VARIABLE.mapType == GameEnum.MapType.Grass && VARIABLE.baseMapRole == null)
            {
                VARIABLE.isCanPlace = true;

            }

        }

    }

    public void PlayNextIndexGuide()
    {
        currentGuideIndex++;
        PlayCurrentIndexGuide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BornEnemy()
    {
        Debug.Log("born");
        StartCoroutine(GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().BornEnemy());
    }
}
