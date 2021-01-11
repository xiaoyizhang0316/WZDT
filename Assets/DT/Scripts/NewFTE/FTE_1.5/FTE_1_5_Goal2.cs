using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal2 : BaseGuideStep
{
    public Transform tapPanel;
    public GameObject rolePanel;
    public GameObject tradePanel;

    public GameObject costBox;
    public GameObject tradeCostBox;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal",0, 0.2f);
        SkipButton();
        yield return new WaitForSeconds(0.5f);
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(3f);
    }

    public override bool ChenkEnd()
    {
        
        return missiondatas.data[0].isFinish ;
    }
    
    void SkipButton()
    {
        if (needCheck && FTE_1_5_Manager.My.needSkip)
        {
            if (endButton != null)
            {
                
                endButton.onClick.AddListener(() =>
                {
                    for (int i = 0; i < missiondatas.data.Count; i++)
                    {
                        missiondatas.data[i].isFinish = true;
                    }
                });
                endButton.interactable = true;
                endButton.gameObject.SetActive(true);
            }
        }
    }

    void CheckGoal()
    {
        if ( missiondatas.data[0].isFinish == false)
        {
            if (rolePanel.activeInHierarchy)
            {
                costBox.SetActive(true);
                if (costBox.GetComponent<MouseOnThis>().isOn)
                {
                    missiondatas.data[0].isFinish = true;
                    costBox.SetActive(false);
                }
            }
            else
            {
                costBox.SetActive(false);
            }
        }

        /*if ( missiondatas.data[1].isFinish == false )
        {
            if (tradePanel.activeInHierarchy)
            {
                tradeCostBox.SetActive(true);
                if (tradeCostBox.GetComponent<MouseOnThis>().isOn )
                {
                    missiondatas.data[1].isFinish = true;
                    tradeCostBox.SetActive(false);
                }
            }
            else
            {
                tradeCostBox.SetActive(false);
            }
        }

        if (missiondatas.data[2].isFinish == false && tapPanel.gameObject.activeInHierarchy)
        {
            missiondatas.data[2].isFinish = true;
        }*/
    }
}
