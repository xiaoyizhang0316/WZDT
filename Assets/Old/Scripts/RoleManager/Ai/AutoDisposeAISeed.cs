using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class AutoDisposeAISeed : MonoBehaviour
{
    public bool AiStart;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AI",0,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 教学关种子商AI逻辑
    /// </summary>
    public void AI()
    {
        if (GetComponent<BaseMapRole>().baseRoleData.inMap && GetComponent<BaseMapRole>().AI&&!AiStart)
        {
            double targetId = 0;
            double selfId = GetComponent<BaseMapRole>().baseRoleData.ID;
            foreach (BaseMapRole r in PlayerData.My.MapRole)
            {
                if (r.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Peasant && r.baseRoleData.inMap && r.GetComponent<RolePosSign>().isRelease)
                {
                    targetId = r.baseRoleData.ID;
                }
            }
            if (targetId > 0)
            {
                TradeManager.My.CreateTradeAI(selfId,targetId,"送货",ProductType.Seed,TradeDestinationType.Warehouse);
                AiStart = true;
            }
        }
    }
    
}
