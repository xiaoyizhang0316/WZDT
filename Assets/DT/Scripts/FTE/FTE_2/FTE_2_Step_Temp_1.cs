using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_Temp_1 : BaseStep
{
    public Button button_1;
    public Button button_2;
    public Button button_3;

    public GameObject image_1;
    public GameObject image_2;
    public GameObject image_3;
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
        button_1.onClick.AddListener(() =>
        {
            button_1.interactable = false;
            transform.DOScale(1, 1).OnComplete(() =>
            {
                image_1.SetActive(false);
                image_2.SetActive(true);
                button_2.gameObject.SetActive(true);
                button_1.gameObject.SetActive(false);
            }).Play();
           

        });
        button_2.onClick.AddListener(() =>
        {
            button_2.interactable = false;
            transform.DOScale(1, 1).OnComplete(() =>
            {
                image_2.SetActive(false);
                image_3.SetActive(true);
                button_2.gameObject.SetActive(false);
                button_3.gameObject.SetActive(true);
            }).Play();
            
        });
        button_3.onClick.AddListener(() =>
        {
            button_3.interactable = false;
            transform.DOScale(1, 1).OnComplete(() =>
            {
                image_3.SetActive(false);
                button_3.gameObject.SetActive(false);

                gameObject.SetActive(false); 
                FTESceneManager.My.PlayNextStep();
            }).Play();
           
        });
        button_1.gameObject.SetActive(false);
        button_2.gameObject.SetActive(false);
        button_3.gameObject.SetActive(false);
       image_1 .SetActive(true); 
       button_1.gameObject.SetActive(true);
       

    }

 
    public override void StopCurrentStep()
    {
        
    }
   
}
