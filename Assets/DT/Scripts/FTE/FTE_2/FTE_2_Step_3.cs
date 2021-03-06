using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_3 : BaseStep
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
        nextButton.interactable = false;
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);

         MaskManager.My.Open(2,130);
         contenText.DOFade(0, 0).OnComplete(() =>
         {
             
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
          
                 nextButton.interactable = true;
                  
             }).Play(); 
         }).Play(); 

    }

 
    public override void StopCurrentStep()
    {
        nextButton.interactable = false;
        NewCanvasUI.My.Panel_Update.SetActive(true);
        RoleUpdateInfo.My.Init(PlayerData.My.RoleData[4]);
       MaskManager.My.Close(2,0);
       
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
   
}
