using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Transportation_PassivitySkill : BasePassivitySkill
{

    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    { 
    }

    public override void ReleaseSkills(BaseMapRole baseMapRole, Action onComplete = null)
    {
        baseMapRole.CarCount = 1;
        targetMapRole.GetComponent<TransportationSkill>().CarMaxCount -= 1;
        transform.DOScale(1, 59.5f).OnComplete(() =>
        {
            baseMapRole.CarCount -=1 ;
            targetMapRole.GetComponent<TransportationSkill>().CarMaxCount += 1;
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

  
}
