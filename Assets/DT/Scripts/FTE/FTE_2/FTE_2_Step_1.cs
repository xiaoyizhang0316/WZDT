using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_Step_1 : BaseStep
{
     
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,1);
        NewCanvasUI.My.GamePause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
   
         MaskManager.My.Open(0,80);
         contenText.DOFade(0, 0).OnComplete(() => { 
             contenText.DOFade(1, 3f).OnComplete(() =>
             {
             
                 nextButton.interactable = true; 
             
             }).Play(); 
         }).Play(); 

    }

    public override void StopCurrentStep()
    {
       RoleListManager.My.OutButton();
       nextButton.interactable = false; 
       MaskManager.My.Close(0,0 );
       contenText.DOFade(0, 2f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
