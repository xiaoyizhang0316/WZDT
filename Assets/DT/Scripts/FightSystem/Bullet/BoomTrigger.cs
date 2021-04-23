using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DT.Fight.Bullet;
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

    public void GetConsumerList(bool isCanRestart = true)
    {
        List<ConsumeSign> signs = FindObjectsOfType<ConsumeSign>().ToList();
        for (int i = 0; i < signs.Count; i++)
        {
            if (signs[i].isCanSelect &&
                radius >= Vector3.Distance(gameObject.transform.position, signs[i].transform.position))
            {
                //Debug.Log("打到"+signs[i].name);
                signs[i].OnHit(ref gameObject.GetComponent<GoodsSign>().productData);
            }
        }
    }
    
    public void GetConsumerListTow( )
    {
        List<ConsumeSign> signs = FindObjectsOfType<ConsumeSign>().ToList();
        gameObject.GetComponent<AutoFireTow>().data.damage *= 3;
        for (int i = 0; i < signs.Count; i++)
        {
            if (signs[i].isCanSelect &&
                radius >= Vector3.Distance(gameObject.transform.position, signs[i].transform.position))
            {
              
              
                signs[i].OnHit(ref gameObject.GetComponent<AutoFireTow>().data);
            }
        }
    }

    public List<Vector3> DrawLine(Vector3 startTarget, Vector3 Target)
    {
        List<Vector3> pointList = new List<Vector3>();
        int vertexCount = 20; //采样点数量
        pointList.Clear();
        pointList.Add(startTarget);
        if (startTarget != null && Target != null)
        {
            float x = startTarget.x * 0.5f + Target.x * (0.5f);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y = 10;
            float z = startTarget.z * 0.5f + Target.z * (0.5f);
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
}
