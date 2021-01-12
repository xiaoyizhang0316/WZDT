﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FTE_2_5_Goal2_1 : BaseGuideStep
{
    // 0.32 
    public Transform peasant1;
    public Transform peasant2;
    public Transform peasant3;
    private int sweetCount = 0;
    private int crispCount = 0;
    private int softCount = 0;

    public Transform tradeMgr;
    public override IEnumerator StepStart()
    {
        //FTE_2_5_Manager.My.isClearGoods = true;
        PlayerData.My.ClearAllRoleWarehouse();
        TradeManager.My.ResetAllTrade();
       // FTE_2_5_Manager.My.isClearGoods = false; 
        NewCanvasUI.My.GameNormal();
        SkipButton();
        InvokeRepeating("CheckGoal", 0.01f, 0.1f);
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
        peasant1.transform.DOMoveY(-8, 1f).Play().OnComplete(() =>
        {
            PlayerData.My.DeleteRole(peasant1.GetComponent<BaseMapRole>().baseRoleData.ID);
        });
        peasant2.transform.DOMoveY(-8, 1f).Play().OnComplete(() =>
        {
            PlayerData.My.DeleteRole(peasant2.GetComponent<BaseMapRole>().baseRoleData.ID);
        });
        peasant3.transform.DOMoveY(-8, 1f).Play().OnComplete(() =>
        {
            PlayerData.My.DeleteRole(peasant3.GetComponent<BaseMapRole>().baseRoleData.ID);
        });
        yield return new WaitForSeconds(2f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            CheckSeed(peasant1, 305);
            missiondatas.data[0].currentNum = sweetCount;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            CheckSeed(peasant2, 304);
            missiondatas.data[1].currentNum = crispCount;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
        
        if (missiondatas.data[2].isFinish == false)
        {
            CheckSeed(peasant3, 303);
            missiondatas.data[2].currentNum = softCount;
            if (missiondatas.data[2].currentNum >= missiondatas.data[2].maxNum)
            {
                missiondatas.data[2].isFinish = true;
            }
        }
    }

    void CheckSeed(Transform peasant, int buff)
    {
        int tempCount = 0;
        for (int i = 0; i < peasant.GetComponent<BaseMapRole>().warehouse.Count; i++)
        {
            if (peasant.GetComponent<BaseMapRole>().warehouse[i].buffList.Contains(buff))
            {
                tempCount++;
            }
            else
            {
                peasant.GetComponent<BaseMapRole>().warehouse.Remove(peasant.GetComponent<BaseMapRole>().warehouse[i]);
            }
        }

        if (buff == 305)
        {
            sweetCount = tempCount;
        }else if (buff == 304)
        {
            crispCount = tempCount;
        }
        else
        {
            softCount = tempCount;
        }

        tempCount = 0;
    }
}
