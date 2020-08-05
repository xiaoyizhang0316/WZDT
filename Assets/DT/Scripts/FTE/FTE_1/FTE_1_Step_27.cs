using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FTE_1_Step_27 : BaseStep
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
        mask.DOFade(0.4f,1.2f).Play();
      
        text.DOFade(0, 0).OnComplete(() => {
           
            text.DOFade(1, 1.5f).OnComplete(() =>
            {
                
            }).Play(); 
        }).Play(); 

    }

    public override void StopCurrentStep()
    {
        nextButton.interactable = false;  
        NewCanvasUI.My.GameNormal();
           
        mask.DOFade(0f,0.8f).Play();
        text.DOFade(0, 0.8f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name == "FTE_1")
            {
                FTESceneManager.My.Steps[27].gameObject.SetActive(true);
                FTESceneManager.My.currentIndex++;
            } 
            else
                FTESceneManager.My.UIFTE.gameObject.SetActive(false);
            for (int i = 0; i <     MapManager.My._mapSigns.Count; i++)
            {
                if (MapManager.My._mapSigns[i].mapType == GameEnum.MapType.Grass&&MapManager.My._mapSigns[i].baseMapRole==null)
                {
                    MapManager.My._mapSigns[i].isCanPlace = true;
                }
            }
        }).Play(); 
    }
   
}
