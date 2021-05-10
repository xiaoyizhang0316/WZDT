using System;
using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class InfinityWarehourse : ProductMerchant
{
    public int goodsQuality;
    public List<int> goodsBuffs=new List<int>();
    public BulletType bulletType;

    private ProductData pd;

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
        if (role.warehouse.Count == 0)
        {
            pd = new ProductData();
            pd.damage = goodsQuality;
            pd.buffList.AddRange(goodsBuffs);
            pd.bulletType = bulletType;
            switch (bulletType) 
            {
                case BulletType.NormalPP:
                    pd.loadingSpeed = 0.5f;
                    break;
                case BulletType.Lightning:
                case BulletType.Bomb:
                    pd.loadingSpeed = 2f;
                    break;
                case BulletType.summon:
                    pd.loadingSpeed = 4f;
                    break;
                default:
                    pd.loadingSpeed = 0.5f;
                    break;
            }
            role.warehouse.Add(pd);
        }
        if (role.warehouse.Count > 0)
        {
            try
            {
                if (PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole)).warehouse
                    .Count + role.tradeList[currentCount].GetGoodsCountOnTradeLine()>= PlayerData.My
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
                //role.warehouse.RemoveAt(0);
                for (int i = 0; i < role.GetEquipBuffList().Count; i++)
                {
                    data.AddBuff(role.GetEquipBuffList()[i]);
                }
                GameObject game = Instantiate(GoodsManager.My.GoodPrb, role.tradeList[currentCount].transform);
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().path = role.tradeList[currentCount].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role = PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole));
                if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
                {
                    game.GetComponent<GoodsSign>().speed = 0.5f * (1 - role.baseRoleData.efficiency > 80 ? 80f : role.baseRoleData.efficiency / 100f);
                    productDatas.Add(data);
                }
                game.transform.position = transform.position;
                game.GetComponent<GoodsSign>().Move();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                currentCount = 0;
            }
            currentCount++;
            if (currentCount >= role.tradeList.Count)
            {
                currentCount = 0;
            }
        }
    }
}
