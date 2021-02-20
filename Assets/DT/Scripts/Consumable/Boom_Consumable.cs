using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boom_Consumable : BaseSpawnItem
{
    public float range;

    public List<ConsumeSign> list;
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Consumer") && other.GetComponentInParent<ConsumeSign>().isCanSelect)
        {
           
            //other.GetComponentInParent<ConsumeSign>().ChangeHealth();
          
                Destroy(transform.parent.gameObject);
         
        }
    }
    
 
}
