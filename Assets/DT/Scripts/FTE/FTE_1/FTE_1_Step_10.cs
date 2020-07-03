using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_Step_10 : BaseStep
{
    public GameObject mask1;
    public GameObject mask2;
    public GameObject mask3;
    public GameObject mask4;
    public GameObject mask5;

    public Button button_1;
    public Button button_2;
    public Button button_3;
    public Button button_4;
    public Button button_5;
    // Start is called before the first frame update
    void Start()
    {
      nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        button_1.onClick.AddListener(() =>
        {
            mask1.SetActive(false);
            button_1.gameObject.SetActive(false);

            mask2.SetActive(true);
            button_2.gameObject.SetActive(true);

            mask2.transform.GetChild(0).GetComponent<Image>().DOFade(0,0.5f).Play();
        });
        button_2.onClick.AddListener(() =>
        {
            mask2.SetActive(false);
            button_2.interactable = false;

            button_2.gameObject.SetActive(false);

            mask3.SetActive(true);
            button_3.gameObject.SetActive(true);

            mask3.transform.GetChild(0).GetComponent<Image>().DOFade(0,0.5f).Play();
        });
        button_3.onClick.AddListener(() =>
        {
            mask3.SetActive(false);
            button_3.interactable = false;

            button_3.gameObject.SetActive(false);

            mask4.SetActive(true);
            button_4.gameObject.SetActive(true);

            mask4.transform.GetChild(0).GetComponent<Image>().DOFade(0,0.5f).Play();
        });
        button_4.onClick.AddListener(() =>
        {
            mask4.SetActive(false);
            button_4.interactable = false;

            button_4.gameObject.SetActive(false);

            mask5.SetActive(true);
            button_5.gameObject.SetActive(true);

            mask5.transform.GetChild(0).GetComponent<Image>().DOFade(0,0.5f).Play();
        });
        button_5.onClick.AddListener(() =>
        {
            button_5.interactable = false;
            mask5.SetActive(false);
            
            button_5.gameObject.SetActive(false);

            StopCurrentStep();
        });
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public override void StartCuttentStep()
    {
     //   MaskManager.My.Open(1);
      // nextButton.interactable = false;
       // MaskManager.My.Open( );
       NewCanvasUI.My.Panel_Update.SetActive(true);
       RoleUpdateInfo.My.Init(PlayerData.My.RoleData[0]);
       MaskManager.My.Close(7,0 );
       contenText.DOFade(0, 0).OnComplete(() => {
           
            mask1.GetComponent<HollowOutMask>().DOFade(0.4f, 1f).OnComplete(() =>
            {
                nextButton.interactable = true;
                mask1.SetActive(true);
                button_1.gameObject.SetActive(true);
                mask1.transform.GetChild(0).GetComponent<Image>().DOFade(0,0.5f).Play();
            }).Play(); 
        }).Play(); 
        
    }

    public override void StopCurrentStep()
    { 
        nextButton.interactable = false;  

        NewCanvasUI.My.Panel_Update.SetActive(false);

       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false); 
          FTESceneManager.My.PlayNextStep();

       }).Play(); 
      // MaskManager.My .Close(1);
    }

     
}
