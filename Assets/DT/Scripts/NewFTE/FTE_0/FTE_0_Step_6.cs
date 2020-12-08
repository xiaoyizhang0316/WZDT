using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FTE_0_Step_6 : BaseGuideStep
{
    public GameObject hand;

    public List<GameObject> image;

    public GameObject money;

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        NewCanvasUI.My.GameNormal();
        money.transform.DOScale(1f, 0f).Play();
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        afterEntry = HandMove;
       
        yield return new WaitForSeconds(1);
        
        ShowInfos();
    }

    void HandMove()
    {
        if(hand!=null)
            hand.SetActive(true);
    }

    void ShowInfos()
    {
        for(int i=0; i< contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

        for(int i=0; i<image.Count; i++)
        {
            image[i].SetActive(true);
        }
    }
}
