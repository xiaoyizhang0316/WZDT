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
    /// <summary>
    /// 炮台
    /// </summary>
    public Transform launchShooter;
    
    public float per;
    public void LanchBoom(Vector3 target)
    {
        
        List<Vector3> pointList = new List<Vector3>();
        pointList = DrawLine(transform,target);
     GameObject gameObject =    BulletObjectPool.My.GetBullet(BulletType.Bomb);
     gameObject.transform.SetParent(launchShooter);
     gameObject.transform.localPosition = Vector3.zero;
     
     launchShooter.DOLookAt(target,0.3f);
     gameObject.transform.DOPath(pointList.ToArray(),5).SetEase(sase).OnComplete(() =>
     {
         BulletObjectPool.My.RecoveryBullet(gameObject);
     });
    }

    public GameObject LanchNormal(Vector3 target,ProductData data)
    {
        
    
         
        GameObject gameObject =    BulletObjectPool.My.GetBullet(BulletType.NormalPP);
        gameObject.GetComponent<GoodsSign>().productData = data;
        gameObject.transform.SetParent(launchShooter);
        gameObject.transform.localPosition = new Vector3(0,0.5f,0);
     
        launchShooter.DOLookAt(target , 0.3f);
        gameObject.transform.DOMove(target,1).SetEase(sase).OnComplete(() =>
        {
            GetComponent<BaseMapRole>().shootTarget.OnHit(data);
            BulletObjectPool.My.RecoveryBullet(gameObject); 
        });
        return gameObject;
    }
    public  List<Vector3>  DrawLine(Transform startTarget ,Transform Target)
    {
        List<Vector3> pointList = new List<Vector3>();
        int vertexCount = 30;//采样点数量
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
    public  List<Vector3>  DrawLine(Transform startTarget ,Vector3 Target)
    {
        List<Vector3> pointList = new List<Vector3>();
        int vertexCount = 30;//采样点数量
        pointList.Clear();
        pointList.Add(startTarget.position);
        if (startTarget != null && Target != null)
        {
            float x = startTarget.position.x * per + Target.x * (per);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y =50;
            float z = startTarget.position.z * per + Target.z * (per);
            Vector3 point3 = new Vector3(x, y, z);
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget.position, point3, ratio);
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
        if (GUILayout.Button("123"))
        {
            LanchNormal(Camera.main.transform.position,new ProductData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
