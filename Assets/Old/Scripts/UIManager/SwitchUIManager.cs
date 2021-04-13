using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchUIManager : MonoBehaviour
{

    public bool showGJJ;

    public bool showDLJ;

    public bool showTSJ;

    public bool showMega;

    public bool showMoney;

    public bool showWaveText;

    public bool showScore;

    public bool showWave;


    public bool showTarget;


    public bool showBaike;

    public bool showHelp;
    /// <summary>
    /// 显示道具切换开关
    /// </summary>
    public bool showSwitchConsumable;
    /// <summary>
    /// 显示回合收支
    /// </summary>
    public bool showincomeExpenses;
    /// <summary>
    /// 是否可以看到眼睛按钮
    /// </summary>
    public bool showCanSee;

    /// <summary>
    /// 显示先后钱
    /// </summary>
    public bool showTrad;

    /// <summary>
    /// 显示激励等级
    /// </summary>
    public bool showStimulateLevel;

    /// <summary>
    /// 血
    /// </summary>
    public bool blood;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGJJ()
    {
        FindObjectOfType<GJJ>().transform.parent.gameObject.SetActive(showGJJ);
    }
    public void InitDLJ()
    {
        FindObjectOfType<DLJ>().transform.parent.gameObject.SetActive(showDLJ);
    }
    public void InitTSJ()
    {
        FindObjectOfType<TSJ>().transform.parent.gameObject.SetActive(showTSJ);
    }

    public void InitMega()
    {
        NewCanvasUI.My.mega.SetActive(showMega);
    }

    public void InitMoney()
    {
        NewCanvasUI.My.money.SetActive(showMoney);
    }

    public void InitWaveText()
    {
        NewCanvasUI.My.level.SetActive(showWaveText);
    }

    public void InitScore()
    {
        NewCanvasUI.My.playerScore .SetActive(showScore);
    }

    public void InitWave()
    {
        FindObjectOfType<WaveCount>().transform.gameObject.SetActive(showWave);
    }

    public void InitTarget()
    {
        NewCanvasUI.My.Panel_Stat.SetActive(showTarget);
    }

    public void Initbaike()
    {
        FindObjectOfType<BaikeButton>().transform.gameObject.SetActive(showBaike);
    }

    public void InitshowHelp()
    {
        NewCanvasUI.My.HelpButton.SetActive(showHelp);
    }

    public void InitSwitchConsumable()
    {
        RoleListManager.My.change.gameObject.SetActive(showSwitchConsumable);
    }

    public void InitincomeExpenses()
    {
        StageGoal.My.turnIncomeAndCostPanel.SetActive(showincomeExpenses);
    }

    public void InitshowCanSee()
    {
        NewCanvasUI.My.iscanSeeButton.SetActive(showCanSee);
    }

    public void InitShowTrad()
    {
            CreateTradeManager.My.startRolePanel.gameObject.SetActive(showTrad);
            CreateTradeManager.My.endRolePanel.gameObject.SetActive(showTrad);
            if (!showTrad)
            {
                CreateTradeManager.My.moneyFirstButton.enabled = false; 
                CreateTradeManager.My.moneyFirstButton.GetComponentInChildren<Text>().text = "???";
                CreateTradeManager.My.moneyFirstButton.GetComponent<Image>().color = new Color( 1f,1f,1f,0.4f);

                CreateTradeManager.My.moneyLastButton.enabled = false; 
                CreateTradeManager.My.moneyLastButton.GetComponentInChildren<Text>().text = "???";
                CreateTradeManager.My.moneyLastButton.GetComponent<Image>().color = new Color( 1f,1f,1f,0.4f);

            }
            else
            {
                CreateTradeManager.My.moneyFirstButton.enabled = true; 
                CreateTradeManager.My.moneyFirstButton.GetComponentInChildren<Text>().text = "先结算";
                CreateTradeManager.My.moneyFirstButton.GetComponent<Image>().color = new Color( 1f,1f,1f,1f);

                CreateTradeManager.My.moneyLastButton.enabled = true; 
                CreateTradeManager.My.moneyLastButton.GetComponentInChildren<Text>().text = "后结算";
                CreateTradeManager.My.moneyLastButton.GetComponent<Image>().color = new Color( 1f,1f,1f,1f);

            }
    }

    public void InitBlood()
    {
        NewCanvasUI.My.blood.SetActive(blood);
    }
}
