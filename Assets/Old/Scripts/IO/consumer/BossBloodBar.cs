using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossBloodBar : IOIntensiveFramework.MonoSingleton.MonoSingleton<BossBloodBar>
{

    public Image barBG;
    public Text killCount;
    public Image bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKillCount(int count )
    {
        killCount.text = count.ToString();
    }

    public void SetBar(float fillAmount,Action end=null)
    {
        bar.DOFillAmount(fillAmount,0.3f).OnComplete(() =>
        {
            if(end!=null)
            end();
        });
    }

    public void ChangeColor(Color color)
    {
      //  barBG.color = bar.color;
        bar.color = color;
    }
}
