using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    /// <summary>
    /// 发起者位置
    /// </summary>
    public Transform startTarget;

    /// <summary>
    /// 承受者位置
    /// </summary>
    public Transform Target;


    public GameObject lineGo;
    /// <summary>
    /// 交易Id
    /// </summary>
    public int ID;

    public float per;

    public int num = 15;

    public List<Vector3> pointList = new List<Vector3>();

    public float offsetX = 0f;

    private void Awake()
    {

    }

    public void InitPos(Transform startTarget, Transform Target, int ID)
    {
        this.startTarget = startTarget;
        this.Target = Target;
        this.ID = ID;
        //per = UnityEngine.Random.Range(0.3f, 0.8f);
        //Vector3 rightPosition = (startTarget.gameObject.transform.position + Target.transform.position) / 2;
        //Vector3 rightRotation = Target.gameObject.transform.position - startTarget.transform.position;
        //float HalfLength = Vector3.Distance(startTarget.transform.position, Target.transform.position) / 2;
        //float LThickness = 0.1f;//线的粗细

        ////创建圆柱体
        //lineGo = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //lineGo.gameObject.transform.parent = transform;
        //lineGo.transform.position = rightPosition;
        //lineGo.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        //lineGo.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
    }


    public void DrawLS(GameObject startP, GameObject finalP)
    {
        Vector3 rightPosition = (startP.transform.position + finalP.transform.position) / 2;
        Vector3 rightRotation = finalP.transform.position - startP.transform.position;
        float HalfLength = Vector3.Distance(startP.transform.position, finalP.transform.position) / 2;
        float LThickness = 0.1f;//线的粗细

        //创建圆柱体
        GameObject MyLine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        MyLine.gameObject.transform.parent = transform;
        MyLine.transform.position = rightPosition;
        MyLine.transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
        MyLine.transform.localScale = new Vector3(LThickness, HalfLength, LThickness);

        //这里可以设置材质，具体自己设置
        //MyLine.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
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
        //points.Add(startTarget.localPosition);
        //points.Add(Target.localPosition);
        //int vertexCount = 20;//采样点数量
        pointList.Clear();
        if (startTarget != null && Target != null)
        {
            pointList.Add(startTarget.localPosition + new Vector3(0f,0.1f,0f));
            pointList.Add(Target.localPosition + new Vector3(0f, 0.1f, 0f));
            //float x = startTarget.localPosition.x * per + Target.localPosition.x * (per);
            ////float y = startTarget.localPosition.y * per + Target.localPosition.y * (1 - per) + 2f;
            //float y = 5f * per;
            //float z = startTarget.localPosition.z * per + Target.localPosition.z * (per);
            //Vector3 point3 = new Vector3(x, y, z);
            //for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            //{
            //    Vector3 tangentLineVertex1 = Vector3.Lerp(startTarget.localPosition , point3, ratio);
            //    Vector3 tangentLineVectex2 = Vector3.Lerp(point3, Target.localPosition , ratio);
            //    Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
            //    pointList.Add(bezierPoint);
            //}
        }
        //pointList.Add(Target.localPosition);
        GetComponent<LineRenderer>().positionCount = pointList.Count;
        GetComponent<LineRenderer>().SetPositions(pointList.ToArray());
        offsetX -= 0.0015f;
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(offsetX, 0f);
        
    }
}
