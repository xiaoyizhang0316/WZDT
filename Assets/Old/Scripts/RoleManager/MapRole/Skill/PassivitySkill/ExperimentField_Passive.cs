using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 试验田-被动技能
/// </summary>
public class ExperimentField_Passive : BasePassivitySkill
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

    public override void ShowImage(BaseMapRole baseMapRole)
    {
        if (skillImage != null)
        {
            if (isOpen)
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
