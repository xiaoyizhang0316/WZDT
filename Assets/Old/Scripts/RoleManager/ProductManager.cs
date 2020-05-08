using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class ProductManager : MonoBehaviour
{
    public SpriteRenderer Sprite;
    /// <summary>
    /// 产品数据
    /// </summary>
    public ProductData ProductsData;

    /// <summary>
    /// 当前生产者
    /// </summary>
    //   public BaseRoleManager currentRole;

    /// <summary>
    /// 当前是否腐烂
    /// </summary>
    public bool isRot;
    /// <summary>
    /// 当前剩余寿命
    /// </summary>
    public int lifetime;
//
   // /// <summary>
   // /// 生成产品
   // /// </summary>
   // public void InitProduct(BaseRoleManager baseRoleManager, ProductData productData)
   // {
   //     currentRole = baseRoleManager;
   //     currentRole.ProductManagers.Add(this);
   //     this.ProductsData = productData;
   // }
//
   // /// <summary>
   // ///   当切换角色拥有时候调用 改变归属信息
   // /// </summary>
   // /// <param name="baseRoleManager"></param>
   // /// <param name="productData"></param>
   // public void ChengeOwnerProduct(BaseRoleManager baseRoleManager)
   // {
   //     //判断当前角色是否是上一个持有该产品的角色
   //     if (baseRoleManager != currentRole)
   //     {
   //         currentRole.ProductManagers.Remove(this);
   //     }
//
   //     currentRole = baseRoleManager;
   //     currentRole.ProductManagers.Add(this);
   // }
//

  ///// <summary>
  ///// 升级产品
  ///// </summary>
  //public void UpdateProduct(ProductData productData)
  //{
  //    this.ProductsData = productData;
  //}

    // Start is called before the first frame update
    void Start()
    {
       
    }

   

    // Update is called once per frame
    void Update()
    {
        if (ProductsData != null && ProductsData.productType != ProductType.Seed)
        {
            ProductsData.time -= Time.deltaTime;
            if (ProductsData.time <= 0)
            {
                isRot = true;
            }
        }
    }
}