using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生产技能
/// </summary>
public class ProductionSkill : BaseSkill 
{
 
    // Start is called before the first frame update


    
    public override bool ReleaseSkills(BaseMapRole baseMapRole,TradeData tradeData, Action onComplete = null)
    {

        print("生产");
        ActivityData data =   GameDataMgr.My.GetActivityDataById(baseMapRole.baseRoleData.baseRoleData.activityId);
         
        ProductData prodata =  baseMapRole.GetWarehouseProductData(data.inputProduct);
        //print(prodata.productType);
        if (prodata == null)
        {
        
            return false;
        }
        prodata.ID =CommonData.My.GetTimestamp(DateTime.Now);
        prodata.productType = data.outputProduct;
        prodata.Quantity  =(int)((1+(baseMapRole.baseRoleData.capacity*data.capacityAdd)/100)* prodata.Quantity)  ;
        prodata.Quality  =(int)((1+(baseMapRole.baseRoleData.quality*data.qualityAdd)/100)* prodata.Quality)  ;
        prodata.Brand  =(int)((1+(baseMapRole.baseRoleData.brand*data.brandAdd)/100)* prodata.Brand)  ;
        
         //   baseMapRole.ShiftProductInputToWarehouse(prodata);
           
        //刷新角色详细信息列表
        if(UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
        UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
       
 

        return true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
