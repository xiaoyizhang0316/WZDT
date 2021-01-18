using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductMerchant : BaseSkill
{
    public List<ProductData> productDatas = new List<ProductData>();

    private int currentCount = 0;

    private int maxCount = 0;

    public new void Start()
    {
        base.Start();
    }

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }
        if (role.encourageLevel <= -3)
        {
            return;
        }
        if (role.warehouse.Count > 0)
        {
            try
            {
                if (PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole)).warehouse
                    .Count >= PlayerData.My
                    .GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole)).baseRoleData
                    .bulletCapacity && role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
                {
                    currentCount++;
                    maxCount++;
                    if (currentCount >= role.tradeList.Count)
                    {
                        currentCount = 0;
                    }
                    if (maxCount >= role.tradeList.Count)
                    {
                        return;
                    }
                    Skill();
                    return;
                }
                ProductData data = role.warehouse[0];
                maxCount = 0;
                role.warehouse.RemoveAt(0);
                for (int i = 0; i < role.GetEquipBuffList().Count; i++)
                {
                    data.AddBuff(role.GetEquipBuffList()[i]);
                }
                //if (role.isNpc)
                //{
                //    if (role.GetComponentInChildren<BaseNpc>().isCanSeeEquip)
                //    {
                //        for (int i = 0; i < role.GetComponentInChildren<BaseNpc>().NPCBuffList.Count; i++)
                //        {
                //            data.AddBuff(role.GetComponentInChildren<BaseNpc>().NPCBuffList[i]);
                //        }
                //        for (int i = 0; i < goodBaseBuffs.Count; i++)
                //        {
                //            goodBaseBuffs[i].OnProduct(ref data);
                //        }
                //    }
                //}
                //for (int i = 0; i < buffList.Count; i++)
                //{
                //    data.AddBuff(buffList[i]);
                //}
                for (int i = 0; i < badBaseBuffs.Count; i++)
                {
                    badBaseBuffs[i].OnProduct(ref data);
                }
                GameObject game = Instantiate(GoodsManager.My.GoodPrb, role.tradeList[currentCount].transform);
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().path = role.tradeList[currentCount].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role = PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole));
                if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
                {
                    game.GetComponent<GoodsSign>().speed = 1f * (1 - role.baseRoleData.efficiency > 80 ? 80f : role.baseRoleData.efficiency / 100f);
                    productDatas.Add(data);
                }

                game.transform.position = transform.position;
                game.GetComponent<GoodsSign>().Move();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            currentCount++;
            if (currentCount >= role.tradeList.Count)
            {
                currentCount = 0;
            }

        }
    }
}
