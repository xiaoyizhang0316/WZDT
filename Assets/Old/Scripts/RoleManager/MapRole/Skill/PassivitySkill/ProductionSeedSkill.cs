using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// 生产技能
/// </summary>
public class ProductionSeedSkill : BasePassivitySkill 
{
 
    // Start is called before the first frame update

    public ProductData ProductData  ;

    public bool isProduct;
    public override void  ReleaseSkills(BaseMapRole baseMapRole,  Action onComplete = null)
    {
         
        
          if (ProductData.Crisp ==1)
        {
           
            ProductData =    RandomSeed(baseMapRole);
            //Debug.Log("随机种子"+ProductData);

        }

        
         
        //print(prodata.productType);
        if ( !isOpen)
        {
            isProduct = false;  
        }
        else
        {
            isProduct = true;
            startTime = Time.time;
            SkillContribution();
            SkillCost(baseMapRole);
            float actualTime = skillTime / (1 + baseMapRole.baseRoleData.efficiency / 100f);
            _tweener_product =   transform.DOScale(1, actualTime).OnComplete(() =>
            {
                ProductData temp = new ProductData();
                temp.ID =CommonData.My.GetTimestamp(DateTime.Now);
                temp.productType = GameEnum.ProductType.Seed;
                temp.Quantity  =1;
                temp.Crisp = ProductData.Crisp;
                temp.Sweetness = ProductData.Sweetness;
                temp.birthday = Time.time;
                //todo
               // temp.Quality  =(int)((1+(baseMapRole.baseRoleData.quality*0.2)/100)* ProductData.Quality)  ;
                //temp.Brand  =(int)((1+(baseMapRole.baseRoleData.brand*0.1)/100)* ProductData.Brand);
                baseMapRole.MoveGoodsToWareHouse(temp);
                StageGoal.My.productList.Add(temp);
                ReleaseSkills(baseMapRole,null)  ;
                if(UIManager.My.Panel_RoleDetalInfo.gameObject.activeSelf)
                    UIManager.My.Panel_RoleDetalInfo.GetComponent<RoleDetalInfoManager>().InitRoleDetalInfo();
            });

         
        //刷新角色详细信息列表
      
        
        }

    }

    private Tweener _tweener;
    private Tweener _tweener_product;
    
    public override void ShowImage(BaseMapRole baseMapRole)
    {
        if (skillImage != null&&isProduct)
        {
     
            skillImage.fillAmount = (Time.time - startTime) / skillTime;
            _tweener =   skillImage.DOFillAmount(1,skillTime- (Time.time - startTime) ).OnComplete(
                () =>
                {
                    ShowImage(baseMapRole);
                    skillImage.fillAmount = 0;
                });
        }  
    }

    public override void SwitchButton(BaseMapRole baseMapRole,Button skillButton)
    {
    skillButton.onClick.AddListener(() =>
        {
            if (!isOpen)
            {
                isOpen = true;
           //     _tweener_product.Play();
           //     _tweener.Play();
           if (!isProduct)
           {
               ReleaseSkills(baseMapRole);
               ShowImage(baseMapRole);
           }

         
            }
            else
            {
                isOpen = false;
               // _tweener_product.Pause();
               // _tweener.Pause();
               _tweener.Kill();
               _tweener_product.Kill();
               isProduct = false
                   ;
               
            }  
        });
    }

    public ProductData  RandomSeed(BaseMapRole baseMapRole)
    {
        ProductData  productData=   new ProductData();
        productData.Crisp = Random.Range(-5, 6);
        productData.Sweetness = Random.Range(-5, 6);

        int random = 0;
        //todo
     //  if (baseMapRole.baseRoleData.quality >=70)
     //  {
     //      random = Random.Range(50,  70);
     //  }
     //  else if (baseMapRole.baseRoleData.quality >=40)
     //  {
     //      random = Random.Range(15,  50);
     //  }
     //  else if (baseMapRole.baseRoleData.quality >= 15)
     //  {
     //      random = Random.Range(5,  25); 
     //  }
     //  else
     //  {
     //      random = Random.Range(1,  10);  
     //  }

     //  productData.Quality =(int)( baseMapRole.baseRoleData.quality*0.6f+random);
     //  productData.Brand =(int)( baseMapRole.baseRoleData.brand * 0.7f);
         Debug.Log("随机种子"+productData.Quality +"|| 当前种子"+ProductData.Quantity);
         return productData;
    }
}
