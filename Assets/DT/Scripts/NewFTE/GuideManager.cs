using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        for (int i = 0; i <baseGuideSteps.Count; i++)
        {
            baseGuideSteps[i].gameObject.SetActive(false);
        }

        for (int i = 0; i <darkEffect._items.Count; i++)
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

    // Start is called before the first frame update
    void Start()
    {
      if (currentGuideIndex >= 0 && PlayerPrefs.GetInt("isUseGuide") == 1)
      {
          currentGuideIndex = 0;
        
      }

      else
      {
          currentGuideIndex = -1;
          CloseFTE();
      }

      PlayCurrentIndexGuide();
    }


    public void CloseFTE()
    {
        ftegob.SetActive(false);
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
}
