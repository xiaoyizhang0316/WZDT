using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTE_0_Step_2 : BaseGuideStep
{
    public GameObject hand;
    public GameObject hand1;

    public List<GameObject> image;
    //public GameObject text;

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        if (hand1 != null)
        {
            hand1.SetActive(false);
        }
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        afterEntry = HandMove;
        if (hand1 != null)
        {
            hand1.SetActive(true);
        }
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
