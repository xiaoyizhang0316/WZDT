using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ConsumableItem
{
    public string consumableId;

    public string consumableName;

    public string consumableDesc;

    public string targetBuffList;

    public string consumableType;

    public string range;
}


[Serializable]
public class ConsumablesData
{
    public List<ConsumableItem> consumableSigns = new List<ConsumableItem>();
}

