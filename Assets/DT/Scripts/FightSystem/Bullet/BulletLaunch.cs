using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
using UnityEngine;
using Random = UnityEngine.Random;

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

        launchShooter.DOLookAt(PlayerData.My.isPrediction? launchShooter.position:pointList[pointList.Count / 2], 0.1f).OnComplete(() =>
          {
              gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
              //gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
              gameObject.transform.DOPath(pointList.ToArray(), 0.5f).SetEase(sase).OnComplete(() =>
              {
                  if(!PlayerData.My.isPrediction)
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
        launchShooter.DOLookAt(PlayerData.My.isPrediction? launchShooter.position: target.transform.position, 0.1f).OnComplete(() =>
        {
            Debug.Log("初始化拖尾" + gameObject.name);
            if(!PlayerData.My.isPrediction)
                gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().tile);
            float flyTime = Vector3.Distance(target.transform.position, gameObject.transform.position) / 24f;
            //            gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
            if (target == null)
            {
                BulletObjectPool.My.RecoveryBullet(gameObject, 0);
            }
            else
            {
                lanchNormalTWE = gameObject.transform.DOMove(target.transform.position, flyTime).SetEase(Ease.Linear).OnComplete(() =>
                {
                    isplay = false;
                    if (gameObject.GetComponent<GoodsSign>().target != null)
                    {
                        Debug.Log("初始化爆炸" + gameObject.name);
                        if(!PlayerData.My.isPrediction)
                            gameObject.GetComponent<BulletEffect>().InitBuff(gameObject.GetComponent<BulletEffect>().explosions);
                        //         gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                        gameObject.GetComponent<GoodsSign>().target.OnHit(ref data);

                        if (PlayerData.My.dingWei[4])
                        {
                            int number = UnityEngine.Random.Range(0, 101);
                            if (number <= 10)
                            {
                                List<ConsumeSign> consumeSigns = FindObjectsOfType<ConsumeSign>().ToList();
                                int targetIndex = -1;
                                float minDis = 99999f;
                                for (int i = 0; i < consumeSigns.Count; i++)
                                {
                                    if (consumeSigns[i].GetInstanceID() != target.GetInstanceID() && consumeSigns[i].isCanSelect && minDis >= Vector3.Distance(target.transform.position, consumeSigns[i].transform.position))
                                    {
                                        minDis = Vector3.Distance(target.transform.position, consumeSigns[i].transform.position);
                                        if (minDis <= 3)
                                        {
                                            targetIndex = i;
                                        }
                                    }
                                }
                                if (targetIndex != -1)
                                {
                                    GameObject go = BulletObjectPool.My.GetBullet(BulletType.NormalPP);
                                    Debug.Log("初始化子弹" + go.name);
                                    go.GetComponent<GoodsSign>().productData = data;
                                    go.GetComponent<GoodsSign>().lunch = this;
                                    go.GetComponent<GoodsSign>().target = consumeSigns[targetIndex];
                                    go.transform.SetParent(target.transform);
                                    go.GetComponent<BulletEffect>().InitBufflist(go.GetComponent<GoodsSign>().productData.buffList);

                                    go.transform.localPosition = new Vector3(0, 0.5f, 0);
                                    go.transform.SetParent(target.transform);

                                    Debug.Log("初始化拖尾" + go.name);
                                    if(!PlayerData.My.isPrediction)
                                        go.GetComponent<BulletEffect>().InitBuff(go.GetComponent<BulletEffect>().tile);
                                    float flyTime2 = Vector3.Distance(consumeSigns[targetIndex].transform.position, go.transform.position) / 24f;
                                    //            gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().Init();
                                    if (consumeSigns[targetIndex] == null)
                                    {
                                        BulletObjectPool.My.RecoveryBullet(go, 0);
                                    }
                                    else
                                    {
                                        go.transform.DOMove(consumeSigns[targetIndex].transform.position, flyTime2).SetEase(Ease.Linear).OnComplete(() =>
                                        {
                                            isplay = false;
                                            if (go.GetComponent<GoodsSign>().target != null)
                                            {
                                                Debug.Log("初始化爆炸" + go.name);
                                                if(!PlayerData.My.isPrediction)
                                                    go.GetComponent<BulletEffect>().InitBuff(go.GetComponent<BulletEffect>().explosions);
                                                //         gameObject.GetComponent<GoodsSign>().GetComponentInChildren<ETFXProjectileScript>().StartShoot();
                                                go.GetComponent<GoodsSign>().target.OnHit(ref data);
                                                BulletObjectPool.My.RecoveryBullet(go, 0.3f);
                                            }
                                        });
                                    }
                                }
                            }
                        }
                        BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
                    }
                    else
                    {
                        BulletObjectPool.My.RecoveryBullet(gameObject, 0.3f);
                    }

                });
                gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
            }
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
            if(!PlayerData.My.isPrediction)
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
        launchShooter.DOLookAt(PlayerData.My.isPrediction? launchShooter.position:gameObject.GetComponent<GoodsSign>().target.transform.position, 0.1f).OnComplete(() =>
        {
            if(!PlayerData.My.isPrediction)
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

                        if (PlayerData.My.dingWei[4])
                        {
                            int number = UnityEngine.Random.Range(0, 101);
                            if (number <= 10)
                            {
                                List<ConsumeSign> consumeSigns = FindObjectsOfType<ConsumeSign>().ToList();
                                int targetIndex = -1;
                                float minDis = 99999f;
                                for (int i = 0; i < consumeSigns.Count; i++)
                                {
                                    if (consumeSigns[i].GetInstanceID() != gameObject.GetComponent<GoodsSign>().target.GetInstanceID() && consumeSigns[i].isCanSelect && minDis >= Vector3.Distance(gameObject.GetComponent<GoodsSign>().target.transform.position, consumeSigns[i].transform.position))
                                    {
                                        minDis = Vector3.Distance(gameObject.GetComponent<GoodsSign>().target.transform.position, consumeSigns[i].transform.position);
                                        if (minDis <= 3)
                                        {
                                            targetIndex = i;
                                        }
                                    }
                                }
                                if (targetIndex != -1)
                                {
                                    GameObject go = BulletObjectPool.My.GetBullet(BulletType.Lightning);
                                    go.GetComponent<GoodsSign>().productData = data;
                                    go.GetComponent<GoodsSign>().lunch = this;
                                    go.GetComponent<GoodsSign>().target = consumeSigns[targetIndex];
                                    go.transform.SetParent(launchShooter);
                                    go.transform.position = go.GetComponent<GoodsSign>().target.transform.position;
                                    go.GetComponent<BulletEffect>().InitBufflist(go.GetComponent<GoodsSign>().productData.buffList);
                                    if(!PlayerData.My.isPrediction)
                                        go.GetComponent<BulletEffect>().InitBuff(go.GetComponent<BulletEffect>().tile);
                                    float flyTime2 = Vector3.Distance(go.GetComponent<GoodsSign>().target.transform.position, go.transform.position) / 10f;
                                    go.transform.DOMove(go.GetComponent<GoodsSign>().target.transform.position, 0.5f).SetEase(sase).OnComplete(() =>
                                    {
                                        if (go.GetComponent<GoodsSign>().target != null)
                                        {
                                            go.GetComponent<LightningTrigger>().GetTriggerList(go.GetComponent<GoodsSign>().target, data);
                                        }
                                        else
                                        {
                                            BulletObjectPool.My.RecoveryBullet(go, 0.3f);
                                        }
                                    });
                                }
                            }
                        }
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
        if (GetComponent<BaseMapRole>().shootTargetList.Count > 0)
        {
            tow.GetComponent<AutoFireTow>().target = GetComponent<BaseMapRole>().shootTargetList[Random.Range(0, GetComponent<BaseMapRole>().shootTargetList.Count)];
        }
        else
        {
            return;
        }


        tow.GetComponent<AutoFireTow>().shootTime = 1f / (GetComponent<BaseMapRole>().baseRoleData.efficiency * 0.04f) * data.loadingSpeed;
        tow.GetComponent<BulletEffect>().InitBufflist(data.buffList);


        tow.transform.localPosition = new Vector3(0, 0.4f, 0);
        tow.transform.SetParent(transform);
        launchShooter.DOLookAt(PlayerData.My.isPrediction? launchShooter.position:tow.GetComponent<AutoFireTow>().target.transform.position, 0.1f).OnComplete(() =>
          {
              if(!PlayerData.My.isPrediction)
                tow.GetComponent<BulletEffect>().InitBuff(tow.GetComponent<BulletEffect>().tile);
              tow.transform.DOMove(tow.GetComponent<AutoFireTow>().target.transform.position, 0.5f).SetEase(sase).OnComplete(() =>
              {
                  if(!PlayerData.My.isPrediction)
                    tow.GetComponent<BulletEffect>().InitBuff(tow.GetComponent<BulletEffect>().explosions);
                  tow.GetComponent<BulletEffect>().explosions.SetActive(false);
                  tow.GetComponent<BulletEffect>().tile.SetActive(false);
                  tow.transform.GetChild(3).gameObject.SetActive(true);
                  tow.GetComponent<AutoFireTow>().isupdate = true;

                  if (PlayerData.My.dingWei[4])
                  {
                      int number = UnityEngine.Random.Range(0, 101);
                      if (number <= 10)
                      {
                          List<ConsumeSign> consumeSigns = FindObjectsOfType<ConsumeSign>().ToList();
                          int targetIndex = -1;
                          float minDis = 99999f;
                          for (int i = 0; i < consumeSigns.Count; i++)
                          {
                              if (consumeSigns[i].GetInstanceID() != tow.GetComponent<AutoFireTow>().target.GetInstanceID() && consumeSigns[i].isCanSelect && minDis >= Vector3.Distance(tow.GetComponent<AutoFireTow>().target.transform.position, consumeSigns[i].transform.position))
                              {
                                  minDis = Vector3.Distance(tow.GetComponent<AutoFireTow>().target.transform.position, consumeSigns[i].transform.position);
                                  if (minDis <= 3)
                                  {
                                      targetIndex = i;
                                  }
                              }
                          }
                          if (targetIndex != -1)
                          {
                              List<Vector3> pointList2 = new List<Vector3>();
                              GameObject towPrb2 = Resources.Load<GameObject>("Bullet/Tow");
                              GameObject tow2 = Instantiate(towPrb2);
                              tow2.transform.SetParent(launchShooter);
                              tow2.transform.position = tow.GetComponent<AutoFireTow>().target.transform.position;
                              tow2.GetComponent<AutoFireTow>().data = data;
                              tow2.GetComponent<AutoFireTow>().destroyTime = 13;
                              tow2.GetComponent<AutoFireTow>().launchShooter = launchShooter;
                              tow2.GetComponent<AutoFireTow>().lunch = this;
                              tow2.GetComponent<AutoFireTow>().target = consumeSigns[targetIndex];
                              tow2.GetComponent<AutoFireTow>().shootTime = 1f / (GetComponent<BaseMapRole>().baseRoleData.efficiency * 0.04f) * data.loadingSpeed;
                              tow2.GetComponent<BulletEffect>().InitBufflist(data.buffList);
                              tow2.transform.SetParent(transform);
                              if(!PlayerData.My.isPrediction)
                                tow2.GetComponent<BulletEffect>().InitBuff(tow2.GetComponent<BulletEffect>().tile);
                              tow2.transform.DOMove(tow2.GetComponent<AutoFireTow>().target.transform.position, 0.5f).SetEase(sase).OnComplete(() =>
                              {
                                  if(!PlayerData.My.isPrediction)
                                    tow2.GetComponent<BulletEffect>().InitBuff(tow2.GetComponent<BulletEffect>().explosions);
                                  tow2.GetComponent<BulletEffect>().explosions.SetActive(false);
                                  tow2.GetComponent<BulletEffect>().tile.SetActive(false);
                                  tow2.transform.GetChild(3).gameObject.SetActive(true);
                                  tow2.GetComponent<AutoFireTow>().isupdate = true;
                              });
                          }
                      }
                  }
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