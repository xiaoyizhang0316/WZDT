using System;
using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using UnityEngine;

public class PruductMelon : BaseSkill
{ 
    private int currentCount= 0 ;
    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }
        if (role.warehouse.Count > 0 && role.warehouse[0].bulletType == BulletType.Seed)
        {
            ProductData data = role.warehouse[0];
            role.warehouse.RemoveAt(0);
            data.bulletType = BulletType.NormalPP;
            data.loadingSpeed *=1f-role.baseRoleData.effect/100f ;
            data.buffList.Add(201);
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
}
