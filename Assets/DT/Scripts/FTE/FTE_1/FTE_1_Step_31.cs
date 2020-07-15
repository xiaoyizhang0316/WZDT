using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_31 : BaseStep
{
    public GameObject Mark;

    public GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        contenText.color = new Color(1,1,1,0);
        nextButton.interactable = false; 
        Mark.SetActive(false);
        InvokeRepeating("CheckStart",1,1);
        InvokeRepeating("CheckWinPanel",1,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
     
        contenText.DOFade(1, 0.3f).Play();
   
    
         
            Mark.SetActive(true);
            nextButton.interactable = true;
            FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
    }

    public override void StopCurrentStep()
    {
        Mark.SetActive(false);
        NewCanvasUI.My.GameNormal(); 
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;
        FTESceneManager.My.UIFTE.SetActive(false);
       
    }


    public void CheckWinPanel()
    {
        if (NewCanvasUI.My.Panel_Review.activeSelf)
        {
            StopCurrentStep();
            CancelInvoke("CheckRoleUpdate");
        }
        else
        {
            
        }
    }

    public void CheckStart()
    {
     
        if (win.activeSelf )
        {
            StartCuttentStep();
            CancelInvoke("CheckStart");
        }

        else
        {
             
        }
    }
}
