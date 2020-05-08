using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static GameEnum;

public class BaseRoleManager : MonoBehaviour
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public int roleId;
    /// <summary>
    /// 角色名字
    /// </summary>
    public string roleName;
    /// <summary>
    /// 产能
    /// </summary>
    public int capacity;

    /// <summary>
    /// 效率
    /// </summary>
    public int efficiency;

    /// <summary>
    /// 风险值
    /// </summary>
    public int risk;

    /// <summary>
    /// 交易成本
    /// </summary>
    public int tradeCost;

    /// <summary>
    /// 产品质量加成
    /// </summary>
    public int productsQualityAdd;

    /// <summary>
    /// 产品外观加成
    /// </summary>
    public int productsFacadeAdd;

    /// <summary>
    /// 产品品牌加成
    /// </summary>
    public int productsBrandAdd;

    /// <summary>
    /// 产品甜度加成
    /// </summary>
    public int productsSweetnessAdd;

    /// <summary>
    /// 产品脆度加成
    /// </summary>
    public int productsBrittlenessAdd;

    /// <summary>
    /// 固定成本
    /// </summary>
    public int fixedCost;

    /// <summary>
    /// 每月成本
    /// </summary>
    public int costMonth;

    /// <summary>
    /// 职业类型
    /// </summary>
    public int vocationalType;

    /// <summary>
    /// 产出时间
    /// </summary>
    public float ProductionTime;
    /// <summary>
    /// 品牌
    /// </summary>
    public int Brand;
    /// <summary>
    ///  产品
    /// </summary>
    public List<ProductData >  ProductDatas;
 
    /// <summary>
    /// 地块数组    0位是IO口 
    /// </summary>
    public List<GameObject> parcels;

    /// <summary>
    /// 是否正在生产
    /// </summary>
    public bool isProductioning;
 
    /// <summary>
    /// 进度条
    /// </summary>
    public GameObject cdSprite;

    /// <summary>
    /// 搜寻值
    /// </summary>
    public int search;

    /// <summary>
    /// 搜寻成本
    /// </summary>
    public int searchCost;

    /// <summary>
    /// 议价成本
    /// </summary>
    public int bargainCost;

    /// <summary>
    /// 交付成本
    /// </summary>
    public int deliveryCost;

    /// <summary>
    /// 议价价值
    /// </summary>
    public float bargainValue;

    /// <summary>
    /// 交付价值
    /// </summary>
    public float deliveryValue;

    /// <summary>
    /// 是否是NPC
    /// </summary>
    public bool IsNpc;

    private void Awake()
    {
        AllRoleManager.My.RoleManagers.Add(this);
    }

    private void OnDestroy()
    {
        //AllRoleManager.My.RoleManagers.Remove(this);
    }

    void Start()
    {
        cdSprite = Instantiate(CommonData.My.cdSprite, CommonData.My.canvas.transform);
        cdSprite.GetComponent<Image>().fillAmount = 0;
        isProductioning = false;
    }

    void Update()
    {
        UpdataCDSprite();
        ///如果当前满足条件并且没有生产中
        if (ProductionSwitch()&&!isProductioning)
        {
            Manufacture();
        }
    }


    public void UpdataCDSprite()
    {
        cdSprite.transform.position = new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y+1);
    }

    /// <summary>
    /// 需要重写此方法  用于角色生产
    /// </summary>
    public virtual void   Production()
    {
     
    }

    /// <summary>
    /// 当满足条件时生产
    /// </summary>
    /// <returns></returns>
    public virtual bool ProductionSwitch()
    {
        return true;
    }

    /// <summary>
    /// 生产
    /// </summary>
    public void Manufacture()
    {
        isProductioning = true;
        cdSprite.GetComponent<Image>().fillAmount = 0;
        cdSprite.GetComponent<Image>().DOFillAmount(1,ProductionTime/(1+(efficiency/100))).OnComplete(() =>
        {
            Production();
            isProductioning = false;
        });
     //   AddProductQuantity.
    }

  
    /// <summary>
    /// 放入物品
    /// </summary>
    /// <param name="baseRoleManager"></param>
    /// <param name="pos"></param>
    public virtual void SetProdect(ProductData productData)
    {
        
      //  productManager.transform.SetParent(pos);
      //  productManager.transform.position = pos.position;
      ProductDatas.Add(productData);
    }

    /// <summary>
    /// 根据类型查找产品
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ProductData GetProdect(ProductType type)
    {
        print("抓取产品");
        for (int i = 0; i < ProductDatas.Count; i++)
        {
            if (ProductDatas[i].productType == type)
            {
                ProductData productData = ProductDatas[i];
                ProductDatas.Remove( ProductDatas[i]);
                print("得到一个产品");
                return productData;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据ID 查找产品
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public ProductData GetProdect( double  ID)
    {
        for (int i = 0; i < ProductDatas.Count; i++)
        {
            if (ProductDatas[i] . ID == ID)
            {
                ProductData productData = ProductDatas[i];
                ProductDatas.Remove( ProductDatas[i]);
                print("得到一个产品");
                return productData;
            }

        }

        return null;
    }
}

