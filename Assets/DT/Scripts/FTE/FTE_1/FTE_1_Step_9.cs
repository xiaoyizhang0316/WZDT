using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_9 : BaseStep
{
     
    // Start is called before the first frame update
    void Start()
    {
      //  nextButton.onClick.AddListener(() => { StopCurrentStep(); });
       
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public override void StartCuttentStep()
    {
        nextButton.interactable = false;
     //   MaskManager.My.Open(1);
     contenText.color = new Color(1,1,1,0);
     nextButton.onClick.AddListener(() =>
     {
         PlayNext();
     });
        MaskManager.My.Open(7,130);
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
                nextButton.interactable = true;

                 
            }).Play(); 
        }).Play(); 
        
    }

    public override void StopCurrentStep()
    { 
   
        nextButton.interactable = false;
   
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
        if (!MapManager.My.GetMapSignByXY(9, 18).isCanPlace)
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
