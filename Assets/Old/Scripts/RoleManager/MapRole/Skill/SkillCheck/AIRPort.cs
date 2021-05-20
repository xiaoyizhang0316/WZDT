using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRPort : BaseProductSkill
{
    private BaseMapRole luxingshe;

    public bool hastrad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Skill()
    {
        if (PlayerData.My.GetMapRoleById(double.Parse(role.endTradeList[0].tradeData.castRole)).baseRoleData
                .baseRoleData.roleType == GameEnum.RoleType.CarRental)
        {
            for (int i = 0; i <PlayerData.My.MapRole.Count; i++)
            {
                if (PlayerData.My.MapRole[i].baseRoleData.baseRoleData.roleType == GameEnum.RoleType.TouristOffice)
                {
                    if (hastrad)
                    {
                        return;
                    }
                    else
                    {
                       TradeManager.My.AutoCreateTrade(role.baseRoleData.ID.ToString(),PlayerData.My.MapRole[i].baseRoleData.ID.ToString());
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
