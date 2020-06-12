using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoomTrigger : MonoBehaviour
{
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetConsumerList( )
    {
        List<ConsumeSign> signs = FindObjectsOfType<ConsumeSign>().ToList();
        for (int i = 0; i < signs.Count; i++)
        {
            if (signs[i].isCanSelect &&
                radius >= Vector3.Distance(gameObject.transform.position, signs[i].transform.position))
            {
                Debug.Log("打到"+signs[i].name);
                signs[i].OnHit(ref gameObject.GetComponent<GoodsSign>().productData);
            }
        }
    }
}
