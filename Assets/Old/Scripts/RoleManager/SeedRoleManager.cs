using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class SeedRoleManager : BaseRoleManager
{
    // Start is called before the first frame update
 

    // Update is called once per frame
 

    public GameObject seedPrb;
    
    /// <summary>
    /// 甜度
    /// </summary>
    /// <returns></returns>
    public int sweetness;

    /// <summary>
    /// 脆度
    /// </summary>
    public int brittleness;

    /// <summary>
    /// 质量
    /// </summary>
    public int quality;

    public override void Production()
    {
       //  GameObject seed =  Instantiate(seedPrb, OutIO.transform);
       ProductData productData = new ProductData();
   
       //seed.GetComponent<ProductManager>().ProductsData = productData;
       //.GetComponent<ProductManager>().Sprite.sprite = CommonData.My.seetSprite;
       SetProdect( productData);
       
    }
}
