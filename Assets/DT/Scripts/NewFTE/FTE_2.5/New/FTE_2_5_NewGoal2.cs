using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FTE_2_5_NewGoal2 : BaseGuideStep
{
    public GameObject tradeSettingPanel;
    public GameObject rolePanel;
    public GameObject firstButton;
    public GameObject lastButton;
    public GameObject startStar;
    public GameObject endStar;
    public GameObject slider;
    public GameObject encorageLevel;
    public List<GameObject> npcEncorageLevel;
    public Transform tradeMgr;
    public GameObject border1;
    public GameObject border2;
    public override IEnumerator StepStart()
    {
        firstButton.SetActive(true);
        lastButton.SetActive(true);
        /*startStar.transform.DOScale(Vector3.one, 0.02f).Play();
        endStar.transform.DOScale(Vector3.one, 0.02f).Play();
        slider.transform.DOScale(Vector3.one, 0.02f).Play();*/
        encorageLevel.SetActive(true);
        for (int i = 0; i < npcEncorageLevel.Count; i++)
        {
            npcEncorageLevel[i].SetActive(true);
        }
        //FTE_2_5_Manager.My.isClearGoods = false;
        SkipButton();
        InvokeRepeating("CheckGoal", 0.02f, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }
    
    void SkipButton()
    {
        if (needCheck && FTE_2_5_Manager.My.needSkip)
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
        foreach (Transform child in tradeMgr)
        {
            TradeManager.My.DeleteTrade(child.GetComponent<TradeSign>().tradeData.ID);
        }
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&& missiondatas.data[1].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            if (tradeSettingPanel.activeInHierarchy)
            {
                border1.SetActive(true);
                if (border1.GetComponent<MouseOnThis>().isOn)
                {
                    missiondatas.data[0].isFinish = true;
                    border1.SetActive(false);
                }
                
            }else
            {
                border1.SetActive(false);
            }
        }

        if (missiondatas.data[1].isFinish == false)
        {
            if (rolePanel.activeInHierarchy)
            {
                border2.SetActive(true);
                if (border2.GetComponent<MouseOnThis>().isOn)
                {
                    missiondatas.data[1].isFinish = true;
                    border2.SetActive(false);
                }
                
            }else
            {
                border2.SetActive(false);
            }
        }
    }
}
