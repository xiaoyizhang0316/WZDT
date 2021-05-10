using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinancialCompanySkill : BaseExtraSkill
{
    public string condition_1;
    public string condition_2;
    public string condition_3;

    public int index =-1;
    // Start is called before the first frame update
    void Start()
    {
//       GetComponent<BaseMapRole>().HideTradeButton(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SkillOn(TradeSign sign)
    {
        if (index == 0)
        {
            StageGoal.My.GetPlayerGold(50000,false,true);
        }
        if (index == 1)
        {
            StageGoal.My.GetPlayerGold(100000,false,true);
        }
        if (index == 2)
        {
            StageGoal.My.GetPlayerGold(200000,false,true);
        }
        SkillCheckManager.My.ActiveRoleCheck(GetComponent<BaseMapRole>(),index);
    }
}
