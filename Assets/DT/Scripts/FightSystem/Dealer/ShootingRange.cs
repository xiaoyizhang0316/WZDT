using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.transform.TryGetComponent(out ConsumeSign sign))
        {
            if (sign.isCanSelect && sign.isStart)
            {
                GetComponentInParent<BaseMapRole>().AddConsumerIntoShootList(sign);
            }
        }
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
