using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using UnityEngine;


public class PruductSeed : BaseSkill
{
  
    // Start is called before the first frame update
    private int currentCount= 0 ;
    public override void Skill()
    {
        if (role.tradeList.Count <= 0)
        {
            return;
        }
        
        ProductData data = new ProductData();
        data.bulletType = BulletType.Seed;
        data.damage = role.baseRoleData.effect * 10;
        data.loadingSpeed = 1;
        data.buffMaxCount = 3;
        data.buffList = new List<BuffData>();

        GameObject game = Instantiate(GoodsManager.My.GoodPrb, GoodsManager.My.transform);
        game.GetComponent<GoodsSign>().productData = data;

       
        
    }
}
