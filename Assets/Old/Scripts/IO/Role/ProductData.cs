using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DT.Fight.Bullet;
using static GameEnum;

  [Serializable]
public class ProductData
{
     
       
    
    public BulletType bulletType;

    /// <summary>
    /// 伤害
    /// </summary>
    public float damage;

    /// <summary>
    /// 装弹速度
    /// </summary>
    public float loadingSpeed;

    /// <summary>
    /// BuffList
    /// </summary>
    public List<int> buffList = new List<int>();

    /// <summary>
    /// Buff上限
    /// </summary>
    public int buffMaxCount;
}


