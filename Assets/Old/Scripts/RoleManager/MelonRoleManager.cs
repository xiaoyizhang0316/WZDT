using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class MelonRoleManager : BaseRoleManager
{
    public override void Production()
    {
        base.Production();
        for (int i = 0; i <ProductDatas.Count; i++)
        {
            if (ProductDatas[i].productType == ProductType.Seed)
            {
                ProductDatas[i].productType = ProductType.Melon;
                ProductDatas[i].Quantity = capacity; 
                return;
                
            }
        }
    }

    public override bool ProductionSwitch()
    {
        for (int i = 0; i <ProductDatas.Count; i++)
        {
            //如果IO口当前有种子
            if (  ProductDatas[i].productType == ProductType.Seed)
            {
                return true;
            }
            
        }

        return false;
    }
}
