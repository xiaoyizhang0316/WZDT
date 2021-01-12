using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FTE_2_5_Goal2 : BaseGuideStep
{
    // 0.32 
    public Transform peasant1;
    public Transform peasant2;
    public Transform peasant3;
    public Transform place1;
    public Transform place2;
    public Transform place3;
    public Transform tradeMgr;
    private int sweetCount = 0;
    private int crispCount = 0;
    private int softCount = 0;
    public override IEnumerator StepStart()
    {
        NewCanvasUI.My.GameNormal();
        FTE_2_5_Manager.My.GetComponent<RoleCreateLimit>().limitPeasantCount = 3;
        peasant1.gameObject.SetActive(true);
        peasant2.gameObject.SetActive(true);
        peasant3.gameObject.SetActive(true);
        SeedBuildRise();
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
        NewCanvasUI.My.GamePause(false);
        DoEnd();
        yield return new WaitForSeconds(1f);
    }

    void DoEnd()
    {
        //FTE_2_5_Manager.My.isClearGoods = true;
        /*foreach (Transform trade in tradeMgr)
        {
            TradeManager.My.DeleteTrade(trade.GetComponent<TradeSign>().tradeData.ID);
        }*/
        TradeManager.My.ResetAllTrade();
        PlayerData.My.ClearAllRoleWarehouse();
        //FTE_2_5_Manager.My.isClearGoods = false;
        NewCanvasUI.My.GameNormal();
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish&&missiondatas.data[1].isFinish&&missiondatas.data[2].isFinish;
    }

    void CheckGoal()
    {
        if (missiondatas.data[0].isFinish == false)
        {
            //CheckSeed(peasant1, 305);
            missiondatas.data[0].currentNum = peasant1.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
        
        if (missiondatas.data[1].isFinish == false)
        {
            //CheckSeed(peasant2, 304);
            missiondatas.data[1].currentNum = peasant2.GetComponent<BaseMapRole>().warehouse.Count;
            if (missiondatas.data[1].currentNum >= missiondatas.data[1].maxNum)
            {
                missiondatas.data[1].isFinish = true;
            }
        }
        
        if (missiondatas.data[2].isFinish == false)
        {
            //CheckSeed(peasant3, 303);
            missiondatas.data[2].currentNum = peasant3.GetComponent<BaseMapRole>().warehouse.Count;
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
            }else
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

    void SeedBuildRise()
    {
        
            //place1.DOMoveY(0, 1f).Play();
            peasant1.DOMoveY(0.32f, 1f).Play();
            peasant1.GetComponent<QualityRole>().checkBuff = 305;
            peasant1.GetComponent<QualityRole>().checkQuality = -1;
            peasant1.GetComponent<QualityRole>().needCheck = true;
        
        
            //place2.DOMoveY(0, 1f).Play();
            peasant2.DOMoveY(0.32f, 1f).Play();
            peasant2.GetComponent<QualityRole>().checkBuff = 304;
            peasant2.GetComponent<QualityRole>().checkQuality = -1;
            peasant2.GetComponent<QualityRole>().needCheck = true;
        
        
            //place3.DOMoveY(0, 1f).Play();
            peasant3.DOMoveY(0.32f, 1f).Play();
            peasant3.GetComponent<QualityRole>().checkBuff = 303;
            peasant3.GetComponent<QualityRole>().checkQuality = -1;
            peasant3.GetComponent<QualityRole>().needCheck = true;
        
    }
}
