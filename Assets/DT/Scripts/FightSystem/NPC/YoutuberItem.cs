using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoutuberItem : MonoBehaviour
{

    public int buffId;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
