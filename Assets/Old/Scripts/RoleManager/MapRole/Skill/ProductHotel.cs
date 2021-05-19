using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProductHotel : BaseProductSkill
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

        
        ///消耗瓜
        StageGoal.My.GetPlayerGold(role.shootTargetList.Count*addMoney); 
        StageGoal.My.ScoreGet(ScoreType.消费者得分,role.shootTargetList.Count*addScore); 
    }

    public override void UnleashSkills()
    {
        transform.DORotate(transform.eulerAngles, 1).OnComplete(() =>
        {
            Skill();
            if (IsOpen)
            {
                UnleashSkills();
            }
        });
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
