using System.Collections;
using System.Collections.Generic;
using DT.Fight.Bullet;
using UnityEngine;

public class PruductDealer : BaseSkill
{

 
    // Start is called before the first frame update
 
    public override void Skill()
    {
        if (role.warehouse.Count > 0)
        {
            if (role.shootTarget == null)
            {
                CancelSkill();
                return;
            }

            //Debug.Log("攻击");
            ProductData data = role.warehouse[0];
            role.warehouse.RemoveAt(0);
            switch (data.bulletType)
            {
                case  BulletType.NormalPP: 
                    GetComponent<BulletLaunch>().LanchNormal( data);
                    break;    
                case  BulletType.Bomb: 
                    GetComponent<BulletLaunch>().LanchBoom( data);
                    break;    
                case  BulletType.Lightning: 
                    GetComponent<BulletLaunch>().LanchNormal( data);
                    break;    
                case  BulletType.summon: 
                    GetComponent<BulletLaunch>().LanchNormal( data);
                    break;
                 
            }
       
        
        }
    }

    private void Update()
    {
        role.SetShootTarget();
    }
}
