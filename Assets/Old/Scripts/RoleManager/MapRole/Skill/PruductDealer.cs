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
            //Debug.Log("攻击");
            ProductData data = role.warehouse[0];
            role.warehouse.RemoveAt(0);
           GetComponent<BulletLaunch>().LanchNormal(role.shootTarget.transform.position,data);
        
        }
    }
}
