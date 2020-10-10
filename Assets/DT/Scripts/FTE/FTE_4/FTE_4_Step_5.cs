using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_4_Step_5 : BaseStep
{
    public GameObject UI;
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
        MaskManager.My.Open(1,80); 

         contenText.DOFade(0, 0).OnComplete(() => { 
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
                 
                 StopCurrentStep();
                 //  PlayNext();
             }).Play(); 
         }).Play();  
    }
 
    public override void StopCurrentStep()
    {
       
       nextButton.interactable = false;  
      

       contenText.DOFade(0, 0.5f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
