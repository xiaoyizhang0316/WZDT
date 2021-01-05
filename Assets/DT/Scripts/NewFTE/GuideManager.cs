using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Button nextButton;

    /// <summary>
    /// 查找当前教学文案
    /// </summary>
    /// <param name="step">第几步</param>
    /// <param name="index">Text索引</param>
    /// <returns></returns>
    public string GetCurrentSetpText(int step, int index)
    {
        for (int i = 0; i < OriginalData.My.fteData.datas.Count; i++)
        {
            if (OriginalData.My.fteData.datas[i].stepID == step)
            {
                if (index == 0)
                {
                    return OriginalData.My.fteData.datas[i].textContent_1;
                }

                if (index == 1)
                {
                    return OriginalData.My.fteData.datas[i].textContent_2;
                }

                if (index == 2)
                {
                    return OriginalData.My.fteData.datas[i].textContent_3;
                }

                if (index == 3)
                {
                    return OriginalData.My.fteData.datas[i].textContent_4;
                }

                if (index == 4)
                {
                    return OriginalData.My.fteData.datas[i].textContent_5;
                }
            }
        }

        Debug.Log("Setp" + step + "index" + index);
        return "";
    }

    public void PlayCurrentIndexGuide()
    {
        if (!ftegob.activeInHierarchy)
            ftegob.SetActive(true);
        for (int i = 0; i < baseGuideSteps.Count; i++)
        {
            baseGuideSteps[i].gameObject.SetActive(false);
            //if (baseGuideSteps[i].isOpen)
            //{
            //    Text[] text = baseGuideSteps[i].transform.GetComponentsInChildren<Text>();
            //    for (int j = 0; j < text.Length; j++)
            //    {
            //        if (text[j].gameObject.activeSelf && text[j].transform.parent == this.baseGuideSteps[i].transform)
            //        {
            //            if (!baseGuideSteps[i].contentText.Contains(text[j]))
            //            {
            //                baseGuideSteps[i].contentText.Add(text[j]);

            //            }

            //        }
            //    }

            //    for (int j = 0; j < baseGuideSteps[i].contentText.Count; j++)
            //    {
            //        baseGuideSteps[i].contentText[j].text = GetCurrentSetpText(i, j);
            //    }
            //    }
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

    public virtual IEnumerator Init()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(() => { PlayNextIndexGuide(); });
        }

        StartCoroutine(OriginalData.My.ReadFTETexts(SceneManager.GetActiveScene().name.Split('_')[1]));
        if (!PlayerData.My.isSOLO && PlayerData.My.creatRole != PlayerData.My.playerDutyID)
        {
            FindObjectOfType<CloseGuide>().gameObject.SetActive(false);
        }
        else
        {
            //  foreach (var item in NewCanvasUI.My.highLight)
            //  {
            //      item.SetActive(false);
            //  }

            if (SceneManager.GetActiveScene().name == "FTE_0-1" || SceneManager.GetActiveScene().name == "FTE_0-2" ||
                SceneManager.GetActiveScene().name == "FTE_0.5"
                || SceneManager.GetActiveScene().name == "FTE_1.5" || SceneManager.GetActiveScene().name == "FTE_2.5")
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

            //   while (true)
            //   {
            //       yield return null;
            //       if (OriginalData.My.fteData.datas.Count > 0)
            //       {
            //           break;
            //           
            //       }
            //   }

            PlayCurrentIndexGuide();
            yield return null;
        }
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
            StartCoroutine(Init());
    }


    public virtual void CloseFTE()
    {
        ftegob.SetActive(false);
        currentGuideIndex = -1;
        foreach (var item in NewCanvasUI.My.highLight)
        {
            item.SetActive(true);
        }

        foreach (var VARIABLE in darkEffect._items)
        {
            VARIABLE.radius = 0;
        }

        darkEffect._darkColor = new Color(1, 1, 1, 0);
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


        if (guideClose != null)
            guideClose.gameObject.SetActive(false);
    }

    public void PlayNextIndexGuide()
    {
        currentGuideIndex++;
        PlayCurrentIndexGuide();
    }

    public void PlayLastIndexGuide()
    {
        currentGuideIndex--;
        PlayCurrentIndexGuide();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void BornEnemy1(int type = -1)
    {
        Debug.Log("born");
        StartCoroutine(GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().BornEnemy1(type));
    }

    public void BornEnemy()
    {
        Debug.Log("born");
        StartCoroutine(GameObject.Find("Build/ConsumerSpot").GetComponent<Building>().BornEnemy());
    }
}