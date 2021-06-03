using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 零售商的范围脚本，会将范围内的消费者注册进零售商basemaprole中的shootTargetList中
/// </summary>
public class ShootingRange : MonoBehaviour
{

    public BaseMapRole mapRole;

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

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Consumer") && other.transform.TryGetComponent(out ConsumeSign sign))
        {
            GetComponentInParent<BaseMapRole>().RemoveConsumerFromShootList(sign);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mapRole = GetComponentInParent<BaseMapRole>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mapRole.baseRoleData.inMap)
        {
            float delta = mapRole.baseRoleData.range / 28f * 5f;
            transform.parent.localScale = new Vector3(delta, 1f, delta);
        }
    }
}
