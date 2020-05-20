﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class PruductMelon_Summon  : BaseSkill
{
    private int currentCount= 0 ;
    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }
        if (role.warehouse.Count > 9 && role.warehouse[0].bulletType == BulletType.NormalPP)
        {
            ProductData data = role.warehouse[0];
            for (int i = 0; i < 8; i++)
            {
                role.warehouse.RemoveAt(i);
            }

            data.damage =(float) (data.damage * 0.3 + role.baseRoleData.effect * 1.5f);
            data.bulletType = BulletType.summon;
            data.loadingSpeed *=1f-role.baseRoleData.effect/100f ;
            data.loadingSpeed += 0.5f;
 
            data.damage -= 20;
            GameObject game = Instantiate(GoodsManager.My.GoodPrb,   role.tradeList[currentCount]  .transform);
            game.GetComponent<GoodsSign>().productData = data;
            game.GetComponent<GoodsSign>().path=  role.tradeList[currentCount].GetDeliverProductPath();
            game.GetComponent<GoodsSign>().role =PlayerData.My.GetMapRoleById(Double.Parse( role.tradeList[currentCount].tradeData.targetRole));

            game.transform.position = transform.position;
            game.GetComponent<GoodsSign>().Move();
        
            currentCount++;
            if (currentCount >= role.tradeList.Count)
            {
                currentCount = 0;
            }

        }

    }
    
    
    public override void UnleashSkills()
    {
        if (role.warehouse.Count > 0)
        {
            ProductData data = role.warehouse[0];
            float d = 1.5f;
            Debug.Log("释放技能" + d);

            transform.DOScale(1, d).OnComplete(() =>
            {
                Debug.Log("释放技能" + d);
                Skill();
                if (IsOpen)
                {
                    UnleashSkills();
                }
            });
        }
        else
        {
            float d =1.5f;
            transform.DOScale(1, d).OnComplete(() =>
            {
           
                if (IsOpen)
                {
                    UnleashSkills();
                }
            });
        }
    }
}