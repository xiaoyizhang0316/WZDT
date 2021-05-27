using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DT.Fight.Bullet;
using MoonSharp.Interpreter.Tree.Statements;
using static GameEnum;

  [Serializable]
public class ProductData
{
    public ProductData()
    {

    }

    public ProductData( ProductData data)
    {
        bulletType = data.bulletType;
        damage = data.damage;
        loadingSpeed = data.loadingSpeed;
        buffList =new List<int>() ;
        wasteBuffList = new List<int>();
        buffList.AddRange(data.buffList);
        wasteBuffList.AddRange(data.wasteBuffList);
        buffMaxCount = data.buffMaxCount;
     AddDamage = data.AddDamage;

     AddScore=data.AddScore;

     AddSatisfaction = data.AddSatisfaction;
        
    }

    public ProductData( SendProductData data)
    {
        bulletType = data.bulletType;
        damage = data.damage;
        loadingSpeed = data.loadingSpeed;
        buffList =new List<int>() ;
        wasteBuffList = new List<int>();
        buffList.AddRange(data.buffList);
        buffMaxCount = data.buffMaxCount;
        RepeatBulletCount = data.count;
        AddDamage = 1;

        AddScore= 1;

        AddSatisfaction = 1;
    }

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
    /// 浪费的buffList
    /// </summary>
    public List<int> wasteBuffList = new List<int>();

    /// <summary>
    /// Buff上限
    /// </summary>
    public int buffMaxCount;


    public float AddDamage = 0;

    public float AddScore= 0;

    public float AddSatisfaction = 0;
    public int RepeatBulletCount;

    public void AddBuff(int buffId)
    {
        if (!buffList.Contains(buffId))
        {
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            if (data.bulletBuffType != BulletBuffType.ConsumableGive)
            {
                buffList.Add(buffId);
            }
        }
    }

    public bool CheckSame(ProductData data)
    {
        if (bulletType == data.bulletType && damage == data.damage &&
            loadingSpeed == data.loadingSpeed && buffList.Count== data.buffList.Count &&
            buffMaxCount == data.buffMaxCount  
        )
        {
            //Debug.Log("1");
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 当改变上限的时候 调用
    /// </summary>
    public void CheckBuff()
    {
        for (int i = 0; i <int.MaxValue; i++)
        {
            if (buffMaxCount < buffList.Count)
            {
                buffList.RemoveAt(buffList.Count-1);
                if (buffMaxCount >= buffList.Count)
                {
                    return;
                }
            }
        }
      
    }
}

[Serializable ]
public class SendProductDataList
{
    public List<SendProductData>datas = new List<SendProductData>();
}

[Serializable ]
public class SendProductData 
{
    
    public SendProductData( ProductData data)
    {
        bulletType = data.bulletType;
        damage = data.damage;
        loadingSpeed = data.loadingSpeed;
        buffList =new List<int>() ;
        buffList.AddRange(data.buffList);
        buffMaxCount = data.buffMaxCount;
        count = data.RepeatBulletCount;
    }

  public BulletType bulletType;

    public int count;
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

