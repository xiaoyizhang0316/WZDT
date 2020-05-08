using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promotion_Mid : BaseSkill
{
    public GameObject CarPrb;

    private GameObject car;

    private ProductData currentProduct = null;
    /// <summary>
    /// 当前运送的车辆
    /// </summary>
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        BaseMapRole target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole));
        currentProduct = target.GetWarehouseProductData(tradeData.selectProduct);
        if (currentProduct != null)
        {
            //print("开始送货");
            car = Instantiate(CarPrb);
            car.transform.position = target.transform.position;
            SkillCost(baseMapRole);
            InitBuff();
            CastBuff(baseMapRole, tradeData);
            Move(baseMapRole, tradeData, onComplete);
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Move(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        car.GetComponent<CarMove>().Init(baseMapRole, 1, PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).transform, (TradeData) =>
        {
            Destroy(car);
            currentProduct.Brand += (int)(baseMapRole.baseRoleData.brand * 0.2f);
            currentProduct.Quality += (int)(baseMapRole.baseRoleData.quality * 0.2f);
            PlayerData.My.GetMapRoleById(double.Parse(tradeData.endRole)).MoveGoodsToWareHouse(currentProduct);
            //刷新角色详细信息列表
            if (UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
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
        for (int i = 0; i < target.warehouse.Count; i++)
        {
            //if (Math.Abs(target.warehouse[i].Sweetness - StageGoal.My.standardSweet) <= 2 &&
            //    Math.Abs(target.warehouse[i].Crisp - StageGoal.My.standardCrisp) <= 2 && target.warehouse[i].Quality >= 5)
            //{
            //    return true;
            //}
            if (target.warehouse[i].Quality >= 45)
            {
                return true;
            }
        }
        return false;
    }
}
