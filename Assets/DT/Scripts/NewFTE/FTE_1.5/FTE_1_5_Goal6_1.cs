using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class FTE_1_5_Goal6_1 : BaseGuideStep
{
    public Text workerTip;

    public List<GameObject> borders;

    public Transform seed_sign;
    public override IEnumerator StepStart()
    {
        SkipButton();
        InvokeRepeating("CheckGoal",0, 0.2f);
        yield return new WaitForSeconds(0.5f);
    }

    public override bool ChenkEnd()
    {
        return missiondatas.data[0].isFinish;
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
        BorderShowOrHide();
        CheckSeed();
        if (PlayerData.My.GetAvailableWorkerNumber() == PlayerData.My.playerWorkers.Count)
        {
            workerTip.color = Color.red;
            workerTip.text = "未装备工人，当前Mega值不会增长！请装备工人！";
        }
        else
        {
            if (DOTween.defaultAutoPlay == AutoPlay.None)
            {
                workerTip.color = Color.red;
                workerTip.text = "游戏暂停，当前Mega值不会增长！请点击左侧开始“>”按钮运行游戏！";
            }
            else
            {
                workerTip.color = new Color(0.2f, 0.63f, 0);
                workerTip.text = "Mega值增长中......";
            }
        }
        if (missiondatas.data[0].isFinish == false)
        {
            missiondatas.data[0].currentNum = StageGoal.My.playerTechPoint;
            if (missiondatas.data[0].currentNum >= missiondatas.data[0].maxNum)
            {
                missiondatas.data[0].isFinish = true;
            }
        }
    }

    void CheckSeed()
    {
        if (PlayerData.My.seedCount == 0)
        {
            seed_sign.GetComponent<CreatRole_Button>().ReadCostTech(0);
        }
        else
        {
            seed_sign.GetComponent<CreatRole_Button>().ReadCostTech(10);
        }
    }

    private bool isShowBorder = true;
    void BorderShowOrHide()
    {
        if (NewCanvasUI.My.Panel_AssemblyRole.activeInHierarchy)
        {
            if (isShowBorder)
            {
                for (int i = 0; i < borders.Count; i++)
                {
                    borders[i].SetActive(false);
                }
                isShowBorder = false;
            }
        }
        else
        {
            if (!isShowBorder)
            {
                for (int i = 0; i < borders.Count; i++)
                {
                    borders[i].SetActive(true);
                }

                isShowBorder = true;
            }
        }
    }

    public override IEnumerator StepEnd()
    {
        CancelInvoke();
        seed_sign.GetComponent<CreatRole_Button>().ReadCostTech(10);
        yield return new WaitForSeconds(1.5f);
    }
}
