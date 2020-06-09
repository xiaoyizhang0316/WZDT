using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DT.Fight.Bullet;
using UnityEngine;

public class ProductMelon : BaseSkill
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
            for (int i = 0; i <role.GetEquipBuffList().Count; i++)
            {
                data.AddBuff(role.GetEquipBuffList()[i]);
            }

            for (int i = 0; i <buffList.Count; i++)
            {
                data.AddBuff(buffList[i]);
            }
            try
            {
                GameObject game = Instantiate(GoodsManager.My.GoodPrb, role.tradeList[currentCount].transform);
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().path = role.tradeList[currentCount].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role = PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole));
                game.transform.position = transform.position;
                game.GetComponent<GoodsSign>().Move();
                currentCount++;
            }
            catch (Exception)
            {


            }
           
            if (currentCount >= role.tradeList.Count)
            {
                currentCount = 0;
            }

        }

    }
}
