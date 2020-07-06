using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_2 : BaseStep
{
    public Text maptext;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
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
        
         MaskManager.My.Open(1,80);
         MaskManager.My.Open(2,130);
         contenText.DOFade(0, 0).OnComplete(() =>
         {
             maptext.DOFade(1, 1.5f);
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
        Debug.Log("检测");
        if (!MapManager.My.GetMapSignByXY(8, 16).isCanPlace)
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

       RoleListManager.My.OutButton(); 
       MaskManager.My.Close(1,0);
       MaskManager.My.Close(2,0);
       maptext.DOFade(0, 0.8f);
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
    public void InitMap()
    {
        for (int i = 0; i <     MapManager.My._mapSigns.Count; i++)
        {
         
            MapManager.My._mapSigns[i].isCanPlace = false;
        }
        MapManager.My.ReleaseLand(8,16);
    }
}
