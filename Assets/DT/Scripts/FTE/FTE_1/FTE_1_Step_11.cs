using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_11 : BaseStep
{
    // Start is called before the first frame update
    void Start()
    {
        //  nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public override void StartCuttentStep()
    {
        //   MaskManager.My.Open(1);
    
        MaskManager.My.Open(8,80);
        contenText.DOFade(0, 0).OnComplete(() => {
           
            contenText.DOFade(1, 1.5f).OnComplete(() =>
            {
          
                MapManager.My.ReleaseLand(11,16);
                PlayNext();
                FTESceneManager.My.PlayNextStep();
            }).Play(); 
        }).Play(); 
        
    }
   
    public void PlayNext()
    {
        Debug.Log("检测");
        if (!MapManager.My.GetMapSignByXY(11, 16).isCanPlace)
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
        

        MaskManager.My.Close(8,0 );
        contenText.DOFade(0, 0.8f).OnComplete(() =>
        {
            gameObject.SetActive(false); 
        

        }).Play(); 
        // MaskManager.My .Close(1);
    }

    
}
