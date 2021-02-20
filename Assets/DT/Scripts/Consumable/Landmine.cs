using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : BaseSpawnItem
{
    public override void Init(int id)
    {
        base.Init(id);
    }

    public GameObject boom;

    public bool isopen;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.GetComponent<ConsumeSign>().isCanSelect&&!isopen)
        {
            other.GetComponent<ConsumeSign>().ChangeHealth(999999);
            isopen = true;
            boom.SetActive(true);
            Destroy(gameObject, 1f);
        }
    }
}
