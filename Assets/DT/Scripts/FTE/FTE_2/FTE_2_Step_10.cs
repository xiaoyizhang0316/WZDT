using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_10 : BaseStep
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
        
       
         MaskManager.My.Open(10,80); 
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
        CreatRoleManager.My.QuitAndSave();
       //NewCanvasUI.My.Panel_AssemblyRole.SetActive(false);
       MaskManager.My.Close(10,0); 
       
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
   
}
