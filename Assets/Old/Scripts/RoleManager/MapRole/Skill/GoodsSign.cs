using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

public class GoodsSign : MonoBehaviour
{

    public ProductData productData;

    public ConsumeSign target;
    /// <summary>
    /// 发射者
    /// </summary>
    public BulletLaunch lunch;
    public List<Vector3> path;
    public  Tweener twe;
    public BaseMapRole role;
    // Start is called before the first frame update
    void Start()
    {
        needUpdate = false;
    //    InvokeRepeating("UpdatePos",0.1f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int count = 0;
    public void Move()
    {
       
        transform.DOMove(path[count],1).OnComplete(() =>
        {
            count++;
            if (count < path.Count)
            {
                Move();
                
            }
            else
            {
                role.AddPruductToWareHouse (productData);
                Destroy(this.gameObject,0.01f);
            }

        }).SetEase(Ease.Linear);
    }

    private bool needUpdate;

    public void OnTriggerEnter(Collider other)
    {
        if (lunch == null)
        {
            return;
        }

        //Debug.Log(other.gameObject.name);
        //Debug.Log(lunch);
        if (other.tag == "Consumer"&&other.GetComponent<ConsumeSign>()== lunch. GetComponent<BaseMapRole>().shootTarget)
        {
            if (twe.IsPlaying())
            {
                twe.Kill();
                target.OnHit(ref productData);
                BulletObjectPool.My.RecoveryBullet(gameObject); 
            }

        
        }
    }
}
