using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolCanteenSkill : BaseSkill
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
        currentProduct =PlayerData.My.GetMapRoleById(Double.Parse(tradeData.endRole) ) .GetWarehouseProductData(GameEnum.ProductType.Melon);
        print(tradeData.selectProduct);
        if (currentProduct != null)
        {
            print("开始送货");
            car = Instantiate(CarPrb);
            car.transform.position = PlayerData.My.GetMapRoleById(Double.Parse(tradeData.endRole) ).transform.position;

            Move(baseMapRole,tradeData, onComplete);

            return true;
        }
        else
        {
            return false;
        }
    }
    public void Move(BaseMapRole baseMapRole,TradeData tradeData, Action onComplete = null)
    {
        car.GetComponent<CarMove>().Init(baseMapRole,1, baseMapRole.transform, (TradeData) =>
        {

            Destroy(car); 
            baseMapRole.MoveGoodsToWareHouse(currentProduct);
           
            //刷新角色详细信息列表
            if (UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
              
            onComplete();
        }, tradeData);

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}