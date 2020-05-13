using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static GameEnum;

[Serializable]
public class ConsumeData 
{

    #region 新属性
    /// <summary>
    /// 生命值（血量）
    /// </summary>
    public int health;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 存活时间
    /// </summary>
    public float liveTime;

    /// <summary>
    /// 同时生产几个
    /// </summary>
    public int spawnNumber;

    /// <summary>
    /// 击杀获得金钱
    /// </summary>
    public int killMoney;

    /// <summary>
    /// 击杀获得满意度
    /// </summary>
    public int killSatisfy;

    /// <summary>
    /// 存活满意度惩罚
    /// </summary>
    public int liveSatisfy;

    /// <summary>
    /// 消费者名称
    /// </summary>
    public string consumerName;

    #endregion

    #region 旧属性

    /// <summary>
    /// 需求的甜度
    /// </summary>
    public int needSweetness;

    /// <summary>
    /// 需求脆度
    /// </summary>
    public int needCrisp;

    /// <summary>
    /// 需求的产品类别
    /// </summary>
    public ProductType needProductType;

    /// <summary>
    /// 心理价位
    /// </summary>
    public int mentalPrice;

    /// <summary>
    /// 偏好
    /// </summary>
    public int preference;

    /// <summary>
    /// 搜寻距离
    /// </summary>
    public float searchDistance;

    /// <summary>
    /// 购买力
    /// </summary>
    public float buyPower;

    /// <summary>
    /// 偏好差距范围
    /// </summary>
    public int buyRange;

    /// <summary>
    /// 搜寻值
    /// </summary>
    public int search;

    /// <summary>
    /// 议价值
    /// </summary>
    public int bargain;

    /// <summary>
    /// 交付值
    /// </summary>
    public int delivery;

    /// <summary>
    /// 风险值
    /// </summary>
    public int risk;

    #endregion

    /// <summary>
    /// 初始化消费者的数据
    /// </summary>
    public ConsumeData(ConsumerType type,int sweet,int crisp)
    {
        ConsumerTypeData data = GameDataMgr.My.GetConsumerTypeDataByType(type);
        needProductType = ProductType.Melon;
        needSweetness = UnityEngine.Random.Range(Mathf.Max(data.minSweet + sweet,-5), Mathf.Min(data.maxSweet + sweet, 5));
        needCrisp = UnityEngine.Random.Range(Mathf.Max(data.minCrisp + crisp, -5), Mathf.Min(data.maxCrisp + crisp, 5));
        mentalPrice = UnityEngine.Random.Range(data.minMentalPrice, data.maxMentalPrice);
        searchDistance = UnityEngine.Random.Range(data.minSearchTime, data.maxSearchTime);
        buyPower = UnityEngine.Random.Range(data.minBuyPower, data.maxBuyPower);
        //buyPower = UnityEngine.Random.Range(1f, 1.6f);
        buyRange = UnityEngine.Random.Range(data.minSearchTime, data.maxSearchTime);
        search = UnityEngine.Random.Range(data.minSearch, data.maxSearch);
        bargain = UnityEngine.Random.Range(data.minBargain, data.maxBargain);
        delivery = UnityEngine.Random.Range(data.minDelivery, data.maxDelivery);
        risk = UnityEngine.Random.Range(data.minRisk, data.maxRisk);
        consumerName = "消费者名称";
        preference = UnityEngine.Random.Range(0, 2);
    }
}
