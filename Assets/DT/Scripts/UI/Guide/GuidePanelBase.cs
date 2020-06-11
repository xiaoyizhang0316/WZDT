using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePanelBase : MonoBehaviour
{
    public List<Action> actions;
    //public List<float> waitTime;

    public int currentStep;
    protected virtual void InitGuidePanel()
    {
        for(int i =0; i<transform.childCount; i++)
        {
            
            int step = i;
            transform.GetChild(i).GetComponent<GuideStep>().eventBtn.onClick.AddListener(() => {
               
               Excute(step);
            });
        }
        transform.GetChild(currentStep).gameObject.SetActive(true);
        //StartCoroutine( ExcuteStep(currentStep));
    }

    public void NextStep()
    {
        ++currentStep;
        Excute(currentStep);
    }

    public void ShowNextStep()
    {
        if (currentStep < transform.childCount - 1)
        {
            transform.GetChild(++currentStep).gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            GuideMgr.My.GuideEnd();
        }
    }

    private void Excute(int step)
    {
        ExcuteStep(step);
    }

    protected virtual void ExcuteStep(int step)
    {
        currentStep = step;
        transform.GetChild(step).gameObject.SetActive(false);
        actions[step]();
        //yield return new WaitForSeconds(waitTime[step]);
        //if (step < transform.childCount - 1)
        //{
        //    transform.GetChild(++step).gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //    GuideMgr.My.GuideEnd();
        //}
    }

    
}

public enum HandMoveType
{
    Move,
    Scale
}
