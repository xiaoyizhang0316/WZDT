using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruductDealer : BaseSkill
{

 
    // Start is called before the first frame update
 
    public override void Skill()
    {
        if (role.warehouse.Count > 0)
        {
            if (!role.shootTarget.isCanSelect)
            {
                return;
            }

            //Debug.Log("攻击");
            ProductData data = role.warehouse[0];
            role.warehouse.RemoveAt(0);
           GetComponent<BulletLaunch>().LanchNormal( data);
        
        }
    }
}
