﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FTE_1_5_Dialogue : BaseGuideStep
{
    public GameObject img;
    public Transform dialogTexts;
    private int currentDialog = 0;
    public override IEnumerator StepStart()
    {
        //CameraPlay.WidescreenH_ON(Color.black, 1);
        if (GetComponent<FTE_DialogDoBase>())
        {
            GetComponent<FTE_DialogDoBase>().DoStart();
        }
        yield return new WaitForSeconds(0.5f);
        currentDialog = 0;
        ShowImgAndTxt();
    }

    public override IEnumerator StepEnd()
    {
        yield return new WaitForSeconds(0.5f);
        if (GetComponent<FTE_DialogDoBase>())
        {
            GetComponent<FTE_DialogDoBase>().DoEnd();
        }
    }

    void ShowImgAndTxt()
    {
        if (img != null)
        {
            img.SetActive(true);
            img.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-700, -285), 0.3f).Play();
        }

        endButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -390), 0.3f).Play().OnComplete(() =>
        {
            if (dialogTexts.childCount>0)
            {
                dialogTexts.GetChild(0).gameObject.SetActive(true);
            }
        });
    }

    void Restore()
    {
        currentDialog = 0;
        img.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1200, -285), 0.01f);
        endButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -1000), 0.01f);
    }

    public override IEnumerator PlayEnd()
    {
        if (dialogTexts.GetChild(currentDialog).GetComponent<FTE_TypeWord>().isTyping)
        {
            dialogTexts.GetChild(currentDialog).GetComponent<FTE_TypeWord>().StopType();
           // Debug.Log("typing");
            endButton.interactable = true;
            yield break;
        }
        else
        {
            currentDialog++;
            if (currentDialog < dialogTexts.childCount)
            {
                dialogTexts.GetChild(currentDialog-1).gameObject.SetActive(false);
                dialogTexts.GetChild(currentDialog).gameObject.SetActive(true);
                //Debug.Log("next");
                endButton.interactable = true;
                yield break;
            }
        }
        //CameraPlay.WidescreenH_OFF();
        Restore();
        //Debug.Log("结束当前步骤"+GuideManager.My.currentGuideIndex);
        yield return StepEnd();
        for (int i = 0; i < highLightCopyObj.Count; i++)
        {
            //Destroy(highLightCopyObj[i], 0f);
            highLightCopyObj[i].SetActive(false);
        }
        CloseHighLight();

        GuideManager.My.PlayNextIndexGuide();
    }
}
