using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAddKillMoney : BaseSpawnItem
{
    public int time;

    public int buffId;

    public override void Init(int id)
    {
        base.Init(id);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.GetComponent<ConsumeSign>().isCanSelect)
        {
            BuffData data = GameDataMgr.My.GetBuffDataByID(buffId);
            BaseBuff baseBuff = new BaseBuff();
            baseBuff.Init(data);
            baseBuff.SetConsumerBuff(other.GetComponent<ConsumeSign>());
        }
    }

    private void Update()
    {
        if (StageGoal.My.timeCount - startTime >= time)
        {
            Destroy(gameObject, 0f);
        }
    }
}
