using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class FTE_PeasantAI : MonoBehaviour
{
    public bool aiStart;

    public double targetId;

    /// <summary>
    /// 教学关农民AI行为逻辑
    /// </summary>
    public void AI()
    {
        if (GetComponent<BaseMapRole>().baseRoleData.inMap && GetComponent<BaseMapRole>().AI && !aiStart)
        {
            Debug.Log(aiStart);
            double selfId = GetComponent<BaseMapRole>().baseRoleData.ID;
            TradeManager.My.CreateTradeAI(selfId, targetId, "送货", ProductType.Melon, TradeDestinationType.Warehouse);
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
