using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSB_2005 : BaseCSB
{
    public override void OnProduct(ref ProductData data)
    {
        if (data.buffList.Count > 0)
        {
            data.damage = data.damage * 130 / 100;
        }
        else
        {
            data.damage = data.damage * 80 / 100;
        }
    }
}
