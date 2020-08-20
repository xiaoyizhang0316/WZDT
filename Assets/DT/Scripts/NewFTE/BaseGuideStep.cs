using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGuideStep : MonoBehaviour
{
    /// <summary>
    /// 当前索引号
    /// </summary>
    public int currentStepIndex;

    public bool isOpen;
    public List<Text> contentText;
    public Button endButton;
    public List<DarkEffect.Item> CameraTarget;

    /// <summary>
    /// 需要检测
    /// </summary>
    public bool needCheck;


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
        for (int i = 0; i < CameraTarget.Count; i++)
        {
            StartCoroutine(OpenOneHighLight(CameraTarget[i]));
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
        for (int i = 0; i < CameraTarget.Count; i++)
        {
            if (CameraTarget[i].radius == CameraTarget[i].EndRandius)
            {
                count++;
            }
        }

        if (count == CameraTarget.Count)
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
        for (int i = 0; i < item.EndRandius; i++)
        {
            item.radius++;
            yield return new WaitForSeconds(0.01f);
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

    public IEnumerator Play()
    {
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

        for (int i = 0; i < CameraTarget.Count; i++)
        {
            AddHighLight(CameraTarget[i], i);
        }


        yield return OpenHighLight();

        yield return StepStart();
        yield return new WaitForSeconds(1);
        if (needCheck)
        {
            while (!ChenkEnd())
            {
                yield return null;
            }

            StartCoroutine(PlayEnd());
        }

        else
        {
            endButton.interactable = true;
        }
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
        
        yield return StepEnd();
        CloseHighLight();
   
        GuideManager.My.PlayNextIndexGuide();
    }
}