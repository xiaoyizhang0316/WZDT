using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSB_2004 : BaseCSB
{
    public override void OnProduct(ref ProductData data)
    {
        data.damage += 50;
    }
}
