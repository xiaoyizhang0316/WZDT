using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_4_Step_9 : BaseStep
{
    public Building build;
    // Start is called before the first frame update
    void Start()
    {
    //    nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
   
       
          
         MaskManager.My.Open(4,130); 
         contenText.DOFade(0, 0).OnComplete(() => { 
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
                 FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                 nextButton.interactable = true; 
              
                 PlayNext();
             }).Play(); 
         }).Play(); 

    }
    public void PlayNext()
    {
        //Debug.Log("检测");
        if (build.isUseTSJ)
        {
            //Debug.Log("检测成功");

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            {
                //Debug.Log("检测失败");

                PlayNext();
            }).Play();
        }
    }
    public override void StopCurrentStep()
    {
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;
        
        for (int i = 0; i <     PlayerData.My.MapRole.Count; i++)
        {
            PlayerData.My.MapRole[i].gameObject.SetActive(true);
                     
        }
       nextButton.interactable = false;  
    
       MaskManager.My.Close(4,0);
       MaskManager.My.Close(2,0);
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
