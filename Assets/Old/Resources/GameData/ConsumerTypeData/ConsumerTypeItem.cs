using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConsumerTypeItem
{
    public string consumerType;

    public string minSweet;

    public string maxSweet;

    public string minCrisp;

    public string maxCrisp;

    public string minMentalPrice;

    public string maxMentalPrice;

    public string minSearchTime;

    public string maxSearchTime;

    public string minBuyPower;

    public string maxBuyPower;

    public string minSearch;

    public string maxSearch;

    public string minBargain;

    public string maxBargain;

    public string minDelivery;

    public string maxDelivery;

    public string minRisk;

    public string maxRisk;
}

[Serializable]
public class ConsumerTypesData
{
    public List<ConsumerTypeItem> consumerTypeSigns = new List<ConsumerTypeItem>();
}