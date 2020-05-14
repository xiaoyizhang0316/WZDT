using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruductDealer : BaseSkill
{

    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Skill()
    {
        if (role.warehouse.Count > 0)
        {
            ProductData data = role.warehouse[0];
            role.warehouse.RemoveAt(0);
           GetComponent<BulletLaunch>().LanchNormal(Target.position,data);
        
        }
    }
}
