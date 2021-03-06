using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_4_Step_4 : BaseStep
{
    public GameObject UI;

    public GameObject UI2;

    public HollowOutMask BG;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
        contenText.color = new Color(1,1,1,0); 
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
                 FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = false;
                 nextButton.interactable = true;
                 PlayNext();
             }).Play(); 
         }).Play();  
    }
    public void PlayNext()
    {
        Debug.Log("检测");
        if (UI.activeSelf && UI2.activeSelf)
        {
            Debug.Log("检测成功"); 
            StopCurrentStep();
        }
        else
        {
            gameObject.transform.DOScale(1, 1f).OnComplete(() =>
            {
                Debug.Log("检测失败");
                PlayNext();
            }).Play();
        }
    }
    public override void StopCurrentStep()
    {
        FTESceneManager.My.UIFTE.GetComponent<Image>().raycastTarget = true; 
       nextButton.interactable = false;
       BG.DOFade(0, 0.5f).Play();
       contenText.DOFade(0, 0.8f).OnComplete(() =>
       {
           gameObject.SetActive(false);
           UI.SetActive(false);
           UI2.SetActive(false);
           FTESceneManager.My.PlayNextStep();

       }).Play(); 
    }
}
