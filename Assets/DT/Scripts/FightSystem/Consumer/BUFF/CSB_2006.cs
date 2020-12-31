using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSB_2006 : BaseCSB
{
    public ConsumeSign targetConsume;

    public override void OnProduct(ref ProductData data)
    {
        if ((targetConsume.consumeData.maxHealth - targetConsume.currentHealth) < data.damage)
        {
            int num = (int)data.damage - targetConsume.consumeData.maxHealth + targetConsume.currentHealth;
            StageGoal.My.GetPlayerGold(num);
            StageGoal.My.Income(num, IncomeType.Other, null, "回头客收入");
        }
    }

    private void Start()
    {
        targetConsume = GetComponentInParent<ConsumeSign>();
    }
}
