using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_4_Step_7 : BaseStep
{
    public GameObject UI;

    public GameObject infolist;
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
    
      UI.SetActive(true);
      infolist.SetActive(true);
         contenText.DOFade(0, 0).OnComplete(() => { 
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
                 FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                 nextButton.interactable = true;
               //  PlayNext();
             }).Play(); 
         }).Play();  
    }
 
    public override void StopCurrentStep()
    {
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true; 
       nextButton.interactable = false;  


    
       contenText.DOFade(0, 0.5f).OnComplete(() =>
       {
           UI.SetActive(false);
           gameObject.SetActive(false);
           infolist.SetActive(false);
           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
