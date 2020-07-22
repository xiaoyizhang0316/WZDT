using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_10 : BaseStep
{
    public Button close;

    public Text closeText;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        close.onClick.AddListener(() =>
        {
            MaskManager.My.CloseMaks(20); 
            NewCanvasUI.My.Panel_Update.SetActive(false);
            closeText.gameObject.SetActive(false);

            gameObject.SetActive(false);
            
            transform.transform.DOScale(1, 1.5f).OnComplete(() => { FTESceneManager.My.PlayNextStep(); }).Play();

        });
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        close.interactable = false;
       
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
       
       
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           CreatRoleManager.My.QuitAndSave();
           //NewCanvasUI.My.Panel_AssemblyRole.SetActive(false);
           MaskManager.My.Close(10,0); 
           MaskManager.My.Open(20,80); 
         NewCanvasUI.My.Panel_Update.SetActive(true);
         closeText.gameObject.SetActive(true);
         contenText.gameObject.SetActive(false);
         close.interactable = true;
         RoleUpdateInfo.My.Init(PlayerData.My.RoleData[4]);
          // FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
   
}
