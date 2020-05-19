using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class NormalPPTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (GetComponent<GoodsSign>(). lunch == null)
        {
            return;
        }
 
        if (other.tag == "Consumer"&&other.GetComponent<ConsumeSign>()== GetComponent<GoodsSign>(). target )
        {
            if (GetComponent<GoodsSign>(). twe!=null &&GetComponent<GoodsSign>(). twe.IsPlaying())
            {
                GetComponent<GoodsSign>(). twe.Kill();
                GetComponent<GoodsSign>(). target.OnHit(ref GetComponent<GoodsSign>(). productData);
                BulletObjectPool.My.RecoveryBullet(gameObject); 
            }

        
        }
    }
}
