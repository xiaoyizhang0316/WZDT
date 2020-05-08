using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RandomSeedSkill : BasePassivitySkill
{
    private bool isReleaseing;

    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    {
        skillButton.onClick.AddListener(() =>
        {
            if (!isOpen && !isReleaseing)
            {
                isOpen = true;
                startTime = Time.time;
                ShowImage(baseMapRole);
               ReleaseSkills(baseMapRole) ;
            }
            else
            {
            }
        });
    }

    public override void  ReleaseSkills(BaseMapRole baseMapRole, Action onComplete = null)
    {
        if (isOpen)
        {
            isReleaseing = true;
            SkillContribution();
            SkillCost(baseMapRole);
            transform.DOScale(1, skillTime).OnComplete(() =>
            {
                Debug.Log("随机切换种子");
                RandomSeed(baseMapRole);
            
                isReleaseing = false;
                isOpen = false;
            });
          
        }
    }

    public override void ShowImage(BaseMapRole baseMapRole)
    {
        Debug.Log( (skillImage!=null)+"||"+!isReleaseing);
        if (skillImage != null)
        {
            Debug.Log("初始化图片" + Time.time + "||" + startTime + "||" + skillTime);
            skillImage.fillAmount = (Time.time - startTime) / skillTime;
            skillImage.DOFillAmount(1, skillTime - (Time.time - startTime));
        }
    }

    public void RandomSeed(BaseMapRole baseMapRole)
    {
        UIManager.My.Panel_SeedChange.SetActive(true);
        ProductData data = baseMapRole.GetComponent<ProductionSeedSkill>().RandomSeed(baseMapRole);
        UIManager.My.Panel_SeedChange.GetComponent<SeedChangeManager>().InitPanel(  baseMapRole.GetComponent<ProductionSeedSkill>().ProductData,data,
            () =>
            {
                baseMapRole.GetComponent<ProductionSeedSkill>().ProductData = data;
                UIManager.My.Panel_SeedChange. SetActive(false);
            }, () =>
            {
                UIManager.My.Panel_SeedChange. SetActive(false);
            });
    }

    void Start()
    {
        
    }
}