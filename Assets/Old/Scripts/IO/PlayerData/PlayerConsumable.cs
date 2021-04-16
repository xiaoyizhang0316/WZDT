using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerConsumable
{
    public int consumableId;

    public int number;

    public int useCount;

 
    public PlayerConsumable(int id ,int _number = 1)
    {
        consumableId = id;
        number = _number;
        useCount = 0;
        
    }
}
