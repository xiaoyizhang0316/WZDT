using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMoneyLine : MonoBehaviour
{
    /// <summary>
    /// 发起者位置
    /// </summary>
    public Transform startTarget;

    /// <summary>
    /// 承受者位置
    /// </summary>
    public Transform Target;

    /// <summary>
    /// 交易Id
    /// </summary>
    public int ID;

    public float per;

    public List<Vector3> pointList = new List<Vector3>();

    private void Awake()
    {
      
    }

    public void InitPos( Transform startTarget,Transform Target,int ID)
    {
        this.startTarget =startTarget;
        this.Target = Target;
        this.ID = ID;
        per = UnityEngine.Random.Range(0.3f, 0.8f);
    }

    // Start is called before the first frame update
    void Start()
    {
    //    GetComponent<LineRenderer>().
  
    }
   
    
    // Update is called once per frame
    void Update()
    {
        //List<Vector3> points = new List<Vector3>();
        //points.Add(startTarget.localPosition + new Vector3(0f,1f,0f));
        //points.Add(Target.localPosition + new Vector3(0f, 1f, 0f));
        int vertexCount = 30;//采样点数量
        pointList.Clear();
        pointList.Add(startTarget.localPosition);
        if (startTarget != null && Target != null)
        {
            float x = startTarget.localPosition.x * per + Target.localPosition.x * (per);
            //float y = startTarget.localPosition.y * per + Target.localPosition.y * (1f - per) ;
            float y = 5f * per;
            float z = startTarget.localPosition.z * per + Target.localPosition.z * (per);
            Vector3 point3 = new Vector3(x, y, z);
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget.localPosition, point3, ratio);
                Vector3 tangentLineVectex2 = Vector3.Lerp(point3, Target.localPosition, ratio);
                Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
                pointList.Add(bezierPoint);
            }
        }
        pointList.Add(Target.localPosition);
        GetComponent<LineRenderer>().positionCount = pointList.Count;
        GetComponent<LineRenderer>().SetPositions(pointList.ToArray());
        //GetComponent<LineRenderer>().SetPositions(points.ToArray());
    }
}
