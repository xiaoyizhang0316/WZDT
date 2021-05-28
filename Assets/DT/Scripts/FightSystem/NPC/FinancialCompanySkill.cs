using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinancialCompanySkill : BaseFinancialSkill
{
    public int totalMaga = 0;
    //public int index =-1;

    // Start is called before the first frame update
    void Start()
    {
//       GetComponent<BaseMapRole>().HideTradeButton(false);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void Skill()
    {
        if (index == 0)
        {
            StageGoal.My.GetPlayerGold(50000,false,true);
            BaseLevelController.My.environmentLevel1 += 20;
        }
        if (index == 1)
        {
            StageGoal.My.GetPlayerGold(100000,false,true);
            BaseLevelController.My.environmentLevel1 += 20;
        }
        if (index == 2)
        {
            StageGoal.My.GetPlayerGold(200000,false,true);
            BaseLevelController.My.environmentLevel1 += 20;
        }
        //SkillCheckManager.My.ActiveRoleCheck(GetComponent<BaseMapRole>(),index);
        base.Skill();
    }
    
}
