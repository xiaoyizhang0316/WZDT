using System;
using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using UnityEngine;

public class AIRPort : BaseProductSkill
{
    public BaseMapRole luxingshe;
    public BaseMapRole zuche;

    public bool hastrad;
    public int addMoney;

    public int addScore;

    public int tradID;


    public override void Skill()
    {
        if (role.warehouse.Count == 0)
        {
            return;
        }

        role.warehouse.RemoveAt(0);
        ///消耗瓜
        StageGoal.My.GetPlayerGold(addMoney);
        StageGoal.My.ScoreGet(ScoreType.其他得分, addScore);


        if ((TradeManager.My.CheckTwoRoleHasTrade(zuche.baseRoleData, role.baseRoleData) ||
             TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, zuche.baseRoleData)) &&
            !luxingshe.npcScript.isLock
        )
        {
            tradID = TradeManager.My.AutoCreateTrade(role.baseRoleData.ID.ToString(),
                luxingshe.baseRoleData.ID.ToString());
        }
        else
        {
            if (!(TradeManager.My.CheckTwoRoleHasTrade(zuche.baseRoleData, role.baseRoleData) &&
                  !TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, zuche.baseRoleData)) &&
                (TradeManager.My.CheckTwoRoleHasTrade(luxingshe.baseRoleData, role.baseRoleData) ||
                 TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, luxingshe.baseRoleData)))
            {
                TradeManager.My.DeleteTrade(tradID);
            }
        }


        if (TradeManager.My.CheckTwoRoleHasTrade(luxingshe.baseRoleData, role.baseRoleData) ||
            TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, luxingshe.baseRoleData))
        {
            ProductData data = new ProductData();
            data.buffList = new List<int>();
            data.bulletType = BulletType.Seed;
            data.damage = role.baseRoleData.effect * 10f;
            data.loadingSpeed = 1.1f;
            data.buffMaxCount = 2;
            for (int i = 0; i < role.GetEquipBuffList().Count; i++)
            {
                data.AddBuff(role.GetEquipBuffList()[i]);
            }

            try
            {
                GameObject game = Instantiate(GoodsManager.My.GoodPrb, luxingshe.transform);
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().productData.buffList = data.buffList;
                game.GetComponent<GoodsSign>().path = TradeManager.My.tradeList[tradID].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role =
                    PlayerData.My.GetMapRoleById(Double.Parse(role.tradeList[tradID].tradeData.targetRole));
                game.transform.position = transform.position;
                game.GetComponent<GoodsSign>().Move();
                //productDatas.Add(new ProductData(data)); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
 
}