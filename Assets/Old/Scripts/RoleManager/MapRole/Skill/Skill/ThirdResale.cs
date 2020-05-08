using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdResale : BaseSkill
{
    public GameObject CarPrb;

    private GameObject car;

    /// <summary>
    /// 当前运送的车辆
    /// </summary>
    private ProductData currentProduct = null;

    // Start is called before the first frame update
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        Debug.Log(name);
        currentProduct = PlayerData.My.GetMapRoleById(Double.Parse(tradeData.thirdPartyRole))
            .GetWarehouseProductData(tradeData.selectProduct);
        print(tradeData.selectProduct);
        if (currentProduct != null)
        {
            print("开始送货");
            car = Instantiate(CarPrb);
            car.transform.position =
                PlayerData.My.GetMapRoleById(Double.Parse(tradeData.thirdPartyRole)).transform.position;
            SkillCost(baseMapRole);
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
        car.GetComponent<CarMove>().Init(baseMapRole, 1,
            PlayerData.My.GetMapRoleById(Double.Parse(tradeData.endRole)).transform, (TradeData) =>
            {
                Destroy(car);

                if (tradeData.selectTradeDestination == GameEnum.TradeDestinationType.Warehouse)
                {
                    PlayerData.My.GetMapRoleById(Double.Parse(tradeData.targetRole))
                        .MoveGoodsToWareHouse(currentProduct);
                    onComplete();
                }
                else
                {
                    ///移动到输入口

                 //   PlayerData.My.GetMapRoleById(Double.Parse(tradeData.targetRole)).MoveGoodsToInput(currentProduct);
                    onComplete();
                }

                //刷新角色详细信息列表
                if (UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                    UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();

                onComplete();
            }, tradeData);
    }
}