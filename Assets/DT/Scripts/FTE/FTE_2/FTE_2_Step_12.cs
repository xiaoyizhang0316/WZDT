using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_12 : BaseStep
{
    public Text maptext;
    // Start is called before the first frame update
    void Start()
    {
        //nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        maptext.color = new Color(1,1,1,0);
        InitMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        
        MaskManager.My.Open(13,130);
        MaskManager.My.Open(14,80);
     
        contenText.DOFade(0, 0).OnComplete(() =>
        {
            maptext.DOFade(1, 1.5f).Play();
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
                FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
         
                PlayNext();
            }).Play(); 
        }).Play(); 

    }

    public void PlayNext()
    {
        Debug.Log("检测");
        if (TradeManager.My.CheckTwoRoleHasTrade(PlayerData.My.RoleData[4],PlayerData.My.RoleData[5]))
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
    }

    public override void StopCurrentStep()
    {
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;

    
        MaskManager.My.Close(14,0); 
        MaskManager.My.Close(13 ,0); 
        maptext.DOFade(0, 0.8f).Play();
        contenText.DOFade(0, 0.8f).OnComplete(() =>
        {
            gameObject.SetActive(false);

            FTESceneManager.My.PlayNextStep();

        }).Play(); 
    }
    public void InitMap()
    {
       
        MapManager.My.ReleaseLand(4,20);
    }
}
