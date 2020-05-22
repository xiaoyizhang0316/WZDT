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

    public List<Vector3> pointList = new List<Vector3>();

    public float offset = 0.02f;

    private float offsetX = 0f;

    private void Awake()
    {
      
    }

    public void InitPos( Transform startTarget,Transform Target,int ID)
    {
        this.startTarget =startTarget;
        this.Target = Target;
        this.ID = ID;
        DrawLine();
    }

    public void DrawLine()
    {
        pointList.Clear();
        pointList.Add(startTarget.localPosition);
        pointList.Add(Target.localPosition);
        GetComponent<LineRenderer>().positionCount = pointList.Count;
        GetComponent<LineRenderer>().SetPositions(pointList.ToArray());
    }

    // Start is called before the first frame update
    void Start()
    {
  
    }
   
    // Update is called once per frame
    void Update()
    {
        offsetX -= offset;
        GetComponent<LineRenderer>().material.mainTextureOffset = new Vector2(offsetX, 0f);
    }
}
