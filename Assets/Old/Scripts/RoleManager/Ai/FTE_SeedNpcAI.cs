using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class FTE_SeedNpcAI : MonoBehaviour
{
    public bool aiStart;

    public double targetId;

    public void AI()
    {
        if (GetComponent<BaseMapRole>().baseRoleData.inMap && GetComponent<BaseMapRole>().AI && !aiStart)
        {
            Debug.Log(aiStart);
            double selfId = GetComponent<BaseMapRole>().baseRoleData.ID;
            TradeManager.My.CreateTradeAI(selfId, targetId, "送货", ProductType.Seed, TradeDestinationType.Warehouse);
            aiStart = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AI", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
