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
        contenText.color = new Color(1,1,1,0);
        nextButton.onClick.AddListener(() =>
        {
            PlayNext();
        });
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public override void StartCuttentStep()
    {
     //   MaskManager.My.Open(1);
     
        MaskManager.My.Open(7,130);
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 3f).OnComplete(() =>
            {
         
                 
            }).Play(); 
        }).Play(); 
        
    }

    public override void StopCurrentStep()
    { 
   
    
   
       contenText.DOFade(0, 2f).OnComplete(() =>
       {
           gameObject.SetActive(false); 
          FTESceneManager.My.PlayNextStep();

       }).Play(); 
      // MaskManager.My .Close(1);
    }

    public void PlayNext()
    {
        Debug.Log("检测");
        if (!MapManager.My.GetMapSignByXY(9, 18).isCanPlace)
        {
            Debug.Log("检测成功");

            StopCurrentStep();
            nextButton.interactable = false;
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
