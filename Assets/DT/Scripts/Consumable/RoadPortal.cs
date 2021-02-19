using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoadPortal : MonoBehaviour
{
    public int count = 5;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.GetComponentInParent<ConsumeSign>().isCanSelect)
        {
            float newPos = Mathf.Max(other.GetComponentInParent<ConsumeSign>().tweener.fullPosition - 1f,0f);
            other.GetComponentInParent<ConsumeSign>().tweener.Goto(newPos,true);
            count--;
            if (count == 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
