using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MemberShipPolicy : BasePassivitySkill
{
    public override void ReleaseSkills(BaseMapRole baseMapRole, Action onComplete = null)
    {
        InitBuff();
        CastBuff(baseMapRole);
        SkillCost(baseMapRole);
        SkillContribution();
        transform.DOScale(1f, 59.5f).OnComplete(() =>
        {
            if (!isLock && isOpen)
            {
                ReleaseSkills(baseMapRole, null);
            }
        });
    }

    /// <summary>
    /// 检测解锁条件
    /// </summary>
    public void CheckLockCondition()
    {
        BasePassivitySkill condition1 = GetComponent<BaseMapRole>().GetPassiveSkillByName("精细运营");
        BasePassivitySkill condition2 = GetComponent<BaseMapRole>().GetPassiveSkillByName("在线支付");
        if (condition1 != null && condition2 != null)
        {
            if (!condition1.isLock && !condition2.isLock && isLock)
            {
                GetComponent<BaseMapRole>().UnLockPassivitySkill(SkillName);
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("CheckLockCondition", 1f, 1f);
    }

    public override void ShowImage(BaseMapRole baseMapRole)
    {
        if (skillImage != null)
        {
            if(isOpen)
            {
                skillImage.fillAmount = 1;
            }
            else
            {
                skillImage.fillAmount = 0;
            }
        }
    }

    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    {
       skillButton.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                isOpen = true;
                ShowImage(baseMapRole);
            }
            else
            {
                isOpen = false;
                ShowImage(baseMapRole);
            }
        });
    }
}
