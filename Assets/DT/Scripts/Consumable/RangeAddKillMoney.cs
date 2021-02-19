using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAddKillMoney : BaseSpawnItem
{
    public int time;

    public int buffId;

    public override void Init()
    {
        base.Init();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer"))
        {
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(data);
            baseBuff.SetConsumerBuff(other.GetComponent<ConsumeSign>());
        }
    }
}
