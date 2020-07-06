using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_3_Step_1 : BaseStep
{
     
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        NewCanvasUI.My.GamePause();
         MaskManager.My.Open(0,130);
         contenText.color = new Color(1,1,1,0);
               contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
             
                 nextButton.interactable = true; 
             
             }).Play(); 
        

    }

    public override void StopCurrentStep()
    {
       RoleListManager.My.OutButton();
       nextButton.interactable = false; 
       MaskManager.My.Close(0,0 );
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
