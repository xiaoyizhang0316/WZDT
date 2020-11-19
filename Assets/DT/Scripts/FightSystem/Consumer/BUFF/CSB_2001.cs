using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSB_2001 : BaseCSB
{
    public override void OnProduct(ref ProductData data)
    {
        if(data.damage >= 500)
        {
            data.damage = 500;
        }
    }
}
