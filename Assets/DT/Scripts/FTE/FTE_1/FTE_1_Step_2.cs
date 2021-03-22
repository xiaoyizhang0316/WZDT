using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_2 : BaseStep
{
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
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
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
        MaskManager.My.Open(0,118);
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
                //Debug.Log("123123123");
                nextButton.interactable = true; 
             
            }).Play(); 
        }).Play();  
    }

    
    public override void StopCurrentStep()
    { 
        //Debug.Log("结束第二部");
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;

        nextButton.interactable = false; 
        MaskManager.My.Close(0,0);
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);
     
          FTESceneManager.My.PlayNextStep();

       }).Play(); 
      // MaskManager.My .Close(1);
    }
}
