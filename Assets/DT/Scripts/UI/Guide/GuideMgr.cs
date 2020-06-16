using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;

public class GuideMgr : MonoSingleton<GuideMgr>
{
    public bool isOnGuide = false;

    private List<Action> actions;
    //private List<float> waitTimes;


    public Button invalidClickBtn;
    public Text tip;
    public List<GuidePanel> guidePanels;
    public GuideActions guideActions;

    public int currentGuidePanel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        invalidClickBtn.onClick.RemoveAllListeners();
        invalidClickBtn.onClick.AddListener(InvalidClick);
        guideActions.InitAllActions();
        InitGuide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGuide()
    {
        // 判断当前教程进行到第几个场景 currentGuidePanel

        // 如果判断当前场景教程进行完毕 则结束教程
        //GuideEnd();
        isOnGuide = true;
        // 对各步骤赋值 actions,waitTimes
        actions = guideActions.actionsList[currentGuidePanel];
        // 初始化该新手引导场景
        InitGuidePanel(actions/*, waitTimes*/);
        invalidClickBtn.interactable = true;
    }

    void InitGuidePanel(List<Action> actions/*, List<float> waitTimes*/)
    {
        foreach(Transform child in transform)
        {
            GuidePanel gd = child.GetComponent<GuidePanel>();
            if (gd != null)
            {
                guidePanels.Add(gd);
            }
        }
        
        guidePanels[currentGuidePanel].actions = actions;
       // guidePanels[currentGuidePanel].waitTime = waitTimes;
        guidePanels[currentGuidePanel].gameObject.SetActive(true);
    }

    public void ShowNextStep()
    {
        guidePanels[currentGuidePanel].ShowNextStep();
    }

    public void GuideEnd()
    {
        transform.GetComponent<Image>().raycastTarget = false;
        invalidClickBtn.interactable = false;
        isOnGuide = false;
    }

    protected void InvalidClick()
    {
        if (tip.gameObject.activeInHierarchy)
        {
            DOTween.Kill("tip");
            tip.DOFade(1, 0.02f);
        }
        tip.text = "请跟随指示点击";
        tip.gameObject.SetActive(true);

        tip.DOFade(0, 1f).SetId("tip").OnComplete(() => {
            tip.gameObject.SetActive(false);
            tip.DOFade(1, 0.01f);
        });
    }
}
