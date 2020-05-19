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


    public void LanchBoom(ProductData data)
    {
        List<Vector3> pointList = new List<Vector3>();
  
        GameObject gameObject = BulletObjectPool.My.GetBullet(BulletType.Bomb);
        gameObject.transform.SetParent(launchShooter);
        gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
        gameObject.GetComponent<GoodsSign>().productData = data;
        pointList = DrawLine(gameObject.transform.position , GetComponent<BaseMapRole>().shootTarget.transform.position);
        gameObject.transform.localPosition = new Vector3(0, 0.4f, 0);
        gameObject.GetComponent<GoodsSign>().lunch = this;
        gameObject.GetComponent<GoodsSign>().target = GetComponent<BaseMapRole>().shootTarget;
    
       // gameObject.transform.localPosition = new Vector3(0, 1, 0);
         
        launchShooter.DOLookAt(pointList[pointList.Count/2], 0.1f).OnComplete(() =>
        {
            gameObject.transform.DOPath(pointList.ToArray(), 1).SetEase(sase).OnComplete(() =>
            {
                gameObject.GetComponent<BoomTrigger>().GetConsumerList();
                BulletObjectPool.My.RecoveryBullet(gameObject);
            });
        });
        gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
    }

    private bool isplay;

    public void LanchNormal(ProductData data, ConsumeSign target)
    {
        GameObject gameObject = BulletObjectPool.My.GetBullet(BulletType.NormalPP);
        gameObject.GetComponent<GoodsSign>().productData = data;
        gameObject.GetComponent<GoodsSign>().lunch = this;
        gameObject.GetComponent<GoodsSign>().target =target;
        gameObject.transform.SetParent(launchShooter);
        gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);

        launchShooter.DOLookAt(target.transform.position, 0.1f).OnComplete(() =>
        {
            lanchNormalTWE = gameObject.transform.DOMove(target.transform.position, 0.5f)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    isplay = false;
                    gameObject.GetComponent<GoodsSign>().target.OnHit(data);
                    BulletObjectPool.My.RecoveryBullet(gameObject);
                });
            gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
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
        gameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
      
        launchShooter.DOLookAt(GetComponent<BaseMapRole>().shootTarget.transform.position, 0.1f).OnComplete(() =>
        {
            lanchNormalTWE = gameObject.transform.DOMove(GetComponent<BaseMapRole>().shootTarget.transform.position, 0.5f)
                .SetEase(sase).OnComplete(() =>
                {
                    isplay = false;
                 gameObject.GetComponent<LightningTrigger>().GetTriggerList(GetComponent<BaseMapRole>().shootTarget,data);
                    
     
                
                });
        });
        gameObject.GetComponent<GoodsSign>().twe = lanchNormalTWE;
        isplay = true;
    }
    
    /// <summary>
    /// 召唤
    /// </summary>
    public void CreatSummonTow( ProductData data )
    {
        List<Vector3> pointList = new List<Vector3>();
        GameObject towPrb = Resources.Load<GameObject>("Bullet/Tow" );
     GameObject tow =   Instantiate(towPrb);
        tow.transform.SetParent(launchShooter);
        tow.transform.localPosition = new Vector3(0, 0.1f, 0);
        tow.GetComponent<AutoFireTow>().data = data;
        tow.GetComponent<AutoFireTow>().destroyTime = 4;
        tow.GetComponent<AutoFireTow>().shootTime = 1f / ( GetComponent<BaseMapRole>().baseRoleData.efficiency * 0.1f) * data.loadingSpeed; 
        pointList = DrawLine(tow.transform.position , GetComponent<BaseMapRole>().shootTarget.transform.position);
        tow.transform.localPosition = new Vector3(0, 0.4f, 0);
        tow.transform.SetParent(transform);
        launchShooter.DOLookAt(pointList[pointList.Count/2], 0.1f).OnComplete(() =>
        {
            tow.transform.DOPath(pointList.ToArray(), 1).SetEase(sase).OnComplete(() =>
            {
                
                tow.transform.position = new Vector3(tow.transform.position .x, 0.4f, tow.transform.position .z);
                tow.transform.eulerAngles = Vector3.zero; 
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
        int vertexCount =20; //采样点数量
        pointList.Clear();
        pointList.Add(startTarget);
        if (startTarget != null && Target != null)
        {
            float x = startTarget .x * per + Target.x * (per);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y = 10;
            float z = startTarget .z * per + Target.z * (per);
            Vector3 point3 = new Vector3(x, y, z);
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget , point3, ratio);
                Vector3 tangentLineVectex2 = Vector3.Lerp(point3, Target, ratio);
                Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
                pointList.Add(bezierPoint);
            }
        }

        pointList.Add(Target);

        return pointList;
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
    }
}