﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 送货
/// </summary>
public class DeliverGoodsSkill : BaseSkill
{
    // Start is called before the first frame update
    public GameObject CarPrb;

    private GameObject car;

    /// <summary>
    /// 当前运送的车辆
    /// </summary>
    private ProductData currentProduct = null;

    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete)
    {
        //Debug.Log(name);
        currentProduct = baseMapRole.GetWarehouseProductData(tradeData.selectProduct);
        //print(tradeData.selectProduct);
        if (currentProduct != null)
        {
            //print("开始送货");
            car = Instantiate(CarPrb);
            car.transform.position = baseMapRole.transform.position;
            SkillCost(baseMapRole);
            Move(baseMapRole,tradeData, onComplete);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(BaseMapRole baseMapRole,TradeData tradeData, Action onComplete = null)
    {
        car.GetComponent<CarMove>().Init(baseMapRole,1, PlayerData.My.GetMapRoleById(Double.Parse(tradeData.endRole)).transform, (TradeData) =>
         {

             Destroy(car);

             PlayerData.My.GetMapRoleById(Double.Parse(tradeData.endRole)).MoveGoodsToWareHouse(currentProduct);

            //刷新角色详细信息列表
            if (UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                 UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
              
             onComplete();
         }, tradeData);

    }


}