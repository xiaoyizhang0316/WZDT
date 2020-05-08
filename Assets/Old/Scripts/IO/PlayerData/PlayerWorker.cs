using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerWorker
{
    public int WorkerId;
    public bool isEquiped;


    public PlayerWorker(int id,bool equiped = false)
    {
        WorkerId = id;
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
