using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_3_Step_2 : BaseStep
{
     
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);

       
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
    }
}
