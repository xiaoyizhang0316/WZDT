using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FTE_0_Step_8 : BaseGuideStep
{
    public GameObject hand;
    public GameObject properties;
    public GameObject money;
    bool isOver = false;
    private void Start()
    {
        //if (properties != null)
        //    properties.SetActive(false);
        StartCoroutine(OwnStep());
    }

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        isOver = false;
        afterEntry = HandMove;
        
        yield return new WaitForSeconds(2f);

        ShowInfos();
    }

    IEnumerator OwnStep()
    {
        NewCanvasUI.My.GamePause(false);
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("0", "1");
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("1", "2");
        yield return new WaitForSeconds(0.5f);
        TradeManager.My.AutoCreateTrade("2", "3");
        //money.transform.DOScale(1f, 0f).Play();

        isOver = true;
    }

    void HandMove()
    {
        hand.SetActive(true);
    }

    void ShowInfos()
    {
        for(int i=0; i<contentText.Count; i++)
        {
            contentText[i].gameObject.SetActive(true);
        }
    }

    public override bool ChenkEnd()
    {
        //if (StageGoal.My.killNumber >= 6)
        //{
        //    NewCanvasUI.My.GamePause(false);
        //    return true;
        //}
        //return false;
        if (isOver)
        {
            //NewCanvasUI.My.GamePause(false);
            return true;
        }
        return false ;
    }
}
