using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductMCN : BaseProductSkill
{
    public int  addMoney;

    public int addScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Skill()
    {
        if (role.tradeList.Count == 0)
        {
            return;
        }

        if (role.warehouse.Count == 0)
        {
            return;
        }
        
    }

    public override void OnEndTurn()
    {
        //role.warehouse[0];
        role.warehouse.Clear();
        StageGoal.My.GetPlayerGold(addMoney);
        StageGoal.My.Income(addMoney, IncomeType.Npc, GetComponentInParent<BaseMapRole>());

        StageGoal.My.ScoreGet(ScoreType.消费者得分, addScore);

    }

// Update is called once per frame
    void Update()
    {
        
    }
}
