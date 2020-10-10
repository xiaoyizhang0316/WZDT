using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_8 : BaseStep
{
    public Button seed;
    // Start is called before the first frame update
    void Start()
    {
      //  nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        InitMap();
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public override void StartCuttentStep()
    {
     //   MaskManager.My.Open(1);
        nextButton.interactable = false;
        MaskManager.My.Open(6,80);
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
                nextButton.interactable = true;
                nextButton.gameObject.SetActive(false);
                PlayNext();
                FTESceneManager.My.PlayNextStep();
            }).Play(); 
        }).Play(); 
        
    }
   
    public void PlayNext()
    {
        Debug.Log("检测");
        if (!MapManager.My.GetMapSignByXY(9, 18).isCanPlace)
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
        nextButton.interactable = false;  

        MaskManager.My.CloseMaks(6 );
        MaskManager.My.darkEffect._items[7].target = PlayerData.My.MapRole[0].transform;
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false); 
        

       }).Play(); 
      // MaskManager.My .Close(1);
    }

    public void InitMap()
    {
        for (int i = 0; i <     MapManager.My._mapSigns.Count; i++)
        {
         
            MapManager.My._mapSigns[i].isCanPlace = false;
        }
        MapManager.My.ReleaseLand(9,18);
    }
}
