using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTE_01_Step15 : BaseGuideStep
{
    public GameObject hand;

    public Slider currentSlider;

    public int sliderValue;

    public override IEnumerator StepEnd()
    {
        Debug.Log("结束教学 " + currentStepIndex);
        
        yield break;
    }

    public override IEnumerator StepStart()
    {
        Debug.Log("开始教学 " + currentStepIndex);
        //afterEntry = HandMove;
        HandMove();
        //currentSlider.interactable = true;
        currentSlider.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        ShowInfos();
    }

    public override bool ChenkEnd()
    {
        if (RoleEditor.My.isDragEnd)
        {
            RoleEditor.My.isDragEnd = false;
            if (currentSlider.name.Equals("Range"))
            {
                RoleEditor.My.isDragEnd = true;
            }
            //currentSlider.interactable = false;
            currentSlider.GetComponent<PropertySlider>().SetValue(sliderValue);
            currentSlider.gameObject.SetActive(true);
            hand.SetActive(false);
            return true;
        }
        return false;
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
    }
}
