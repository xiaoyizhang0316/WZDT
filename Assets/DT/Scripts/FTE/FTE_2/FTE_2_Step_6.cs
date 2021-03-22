using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_2_Step_6 : BaseStep
{
    public GameObject top; 
    public ScrollRect rect;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0);
        MaskManager.My.darkEffect._items[5].radius  = 0  ;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        rect.vertical = false;
       
         MaskManager.My.Open(6,90);
         MaskManager.My.Open(5,180);
         top.transform.GetChild(0) .GetComponent<Image>().DOFade(0.1f, 0.5f).OnComplete(() =>
         {
             top.transform.GetChild(0) .GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() =>
             {
                 top.transform.GetChild(0) .GetComponent<Image>().DOFade(0.1f, 0.5f).OnComplete(() =>
                 {
                     top.transform.GetChild(0) .GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() =>
                     {
                         top.transform.GetChild(0) .GetComponent<Image>().DOFade(0.1f, 0.5f).OnComplete(() =>
                         {
                             top.transform.GetChild(0) .GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() =>
                             {
                                 top.transform.GetChild(0) .GetComponent<Image>().DOFade(0.1f, 0.5f).OnComplete(() =>
                                 {
                                     top.transform.GetChild(0) .GetComponent<Image>().DOFade(1f, 0.5f).OnComplete(() =>
                                     {
                 
                 
                                     }).Play();
             
                                 }).Play();
                 
                             }).Play();
             
                         }).Play();
                 
                     }).Play();
             
                 }).Play();
                 
             }).Play();
             
         }).Play();
         contenText.DOFade(0, 0).OnComplete(() =>
         {
             
             contenText.DOFade(1, 1.5f).OnComplete(() =>
             {
                 FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;

                
                 PlayNext();
             }).Play(); 
         }).Play(); 

    }

    public void PlayNext()
    {
        //Debug.Log("检测");
        if (CreatRoleManager.My.peoPleList.Count>0)
        {
            //Debug.Log("检测成功");

            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 0.1f).OnComplete(() =>
            {
                //Debug.Log("检测失败");

                PlayNext();
            }).Play();
        }
    }

    public override void StopCurrentStep()
    { 
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true;
        rect.vertical = true;

       MaskManager.My.Close(6,0);
       MaskManager.My.Close(5,0);
       
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);

           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
   
}
