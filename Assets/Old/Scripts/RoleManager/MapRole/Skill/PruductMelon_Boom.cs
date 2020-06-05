using System;
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
            for (int i = 0; i <role.GetEquipBuffList().Count; i++)
            {
                data.AddBuff(role.GetEquipBuffList()[i]);
            }

            for (int i = 0; i <buffList.Count; i++)
            {
                data.AddBuff(buffList[i]);
            }
            data.bulletType = BulletType.Bomb;
            data.loadingSpeed *=1f-role.baseRoleData.effect/100f ;
            data.loadingSpeed += 2;
            data.buffList.Add(101);
            data.buffList.Add(102);
            data.damage =(float) (data.damage + role.baseRoleData.effect  );
            try
            {

            GameObject game = Instantiate(GoodsManager.My.GoodPrb,   role.tradeList[currentCount]  .transform);
            game.GetComponent<GoodsSign>().productData = data;
            game.GetComponent<GoodsSign>().path=  role.tradeList[currentCount].GetDeliverProductPath();
            game.GetComponent<GoodsSign>().role =PlayerData.My.GetMapRoleById(Double.Parse( role.tradeList[currentCount].tradeData.targetRole));

            game.transform.position = transform.position;
            game.GetComponent<GoodsSign>().Move();
        
            currentCount++;
            
            }
            catch (Exception e)
            {
             
            }
            if (currentCount >= role.tradeList.Count)
            {
                currentCount = 0;
            }

        }

    }
     
}
