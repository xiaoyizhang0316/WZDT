using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_Temp_2 : BaseStep
{
    
    public GameObject image_1;
   
    // Start is called before the first frame update
    void Start()
    {
      nextButton.onClick.AddListener(() =>
      {
          StopCurrentStep();
          
      });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    { 
        image_1.SetActive(true);
    }
     
    public override void StopCurrentStep()
    {
        image_1.SetActive(false);
        gameObject.SetActive(false);
        FTESceneManager.My.PlayNextStep();
    }
   
}
