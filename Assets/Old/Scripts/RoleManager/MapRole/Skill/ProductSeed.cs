using System;
using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using UnityEngine;


public class ProductSeed : BaseSkill
{
    public List<ProductData> productDatas = new List<ProductData>();
    // Start is called before the first frame update
    private int currentCount = 0;
    public override void Skill()
    {
        if (role.tradeList.Count <= 0)
        {
            return;
        }
        //Debug.Log("产种子");
        ProductData data = new ProductData();
        data.buffList = new List<int>();
        data.bulletType = BulletType.Seed;
        data.damage = role.baseRoleData.effect * 10f;
        data.loadingSpeed = 1.2f;
        data.buffMaxCount = 3;
        for (int i = 0; i < role.GetEquipBuffList().Count; i++)
        {
            data.AddBuff(role.GetEquipBuffList()[i]);
        }
        for (int i = 0; i < buffList.Count; i++)
        {
            data.AddBuff(buffList[i]);
        }
        try
        {
            GameObject game = Instantiate(GoodsManager.My.GoodPrb, role.tradeList[currentCount].transform);
            game.GetComponent<GoodsSign>().productData = data;
            game.GetComponent<GoodsSign>().productData.buffList = data.buffList;
            game.GetComponent<GoodsSign>().path = role.tradeList[currentCount].GetDeliverProductPath();
            game.GetComponent<GoodsSign>().role = PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[currentCount].tradeData.targetRole));
            game.transform.position = transform.position;
            game.GetComponent<GoodsSign>().Move();
            productDatas.Add(new ProductData(data));
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
