using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_4 : BaseStep
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
        
       
         MaskManager.My.Open(3,130);
         contenText.DOFade(0, 0).OnComplete(() =>
         {
             
             contenText.DOFade(1, 3f).OnComplete(() =>
             {
          
                 nextButton.interactable = true;
                  
             }).Play(); 
         }).Play(); 

    }

 
    public override void StopCurrentStep()
    {
        NewCanvasUI.My.Panel_AssemblyRole.SetActive(true);
        NewCanvasUI.My.Panel_Update.SetActive(false);
        CreatRoleManager.My.Open(PlayerData.My.RoleData[3]);
        nextButton.interactable = false;
       MaskManager.My.Close(3,0);
       
       contenText.DOFade(0, 2f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
   
}
