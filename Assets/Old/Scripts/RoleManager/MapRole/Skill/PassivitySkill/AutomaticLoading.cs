using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI; 
public class AutomaticLoading : BasePassivitySkill
{
    /// <summary>
    /// 剩余时间
    /// </summary>
    /// <returns></returns>
    public float residue =0;
    public Tweener tweer;
    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    {
        skillButton.onClick.AddListener(() =>
        {
          UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitAUTOUI();
            
        });
        
    }

    public override void ReleaseSkills(BaseMapRole baseMapRole, Action onComplete = null)
    {
       
    }

    public override void ShowImage(BaseMapRole baseMapRole)
    {
        skillImage.GetComponent<Image>().fillAmount = residue;
        if (baseMapRole.autoLoading)
        {
  
            float add = skillImage.GetComponent<Image>().fillAmount + 0.016f;
            if (add >= 1)
            {
                skillImage.GetComponent<Image>().fillAmount = 0;
                Debug.Log("扣除自动上货成本");
                //SkillCost(baseMapRole);
                //SkillContribution();
                add = 0.016f;
            }
    
            skillImage.GetComponent<Image>().DOFillAmount(  add, 1)
                .OnComplete(
                    () =>
                    {
                        residue = skillImage.GetComponent<Image>().fillAmount;
                        if (  baseMapRole.autoLoading)
                        {
                            ShowImage(baseMapRole);
                        }
                    });
        }


    }
    public void  Open()
    {
        if (tweer!=null &&!tweer.IsPlaying())
        {
            tweer.Play();
        }

    }

    public void Close()
    {
        if (tweer!=null &&tweer.IsPlaying())
        {
            tweer.Pause();

        }

    }
}
