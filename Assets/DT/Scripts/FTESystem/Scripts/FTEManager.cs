using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IOIntensiveFramework.MonoSingleton;

public class FTEManager : MonoSingleton<FTEManager>
{
    //public Dictionary<GameObject, Transform> keys = new Dictionary<GameObject, Transform>();
    public bool isOnFTE = false;

    public int currentFTEPanel = 0;
    public int currentFTEStep = 0;

    private FTEStep[] fteSteps;
    public FTEController controller;

    public Transform mask;
    public Transform targetPanels;
    public Transform currentPanel;
    public Text tip;

    public Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        // 判断是否开启教学模式, 并设定当前教学场景和当前教学步骤 （currentFTEPanel, currentFTEStep）
        InitFTE();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitFTE()
    {
        if (mask == null)
        {
            // 获取mask 和 target 游戏对象
            mask = transform.Find("Mask");
            targetPanels = transform.Find("TargetPanel");

            // 设置mask 和target 游戏对象为可见
            mask.gameObject.SetActive(true);
            targetPanels.gameObject.SetActive(true);
            targetPanels.GetChild(currentFTEPanel).gameObject.SetActive(true);

            // 获取 fte controller组件
            controller = mask.GetComponent<FTEController>();

            // 获取当前教学场景的panel，并设置可见
            currentPanel = mask.GetChild(currentFTEPanel);
            currentPanel.gameObject.SetActive(true);
        }

        isOnFTE = true;
        
        // 初始化当前场景的教学步骤
        fteSteps = new FTEStep[mask.GetChild(currentFTEPanel).childCount];
        for(int i=0; i < mask.GetChild(currentFTEPanel).childCount; i++)
        {
            fteSteps[i] = mask.GetChild(currentFTEPanel).GetChild(i).GetComponent<FTEStep>();
        }
        Debug.LogError(fteSteps.Length);
        // 从 currentStep 开始教学
        StartCoroutine(ExcuteStep(0.5f));
    }

    public void NextStep(float waitTime = 0, Action doSomeThing = null)
    {
        currentTarget.gameObject.SetActive(false);
        ++currentFTEStep;
        doSomeThing();
       StartCoroutine(ExcuteStep(waitTime));
    }

    public void NextStep(float waitTime)
    {
        currentTarget.gameObject.SetActive(false);
        ++currentFTEStep;
        StartCoroutine(ExcuteStep(waitTime));
    }

    private IEnumerator ExcuteStep(float waitTime)
    {
        HideAllStep();
        yield return new WaitForSeconds(waitTime);
        if(currentFTEStep>=0&& currentFTEStep < fteSteps.Length)
        {
            currentTarget = fteSteps[currentFTEStep].target;
            currentTarget.gameObject.SetActive(true);
            fteSteps[currentFTEStep].ExcuteStep(controller, this.GetComponent<Canvas>());
        }
        else
        {
            // 教学结束
            FTEEnd();
        }
    }

    private void HideAllStep()
    {
        foreach(var f in fteSteps)
        {
            f.gameObject.SetActive(false);
        }
    }

    private void FTEEnd()
    {
        targetPanels.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
        tip.gameObject.SetActive(false);
        isOnFTE = false;
    }
}
