using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RangeDecreaseSpeed : BaseSpawnItem
{
    public int buffId;

    public int time;

    public override void Init(int id)
    {
        base.Init(id);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer"))
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
