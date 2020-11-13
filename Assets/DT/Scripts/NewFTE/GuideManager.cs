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

    public CloseGuide guideClose;
    
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
        if (!PlayerData.My.isSOLO)
        {
            string str1 = "OpenGuide|true";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
        }
        baseGuideSteps[currentGuideIndex].gameObject.SetActive(true);

        StartCoroutine(baseGuideSteps[currentGuideIndex].Play());
    }

    public virtual void Init()
    {
        if (!PlayerData.My.isSOLO && PlayerData.My.creatRole != PlayerData.My.playerDutyID)
        {
            FindObjectOfType<CloseGuide>().gameObject.SetActive(false);
            return;
        }
            foreach (var item in NewCanvasUI.My.highLight)
            {
                item.SetActive(false);
            }
        
        if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2")
        {
            currentGuideIndex = 0;
            PlayerPrefs.SetInt("isUseGuide", 1);
        }
        else if (currentGuideIndex >= 0 && PlayerPrefs.GetInt("isUseGuide") == 1)
        {
            currentGuideIndex = 0;
            
                NewCanvasUI.My.GamePause(false);
                guideClose.Init();

            
        }
        else
        {
            currentGuideIndex = -1;
            CloseFTE();
            
                guideClose.Init();
            
        }
        PlayCurrentIndexGuide();

    }

    // Start is called before the first frame update
    void Start()
    {
        Prologue temp = FindObjectOfType<Prologue>();
        //if(SceneManager.GetActiveScene().name=="FTE_0-1"|| SceneManager.GetActiveScene().name == "FTE_0-2")
        //{
        //    return;
        //}
        if (temp == null)
            Init();
    }


    public virtual void CloseFTE()
    {
        ftegob.SetActive(false);
        currentGuideIndex = -1;
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
        if (!PlayerData.My.isSOLO)
        {
            string str1 = "OpenGuide|false";
            if (PlayerData.My.isServer)
            {
                PlayerData.My.server.SendToClientMsg(str1);
            }
            else
            {
                PlayerData.My.client.SendToServerMsg(str1);
            }
        }
        NewCanvasUI.My.Panel_Update.transform.localPosition = Vector3.one;
        if(guideClose!=null)
            guideClose.gameObject.SetActive(false);
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
