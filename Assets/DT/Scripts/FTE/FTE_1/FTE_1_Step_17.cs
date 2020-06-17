﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_17 : BaseStep
{
    // Start is called before the first frame update
    void Start()
    {
        contenText.color = new Color(1,1,1,0);
    //    nextButton.onClick.AddListener(() => { StopCurrentStep(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
     
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 3f).OnComplete(() =>
            {
                MaskManager.My.Open(14,130);
                MaskManager.My.FadeMask(9,130);
                FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                PlayNext();
            }).Play(); 
        }).Play(); 

    }

    public override void StopCurrentStep()
    {
        nextButton.interactable = false;  
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;

        MaskManager.My.Close(14,0 );
        MaskManager.My.CloseMaks(9 );
        contenText.DOFade(0, 2f).OnComplete(() =>
        {
            gameObject.SetActive(false); 
            FTESceneManager.My.PlayNextStep();

        }).Play(); 
    }
    public void PlayNext()
    {
        Debug.Log("检测");
        if (!MapManager.My.GetMapSignByXY(11, 22).isCanPlace)
        {
            Debug.Log("检测成功");

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            {
                Debug.Log("检测失败");

                PlayNext();
            }).Play();
        }
    }
}
