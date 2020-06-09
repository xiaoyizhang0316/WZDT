using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Tweener moveTween;
    public BaseMapRole role;
    private float speedAdd = 1f;
    public float speed=1;

    public Dictionary<double, int> speedBuffList = new Dictionary<double, int>();
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
  
        if (role.baseRoleData.baseRoleData.roleType == GameEnum.RoleType.Merchant)
        {
            speed=  1f * (1 - role.baseRoleData.efficiency / 100f);
        }

        // print("bullet start move" + path[count]);
        moveTween = transform.DOMove(path[count],speed ).OnComplete(() =>
        {
           // print("bullet move");
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
        moveTween.timeScale = speedAdd;
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
            if (twe!=null &&twe.IsPlaying())
            {
                twe.Kill();
                target.OnHit(ref productData);
                GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                BulletObjectPool.My.RecoveryBullet(gameObject); 
            }
        }
    }

    public void AddSpeedBuff(double roleId, int num)
    {
        if (speedBuffList.ContainsKey(roleId))
            return;
        if (speedBuffList.Count == 0)
        {
            speedBuffList.Add(roleId, num);
            ChangeMoveSpeed();
        }
        else
        {
            List<double> temp = speedBuffList.Keys.ToList();
            if (speedBuffList[temp[0]] >= num)
            {
                return;
            }
            else
            {
                speedBuffList.Remove(temp[0]);
                speedBuffList.Add(roleId, num);
                ChangeMoveSpeed();
            }
        }
    }

    public void RemoveSpeedBuff(double roleId)
    {
        if (!speedBuffList.ContainsKey(roleId))
            return;
        else
        {
            speedBuffList.Remove(roleId);
            ChangeMoveSpeed();
        }
    }

    public void ChangeMoveSpeed()
    {
        speedAdd = 1f;
        moveTween.timeScale = 1f;
        foreach (var v in speedBuffList)
        {
            speedAdd += v.Value / 100f;
        }
        moveTween.timeScale = speedAdd;
    }
}
