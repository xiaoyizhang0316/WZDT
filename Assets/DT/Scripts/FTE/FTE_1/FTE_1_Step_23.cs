using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_23 : BaseStep
{
    public Text text;

    public HollowOutMask mask;

    
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(1,1,1,0);
     nextButton.onClick.AddListener(() => { StopCurrentStep(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        mask.DOFade(0.4f,2).Play();
      
        text.DOFade(0, 0).OnComplete(() => {
           
            text.DOFade(1, 3f).OnComplete(() =>
            {
                
            }).Play(); 
        }).Play(); 

    }

    public override void StopCurrentStep()
    {
        nextButton.interactable = false;  
        

        mask.DOFade(0f,2).Play();
        text.DOFade(0, 2f).OnComplete(() =>
        {
            gameObject.SetActive(false); 
            FTESceneManager.My.PlayNextStep();

        }).Play(); 
    }
   
}
