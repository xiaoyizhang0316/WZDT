using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网红生效的实际光圈范围脚本
/// 消费者在圈内会暂时被洗脑，喜好被抹掉换成指定的喜好
/// buffId：指定喜好的buffId
/// </summary>
public class YoutuberItem : MonoBehaviour
{
    //对应的buffId
    public int buffId;
    //该光圈对应的交易Id
    public int tradeId;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Consumer"))
        {
            other.GetComponent<ConsumeSign>().buffList[0].ConsumerBuffRemove();
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(data);
            baseBuff.SetConsumerBuff(other.GetComponent<ConsumeSign>());
            baseBuff.ConsumerBuffRemove();
            YoutuberItem[] items = FindObjectsOfType<YoutuberItem>();
            float per = (items.Length - 1) * 0.1f + 1;
            baseBuff.buffConfig.consumerCrispChange = (int)(per * baseBuff.buffConfig.consumerCrispChange);
            baseBuff.buffConfig.consumerDiscountChange = (int)(per * baseBuff.buffConfig.consumerDiscountChange);
            baseBuff.buffConfig.consumerGoodPackChange = (int)(per * baseBuff.buffConfig.consumerGoodPackChange);
            baseBuff.buffConfig.consumerSoftChange = (int)(per * baseBuff.buffConfig.consumerSoftChange);
            baseBuff.buffConfig.consumerSweetChange = (int)(per * baseBuff.buffConfig.consumerSweetChange);
            baseBuff.ConsumerBuffAdd();
        }
    }

    private void OnTriggerExit(Collider other)
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

    public void Init(int bId,int tId)
    {
        buffId = bId;
        tradeId = tId;
    }
}
