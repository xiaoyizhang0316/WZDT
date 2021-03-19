using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_25 : BaseStep
{
    void Start()
    {
        
        contenText.color = new Color(1,1,1,0);
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public override void StartCuttentStep()
    {
        //   MaskManager.My.Open(1);
        nextButton.interactable = false;
        MaskManager.My.Open(20,80);
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
                nextButton.interactable = true;
                FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                PlayNext();
            }).Play(); 
        }).Play(); 
        
    }

    public override void StopCurrentStep()
    { 
        nextButton.interactable = false;  
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;

        MaskManager.My.Close(20,0 );
        contenText.DOFade(0, 0.8f).OnComplete(() =>
        {
            gameObject.SetActive(false); 
            FTESceneManager.My.PlayNextStep();

        }).Play(); 
        // MaskManager.My .Close(1);
    }
    public void PlayNext()
    {
        //Debug.Log("检测");
        if (WaveCount.My.waveBg.childCount>0)
        {
            //Debug.Log("检测成功");

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            {
                //Debug.Log("检测失败");

                PlayNext();
            }).Play();
        }
    }
}
 
