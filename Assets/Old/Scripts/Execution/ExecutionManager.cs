using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using IOIntensiveFramework.MonoSingleton;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionManager : MonoSingleton<ExecutionManager>
{
    public Image executionBar;

    public float executionValue;

    /// <summary>
    /// 兑换交易
    /// </summary>
    public Button ExchangeSkills;

    /// <summary>
    /// 兑换装备
    /// </summary>
    public Button ExchangeEquip;
    /// <summary>
    /// 默认1秒 涨一格
    /// </summary>
    public float time =1 ;
    private Tweener fill;

    /// <summary>
    /// 放角色 ----格为单位
    /// </summary>
    public float putRole;


    /// <summary>
    /// 回收角色
    /// </summary>
    public float removeRole;

    /// <summary>
    /// 修改角色
    /// </summary>
    public float modifyRole;

    /// <summary>
    /// 修改交易
    /// </summary>
    public float modifyDeal;

    /// <summary>
    /// 解锁技能
    /// </summary>
    public float unlockSkills;
                           
    /// <summary>
    /// 修改被动技能
    /// </summary>
    public float moditySkill;

    /// <summary>
    /// 获取装备
    /// </summary>
    public float getEquipment;

    /// <summary>
    /// 获取工人
    /// </summary>
    public float getWork;

    public Text unlockText;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddExecutionOnTime", 0,  time);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddExecutionOnTime()
    {
        executionValue += 0.05f /  10*time;
        if (executionValue > 1)
        {
            executionValue = 1;
        }
      //  executionBar.fillAmount += 0.05f / 5*time;
      executionBar.DOFillAmount(executionValue, time).SetEase(Ease.Linear).SetUpdate(true);
    }

    /// <summary>
    /// 添加行动力
    /// </summary>
    public void AddExecution(float action )
    {
        executionValue += action*0.05f;
        if (executionValue > 1)
        {
            executionValue = 1;
        }

        FillAmount(time);
    }

    /// <summary>
    /// 减少行动力
    /// </summary>
    public bool SubExecution(float action ) 
    {
        if (executionValue < action * 0.05f) 
        {
         POPUIManager.My.POPTipUI("提示","当前行动力不足", () =>
         {
             
         });
         return false;
        }
        else
        {
            executionValue -= action*0.05f;
            if (executionValue < 0)
            {
                executionValue = 0;
            }

            FillAmount(time);
            return true;
        }
    }

    public void FillAmount(float time)
    {
        if (fill != null)
        {
            fill.Kill();
        }

        fill = executionBar.DOFillAmount(executionValue, time).SetEase(Ease.Linear).OnComplete(() => { }).SetUpdate(true);
    }

    public void ExchangeSkill(TradeSkillData skilldata)
    {
        SkillData data = GameDataMgr.My.GetSkillDataByID(skilldata.skillId);
        string str = "解锁了" + skilldata.startRole.ToString() + "和" + skilldata.endRole.ToString() + "之间的" + data.skillName + "技能";
        unlockText.text = str;
    }

     
}