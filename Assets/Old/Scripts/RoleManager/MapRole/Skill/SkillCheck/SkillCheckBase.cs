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
    public int checkedCount;

    /// <summary>
    /// 针对不同检测脚本的自定义的初始化
    /// </summary>
    protected virtual void InitCheck()
    {
        
    }

    /// <summary>
    /// 初始化检测
    /// </summary>
    void CheckInit()
    {
        if (StageGoal.My.isTurnStart)
        {
            currentText.text = "本回合不计入检测！";
            InvokeRepeating("CheckStartInNextTurn", 1, 1);
            return;
        }
        
        currentText.text = isPercent ? "当前：0%" : "当前：0";
        isTurnEnd = true;
        checkedTurn = 0;
        InitCheck();
        InvokeRepeating("Check", 1f, 1f);
    }
    
    /// <summary>
    /// 检测从下回合开始检测本次检测
    /// </summary>
    void CheckStartInNextTurn()
    {
        if (!StageGoal.My.isTurnStart)
        {
            CancelInvoke();
            isTurnEnd = true;
            currentText.text = isPercent ? "当前：0%" : "当前：0";
            checkedTurn = 0;
            InitCheck();
            InvokeRepeating("Check", 0.5f, 0.5f);
        }
    }
    
    /// <summary>
    /// 检测目标（每秒检测一次）
    /// </summary>
    protected virtual void Check()
    {
        if (StageGoal.My.isTurnStart)
        {
            if (isTurnEnd)
            {
                isTurnEnd = false;
            }
        }
        
        if (!StageGoal.My.isTurnStart && !isTurnEnd)
        {
            Debug.Log("check Turn end");
            isTurnEnd = true;
            checkedTurn += 1;
            if (checkedTurn >= checkTurn)
            {
                CancelInvoke();
                NotifyRoleEnd();
            }
        }
    }

    /// <summary>
    /// 激活检测，玩家选择选项之后，激活对应的检测
    /// </summary>
    /// <param name="role">激活该检测的角色</param>
    /// <param name="checkTurn">需要检测的回合数</param>
    /// <param name="detail">检测的详细信息</param>
    /// <param name="checkCount">最多的检测次数</param>
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
        Debug.Log("checkCount "+checkCount);

        //InitCheck();
        CheckInit();
    }

    /// <summary>
    /// 初始化该项检测的相关gameobject
    /// </summary>
    /// <param name="content"></param>
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

    /// <summary>
    /// 当达到检测的回合数后，通知SkillCheckManager关闭或重启本次检测
    /// </summary>
    private void NotifyRoleEnd()
    {
        checkedCount += 1;
        Debug.Log("checkCount "+checkedCount);
        if (!isSuccess)
        {
            leftCountStatus[checkedCount-1].GetComponent<Image>().color = Color.red;
        }
        if (detail.isMainTarget)
        {
            SkillCheckManager.My.NotifyEnd(dependRole);
        }
    }

    /// <summary>
    /// 重启检测
    /// </summary>
    public void ResetCheck()
    {
        CancelInvoke("Check");
        isSuccess = false;
        checkedCount = 0;
        InitCheck();
    }
}