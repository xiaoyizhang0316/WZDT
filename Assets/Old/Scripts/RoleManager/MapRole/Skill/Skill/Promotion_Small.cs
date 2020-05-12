using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class Promotion_Small : BaseSkill
{
    public GameObject CarPrb;

    private GameObject car;

    private ProductData currentProduct = null;

    private BaseMapRole bigDealPos;

    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        currentProduct = target.GetWarehouseProductData(tradeData.selectProduct);
        if (currentProduct == null)
        {
            return false;
            
        }
        else
        {
           // print("开始送货");
            car = Instantiate(CarPrb);
            foreach (BaseMapRole b in PlayerData.My.MapRole)
            {
                if (b.baseRoleData.baseRoleData.roleType == RoleType.BigDealer)
                {
                    bigDealPos = b;
                    break;
                }
            }
            car.transform.position = target.transform.position;
            car.GetComponent<CarMove>().Init(baseMapRole, 1, baseMapRole.transform, (t) =>
            {
                Move(baseMapRole, tradeData, () =>
                {
                    onComplete();
                });
            }, tradeData);
            SkillCost(baseMapRole);
            InitBuff();
            CastBuff(baseMapRole, tradeData);
            return true;
        }
    }

    public void Move(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        car.GetComponent<CarMove>().Init(baseMapRole, 1,bigDealPos.transform, (TradeData) =>
        {
            //todo
         //   currentProduct.Brand += (int)(baseMapRole.baseRoleData.brand * 0.2f);
         //   currentProduct.Quality += (int)(baseMapRole.baseRoleData.quality * 0.2f);
            bigDealPos.MoveGoodsToWareHouse(currentProduct);
            //刷新角色详细信息列表
            if (UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
            Destroy(car);
            onComplete();
        }, tradeData);
    }

    /// <summary>
    /// 检测条件
    /// </summary>
    /// <param name="tradeData"></param>
    /// <returns></returns>
    public override bool DetectionCanRelease(BaseMapRole target)
    {
        bool roletype = PlayerData.My.IsRoleTypeInMap(RoleType.BigDealer);
        if (!roletype)
        {
            return false;
        }
        for (int i = 0; i<target.warehouse.Count; i++)
        {
            //if (Math.Abs(target.warehouse[i].Sweetness - StageGoal.My.standardSweet) <= 3 &&
            //    Math.Abs(target.warehouse[i].Crisp - StageGoal.My.standardCrisp) <= 3 && target.warehouse[i].Quality >= 20)
            //{
            //    return true;
            //}
            if (target.warehouse[i].Quality >= 40)
            {
                return true;
            }
        }
        return false;
    }

}
