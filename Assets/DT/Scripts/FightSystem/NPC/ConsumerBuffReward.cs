using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerBuffReward : BaseExtraSkill
{
    public List<int> targetBuffRewardList = new List<int>();

    public GameObject effectPrb1;

    public GameObject effectPrb2;

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
                other.transform.GetComponent<ConsumeSign>().enterMarketingTime = StageGoal.My.timeCount;
                StageGoal.My.playerHealth += Mathf.Abs(other.transform.GetComponent<ConsumeSign>().consumeData.liveSatisfy * 80 / 100);
                int number = other.transform.GetComponent<ConsumeSign>().consumeData.killMoney * 12 / 100;
                StageGoal.My.GetPlayerGold(number);
                StageGoal.My.Income(number, IncomeType.Npc, GetComponentInParent<BaseMapRole>());
                GameObject go = Instantiate(effectPrb1,transform);
                go.transform.position = other.transform.position;
                Destroy(go, 1f);
                GameObject go1 = Instantiate(effectPrb2, transform);
                go1.transform.position = other.transform.position;
                Destroy(go1, 1f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && isOpen)
        {
            if (other.transform.GetComponent<ConsumeSign>().enterMarketingTime > 0)
            {
                if ((StageGoal.My.timeCount - other.transform.GetComponent<ConsumeSign>().enterMarketingTime) >= 3)
                {
                    other.transform.GetComponent<ConsumeSign>().enterMarketingTime = StageGoal.My.timeCount;
                    StageGoal.My.playerHealth += Mathf.Abs(other.transform.GetComponent<ConsumeSign>().consumeData.liveSatisfy * 80 / 100);
                    int number = other.transform.GetComponent<ConsumeSign>().consumeData.killMoney * 12 / 100;
                    StageGoal.My.GetPlayerGold(number);
                    StageGoal.My.Income(number, IncomeType.Npc, GetComponentInParent<BaseMapRole>());
                    GameObject go = Instantiate(effectPrb1, transform);
                    go.transform.position = other.transform.position;
                    Destroy(go, 1f);
                    GameObject go1 = Instantiate(effectPrb2, transform);
                    go1.transform.position = other.transform.position;
                    Destroy(go1, 1f);
                }
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
