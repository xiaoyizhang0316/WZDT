﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class PruductMelon_Boom : BaseSkill
{ 
    private int currentCount= 0 ;
    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }
        if (role.warehouse.Count > 3 && role.warehouse[0].bulletType == BulletType.NormalPP)
        {
            ProductData data = role.warehouse[0];
            for (int i = 0; i < 2; i++)
            {
                role.warehouse.RemoveAt(i);
            }
            data.bulletType = BulletType.Bomb;
            data.loadingSpeed *=1f-role.baseRoleData.effect/100f ;
            data.loadingSpeed += 2;
            data.buffList.Add(101);
            data.buffList.Add(102);
            data.damage =(float) (data.damage + role.baseRoleData.effect  );

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
            float d = 1;
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
            float d =1;
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
