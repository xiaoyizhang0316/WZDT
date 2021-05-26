using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTourist : BaseProductSkill
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Skill()
    {
        if (role.warehouse.Count > 0)
        {
            role.warehouse.RemoveAt(0);
         
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
