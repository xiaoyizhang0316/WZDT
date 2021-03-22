using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_4_Step_10 : BaseStep
{
 
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
 
        //Debug.Log(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
     
         MaskManager.My.Open(5,50); 
         //Debug.Log(2);

         contenText.DOFade(0, 0).OnComplete(() => { 
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
                 FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                 PlayNext();
                 nextButton.interactable = true; 
             
             }).Play(); 
         }).Play(); 

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
    public override void StopCurrentStep()
    {
    
       nextButton.interactable = false; 
   
       MaskManager.My.Close(5,0); 
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);
           FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
