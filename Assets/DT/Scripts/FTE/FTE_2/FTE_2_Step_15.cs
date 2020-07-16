using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_15 : BaseStep
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
        
       
         MaskManager.My.Open(17,130); 
         MaskManager.My.Open(18,130); 
         MaskManager.My.Open(19,130); 
         contenText.DOFade(0, 0).OnComplete(() =>
         {
             
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
                 

                 
                
             }).Play(); 
         }).Play(); 

    }

   

    public override void StopCurrentStep()
    {

        nextButton.interactable = false;
        MaskManager.My.Close(17,0); 
       MaskManager.My.Close(18,0); 
       MaskManager.My.Close(19,0); 
       
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);
           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
   
}
