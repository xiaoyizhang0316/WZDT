using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruductMerchant : BaseSkill
{
    public List<ProductData> productDatas = new List<ProductData>();

    private int currentCount = 0;

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
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
                    .bulletCapacity)
                {
                    Debug.Log("储存");
                    currentCount++;
                    if (currentCount >= role.tradeList.Count)
                    {
                        currentCount = 0;
                    }
                    return;
                }
                print("贸易商技能");
                ProductData data = role.warehouse[0];
                role.warehouse.RemoveAt(0);
                for (int i = 0; i < role.GetEquipBuffList().Count; i++)
                {
                    data.AddBuff(role.GetEquipBuffList()[i]);
                }

                for (int i = 0; i < buffList.Count; i++)
                {
                    data.AddBuff(buffList[i]);
                }
                GameObject game = Instantiate(GoodsManager.My.GoodPrb, role.tradeList[currentCount].transform);
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().path = role.tradeList[currentCount].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role = PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole));
                if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
                {
                    game.GetComponent<GoodsSign>().speed = 1f * (1 - role.baseRoleData.efficiency / 100f);
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
