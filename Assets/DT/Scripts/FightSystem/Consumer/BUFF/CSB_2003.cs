using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DT.Fight.Bullet;

public class CSB_2003 : BaseCSB
{
    public override void OnProduct(ref ProductData data)
    {
        if (data.bulletType == BulletType.NormalPP)
        {
            data.damage = data.damage * 80 / 100;
        }
        else
        {
            data.damage = data.damage * 140 / 100;
        }
    }
}
