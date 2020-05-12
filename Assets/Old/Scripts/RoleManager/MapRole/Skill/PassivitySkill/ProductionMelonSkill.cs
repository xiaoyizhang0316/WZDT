using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 生产技能
/// </summary>
public class ProductionMelonSkill : BasePassivitySkill
{
    // Start is called before the first frame update

    public bool isProduct;

    public override void ReleaseSkills(BaseMapRole baseMapRole, Action onComplete = null)
    { 
        if ( !isOpen)
        {
            isProduct = false;  
        }
        else
        {
            //print("生产瓜");
            ProductData prodata = baseMapRole.SearchWarehouseProductData(GameEnum.ProductType.Seed);
            //print(prodata.productType);

            //Debug.Log("当前没有种子"); 
            if (prodata == null)
            {
                tw1 = transform.DOScale(1, 1).OnComplete(() =>
                {
                    //Debug.Log("查找中");
                    ReleaseSkills(baseMapRole, null);
                });

            }
            else
            {
                //Debug.Log("开始 生产瓜");
                isProduct = true;
                startTime = Time.time;
                ShowImage(baseMapRole);
                SkillContribution();
                SkillCost(baseMapRole);     
                float actualTime = skillTime / (1 + baseMapRole.baseRoleData.efficiency / 100f);
                tw2 = transform.DOScale(1, actualTime).OnComplete(() =>
                {             
                    prodata = baseMapRole.GetWarehouseProductData(GameEnum.ProductType.Seed);
                    prodata.ID = CommonData.My.GetTimestamp(DateTime.Now);
                    prodata.productType = GameEnum.ProductType.Melon;
                    prodata.Quantity = 500;
                    //todo
                //    prodata.Quality = GetQuality(prodata.Quality, baseMapRole.baseRoleData.quality);
                  //  prodata.Brand = Getband(prodata.Brand, baseMapRole.baseRoleData.brand);
                    prodata.birthday = TimeManager.My.cumulativeTime;
                    prodata.time = 240f;

                    //print("生产完毕");
                    //baseMapRole.ShiftProductInputToWarehouse(prodata);
                    baseMapRole.MoveGoodsToWareHouse(prodata);
                    //刷新角色详细信息列表
                    if (UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                    {
                        UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
                    }

                    ReleaseSkills(baseMapRole, null);
                });

            }
        }
    }
 
    private Tweener tw1;
    private Tweener tw2;
    private Tweener tw3;
    public int   GetQuality(int seedQuality,int roleQuality)
    {
        if (seedQuality * 0.8f > roleQuality)
        {
          return  (int) (seedQuality * (100f - Mathf.Abs(seedQuality - roleQuality)) / 100f);
        }
        else if (seedQuality * 0.8f == roleQuality)
        {
            return seedQuality;
        }
        else if (seedQuality * 0.8f < roleQuality)
        {
            return (int)(seedQuality * roleQuality * 0.2f);

        }

        return 0;
    }
    public int   Getband(int seedBand,int roleBand)
    {
        if (seedBand * 0.8f > roleBand)
        {
            return  (int) (seedBand * (100f - Mathf.Abs(seedBand - roleBand)) / 100f);
        }
        else if (seedBand * 0.8f == roleBand)
        {
            return seedBand;
        }
        else if (seedBand * 0.8f < roleBand)
        {
            return (int)(seedBand * roleBand * 0.2f);

        }

        return 0;
    }
    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    {
       skillButton.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                isOpen = true;
                if (!isProduct)
                {
                    ReleaseSkills(baseMapRole);
                }
                
            }
            else
            {
                isOpen = false;
                tw1.Kill();
                tw2.Kill();
                tw3.Kill();
                isProduct = false;
            }
        });
    }

    public override void ShowImage(BaseMapRole baseMapRole)
    {
        if (skillImage != null && isProduct)
        {
            float actualTime = skillTime / (1 + baseMapRole.baseRoleData.efficiency / 100f);
            skillImage.fillAmount = (Time.time - startTime) / actualTime;
            tw3 =    skillImage.DOFillAmount(1, actualTime - (Time.time - startTime))
                .OnComplete(
                    () =>
                    {
                        ShowImage(baseMapRole);
                        skillImage.fillAmount = 0;
                    });
        }
    }
 
}