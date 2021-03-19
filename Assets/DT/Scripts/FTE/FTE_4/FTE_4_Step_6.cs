using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_4_Step_6 : BaseStep
{
    public GameObject modle;

    public NPC npc;
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
 
        contenText.color = new Color(1,1,1,0);
         MaskManager.My.Open(1,94);
         MaskManager.My.Open(3,130); 
         FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
         //Debug.Log("第六步执行");
         nextButton.interactable = true; 
      
         PlayNext();
         contenText.DOFade(0, 0).OnComplete(() => { 
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
             
                 
             }).Play(); 
         }).Play(); 

    }
    public void PlayNext()
    { 
        if (npc.isCanSeeEquip)
        { 

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            { 

                PlayNext();
            }).Play();
        }
    }
    public override void StopCurrentStep()
    {
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;
 
 
       MaskManager.My.Close(1,0);
       MaskManager.My.Close(3,0);
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
