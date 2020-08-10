using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FTE_9_Step_3 : BaseStep
{
    public HollowOutMask mask;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(1, 1, 1, 0);
        nextButton.onClick.AddListener(() => { StopCurrentStep(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartCuttentStep()
    {
        mask.DOFade(0.4f, 2).Play();

        text.DOFade(0, 0).OnComplete(() => {

            text.DOFade(1, 1.5f).OnComplete(() =>
            {

            }).Play();
        }).Play();

    }

    public override void StopCurrentStep()
    {
        nextButton.interactable = false;


        mask.DOFade(0f, 2).Play();
        text.DOFade(0, 0.8f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            NewCanvasUI.My.GameNormal();
            FTESceneManager.My.PlayNextStep();

        }).Play();
    }
}
