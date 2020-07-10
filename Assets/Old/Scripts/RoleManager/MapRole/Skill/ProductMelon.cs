using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DT.Fight.Bullet;
using UnityEngine;

public class ProductMelon : BaseSkill
{
    public List<ProductData> productDatas = new List<ProductData>();

    private int currentCount = 0;
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
            data.loadingSpeed *= 1f - role.baseRoleData.effect / 150f;
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
                }
            }
            for (int i = 0; i < buffList.Count; i++)
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
                productDatas.Add(new ProductData(data));
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

    private float number = 1f;

    private void Update()
    {
        if (role.tradeList.Count > 0 && IsOpen)
        {
            foreach (GameObject item in animationPart)
            {
                if (!item.name.Equals("农块3"))
                {
                    item.transform.Rotate(Vector3.right, Space.World);
                }
                else
                {
                    if (item.transform.position.y >= 1.75f)
                        number = -1f;
                    if (item.transform.position.y <= 1.5f)
                        number = 1f;
                    item.transform.position += new Vector3(0f, 0.02f, 0f) * number;
                }
            }
        }
    }
}
