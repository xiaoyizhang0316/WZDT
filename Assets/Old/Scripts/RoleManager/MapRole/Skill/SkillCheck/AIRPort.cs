using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AIRPort : BaseProductSkill
{
    public BaseMapRole luxingshe;
    public BaseMapRole zuche;

    public bool hastrad;
    public int addMoney;

    public int addScore;

    public int tradID;

    public int count = 0 ;

  
    public override void Skill()
    {
        Debug.Log("jinru ");
        Debug.Log("数量"+ BuildingManager.My.GetExtraConsumerNumber("100"));
        if (BuildingManager.My.extraConsumer.Count<=0|| count> BuildingManager.My.GetExtraConsumerNumber("100"))
        {
            Debug.Log(count+"跳出 "+BuildingManager.My.GetExtraConsumerNumber("100"));

            return;
        }

        if (role.warehouse.Count != 0 )
        {
            
            role.warehouse.RemoveAt(0);
            ///消耗瓜
            StageGoal.My.GetPlayerGold(addMoney);
            StageGoal.My.ScoreGet(ScoreType.其他得分, addScore);
        }


     

        if ((TradeManager.My.CheckTwoRoleHasTrade(luxingshe.baseRoleData, role.baseRoleData) ||
            TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, luxingshe.baseRoleData) )&&
            (TradeManager.My.CheckTwoRoleHasTrade(zuche.baseRoleData, role.baseRoleData) ||
             TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, zuche.baseRoleData) )
            )
        {
            hastrad = true;
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
            

                GameObject game = Instantiate(GoodsManager.My.GoodPrb, luxingshe.transform);
                Debug.Log(game.name+"名字");
                game.GetComponent<GoodsSign>().productData = data;
                game.GetComponent<GoodsSign>().productData.buffList = data.buffList;
                game.GetComponent<GoodsSign>().path = TradeManager.My.tradeList[tradID].GetDeliverProductPath();
                game.GetComponent<GoodsSign>().role = luxingshe;
                game.transform.position = transform.position;
                game.GetComponent<GoodsSign>().Move(); 
                count++;
                Debug.Log("发送");
                
          
        }
        else
        {
            hastrad = false;
        }
        
    }
    

    public override void UnleashSkills()
    {
        Debug.Log("飞机场");
        transform.DOScale(1, 1).OnComplete(() =>
        {
            Skill();
            Debug.Log("释放技能");

            UnleashSkills();
        }).Play();
    }

    public override void OnEndTurn()
    {
        count = 0;
    }

    public override void Start()
    {
      base.Start(); 
      Debug.Log("23");
      InvokeRepeating("CheckTrad",1,1);
    }

    public void CheckTrad()
    {
        
        if (luxingshe == null || zuche == null|| !IsOpen )
        {
            return;
        }
 
        if ((TradeManager.My.CheckTwoRoleHasTrade(zuche.baseRoleData, role.baseRoleData) ||
             TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, zuche.baseRoleData)) &&
            !luxingshe.npcScript.isLock&&! TradeManager.My.CheckTwoRoleHasTrade(luxingshe.baseRoleData, role.baseRoleData) &&
                                           !TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, luxingshe.baseRoleData) 
        )
        {
            Debug.Log("创建交易");
            tradID = TradeManager.My.AutoCreateTrade(role.baseRoleData.ID.ToString(),
                luxingshe.baseRoleData.ID.ToString());
        }
        else
        {
            if (! TradeManager.My.CheckTwoRoleHasTrade(zuche.baseRoleData, role.baseRoleData ) &&
                  !TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, zuche.baseRoleData) &&
                (TradeManager.My.CheckTwoRoleHasTrade(luxingshe.baseRoleData, role.baseRoleData) ||
                 TradeManager.My.CheckTwoRoleHasTrade(role.baseRoleData, luxingshe.baseRoleData)))
            {
                TradeManager.My.DeleteTrade(tradID);
            }
        }

    }
}