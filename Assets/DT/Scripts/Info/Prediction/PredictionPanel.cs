using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PredictionPanel : MonoSingleton<PredictionPanel>
{
    public Text startGold;
    public Text endGold;
    public Text income;
    public Text cost;
    public Text startBlood;
    public Text endBlood;
    public Text killNum;
    public Text score;

    private int currentWave = -1;

    public Button close_btn;
    private void Start()
    {
        close_btn.onClick.AddListener(() =>
        {
            transform.DOScale(0, 0.2f).Play();
            StageGoal.My.ShowTurnIncomeAndCost();
        });
    }

    public void ShowPrediction(bool isPay = false)
    {
        currentWave = StageGoal.My.currentWave;
        if (isPay)
        {
            startGold.text = StageGoal.My.playerGold.ToString();
            endGold.text = StageGoal.My.predictGold.ToString();
            income.text = StageGoal.My.predictIncome.ToString();
            cost.text = StageGoal.My.predictCost.ToString();
            startBlood.text = StageGoal.My.playerHealth.ToString();
            if (StageGoal.My.predictHealth <= 0)
            {
                endBlood.text = "<color=red>可能失败</color>";
            }
            else
            {
                endBlood.text = StageGoal.My.predictHealth.ToString();
            }
            killNum.text = StageGoal.My.predictKillNum + "/"+BuildingManager.My.CalculateConsumerNumber(StageGoal.My.currentWave);
            score.text = StageGoal.My.predictScore.ToString();
        }
        else
        {
            startGold.text = StageGoal.My.playerGold.ToString();
            endGold.text = StageGoal.My.predictGold.ToString();
            income.text = StageGoal.My.predictIncome.ToString();
            cost.text = StageGoal.My.predictCost.ToString();
            startBlood.text = "????";
            endBlood.text = "????";
            killNum.text = "????";
            score.text = "????";
        }
        //transform.DOScale(1, 0.5f).Play();
    }

    public void RefreshPredict()
    {
        if (StageGoal.My.currentWave != currentWave)
        {
            income.text = "?????";
            cost.text = "?????";
        }
    }
}
