using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Youtuber : BaseExtraSkill
{

    public int buffId;

    public GameObject buffEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Consumer") && isOpen)
        {
            other.GetComponent<ConsumeSign>().buffList[0].ConsumerBuffRemove();
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(data);
            baseBuff.SetConsumerBuff(other.GetComponent<ConsumeSign>());
            baseBuff.ConsumerBuffRemove();
            float per = (GetComponentInParent<BaseMapRole>().tradeList.Count - 1) * 0.1f + 1;
            baseBuff.buffConfig.consumerCrispChange = (int)(per * baseBuff.buffConfig.consumerCrispChange);
            baseBuff.buffConfig.consumerDiscountChange = (int)(per * baseBuff.buffConfig.consumerDiscountChange);
            baseBuff.buffConfig.consumerGoodPackChange = (int)(per * baseBuff.buffConfig.consumerGoodPackChange);
            baseBuff.buffConfig.consumerSoftChange = (int)(per * baseBuff.buffConfig.consumerSoftChange);
            baseBuff.buffConfig.consumerSweetChange = (int)(per * baseBuff.buffConfig.consumerSweetChange);
            baseBuff.ConsumerBuffAdd();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Consumer"))
        {
            ConsumeSign sign = other.GetComponent<ConsumeSign>();
            sign.buffList[0].ConsumerBuffAdd();
            for (int i = 0; i < sign.buffList.Count; i++)
            {
                if (sign.buffList[i].buffId == buffId)
                {
                    sign.RemoveBuff(sign.buffList[i]);
                }
            }
        }
    }

    public override void SkillOn()
    {
        base.SkillOn();
        buffEffect.SetActive(true);
    }

    public override void SkillOff()
    {
        base.SkillOff();
        if (GetComponentInParent<BaseMapRole>().tradeList.Count == 0)
        {
            isOpen = false;
            buffEffect.SetActive(false);
        }
    }

    private void Update()
    {
        if (isOpen)
        {

        }
    }
}
