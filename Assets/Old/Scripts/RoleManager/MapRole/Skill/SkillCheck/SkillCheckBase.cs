using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckBase : MonoBehaviour
{
    public string target;
    //public string current;

    public bool isPercent;

    public string checkContent;

    public BaseMapRole dependRole;

    public bool isSuccess;

    public Text contentText;
    public Text currentText;
    public int checkTurn;
    public bool isTurnEnd;
    public CheckDetail detail;

    protected virtual void InitCheck()
    {
        
    }
    
    protected virtual void Check()
    {
        
    }

    public void ActiveCheck(BaseMapRole role, int checkTurn, CheckDetail detail)
    {
        dependRole = role;
        target = detail.target;
        isPercent = detail.isPersent;
        checkContent = detail.checkContent;
        contentText = transform.Find("contentText").GetComponent<Text>();
        currentText = transform.Find("currentText").GetComponent<Text>();
        contentText.text = checkContent;
        this.checkTurn = checkTurn;
        this.detail = detail;

        InitCheck();
    }

    public void ResetCheck()
    {
        CancelInvoke("Check");
        isSuccess = false;
        InitCheck();
    }
}