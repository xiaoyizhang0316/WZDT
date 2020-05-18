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

    public Tweener moveingTwe;
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
       
        moveingTwe =transform.DOMove(path[count],1).OnComplete(() =>
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

   
}
