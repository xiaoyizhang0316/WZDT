using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;

/// <summary>
/// 炮弹发射器
/// </summary>
public class BulletLaunch : MonoBehaviour
{
    public Ease sase;
    public GameObject fire;
    /// <summary>
    /// 炮台
    /// </summary>
    public Transform launchShooter;

    public Tweener lanchNormalTWE;
    public float per;

    public List<GameObject> paos;


    public void LanchBoom(ProductData data)
    {
        List<Vector3> pointList = new List<Vector3>();

        GameObject gameObject = BulletObjectPool.My.GetBullet(BulletType.Bomb);
        gameObject.transform.SetParent(launchShooter);
        gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
        gameObject.GetComponent<GoodsSign>().productData = data;
        pointList = DrawLine(gameObject.transform.position, GetComponent<BaseMapRole>().shootTarget.transform.position);
        gameObject.transform.localPosition = new Vector3(0, 0.4f, 0);
        gameObject.GetComponent<GoodsSign>().lunch = this;
        gameObject.GetComponent<GoodsSign>().target = GetComponent<BaseMapRole>().shootTarget;
        gameObject.transform.SetParent(this.transform);
        gameObject.GetComponent<BulletEffect>().InitBufflist(gameObject.GetComponent<GoodsSign>().productData.buffList);

        // gameObject.transform.localPosition = new Vector3(0, 1, 0);

        launchShooter.DOLookAt(pointList[pointList.Count / 2], 0.1f).OnComplete(() =>
          {
              gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
              //gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
              gameObject.transform.DOPath(pointList.ToArray(), 0.5f).SetEase(sase).OnComplete(() =>
              {
                  gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);
                  //                  gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                  gameObject.GetComponent<BoomTrigger>().GetConsumerList();


                  BulletObjectPool.My.RecoveryBullet(gameObject, 0.5f);
              });
          });
        gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
    }

    public bool isplay;

    public void LanchNormal(ProductData data, ConsumeSign target)
    {
        GameObject gameObject = BulletObjectPool.My.GetBullet(BulletType.NormalPP);
        Debug.Log("初始化子弹" + gameObject.name);
        gameObject.GetComponent<GoodsSign>().productData = data;
        gameObject.GetComponent<GoodsSign>().lunch = this;
        gameObject.GetComponent<GoodsSign>().target = target;
        gameObject.transform.SetParent(launchShooter);
        gameObject.GetComponent<BulletEffect>().InitBufflist(gameObject.GetComponent<GoodsSign>().productData.buffList);

        gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
        gameObject.transform.SetParent(launchShooter.parent);
        launchShooter.DOLookAt(target.transform.position, 0.1f).OnComplete(() =>
        {
            Debug.Log("初始化拖尾" + gameObject.name);
            gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
            float flyTime = Vector3.Distance(target.transform.position, gameObject.transform.position) / 8f;
            //            gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
            lanchNormalTWE = gameObject.transform.DOMove(target.transform.position, flyTime)
                 .SetEase(Ease.Linear).OnComplete(() =>
                 {
                     isplay = false;
                     if (gameObject.GetComponent<GoodsSign>().target != null)
                     {
                         Debug.Log("初始化爆炸" + gameObject.name);
                         gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);
                        //         gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                        gameObject.GetComponent<GoodsSign>().target.OnHit(ref data);

                         BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
                     }
                     else
                     {
                         BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
                     }

                 });
            gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
        });
        isplay = true;
    }
    public void LanchNormalTest(Vector3 target, GameObject bullet, float time)
    {
        GameObject gameObject = Instantiate(bullet, transform);
        gameObject.transform.SetParent(launchShooter);
        gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);

        launchShooter.DOLookAt(target, 0.1f).OnComplete(() =>
        {
            float flyTime = 1;
            //   gameObject .GetComponent <ETFXProjectileScript>().Init(); 
            gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
            lanchNormalTWE = gameObject.transform.DOMove(target, time)
               .SetEase(Ease.Linear).OnComplete(() =>
               {
                   isplay = false;

                   gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);

                   Destroy(gameObject, 1f);
               });

        });
        isplay = true;
    }
    public void LanchLightning(ProductData data)
    {
        GameObject gameObject = BulletObjectPool.My.GetBullet(BulletType.Lightning);
        gameObject.GetComponent<GoodsSign>().productData = data;
        gameObject.GetComponent<GoodsSign>().lunch = this;
        gameObject.GetComponent<GoodsSign>().target = GetComponent<BaseMapRole>().shootTarget;
        gameObject.transform.SetParent(launchShooter);
        gameObject.transform.localPosition = new Vector3(0, 0f, 0);
        gameObject.GetComponent<BulletEffect>().InitBufflist(gameObject.GetComponent<GoodsSign>().productData.buffList);
        launchShooter.DOLookAt(gameObject.GetComponent<GoodsSign>().target.transform.position, 0.1f).OnComplete(() =>
    {
        gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
        float flyTime = Vector3.Distance(gameObject.GetComponent<GoodsSign>().target.transform.position, gameObject.transform.position) / 10f;
        lanchNormalTWE = gameObject.transform.DOMove(gameObject.GetComponent<GoodsSign>().target.transform.position, 0.5f)
            .SetEase(sase).OnComplete(() =>
            {
                isplay = false;
                if (gameObject.GetComponent<GoodsSign>().target != null)
                {
                    gameObject.GetComponent<LightningTrigger>()
                        .GetTriggerList(gameObject.GetComponent<GoodsSign>().target, data);
                }
                else
                {
                    BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
                }
            });
    });
        gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
        isplay = true;
    }

    /// <summary>
    /// 召唤
    /// </summary>
    public void CreatSummonTow(ProductData data)
    {
        List<Vector3> pointList = new List<Vector3>();
        GameObject towPrb = Resources.Load<GameObject>("Bullet/Tow");
        GameObject tow = Instantiate(towPrb);
        tow.transform.SetParent(launchShooter);
        tow.transform.localPosition = new Vector3(0, 0.1f, 0);
        tow.GetComponent<AutoFireTow>().data = data;
        tow.GetComponent<AutoFireTow>().destroyTime = 13;
        tow.GetComponent<AutoFireTow>().launchShooter = launchShooter;
        tow.GetComponent<AutoFireTow>().lunch = this;
        tow.GetComponent<AutoFireTow>().target = GetComponent<BaseMapRole>().shootTarget;

        tow.GetComponent<AutoFireTow>().shootTime = 1f / (GetComponent<BaseMapRole>().baseRoleData.efficiency * 0.04f) * data.loadingSpeed;
        tow.GetComponent<BulletEffect>().InitBufflist(data.buffList);


        pointList = DrawLine(tow.transform.position, GetComponent<BaseMapRole>().shootTarget.transform.position);
        tow.transform.localPosition = new Vector3(0, 0.4f, 0);
        tow.transform.SetParent(transform);
        launchShooter.DOLookAt(tow.GetComponent<AutoFireTow>().target.transform.position, 0.1f).OnComplete(() =>
          {
              tow.GetComponent<BulletEffect>().InitBuff(tow.GetComponent<BulletEffect>().tile);

              tow.transform.DOMove(tow.GetComponent<AutoFireTow>().target.transform.position, 0.5f).SetEase(sase).OnComplete(() =>
              {
                  tow.GetComponent<BulletEffect>().InitBuff(tow.GetComponent<BulletEffect>().explosions);
                  tow.GetComponent<BulletEffect>().explosions.SetActive(false);
                  tow.GetComponent<BulletEffect>().tile.SetActive(false);
                  tow.transform.GetChild(3).gameObject.SetActive(true);
                  tow.GetComponent<AutoFireTow>().isupdate = true;


              });
          });
    }
    public List<Vector3> DrawLine(Transform startTarget, Transform Target)
    {
        List<Vector3> pointList = new List<Vector3>();
        int vertexCount = 30; //采样点数量
        pointList.Clear();
        pointList.Add(startTarget.position);
        if (startTarget != null && Target != null)
        {
            float x = startTarget.position.x * per + Target.position.x * (per);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y = 5f * per;
            float z = startTarget.position.z * per + Target.position.z * (per);
            Vector3 point3 = new Vector3(x, y, z);
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget.position, point3, ratio);
                Vector3 tangentLineVectex2 = Vector3.Lerp(point3, Target.position, ratio);
                Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
                pointList.Add(bezierPoint);
            }
        }

        pointList.Add(Target.position);
        return pointList;
    }

    public List<Vector3> DrawLine(Vector3 startTarget, Vector3 Target)
    {
        List<Vector3> pointList = new List<Vector3>();
        int vertexCount = 20; //采样点数量
        pointList.Clear();
        pointList.Add(startTarget);
        if (startTarget != null && Target != null)
        {
            float x = startTarget.x * per + Target.x * (per);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y = 10;
            float z = startTarget.z * per + Target.z * (per);
            Vector3 point3 = new Vector3(x, y, z);
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget, point3, ratio);
                Vector3 tangentLineVectex2 = Vector3.Lerp(point3, Target, ratio);
                Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
                pointList.Add(bezierPoint);
            }
        }

        pointList.Add(Target);

        return pointList;
    }
    public void LanchLeaser(ProductData data, ConsumeSign target, Transform BulletLaunch, BulletLaunch lunch, Transform pos)
    {
        GameObject gameObject = BulletObjectPool.My.GetBullet(BulletType.Leaser);

        gameObject.GetComponent<GoodsSign>().productData = data;
        gameObject.GetComponent<GoodsSign>().lunch = lunch;
        gameObject.GetComponent<GoodsSign>().target = target;

        gameObject.GetComponent<BulletEffect>().InitBufflist(gameObject.GetComponent<GoodsSign>().productData.buffList);

        gameObject.transform.position = pos.position;

        launchShooter.DOLookAt(target.transform.position, 0.1f).OnComplete(() =>
        {

            gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
            float flyTime = Vector3.Distance(target.transform.position, gameObject.transform.position) / 8f;
            //            gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
            lanchNormalTWE = gameObject.transform.DOMove(target.transform.position, flyTime)
                 .SetEase(Ease.Linear).OnComplete(() =>
                 {
                     isplay = false;
                     if (gameObject.GetComponent<GoodsSign>().target != null)
                     {

                         gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);


                        //         gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                        gameObject.GetComponent<GoodsSign>().target.OnHit(ref data);

                         BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
                     }

                 });
            gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
        });
        isplay = true;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnGUI()
    {
    }

    // Update is called once per frame
    void Update()
    {
        BaseMapRole role = GetComponent<BaseMapRole>();
        if (role != null)
        {
            for (int i = 0; i < role.levelModels.Count; i++)
            {
                if (role.levelModels[i].activeSelf)
                {
                    paos[0].gameObject.SetActive(false);
                    paos[1].gameObject.SetActive(false);
                    paos[2].gameObject.SetActive(false);
                    paos[i].gameObject.SetActive(true);
                    launchShooter = paos[i].transform;
                }
            }
        }
    }
}