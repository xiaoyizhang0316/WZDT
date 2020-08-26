﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGuideStep : MonoBehaviour
{
    /// <summary>
    /// 当前索引
    /// </summary>
    public int currentStepIndex;
    /// <summary>
    /// 是否开启当前步骤
    /// </summary>
    public bool isOpen;
    /// <summary>
    /// 文本框
    /// </summary>
    public List<Text> contentText;
    /// <summary>
    /// 下一步按钮
    /// </summary>
    public Button endButton;
    /// <summary>
    /// 高亮区域
    /// </summary>
    public List<DarkEffect.Item> Camera3DTarget;

    /// <summary>
    /// 需要高亮的UI元素
    /// </summary>
    public List<GameObject> highLight2DObjList;
    
    
 

    /// <summary>
    /// 需要高亮的UI元素复制
    /// </summary>
    public List<GameObject> highLightCopyObj;

    /// <summary>
    /// 需要检测
    /// </summary>
    public bool needCheck;

    /// <summary>
    /// 该步等待时间
    /// </summary>
    [SerializeField]
    private float entryTime = 1f;

    public void OpenFade()
    {
        GuideManager.My.darkEffect._darkColor = new Color(0, 0, 0, 0.4f);
    }

    public void CloseFade()
    {
        GuideManager.My.darkEffect._darkColor = new Color(0, 0, 0, 0f);
    }

    public IEnumerator OpenHighLight()
    {
        OpenFade();
        for (int i = 0; i < Camera3DTarget.Count; i++)
        {
            StartCoroutine(OpenOneHighLight(Camera3DTarget[i]));
        }

        while (!CheckAllHighLightRight())
        {
            yield return null;
        }
    }


    /// <summary>
    /// 检查是否所有高亮区域都打开
    /// </summary>
    /// <returns></returns>
    public bool CheckAllHighLightRight()
    {
        int count = 0;
        for (int i = 0; i < Camera3DTarget.Count; i++)
        {
            if (Camera3DTarget[i].radius >= Camera3DTarget[i].EndRandius)
            {
                count++;
            }
        }

        if (count == Camera3DTarget.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 打开一个高亮区域
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private IEnumerator OpenOneHighLight(DarkEffect.Item item)
    {
        yield return new WaitForSeconds(item.waitTime);
        for (int i = 0; item.radius < item.EndRandius; i++)
        {
            item.radius+= item.speed;
            yield return null;
        }
    }

    /// <summary>
    /// 开始的时候添加高亮区域到DarkEffect中
    /// </summary>
    /// <param name="highLight"></param>
    /// <param name="count"></param>
    private void AddHighLight(DarkEffect.Item highLight, int count)
    {
        if (GuideManager.My.darkEffect._items.Contains(highLight))
        {
            return;
        }

        GuideManager.My.darkEffect._items[count] = highLight;
    }


    public void CloseHighLight()
    {
        CloseFade();
    }

    /// <summary>
    /// 初始化所有需要高亮的UI元素
    /// </summary>
    public void ShowAllHighlightUI()
    {
        foreach (var VARIABLE in GetComponentsInChildren<BaseTween>())
        {
            VARIABLE.Move();
        }
        if (highLight2DObjList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < highLight2DObjList.Count; i++)
        {
            GameObject go = Instantiate(highLight2DObjList[i], transform);
            go.transform.position = highLight2DObjList[i].transform.position;
            go.transform.SetAsFirstSibling();
            highLightCopyObj.Add(go);
        }

     
        
    }


    public virtual void InitHighlightUI()
    {
        
    }

    public virtual void InitHighlight3d()
    {
        
    }

    public IEnumerator Play()
    {
        Debug.Log("开始当前步骤"+GuideManager.My.currentGuideIndex);
        if (!isOpen)
        {
            GuideManager.My.PlayNextIndexGuide();
        }
        else
        {
            if (!needCheck)
            {
                endButton.interactable = false;

                if (endButton != null && endButton.gameObject.activeSelf)
                {
                    endButton.onClick.AddListener(() =>
                    {
                        endButton.interactable = false;
                        StartCoroutine(PlayEnd());

                    });
                }
            }
            InitHighlight3d();
            for (int i = 0; i < Camera3DTarget.Count; i++)
            {
                AddHighLight(Camera3DTarget[i], i);
            }

  
            yield return OpenHighLight();
            InitHighlightUI();
            ShowAllHighlightUI();
            Debug.Log("start start");
            yield return StepStart();
            
            yield return new WaitForSeconds(entryTime);
            
            if (needCheck)
            {
                while (!ChenkEnd())
                {
                    Debug.Log("当前步骤"+GuideManager.My.currentGuideIndex+"检测中");
                    yield return null;
                }
                Debug.Log("当前步骤"+GuideManager.My.currentGuideIndex+"检测成功");

              StartCoroutine(PlayEnd());
            }

            else
            {
                endButton.interactable = true;
            }
            afterEntry?.Invoke();
        }

    }

    public abstract IEnumerator StepStart();
    public abstract IEnumerator StepEnd();


    /// <summary>
    /// 检查
    /// </summary>
    /// <returns></returns>
    public virtual bool ChenkEnd()
    {
        return true;
    }

    public IEnumerator PlayEnd()
    {
    Debug.Log("结束当前步骤"+GuideManager.My.currentGuideIndex);
        yield return StepEnd();
        for (int i = 0; i < highLightCopyObj.Count; i++)
        {
            Destroy(highLightCopyObj[i], 0.1f);
        }
        CloseHighLight();

        GuideManager.My.PlayNextIndexGuide();
    }

    public Action afterEntry; 
}