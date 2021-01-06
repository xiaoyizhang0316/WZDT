using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class FTE_1_5_Goal4 : BaseGuideStep
{
    public GameObject statPanel;

    public GameObject statBox;
    public GameObject box2;
    public override IEnumerator StepStart()
    {
        InvokeRepeating("CheckGoal",0, 0.2f);
        SkipButton();
        yield return new WaitForSeconds(0.5f);
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

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish==false )
        {
            if (statPanel.activeInHierarchy)
            {
                box2.SetActive(false);
                statBox.SetActive(true);
                if (statBox.GetComponent<MouseOnThis>().isOn)
                {
                    missiondatas.data[0].isFinish = true;
                    statBox.SetActive(false);
                }
            }
            else
            {
                statBox.SetActive(false);
            }
        }
    }
}
