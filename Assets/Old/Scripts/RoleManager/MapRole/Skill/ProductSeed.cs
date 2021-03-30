using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DT.Fight.Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ProductSeed : BaseSkill
{
    public List<ProductData> productDatas = new List<ProductData>();
    // Start is called before the first frame update
    private int currentCount = 0;

    public override void Skill()
    {
        if (!PlayerData.My.isSOLO && PlayerData.My.creatRole != PlayerData.My.playerDutyID)
        {
            return;
        }
        if (role.tradeList.Count <= 0)
        {
            return;
        }
        if (role.encourageLevel <= -3)
        {
            return;
        }
        //Debug.Log("产种子");
        ProductData data = new ProductData();
        data.buffList = new List<int>();
        data.bulletType = BulletType.Seed;
        data.damage = role.baseRoleData.effect * 10f;
        data.loadingSpeed = 1.1f;
        data.buffMaxCount =2;
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

        // 在教学关，种子商效果改变的时候，重置相关角色的仓库
        if(CommonParams.fteList.Contains( SceneManager.GetActiveScene().name))
        {
            if (lastEffect == 0)
            {
                lastEffect = GetComponent<BaseMapRole>().baseRoleData.effect;
            }
            else
            {
                if (lastEffect != GetComponent<BaseMapRole>().baseRoleData.effect)
                {
                    lastEffect = GetComponent<BaseMapRole>().baseRoleData.effect;
                    if (role.tradeList.Count > 0 && IsOpen)
                    {
                        ClearRelateRole(role);
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (role.tradeList.Count > 0 && IsOpen)
        {
            foreach (GameObject item in animationPart)
            {
                item.transform.Rotate(Vector3.up,Space.World);
            }
        }
    }

    private int lastEffect = 0;

    void ClearRelateRole(BaseMapRole mapRole)
    {
        Debug.LogWarning("clear "+mapRole.baseRoleData.baseRoleData.roleName);
        if(mapRole.tradeList.Count>0)
        {
            for (int i = 0; i < mapRole.tradeList.Count; i++)
            {
                if (mapRole.tradeList[i].tradeData.startRole.Equals(mapRole.baseRoleData.ID.ToString()))
                {
                    mapRole.tradeList[i].ResetThisTrade();
                    BaseMapRole next= PlayerData.My.GetMapRoleById(double.Parse( mapRole.tradeList[i].tradeData.endRole));
                    next.ClearWarehouse();
                    ClearRelateRole(next);
                }
            }
        }
        else
        {
            return;
        }
    }
}
