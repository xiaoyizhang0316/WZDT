using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadPortal : BaseSpawnItem
{
    public int count = 5;

    public int health = 30;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.GetComponentInParent<ConsumeSign>().isCanSelect && !other.GetComponentInParent<ConsumeSign>().isBlocked)
        {
            if (other.GetComponentInParent<ConsumeSign>().consumerType == GameEnum.ConsumerType.Boss)
            {
                return;
            }
            other.GetComponentInParent<ConsumeSign>().Block();
            health -= other.GetComponentInParent<ConsumeSign>().consumeData.liveSatisfy;
            if (health < 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public override void Init(int id)
    {
        base.Init(id);
        health = 30;
    }
}
