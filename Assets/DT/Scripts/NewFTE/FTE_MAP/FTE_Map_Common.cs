using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_Map_Common : BaseGuideStep
{
    public GameObject hand;

    public List<GameObject> image;

    public override IEnumerator StepEnd()
    {
        //Debug.Log("结束教学 " + currentStepIndex);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        //Debug.Log("开始教学 " + currentStepIndex);
        afterEntry = HandMove;
        yield return new WaitForSeconds(1);
        ShowInfos();
    }

    void HandMove()
    {
        if (hand != null)
            hand.SetActive(true);
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < image.Count; i++)
        {
            image[i].SetActive(true);
        }
    }
}
