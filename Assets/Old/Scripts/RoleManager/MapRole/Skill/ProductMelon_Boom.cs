using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class ProductMelon_Boom : BaseSkill
{
    private int currentCount = 0;
    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }
        if (role.warehouse.Count > 2 && role.warehouse[0].bulletType == BulletType.NormalPP)
        {
            ProductData data = role.warehouse[0];
            for (int i = 0; i < 1; i++)
            {
                role.warehouse.RemoveAt(0);
            }
            for (int i = 0; i < role.GetEquipBuffList().Count; i++)
            {
                data.AddBuff(role.GetEquipBuffList()[i]);
            }
            if (role.isNpc)
            {
                if (role.GetComponentInChildren<BaseNpc>().isCanSeeEquip)
                {
                    for (int i = 0; i < role.GetComponentInChildren<BaseNpc>().NPCBuffList.Count; i++)
                    {
                        data.AddBuff(role.GetComponentInChildren<BaseNpc>().NPCBuffList[i]);
                    }
                    for (int i = 0; i < goodBaseBuffs.Count; i++)
                    {
                        goodBaseBuffs[i].OnProduct(ref data);
                    }
                }
            }
            for (int i = 0; i < buffList.Count; i++)
            {
                data.AddBuff(buffList[i]);
            }
            for (int i = 0; i < badBaseBuffs.Count; i++)
            {
                badBaseBuffs[i].OnProduct(ref data);
            }
            data.bulletType = BulletType.Bomb;
            data.loadingSpeed *= 1f - role.baseRoleData.effect / 100f;
            data.loadingSpeed += 2;
            data.buffMaxCount = 3;
            data.damage = (float)(data.damage + role.baseRoleData.effect);
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
