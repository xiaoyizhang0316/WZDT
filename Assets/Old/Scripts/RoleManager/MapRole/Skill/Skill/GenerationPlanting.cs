using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GenerationPlanting : BaseSkill
{
    public GameObject CarPrb;

    private GameObject car;
 
    public double ID;
    /// <summary>
    /// 当前运送的车辆
    /// </summary>
    private ProductData currentProduct = null;

    // Start is called before the first frame update
    public override bool ReleaseSkills(BaseMapRole baseMapRole, TradeData tradeData, Action onComplete = null)
    {
        currentProduct =  PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)).GetWarehouseProductData(GameEnum.ProductType.Seed);

      
        if (currentProduct == null)
        {
            return false;
        }
        else
        {
            ID = currentProduct.ID;

            car = Instantiate(CarPrb);
            Transform target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)).transform;
            car.transform.position =target.position;
            car.GetComponent<CarMove>().Init(baseMapRole,1,baseMapRole.transform, (t) =>
            {
    
                Product(baseMapRole, tradeData,()=>
                {
                    onComplete();
                    baseMapRole.MoveGoodsToWareHouse(currentProduct);
                });
            },tradeData);
            car.GetComponent<CarMove>().MoveCar();
            return true;
        }


    }

    /// <summary>
    /// 等待生产完毕
    /// </summary>
    public void  Product(BaseMapRole baseMapRole,TradeData tradeData, Action onComplete = null)
    {
        transform.DOScale(1, 12).OnComplete(() =>
        {
            currentProduct.ID = CommonData.My.GetTimestamp(DateTime.Now);
            currentProduct.productType = GameEnum.ProductType.Melon;
            currentProduct.Quantity = 500;
            //todo
         //   currentProduct.Quality = (int)((1 + (baseMapRole.baseRoleData.quality * 0.15) / 100) * currentProduct.Quality);
          //  currentProduct.Brand = (int)((1 + (baseMapRole.baseRoleData.brand * 0.18) / 100) * currentProduct.Brand);
            currentProduct.birthday =TimeManager.My.cumulativeTime;
            currentProduct.time = 480;
            Transform target = PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)).transform;
            car.transform.LookAt(target);
            car.GetComponent<CarMove>().Init(baseMapRole, 1, target, (t) =>
            {
                Debug.Log("小车存入");
                PlayerData.My.GetMapRoleById(double.Parse(tradeData.targetRole)).MoveGoodsToWareHouse(currentProduct);
                onComplete();
             Destroy(car);
            }, tradeData);
            car.GetComponent<CarMove>().MoveCar();

        });
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
