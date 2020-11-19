using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DT.Fight.Bullet;

public class CSB_2002 : BaseCSB
{
    public BulletType lastType = BulletType.Seed;

    public override void OnProduct(ref ProductData data)
    {
        if (data.bulletType != lastType)
        {
            lastType = data.bulletType;
            data.damage *= 1.4f;
        }
        else
        {
            data.damage *= 0.8f;
        }
    }
}
