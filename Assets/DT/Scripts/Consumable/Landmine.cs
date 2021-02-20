using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : BaseSpawnItem
{
    public override void Init(int id)
    {
        base.Init(id);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.GetComponent<ConsumeSign>().isCanSelect)
        {
            other.GetComponent<ConsumeSign>().ChangeHealth(999999);
            Destroy(gameObject, 0f);
        }
    }
}
