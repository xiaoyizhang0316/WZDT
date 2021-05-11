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

    public double roleID;

    public bool isSuccess;

    public Text contentText;
    public Text currentText;
    public Image roleIcon;
    public List<GameObject> leftCountStatus=new List<GameObject>(); 
    public int checkTurn;
    public int checkedTurn;
    public bool isTurnEnd;
    public CheckDetail detail;
    public int checkCount;

    protected virtual void InitCheck()
    {
        
    }
    
    protected virtual void Check()
    {
        
    }

    public void ActiveCheck(BaseMapRole role, int checkTurn, CheckDetail detail, int checkCount)
    {
        dependRole = role;
        target = detail.target;
        isPercent = detail.isPercent;
        checkContent = detail.checkContent;
        roleID = role.baseRoleData.ID;
        InitTransforms(checkContent);
        contentText.text = checkContent;
        this.checkTurn = checkTurn;
        this.detail = detail;
        this.checkCount = checkCount;

        InitCheck();
    }

    void InitTransforms(string content)
    {
        contentText = transform.Find("contentText").GetComponent<Text>();
        currentText = transform.Find("currentText").GetComponent<Text>();
        roleIcon = transform.Find("Icon").GetComponent<Image>();
        Transform status = transform.Find("Status");
        foreach (Transform child in status)
        {
            leftCountStatus.Add(child.gameObject);
        }

        contentText.text = content;
    }

    protected void NotifyRoleEnd()
    {
        CancelInvoke();
        checkCount += 1;
        if (!isSuccess)
        {
            leftCountStatus[checkCount-1].GetComponent<Image>().color = Color.red;
        }
        if (detail.isMainTarget)
        {
            SkillCheckManager.My.NotifyEnd(dependRole);
        }
    }

    public void ResetCheck()
    {
        CancelInvoke("Check");
        isSuccess = false;
        InitCheck();
    }
}