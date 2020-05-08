using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerGear
{
    public int GearId;
    public bool isEquiped;

    public PlayerGear(int id, bool equiped = false)
    {
        GearId = id;
        isEquiped = equiped;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
