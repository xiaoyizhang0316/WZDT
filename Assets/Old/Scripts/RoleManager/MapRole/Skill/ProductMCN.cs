using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductMCN : BaseProductSkill
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }

        if (role.warehouse.Count == 0)
        {
            return;
        }
        
    }

    public override void OnEndTurn()
    {
        //role.warehouse[0];
        role.warehouse.Clear();
        
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
