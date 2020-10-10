using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoSingleton<TimeManager>
{
    public float currentTime;

    public int maxTime;

    public int month;
    public Action timeOver;

    public float cumulativeTime;
    /// <summary>
    /// 小时
    /// </summary>
    public Text hour;

    /// <summary>
    /// 时间进度条
    /// </summary>
    public Image monthImage;
    public Text monthText;
    // Start is called before the first frame update
    void Start()
    {
        month = 1;
        monthText.text = month.ToString();
        Biao();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            month++;
            monthText.text = month.ToString();
            if(timeOver!=null)
             timeOver();
        }
        else
        {
            currentTime += Time.deltaTime;
            cumulativeTime +=Time.deltaTime;
        }
    
    }

    public void Biao()
    {
        monthImage.DOFillAmount( 1,maxTime).OnComplete(() => { monthImage.fillAmount = 0; }).SetEase(Ease.Linear).SetLoops(int.MaxValue);
    }


}
