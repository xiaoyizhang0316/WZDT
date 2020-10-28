using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_0_Step_1 : BaseGuideStep
{
    public GameObject hand;

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        DOTween.PlayAll();
        DOTween.timeScale = 1f;
        DOTween.defaultAutoPlay = AutoPlay.All;
        afterEntry = HandMove;
        yield return new WaitForSeconds(0.5f);
        ShowInfos();
        yield break;
    }

    void HandMove()
    {
        hand.SetActive(true);
    }

    void ShowInfos()
    {
        for (int i = 0; i < contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }
    }
}
