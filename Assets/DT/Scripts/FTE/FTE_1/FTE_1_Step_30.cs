using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_30 : BaseStep
{

    void Start()
    {
        contenText.color = new Color(1,1,1,0);
        //    nextButton.onClick.AddListener(() => { StopCurrentStep(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
     
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
                MaskManager.My.Open(16,130);
                MaskManager.My.FadeMask(28,130);
                FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                PlayNext();
            }).Play(); 
        }).Play(); 

    }

    public override void StopCurrentStep()
    {
        nextButton.interactable = false;  
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;

        MaskManager.My.Close(16,0 );
        MaskManager.My.CloseMaks(28 );
        NewCanvasUI.My.GameNormal();
        contenText.DOFade(0, 0.8f).OnComplete(() =>
        {
            gameObject.SetActive(false); 
            FTESceneManager.My. Steps[30].gameObject.SetActive(true);
            FTESceneManager.My.currentIndex++;

        }).Play(); 
    }
    public void PlayNext()
    {
        Debug.Log("检测");
        if (TradeManager.My.CheckTwoRoleHasTrade(PlayerData.My.RoleData[2],PlayerData.My.RoleData[4]))
        {
            Debug.Log("检测成功");

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            {
                Debug.Log("检测失败");

                PlayNext();
            }).Play();
        }
        if (NewCanvasUI.My.Panel_Update.activeInHierarchy)
            NewCanvasUI.My.Panel_Update.SetActive(false);
    }
}
