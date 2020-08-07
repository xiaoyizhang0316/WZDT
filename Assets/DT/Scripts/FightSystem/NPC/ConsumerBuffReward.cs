using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerBuffReward : BaseExtraSkill
{
    public List<int> targetBuffRewardList = new List<int>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Consumer") && isOpen)
        {
            int count = targetBuffRewardList.Count;
            print("广告成功");
            foreach (BaseBuff b in other.transform.GetComponent<ConsumeSign>().buffList)
            {
                if (targetBuffRewardList.Contains(b.buffId))
                {
                    count--;
                }
            }
            if (count == 0)
            {
                StageGoal.My.playerHealth += other.transform.GetComponent<ConsumeSign>().consumeData.liveSatisfy * 80 / 100;
                //int number = Mathf.Min(other.transform.GetComponent<ConsumeSign>().consumeData.killMoney * 30 / 100, 2000);
                int number = other.transform.GetComponent<ConsumeSign>().consumeData.killMoney * 30 / 100;
                StageGoal.My.GetPlayerGold(number);
                StageGoal.My.Income(number, IncomeType.Npc, GetComponentInParent<BaseMapRole>());
            }
        }
    }

    public override void SkillOff(TradeSign sign)
    {
        base.SkillOff(sign);
        if (GetComponentInParent<BaseMapRole>().tradeList.Count == 0)
        {
            isOpen = false;
        }
    }
}
