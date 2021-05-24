using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductMCN : BaseProductSkill
{
    public int  addMoney;

    public int addScore;

    public int normalEncourageId;

    public int specialEncourageId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 根据消耗的产品切换激励等级技能
    /// </summary>
    /// <param name="type"></param>
    public void ChangeEncourageSkill(int type)
    {
        if (role.encourageSkill.isSkillOpen)
        {
            role.encourageSkill.SkillOff();
        }
        if (type == 0)
        {
            role.encourageSkillId = normalEncourageId;
            role.encourageSkill = null;
            role.InitEncourageSkill();
            role.encourageSkill.OnEncourageValueChange();
        }
        else
        {
            role.encourageSkillId = specialEncourageId;
            role.encourageSkill = null;
            role.InitEncourageSkill();
            role.encourageSkill.OnEncourageValueChange();
        }
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
