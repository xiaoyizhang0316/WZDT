using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_4_Step_8 : BaseStep
{
     
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
 
        Debug.Log(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
 
 
         MaskManager.My.Open(2,94);
         Debug.Log(2);

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
